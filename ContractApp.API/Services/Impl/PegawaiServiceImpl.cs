using ContractApp.API.Constants;
using ContractApp.API.DB;
using ContractApp.API.Models.DTOs;
using ContractApp.API.Models.Entities;
using ContractApp.API.Repositories;
using OfficeOpenXml;

namespace ContractApp.API.Services.Impl
{
    public class PegawaiServiceImpl : IPegawaiService
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        private readonly IJabatanRepository _jabatanRepository;
        private readonly ICabangRepository _cabangRepository;
        private readonly AppDbContext _context;

        public PegawaiServiceImpl(IPegawaiRepository pegawaiRepository, IJabatanRepository jabatanRepository, ICabangRepository cabangRepository, AppDbContext context)
        {
            this._context = context;
            _pegawaiRepository = pegawaiRepository;
            _jabatanRepository = jabatanRepository;
            _cabangRepository = cabangRepository;
        }

        public async Task<PegawaiCountRes> CountPegawai()
        {
            int countActive = await _pegawaiRepository.GetCountActive();
            int countInActive = await _pegawaiRepository.GetCountInActive();

            return new PegawaiCountRes
            {
                Active = countActive,
                NotActive = countInActive
            };
        }

        public Task<IEnumerable<PegawaiRes>> FindListPegawai(DateOnly? startDate, DateOnly? endDate)
        {
            return _pegawaiRepository.GetAllProcedure(startDate, endDate);
        }

        public async Task<IEnumerable<PegawaiExcelRes>> SaveExcelPegawai(IFormFile file)
        {

            if (file == null || file.Length == 0)
                throw new ApplicationException("File not found", 404);

            var allowedExtensions = new[] { ".xls", ".xlsx" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new ApplicationException("Hanya file Excel (.xls / .xlsx) yang diperbolehkan", 404);

            var allowedContentTypes = new[] {
                "application/vnd.ms-excel", // .xls
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" // .xlsx
            };

            if (!allowedContentTypes.Contains(file.ContentType))
                throw new ApplicationException("File bukan Excel", 404);

            using var fileStream = file.OpenReadStream();

            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");
            var pegawaiList = new List<PegawaiExcelRes>();

            using (var package = new ExcelPackage(fileStream))
            {
                var sheet = package.Workbook.Worksheets.First();
                int rowMax = sheet.Dimension.Rows;

                for (int row = 2; row <= rowMax; row++)
                {

                    var transaction = _context.Database.BeginTransaction();

                    var kodePegawai = sheet.Cells[row, 1].Text;
                    var namaPegawai = sheet.Cells[row, 2].Text;
                    var kodeCabang = sheet.Cells[row, 3].Text;
                    var namaCabang = sheet.Cells[row, 4].Text;
                    var kodeJabatan = sheet.Cells[row, 5].Text;
                    var namaJabatan = sheet.Cells[row, 6].Text;
                    DateOnly? startContract = String.IsNullOrEmpty(sheet.Cells[row, 7].Text) ? null : sheet.Cells[row, 7].GetValue<DateOnly>();
                    DateOnly? endContract = String.IsNullOrEmpty(sheet.Cells[row, 8].Text) ? null : sheet.Cells[row, 8].GetValue<DateOnly>();
                    EStatus? status = null;

                    try
                    {
                        if (startContract == null || endContract == null)
                            throw new Exception("Null references");

                        var currJabatan = await _jabatanRepository.GetById(kodeJabatan);
                        if (currJabatan == null)
                        {
                            var jabatan = new Jabatan
                            {
                                KodeJabatan = kodeJabatan,
                                NamaJabatan = namaJabatan
                            };
                            await _jabatanRepository.Add(jabatan);
                        }
                        else
                        {
                            currJabatan.NamaJabatan = namaJabatan;
                            await _jabatanRepository.Update(currJabatan);
                        }

                        var currCabang = await _cabangRepository.GetById(kodeCabang);
                        if (currCabang == null)
                        {
                            var cabang = new Cabang
                            {
                                KodeCabang = kodeCabang,
                                NamaCabang = namaCabang
                            };
                            await _cabangRepository.Add(cabang);
                        }
                        else
                        {
                            currCabang.NamaCabang = namaCabang;
                            await _cabangRepository.Update(currCabang);
                        }

                        var currPegawai = await _pegawaiRepository.GetById(kodePegawai);
                        if (currPegawai == null)
                        {
                            var pegawai = new Pegawai
                            {
                                KodePegawai = kodePegawai,
                                NamaPegawai = namaPegawai,
                                KodeCabang = kodeCabang,
                                KodeJabatan = kodeJabatan,
                                StartContract = (DateOnly)startContract,
                                EndContract = (DateOnly)endContract
                            };
                            await _pegawaiRepository.Add(pegawai);
                        }
                        else
                        {
                            currPegawai.NamaPegawai = namaPegawai;
                            currPegawai.KodeCabang = kodeCabang;
                            currPegawai.KodeJabatan = kodeJabatan;
                            currPegawai.StartContract = (DateOnly)startContract;
                            currPegawai.EndContract = (DateOnly)endContract;
                            await _pegawaiRepository.Update(currPegawai);
                            status = EStatus.Updated;
                        }

                        if (status == null)
                        {
                            status = EStatus.Created;
                        }

                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception err)
                    {
                        status = EStatus.Failed;
                        await transaction.RollbackAsync();
                    }

                    pegawaiList.Add(new PegawaiExcelRes
                    {
                        KodePegawai = kodePegawai,
                        NamaPegawai = namaPegawai,
                        KodeCabang = kodeCabang,
                        NamaCabang = namaCabang,
                        KodeJabatan = kodeJabatan,
                        NamaJabatan = namaJabatan,
                        StartContract = startContract,
                        EndContract = endContract,
                        Status = (EStatus)status
                    });
                }
            }

            return pegawaiList;
        }
    }
}

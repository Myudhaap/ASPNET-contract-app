using ContractApp.API.Models.DTOs;
using ContractApp.API.Repositories;

namespace ContractApp.API.Services.Impl
{
    public class PegawaiServiceImpl : IPegawaiService
    {
        private readonly IPegawaiRepository _pegawaiRepository;

        public PegawaiServiceImpl(IPegawaiRepository pegawaiRepository)
        {
            this._pegawaiRepository = pegawaiRepository;
        }

        public Task<IEnumerable<PegawaiRes>> FindListPegawai(DateOnly startDate, DateOnly endDate)
        {
            return _pegawaiRepository.GetAllProcedure(startDate, endDate);
        }
    }
}

using ContractApp.API.Models.DTOs;

namespace ContractApp.API.Services
{
    public interface IPegawaiService
    {
        Task<IEnumerable<PegawaiRes>> FindListPegawai(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<PegawaiExcelRes>> SaveExcelPegawai(IFormFile fileStream);
    }
}

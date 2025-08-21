using ContractApp.API.Models.DTOs;
using ContractApp.API.Models.Entities;

namespace ContractApp.API.Repositories
{
    public interface IPegawaiRepository
    {
        Task<IEnumerable<PegawaiRes>> GetAllProcedure(DateOnly? startDate, DateOnly? endDate);
        Task<Pegawai?> GetById(string id);
        Task Add(Pegawai entity);
        Task Update(Pegawai entity);
        Task Delete(string id);
    }
}

using ContractApp.API.Models.Entities;

namespace ContractApp.API.Repositories
{
    public interface IJabatanRepository
    {
        Task<Jabatan?> GetById(string id);
        Task Add(Jabatan entity);
        Task Update(Jabatan entity);
        Task Delete(string id);
    }
}

using ContractApp.API.Models.Entities;

namespace ContractApp.API.Repositories
{
    public interface ICabangRepository
    {
        Task<Cabang?> GetById(string id);
        Task Add(Cabang entity);
        Task Update(Cabang entity);
        Task Delete(string id);
    }
}

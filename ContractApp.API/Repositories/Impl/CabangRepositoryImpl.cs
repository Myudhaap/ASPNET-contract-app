using ContractApp.API.DB;
using ContractApp.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractApp.API.Repositories.Impl
{
    public class CabangRepositoryImpl : ICabangRepository
    {
        private readonly AppDbContext _context;

        public CabangRepositoryImpl(AppDbContext context)
        {
            this._context = context;
        }

        public async Task Add(Cabang entity)
        {
            _context.Cabangs.Add(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _context.Cabangs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cabang?> GetById(string id)
        {
            return await _context.Cabangs
                .FirstOrDefaultAsync(t => t.KodeCabang == id);
        }

        public async Task Update(Cabang entity)
        {
            _context.Cabangs.Update(entity);
        }
    }
}

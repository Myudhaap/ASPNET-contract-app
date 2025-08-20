using ContractApp.API.DB;
using ContractApp.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractApp.API.Repositories.Impl
{
    public class JabatanRepositoryImpl : IJabatanRepository
    {
        private readonly AppDbContext _context;
        public JabatanRepositoryImpl(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Jabatan entity)
        {
            _context.Jabatans.Add(entity);
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Jabatan?> GetById(string id)
        {
            return await _context.Jabatans
                .FirstOrDefaultAsync(t => t.KodeJabatan == id);
        }

        public async Task Update(Jabatan entity)
        {
            _context.Jabatans.Update(entity);
        }
    }
}

using ContractApp.API.DB;
using ContractApp.API.Models.DTOs;
using ContractApp.API.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ContractApp.API.Repositories.Impl
{
    public class PegawaiRepositoryImpl : IPegawaiRepository
    {
        private readonly AppDbContext _context;

        public PegawaiRepositoryImpl(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Pegawai entity)
        {
            _context.Pegawais.Add(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PegawaiRes>> GetAllProcedure(DateOnly startDate, DateOnly endDate)
        {
            Console.WriteLine($"startDate: {startDate}, endDate: {endDate}");
            var result = new List<PegawaiRes>();

            using var conn = _context.Database.GetDbConnection();
            using var command = conn.CreateCommand();
            command.CommandText = "GetPegawai";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@start_date", startDate));
            command.Parameters.Add(new SqlParameter("@end_date", endDate));

            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new PegawaiRes
                {
                    KodePegawai = reader["kode_pegawai"].ToString()!,
                    NamaPegawai = reader["nama_pegawai"].ToString()!,
                    KodeCabang = reader["kode_cabang"].ToString()!,
                    NamaCabang = reader["nama_cabang"].ToString()!,
                    KodeJabatan = reader["kode_jabatan"].ToString()!,
                    NamaJabatan = reader["nama_jabatan"].ToString()!,
                    StartContract = DateOnly.FromDateTime((DateTime)reader["start_contract"]),
                    EndContract = DateOnly.FromDateTime((DateTime)reader["end_contract"])
                });
            }

            return result;
        }

        public async Task<Pegawai?> GetById(string id)
        {
            return await _context.Pegawais
                .FirstOrDefaultAsync(t => t.KodePegawai == id);
        }

        public async Task Update(Pegawai entity)
        {
            _context.Pegawais.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

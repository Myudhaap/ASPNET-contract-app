using ContractApp.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractApp.API.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cabang> Cabangs { get; set; }
        public DbSet<Jabatan> Jabatans { get; set; }
        public DbSet<Pegawai> Pegawais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pegawai>()
                .HasOne(p => p.Cabang)
                .WithMany(c => c.PegawaiList)
                .HasForeignKey(p => p.KodeCabang);

            modelBuilder.Entity<Pegawai>()
                .HasOne(p => p.Jabatan)
                .WithMany(c => c.PegawaiList)
                .HasForeignKey(p => p.KodeJabatan);
        }
    }
}

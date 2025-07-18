using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Entities.Tanimlamalar.StokTanimlamalar;
using zeynerp.Domain.Enums;

namespace zeynerp.Infrastructure.Data.Contexts
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {

        }

        public DbSet<StokGrupTanim> StokGrupTanimlar { get; set; }
        public DbSet<CariTanim> CariTanimlar { get; set; }
        public DbSet<CariTurTanim> CariTurTanimlar { get; set; }
        public DbSet<CariYetkiliTanim> CariYetkiliTanimlar { get; set; }
        public DbSet<TeslimatAdresTanim> TeslimatAdresTanimlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StokGrupTanim>().HasData(
                new StokGrupTanim { StokTanimAdi = "Yok", Sira = 1, Durum = Status.Aktif }
            );
        }
    }
}
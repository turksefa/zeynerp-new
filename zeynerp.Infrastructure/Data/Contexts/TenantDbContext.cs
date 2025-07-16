using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Products;

namespace zeynerp.Infrastructure.Data.Contexts
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product() { Name = "iPhone" }
            );
        }
    }
}
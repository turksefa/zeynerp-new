using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Entities.User;

namespace zeynerp.Infrastructure.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Tenant> Tenants { get; set; }
    }
}
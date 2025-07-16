using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.Common.Models;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Entities.User;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Services.MultiTenancy
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;

        public TenantService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _applicationDbContext = context;
            _configuration = configuration;
        }

        public async Task<string> GetCurrentTenantConnectionStringAsync()
        {
            var userId = await GetCurrentUserIdAsync();
            var user = await _userManager.FindByIdAsync(userId);
            var tenant = await _applicationDbContext.Tenants
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == user.TenantId);
            return tenant.ConnectionString;
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user?.Id ?? string.Empty;
        }

        public async Task<Result<Guid>> CreateTenantDatabaseAsync()
        {
            var databaseName = $"TenantDb_{Guid.NewGuid():N}";
            var masterConnectionString = _configuration.GetConnectionString("DefaultConnection");
            var tenantConnectionString = masterConnectionString.Replace("zeynerp", databaseName);

            // Veritabanı oluştur
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
            optionsBuilder.UseSqlServer(tenantConnectionString);

            using var tenantDbContext = new TenantDbContext(optionsBuilder.Options);
            await tenantDbContext.Database.MigrateAsync();

            // Tenant kaydını ana veritabanına ekle
            var tenant = new Tenant
            {
                DatabaseName = databaseName,
                ConnectionString = tenantConnectionString,
                CreatedDate = DateTime.Now
            };

            _applicationDbContext.Tenants.Add(tenant);
            await _applicationDbContext.SaveChangesAsync();

            return Result<Guid>.Success(tenant.Id);
        }        
    }
}
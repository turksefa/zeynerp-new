using zeynerp.Domain.Entities.User;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class TenantRepository : ApplicationRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Tenant?> GetTenantByIdAsync(Guid tenantId) =>
            await _dbSet.FindAsync(tenantId);
    }
}
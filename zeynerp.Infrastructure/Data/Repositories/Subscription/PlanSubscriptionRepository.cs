using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Subscription;
using zeynerp.Domain.Repositories.Subscription;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Subscription
{
    public class PlanSubscriptionRepository : ApplicationRepository<PlanSubscription>, IPlanSubscriptionRepository
    {
        public PlanSubscriptionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<PlanSubscription>> GetSubscriptionsByTenantIdAsync(Guid tenantId) => await _dbSet
                .Where(ps => ps.TenantId == tenantId)
                .Include(ps => ps.PlanPricing)
                .Include(ps => ps.Plan)
                .ToListAsync();
    }
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Subscription;
using zeynerp.Domain.Repositories.Subscription;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Subscription
{
    public class PlanPricingRepository : ApplicationRepository<PlanPricing>, IPlanPricingRepository
    {
        public PlanPricingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<PlanPricing?> GetLatestPlanPricingByDateAsync(int planId) =>
            await _dbSet.Where(pp => pp.PlanId == planId).OrderBy(pp => pp.CreatedDate).LastOrDefaultAsync();
    }
}
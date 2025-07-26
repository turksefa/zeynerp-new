using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Subscription;
using zeynerp.Domain.Repositories.Subscription;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Subscription
{
    public class PlanRepository : ApplicationRepository<Plan>, IPlanRepository
    {
        public PlanRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<IEnumerable<Plan>> GetAllAsync() => await _dbSet.Include(p => p.PlanPricings).ToListAsync();
    }
}
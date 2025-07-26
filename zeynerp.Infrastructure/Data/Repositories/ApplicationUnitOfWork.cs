using zeynerp.Domain.Repositories;
using zeynerp.Domain.Repositories.Subscription;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPlanRepository _planRepository;
        private readonly IPlanSubscriptionRepository _planSubscriptionRepository;
        private readonly IPlanPricingRepository _planPricingRepository;

        public ApplicationUnitOfWork(ApplicationDbContext applicationDbContext, IPlanRepository planRepository, IPlanSubscriptionRepository planSubscriptionRepository, IPlanPricingRepository planPricingRepository)
        {
            _applicationDbContext = applicationDbContext;
            _planRepository = planRepository;
            _planSubscriptionRepository = planSubscriptionRepository;
            _planPricingRepository = planPricingRepository;
        }

        public IPlanRepository PlanRepository => _planRepository;

        public IPlanSubscriptionRepository PlanSubscriptionRepository => _planSubscriptionRepository;

        public IPlanPricingRepository PlanPricingRepository => _planPricingRepository;

        public async Task<int> SaveChangesAsync() => await _applicationDbContext.SaveChangesAsync();
    }
}
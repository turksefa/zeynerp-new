using zeynerp.Domain.Repositories.Subscription;

namespace zeynerp.Domain.Repositories
{
    public interface IApplicationUnitOfWork
    {
        IPlanRepository PlanRepository { get; }
        IPlanSubscriptionRepository PlanSubscriptionRepository { get; }
        IPlanPricingRepository PlanPricingRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
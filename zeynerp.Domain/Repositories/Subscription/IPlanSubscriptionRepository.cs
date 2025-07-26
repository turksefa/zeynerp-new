using zeynerp.Domain.Entities.Subscription;

namespace zeynerp.Domain.Repositories.Subscription
{
    public interface IPlanSubscriptionRepository : IRepository<PlanSubscription>
    {
        Task<IEnumerable<PlanSubscription>> GetSubscriptionsByTenantIdAsync(Guid tenantId);
    }
}
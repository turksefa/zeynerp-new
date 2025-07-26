using zeynerp.Domain.Entities.Subscription;

namespace zeynerp.Domain.Repositories.Subscription
{
    public interface IPlanPricingRepository : IRepository<PlanPricing>
    {
        Task<PlanPricing?> GetLatestPlanPricingByDateAsync(Guid planId);
    }
}
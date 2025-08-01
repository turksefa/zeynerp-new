using zeynerp.Domain.Entities.Subscription;

namespace zeynerp.Domain.Repositories.Subscription
{
    public interface IPlanPricingRepository : IApplicationRepository<PlanPricing>
    {
        Task<PlanPricing?> GetLatestPlanPricingByDateAsync(int planId);
    }
}
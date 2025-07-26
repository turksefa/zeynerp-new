using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Subscription;

namespace zeynerp.Application.Services.Subscription
{
    public interface IPlanSubscriptionService
    {
        Task<IEnumerable<PlanSubscriptionDto>> GetSubscriptionsAsync();
        Task<Result<bool>> CreateSubscriptionAsync(PlanSubscriptionDto planSubscriptionDto);
    }
}
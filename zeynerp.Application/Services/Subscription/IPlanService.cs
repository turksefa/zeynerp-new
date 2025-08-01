using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Subscription;

namespace zeynerp.Application.Services.Subscription
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanDto>> GetPlansAsync();
        Task<Result<bool>> PlanExistsAsync(int planId);
    }
}
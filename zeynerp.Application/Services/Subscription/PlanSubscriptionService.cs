using AutoMapper;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Subscription;
using zeynerp.Domain.Entities.Subscription;
using zeynerp.Domain.Repositories;

namespace zeynerp.Application.Services.Subscription
{
    public class PlanSubscriptionService : IPlanSubscriptionService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IPlanService _planService;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public PlanSubscriptionService(IApplicationUnitOfWork applicationUnitOfWork, IPlanService planService, ITenantService tenantService, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _planService = planService;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlanSubscriptionDto>> GetSubscriptionsAsync()
        {
            var tenantId = await _tenantService.GetTenantIdAsync();
            
            var subscriptions = await _applicationUnitOfWork.PlanSubscriptionRepository.GetSubscriptionsByTenantIdAsync(tenantId);
            var group = subscriptions.GroupBy(ps => ps.PlanId).Select(g => new PlanSubscriptionDto
            {
                PlanId = g.Key,
                Plan = _mapper.Map<PlanDto>(g.First().Plan),
                Count = g.Count()
            });

            return group;
        }

        public async Task<Result<bool>> CreateSubscriptionAsync(PlanSubscriptionDto planSubscriptionDto)
        {
            var exists = await _planService.PlanExistsAsync(planSubscriptionDto.PlanId);
            if (!exists.Data)
                return Result<bool>.Failure("Plan does not exist.");

            var planSubscription = _mapper.Map<PlanSubscription>(planSubscriptionDto);
            planSubscription.UserId = await _tenantService.GetCurrentUserIdAsync();
            planSubscription.TenantId = await _tenantService.GetTenantIdAsync();

            var planPricing = await _applicationUnitOfWork.PlanPricingRepository.GetLatestPlanPricingByDateAsync(planSubscriptionDto.PlanId);
            planSubscription.PlanPricingId = planPricing.Id;

            if (planSubscription.UserId == null || planSubscription?.TenantId == null)
                return Result<bool>.Failure("User or Tenant information is missing.");

            await _applicationUnitOfWork.PlanSubscriptionRepository.AddAsync(planSubscription);

            return Result<bool>.Success();
        }        
    }
}
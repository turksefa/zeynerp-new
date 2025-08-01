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
        private readonly ITenantRepository _tenantRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserRepository _userRepository;

        public ApplicationUnitOfWork(ApplicationDbContext applicationDbContext,
            IPlanRepository planRepository,
            IPlanSubscriptionRepository planSubscriptionRepository,
            IPlanPricingRepository planPricingRepository,
            ITenantRepository tenantRepository,
            IInvitationRepository invitationRepository,
            IUserRepository userRepository)
        {
            _applicationDbContext = applicationDbContext;
            _planRepository = planRepository;
            _planSubscriptionRepository = planSubscriptionRepository;
            _planPricingRepository = planPricingRepository;
            _tenantRepository = tenantRepository;
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
        }

        public IPlanRepository PlanRepository => _planRepository;

        public IPlanSubscriptionRepository PlanSubscriptionRepository => _planSubscriptionRepository;

        public IPlanPricingRepository PlanPricingRepository => _planPricingRepository;

        public ITenantRepository TenantRepository => _tenantRepository;

        public IInvitationRepository InvitationRepository => _invitationRepository;

        public IUserRepository UserRepository => _userRepository;

        public async Task<int> SaveChangesAsync() => await _applicationDbContext.SaveChangesAsync();
    }
}
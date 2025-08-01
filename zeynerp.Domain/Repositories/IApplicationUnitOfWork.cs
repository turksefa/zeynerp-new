using zeynerp.Domain.Repositories.Subscription;

namespace zeynerp.Domain.Repositories
{
    public interface IApplicationUnitOfWork
    {
        ITenantRepository TenantRepository { get; }
        IUserRepository UserRepository { get; }
        IInvitationRepository InvitationRepository { get; }
        IPlanRepository PlanRepository { get; }
        IPlanSubscriptionRepository PlanSubscriptionRepository { get; }
        IPlanPricingRepository PlanPricingRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
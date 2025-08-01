using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Entities.User;
using zeynerp.Domain.Enums;

namespace zeynerp.Domain.Entities.Subscription
{
    public class PlanSubscription : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public int TenantId { get; set; }
        public int PlanId { get; set; }
        public int PlanPricingId { get; set; }

        // Subscription details
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public SubscriptionPeriod Period { get; set; }
        public decimal PaidAmount { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public Tenant Tenant { get; set; } = null!;
        public Plan Plan { get; set; } = null!;
        public PlanPricing PlanPricing { get; set; } = null!;
    }
}
using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Enums;

namespace zeynerp.Domain.Entities.Subscription
{
    public class PlanPricing : BaseEntity
    {
        public Guid PlanId { get; set; }
        public SubscriptionPeriod Period { get; set; } // Aylık/Yıllık
        public decimal Price { get; set; }
        public decimal? DiscountPercentage { get; set; } // Yıllık için indirim

        // Navigation properties
        public Plan Plan { get; set; } = null!;
        public ICollection<PlanSubscription> PlanSubscriptions { get; set; } = new List<PlanSubscription>();
    }
}
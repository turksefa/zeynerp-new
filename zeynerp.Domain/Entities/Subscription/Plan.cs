using zeynerp.Domain.Entities.Common;

namespace zeynerp.Domain.Entities.Subscription
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public ICollection<PlanPricing> PlanPricings { get; set; } = new List<PlanPricing>();
        public ICollection<PlanSubscription> PlanSubscriptions { get; set; } = new List<PlanSubscription>();
        
    }
}
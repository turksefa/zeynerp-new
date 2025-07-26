using zeynerp.Domain.Entities.Subscription;

namespace zeynerp.Web.Models.Subscription
{
    public class PlanViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<PlanPricingViewModel> PlanPricings { get; set; } = new List<PlanPricingViewModel>();
    }
}
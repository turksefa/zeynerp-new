namespace zeynerp.Application.DTOs.Subscription
{
    public class PlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<PlanPricingDto> PlanPricings { get; set; } = new List<PlanPricingDto>();
    }
}
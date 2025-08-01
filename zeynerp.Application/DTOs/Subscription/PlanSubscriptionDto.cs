namespace zeynerp.Application.DTOs.Subscription
{
    public class PlanSubscriptionDto
    {
        public int PlanId { get; set; }
        public PlanDto Plan { get; set; } = null!;
        public int Count { get; set; }
    }
}
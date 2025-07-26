namespace zeynerp.Web.Models.Subscription
{
    public class PlanSubscriptionViewModel
    {
        public Guid PlanId { get; set; }
        public PlanViewModel Plan { get; set; } = null!;
        public int Count { get; set; }
    }
}
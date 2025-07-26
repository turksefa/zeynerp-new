using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Entities.Subscription;
using zeynerp.Domain.Entities.User;
using zeynerp.Domain.Enums;

namespace zeynerp.Infrastructure.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanPricing> PlanPricings { get; set; }
        public DbSet<PlanSubscription> PlanSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var purchaseId = Guid.NewGuid();
            var marketingId = Guid.NewGuid();

            builder.Entity<Plan>().HasData(
                new Plan
                {
                    Id = purchaseId,
                    Name = "Satın Alma",
                    Code = "purchase",
                    IsActive = true
                },
                new Plan
                {
                    Id = marketingId,
                    Name = "Pazarlama",
                    Code = "marketing",
                    IsActive = true
                }
            );

            builder.Entity<PlanPricing>().HasData(
                new PlanPricing
                {
                    PlanId = purchaseId,
                    Period = SubscriptionPeriod.Monthly,
                    Price = 0,
                    DiscountPercentage = null,
                    CreatedDate = DateTime.Now
                },
                new PlanPricing
                {
                    PlanId = marketingId,
                    Period = SubscriptionPeriod.Monthly,
                    Price = 1500.00m,
                    DiscountPercentage = null,
                    CreatedDate = DateTime.Now
                }
            );

            builder.Entity<PlanSubscription>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Tenant)
                    .WithMany()
                    .HasForeignKey(e => e.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Plan)
                    .WithMany(e => e.PlanSubscriptions)
                    .HasForeignKey(e => e.PlanId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PlanPricing)
                    .WithMany(e => e.PlanSubscriptions)
                    .HasForeignKey(e => e.PlanPricingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Domain.Repositories;
using zeynerp.Domain.Repositories.Subscription;
using zeynerp.Domain.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.StokTanimlamalar;
using zeynerp.Infrastructure.Data.Repositories;
using zeynerp.Infrastructure.Data.Repositories.Subscription;
using zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.StokTanimlamalar;
using zeynerp.Infrastructure.Services;
using zeynerp.Infrastructure.Services.Email;
using zeynerp.Infrastructure.Services.Identity;
using zeynerp.Infrastructure.Services.MultiTenancy;

namespace zeynerp.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddScoped<IInvitationService, InvitationService>();

            services.AddScoped<IInvitationRepository, InvitationRepository>();

            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanSubscriptionRepository, PlanSubscriptionRepository>();
            services.AddScoped<IPlanPricingRepository, PlanPricingRepository>();

            services.AddScoped<IStokGrupTanimRepository, StokGrupTanimRepository>();

            services.AddScoped<ICariTanimRepository, CariTanimRepository>();
            services.AddScoped<ICariTurTanimRepository, CariTurTanimRepository>();
            services.AddScoped<ICariYetkiliTanimRepository, CariYetkiliTanimRepository>();
            services.AddScoped<ITeslimatAdresTanimRepository, TeslimatAdresTanimRepository>();

            return services;
        }
    }
}
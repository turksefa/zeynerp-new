using Microsoft.Extensions.DependencyInjection;
using zeynerp.Application.Services.Subscription;
using zeynerp.Application.Services.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Application.Services.Tanimlamalar.StokTanimlamalar;

namespace zeynerp.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IPlanSubscriptionService, PlanSubscriptionService>();

            services.AddScoped<IStokGrupTanimService, StokGrupTanimService>();

            services.AddScoped<ICariTanimService, CariTanimService>();
            services.AddScoped<ICariTurTanimService, CariTurTanimService>();

            return services;
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.Mapper;
using zeynerp.Application.Services.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Application.Services.Tanimlamalar.StokTanimlamalar;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Repositories;
using zeynerp.Domain.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.StokTanimlamalar;
using zeynerp.Infrastructure.Data.Contexts;
using zeynerp.Infrastructure.Data.Repositories;
using zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.StokTanimlamalar;
using zeynerp.Infrastructure.Services.Authentication;
using zeynerp.Infrastructure.Services.Email;
using zeynerp.Infrastructure.Services.MultiTenancy;
using zeynerp.Web.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<TenantDbContext>(serviceProvider =>
{
    var tenantService = serviceProvider.GetRequiredService<ITenantService>();
    var connectionString = tenantService.GetCurrentTenantConnectionStringAsync().Result;

    if (string.IsNullOrEmpty(connectionString))
        return null;

    var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
    optionsBuilder.UseSqlServer(connectionString);

    return new TenantDbContext(optionsBuilder.Options);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IStokGrupTanimRepository, StokGrupTanimRepository>();
builder.Services.AddScoped<IStokGrupTanimService, StokGrupTanimService>();

builder.Services.AddScoped<ICariTanimRepository, CariTanimRepository>();
builder.Services.AddScoped<ICariTanimService, CariTanimService>();

builder.Services.AddScoped<ICariTurTanimRepository, CariTurTanimRepository>();
builder.Services.AddScoped<ICariTurTanimService, CariTurTanimService>();


builder.Services.AddScoped<ICariYetkiliTanimRepository, CariYetkiliTanimRepository>();

builder.Services.AddScoped<ITeslimatAdresTanimRepository, TeslimatAdresTanimRepository>();

builder.Services.AddAuthentication().AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddMicrosoftAccount(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

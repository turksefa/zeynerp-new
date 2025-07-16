using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.Services.Identity;
using zeynerp.Application.Services.Products;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;
using zeynerp.Infrastructure.Data.Repositories;
using zeynerp.Infrastructure.Data.Repositories.Products;
using zeynerp.Infrastructure.Services.Identity;
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

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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

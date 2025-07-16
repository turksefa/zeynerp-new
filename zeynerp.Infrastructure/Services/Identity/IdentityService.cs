using Microsoft.AspNetCore.Identity;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Authentication;
using zeynerp.Application.Services.Identity;
using zeynerp.Domain.Entities.Identity;

namespace zeynerp.Infrastructure.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITenantService _tenantService;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITenantService tenantService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tenantService = tenantService;
        }

        public async Task<Result<string>> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                FullName = registerDto.FullName,
                CompanyName = registerDto.CompanyName,
                UserName = registerDto.Email,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return Result<string>.Failure(result.Errors.Select(e =>
                    e.Code == "DuplicateUserName" ? "Bu e-posta adresi zaten kullanılmakta." : e.Description
                ).ToList());

            var tenantId = await _tenantService.CreateTenantDatabaseAsync();

            user.TenantId = tenantId.Data;
            await _userManager.UpdateAsync(user);

            return Result<string>.Success("Success");
        }

        public async Task<Result<string>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Result<string>.Failure("Kullanıcı bulunamadı.");

            if(user.EmailConfirmed == false)
                return Result<string>.Failure("Kayıt işlemini tamamlamak için e-posta adresinize gönderilen bağlantıya tıklamanız gerekiyor.");

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (!result.Succeeded)
                return Result<string>.Failure("Geçersiz kullanıcı adı veya şifre.");

            return Result<string>.Success("Success");
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}
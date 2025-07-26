using System.Net;
using Microsoft.AspNetCore.Identity;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Authentication;
using zeynerp.Domain.Entities.Identity;

namespace zeynerp.Infrastructure.Services.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITenantService _tenantService;
        private readonly IEmailService _emailService;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITenantService tenantService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tenantService = tenantService;
            _emailService = emailService;
        }

        public async Task<Result<bool>> RegisterAsync(RegisterDto registerDto)
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
                return Result<bool>.Failure(result.Errors.Select(e => e.Description).ToList());

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(emailConfirmationToken);
            var confirmationLink = $"https://localhost:7240/Authentication/ConfirmEmail?userId={user.Id}&token={encodedToken}";

            var emailSent = await _emailService.SendConfirmationEmailAsync(user.Email, $"{user.FullName}", confirmationLink);
            if (!emailSent)
            {
                await _userManager.DeleteAsync(user);
                return Result<bool>.Failure("Kayıt işlemi tamamlanamadı. Lütfen tekrar deneyin.");
            }

            var tenantResult = await _tenantService.CreateTenantDatabaseAsync();
            if (!tenantResult.IsSuccess)
            {
                await _userManager.DeleteAsync(user);
                return Result<bool>.Failure("Kayıt işlemi tamamlanamadı. Lütfen tekrar deneyin.");
            }

            user.TenantId = tenantResult.Data;
            await _userManager.UpdateAsync(user);

            // var updateResult = await _userManager.UpdateAsync(user);
            // if (!updateResult.Succeeded)
            // {
            //     await _userManager.DeleteAsync(user);
            //     Delete tenant
            // }

            return Result<bool>.Success();
        }

        public async Task<Result<bool>> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<bool>.Failure("Aktivasyon bağlantısı geçersiz veya bozulmuş.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return Result<bool>.Failure("Aktivasyon bağlantısı geçersiz veya bozulmuş.");

            return Result<bool>.Success();
        }

        public async Task<Result<bool>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Result<bool>.Failure("Bu e-posta adresi sistemde kayıtlı değil.");

            if (user.EmailConfirmed == false)
                return Result<bool>.Failure("E-posta adresinize gönderilen aktivasyon bağlantısına tıklayarak hesabınızı aktifleştirin.");

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (!result.Succeeded)
                return Result<bool>.Failure("Geçersiz e-posta veya şifre.");

            return Result<bool>.Success();
        }

        public async Task<Result<bool>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return Result<bool>.Failure("Bu e-posta adresi sistemde kayıtlı değil.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var resetLink = $"https://localhost:7240/sifre-sifirla?userId={user.Id}&token={encodedToken}";


            var emailSent = await _emailService.SendPasswordResetEmailAsync(user.Email, $"{user.FullName}", resetLink);
            if (!emailSent)
                return Result<bool>.Failure("Şifre sıfırlama işlemi tamamlanamadı. Lütfen tekrar deneyin.");

            return Result<bool>.Success();
        }

        public async Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
            if (user == null)
                return Result<bool>.Failure("Bu e-posta adresi sistemde kayıtlı değil.");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!result.Succeeded)
                return Result<bool>.Failure(result.Errors.Select(e => e.Description).ToList());

            return Result<bool>.Success();
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}
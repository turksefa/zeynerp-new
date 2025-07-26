using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Authentication;

namespace zeynerp.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<bool>> RegisterAsync(RegisterDto registerDto);
        Task<Result<bool>> ConfirmEmailAsync(string userId, string token);
        Task<Result<bool>> LoginAsync(LoginDto loginDto);
        Task<Result<bool>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task LogoutAsync();
    }
}
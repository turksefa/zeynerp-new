using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Authentication;

namespace zeynerp.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<string>> RegisterAsync(RegisterDto registerDto);
        Task<Result<string>> ConfirmEmailAsync(string userId, string token);
        Task<Result<string>> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
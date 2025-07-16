using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Authentication;

namespace zeynerp.Application.Services.Identity
{
    public interface IIdentityService
    {
        Task<Result<string>> RegisterAsync(RegisterDto registerDto);
        Task<Result<string>> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
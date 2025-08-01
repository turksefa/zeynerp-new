using zeynerp.Domain.Entities.Identity;

namespace zeynerp.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    }
}
using zeynerp.Domain.Entities.Identity;

namespace zeynerp.Domain.Repositories
{
    public interface IUserRepository : IApplicationRepository<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(Guid tenantId);
    }
}
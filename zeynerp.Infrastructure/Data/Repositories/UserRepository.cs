using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class UserRepository : ApplicationRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(Guid tenantId) =>
            await _applicationDbContext.Users
                .Where(user => user.TenantId == tenantId)
                .OrderByDescending(u => u.EmailConfirmed)
                .ToListAsync();
    }
}
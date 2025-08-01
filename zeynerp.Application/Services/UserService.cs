using zeynerp.Application.Common.Interfaces;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Repositories;

namespace zeynerp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly ITenantService _tenantService;

        public UserService(IApplicationUnitOfWork applicationUnitOfWork, ITenantService tenantService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var tenantId = await _tenantService.GetTenantIdAsync();
            return await _applicationUnitOfWork.UserRepository.GetAllUsersAsync(tenantId);
        }
    }
}
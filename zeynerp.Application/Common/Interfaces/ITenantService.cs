using zeynerp.Application.Common.Models;

namespace zeynerp.Application.Common.Interfaces
{
    public interface ITenantService
    {
        Task<string> GetCurrentTenantConnectionStringAsync();
        Task<Result<Guid>> CreateTenantDatabaseAsync();
        Task<string> GetCurrentUserIdAsync();
    }
}
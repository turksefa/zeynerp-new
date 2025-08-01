using zeynerp.Domain.Entities.User;

namespace zeynerp.Domain.Repositories
{
    public interface ITenantRepository : IApplicationRepository<Tenant>
    {
        Task<Tenant?> GetTenantByIdAsync(Guid tenantId);
    }
}
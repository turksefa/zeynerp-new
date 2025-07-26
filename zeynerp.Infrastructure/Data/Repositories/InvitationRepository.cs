using zeynerp.Domain.Entities;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class InvitationRepository : Repository<Invitation>, IInvitationRepository
    {
        public InvitationRepository(TenantDbContext tenantDbContext) : base(tenantDbContext)
        {
        }
    }
}
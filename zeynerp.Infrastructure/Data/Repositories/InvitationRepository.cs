using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class InvitationRepository : ApplicationRepository<Invitation>, IInvitationRepository
    {
        public InvitationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<Invitation?> GetInvitationByIdAsync(Guid id) =>
            await _dbSet.Include(i => i.User).FirstOrDefaultAsync(i => i.Id == id);
    }
}
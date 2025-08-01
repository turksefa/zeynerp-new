using zeynerp.Domain.Entities;

namespace zeynerp.Domain.Repositories
{
    public interface IInvitationRepository : IApplicationRepository<Invitation>
    {
        Task<Invitation?> GetInvitationByIdAsync(Guid id);
    }
}
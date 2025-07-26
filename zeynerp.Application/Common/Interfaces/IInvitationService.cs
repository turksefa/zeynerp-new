using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs;

namespace zeynerp.Application.Common.Interfaces
{
    public interface IInvitationService
    {
        Task<Result<bool>> SendInvitationAsync(InvitationDto invitationDto);
        // Task<IReadOnlyList<InvitationDto>> GetInvitationsByTenantIdAsync(Guid tenantId);
        // Task<InvitationDto> InvitationExistsAsync(string email);
    }
}
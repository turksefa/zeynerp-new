using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs;

namespace zeynerp.Application.Common.Interfaces
{
    public interface IInvitationService
    {
        Task<InvitationDto> GetInvitationByIdAsync(Guid invitationId);
        Task<Result<bool>> SendInvitationAsync(InvitationDto invitationDto);
        Task<Result<bool>> AcceptInvitationAsync(InvitationDto invitationDto);
        // Task<IReadOnlyList<InvitationDto>> GetInvitationsByTenantIdAsync(Guid tenantId);
        // Task<InvitationDto> InvitationExistsAsync(string email);
    }
}
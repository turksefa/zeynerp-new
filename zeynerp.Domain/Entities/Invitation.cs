using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Enums;

namespace zeynerp.Domain.Entities
{
    public class Invitation : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public InvitationStatus Status { get; set; }

        public ApplicationUser User { get; set; } = null!;
    }
}
using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Enums;

namespace zeynerp.Domain.Entities
{
    public class Invitation : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
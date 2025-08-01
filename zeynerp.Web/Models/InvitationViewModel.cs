using zeynerp.Domain.Entities.Identity;

namespace zeynerp.Web.Models
{
    public class InvitationViewModel
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public ApplicationUser? User { get; set; }
        public IEnumerable<ApplicationUser>? Users { get; set; }
    }
}
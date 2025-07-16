using Microsoft.AspNetCore.Identity;
using zeynerp.Domain.Entities.User;

namespace zeynerp.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public Guid? TenantId { get; set; }

        // Navigation properties
        public Tenant Tenant { get; set; } = null!;
    }
}
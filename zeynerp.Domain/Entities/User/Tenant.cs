using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Entities.Identity;

namespace zeynerp.Domain.Entities.User
{
    public class Tenant : BaseEntity
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
 
        // Navigation properties
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
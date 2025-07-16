namespace zeynerp.Web.Models.Identity
{
    public class RegisterViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
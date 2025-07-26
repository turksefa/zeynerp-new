namespace zeynerp.Web.Models.Authentication
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
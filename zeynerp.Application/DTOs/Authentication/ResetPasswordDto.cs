namespace zeynerp.Application.DTOs.Authentication
{
    public class ResetPasswordDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
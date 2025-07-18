namespace zeynerp.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true);
        Task<bool> SendConfirmationEmailAsync(string to, string userName, string confirmationLink);
        Task<bool> SendPasswordResetEmailAsync(string to, string userName, string resetLink);
    }
}
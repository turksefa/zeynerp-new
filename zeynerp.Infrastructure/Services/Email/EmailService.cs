using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using zeynerp.Application.Common.Interfaces;

namespace zeynerp.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sturk", "sefa@zeynerp.com"));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = body };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("mail.zeynerp.com", 587, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync("sefa@zeynerp.com", "A54ScPprrJ");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendConfirmationEmailAsync(string to, string userName, string confirmationLink)
        {
            var subject = "Hesap Onaylama";
            var body = $@"
            <html>
            <body>
                <h2>Merhaba {userName},</h2>
                <p>Hesabınızı onaylamak için aşağıdaki bağlantıya tıklayın:</p>
                <a href='{confirmationLink}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Hesabı Onayla</a>
                <p>Eğer bu işlemi siz yapmadıysanız, bu e-postayı görmezden gelebilirsiniz.</p>
                <br>
                <p>Teşekkürler,<br>Uygulama Ekibi</p>
            </body>
            </html>";

            return await SendEmailAsync(to, subject, body);
        }

        public async Task<bool> SendPasswordResetEmailAsync(string to, string userName, string resetLink)
        {
            var subject = "Şifre Sıfırlama";
            var body = $@"
            <html>
            <body>
                <h2>Merhaba {userName},</h2>
                <p>Şifrenizi sıfırlamak için aşağıdaki bağlantıya tıklayın:</p>
                <a href='{resetLink}' style='background-color: #FF6B6B; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Şifreyi Sıfırla</a>
                <p>Bu bağlantı 24 saat geçerlidir.</p>
                <p>Eğer bu işlemi siz yapmadıysanız, bu e-postayı görmezden gelebilirsiniz.</p>
                <br>
                <p>Teşekkürler,<br>Uygulama Ekibi</p>
            </body>
            </html>";

            return await SendEmailAsync(to, subject, body);
        }
    }
}
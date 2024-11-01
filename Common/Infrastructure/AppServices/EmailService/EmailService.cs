using MailKit.Net.Smtp;
using MimeKit;

namespace Infrastructure.AppServices.EmailService
{
    public class EmailService(string? smtpServer, int smtpPort, string? smtpUser, string? smtpPass)
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("HAZAR", smtpUser));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort, true);
                await client.AuthenticateAsync(smtpUser, smtpPass);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}

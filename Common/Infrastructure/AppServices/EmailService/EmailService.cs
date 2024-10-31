using MailKit.Net.Smtp;
using MimeKit;

namespace Infrastructure.AppServices.EmailService
{
    public class EmailService
    {
        private readonly string? _smtpServer;
        private readonly int _smtpPort;
        private readonly string? _smtpUser;
        private readonly string? _smtpPass;

        public EmailService(string? smtpServer, int smtpPort, string? smtpUser, string? smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("HAZAR", _smtpUser));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, true);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}

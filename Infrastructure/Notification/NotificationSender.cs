using Microsoft.Extensions.Options;
using Application.Common.Interfaces;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Threading.Tasks;

namespace Infrastructure.Notification
{
    public class NotificationSender : INotificationSender
    {
        private readonly NotificationSettings _config;

        public NotificationSender(IOptions<NotificationSettings> config)
        {
            _config = config.Value;
        }

        public async Task SendEmailAsync(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_config.SmtpHost, _config.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.SmtpUser, _config.SmtpPass);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
        public async Task SendNotificationEmailAsync(string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.SmtpUser));
            email.To.Add(MailboxAddress.Parse(_config.NotificationEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_config.SmtpHost, _config.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.SmtpUser, _config.SmtpPass);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
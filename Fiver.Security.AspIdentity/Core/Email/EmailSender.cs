using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Fiver.Security.AspIdentity.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            var message = CreateMessage(to, subject, body);
            var task = Task.Run(() => SendEmail(message));

            _logger.LogInformation($"{body}");
            return task;
        }

        private void SendEmail(MimeMessage message)
        {
            // Should get params from config
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("z.f.blkn@gmail.com", "Zafer3092");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        private static MimeMessage CreateMessage(string to, string subject, string body)
        {
            // Should get params from config
            var message = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart("plain")
                {
                    Text = body,
                    ContentType = {Charset = "UTF-8"}
                }
            };
            message.From.Add(new MailboxAddress("CRM", "z.f.blkn@gmail.com"));
            message.To.Add(new MailboxAddress("User", to));
            return message;
        }
    }
}
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Fiver.Security.AspIdentity.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly string _sender;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _sender = "z.f.blkn@gmail.com";





            _logger = logger;
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CRM", "z.f.blkn@gmail.com"));
            message.To.Add(new MailboxAddress("User", to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            var task = Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_sender, "Zafer3092");
                    client.Send(message);
                    client.Disconnect(true);
                }
            });

            _logger.LogInformation($"{body}");
            //return Task.CompletedTask;
            return task;
        }
    }
}
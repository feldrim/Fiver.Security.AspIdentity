using System.Threading.Tasks;
using FluentEmail.Smtp;
using Microsoft.Extensions.Logging;

namespace Fiver.Security.AspIdentity.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            // Using Smtp Sender package
            FluentEmail.Core.Email.DefaultSender = new SmtpSender();

            _logger = logger;
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            var emailInstance = FluentEmail.Core.Email
                .From("feldrim@gmail.com")
                .To(to)
                .Subject(subject)
                .Body(body)
                .HighPriority();

            emailInstance.SendAsync();

            _logger.LogInformation($"{body}");
            return Task.CompletedTask;
        }
    }
}
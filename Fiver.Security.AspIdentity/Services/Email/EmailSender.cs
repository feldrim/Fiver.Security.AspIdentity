using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Fiver.Security.AspIdentity.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation($"{message}");
            return Task.CompletedTask;
        }
    }
}
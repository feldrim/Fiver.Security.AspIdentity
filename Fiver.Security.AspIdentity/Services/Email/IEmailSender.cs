using System.Threading.Tasks;

namespace Fiver.Security.AspIdentity.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
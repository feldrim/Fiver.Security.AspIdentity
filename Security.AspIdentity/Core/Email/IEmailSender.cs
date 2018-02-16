using System.Threading.Tasks;

namespace Security.AspIdentity.Core.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
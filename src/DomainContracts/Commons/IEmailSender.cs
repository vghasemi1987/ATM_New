using System.Threading.Tasks;

namespace DomainContracts.Commons
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, string fileAttachment);
    }
}
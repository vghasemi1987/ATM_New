using System.Threading.Tasks;
//using DomainEntities.EmailAggregate;

namespace DomainEntities.EmailAggregate
{
    public interface IEmailService
    {
        //Task SaveAsync(EmailOutbox model);
        Task BackgroundEmailSendAsync();
        Task SendEmailConfirmationAsync(string email, string link);
        Task SendResetPasswordAsync(string email, string link);
    }
}
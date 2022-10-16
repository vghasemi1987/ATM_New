using System.Threading.Tasks;
using DomainEntities.NotificationAggregate;

namespace DomainContracts.NotificationAggregate
{
    public interface INotificationService
    {
        Task<string> ConvertToReadAsync(int id);
        bool AddNotification(int creatorUser, string message, int forUser, NotificationType notiType, int fId, CategoryEnum category, bool sendToYourself = false);
        //Task BackgroundNotificationSend();
    }
}
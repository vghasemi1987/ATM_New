using System.Collections.Generic;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainEntities.NotificationAggregate;
using KendoHelper;

namespace DomainContracts.NotificationAggregate
{
    public interface INotificationRepository : IRepository<NotificationItem>, IAsyncRepository<NotificationItem>
    {
        Task<List<NotificationItem>> GetByUserIdNotificationsAsync(int userId, int count);
        DataSourceResult GetByUserIdNotificationsAsync(DataSourceRequest request, int userId);
    }
}

using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainContracts.NotificationAggregate;
using DomainEntities.NotificationAggregate;
using DomainEntities.SettingAggregate;

namespace DomainApplication.Services.NotificationAggregate
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IAsyncRepository<NotificationItem> _asyncRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private AuthNotificationOptions Options { get; }
        private readonly IAsyncRepository<Setting> _settingRepository;

        public NotificationService(INotificationRepository notificationRepository,
            //IOptions<AuthNotificationOptions> optionsAccessor,
            IUnitOfWork unitOfWork,
            IAsyncRepository<Setting> settingRepository,
            IAsyncRepository<NotificationItem> asyncRepository)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
            _asyncRepository = asyncRepository;
            //Options = optionsAccessor.Value;
        }

        public async Task<string> ConvertToReadAsync(int id)
        {
            var notification = _notificationRepository.GetSingleBySpec(o=>o.Id.Equals(id));
            notification.IsRead = true;
            //notificationList.ForEach(o => o.IsRead = true);
            _notificationRepository.Update(notification, o => o.IsRead);
            await _unitOfWork.SaveAsync();
            return notification.Url;
        }

        public bool AddNotification(int creatorUser, string message, int forUser, NotificationType notiType, int fId, CategoryEnum category, bool sendToYourself = false)
        {
            if (creatorUser == forUser && sendToYourself == false) return false;
            var notification = new NotificationItem
            {
                CreatedByUserId = creatorUser,
                ForUserId = forUser,
                Text = message,
                IsSent = false,
                CategoryId = (short)category
            };
            switch (notiType)
            {
                case NotificationType.ToDoTask:
                    notification.Url = $"/todotask/detail/{fId}";
                    break;
            }
            _notificationRepository.Add(notification);
            return true;
        }
    }
}
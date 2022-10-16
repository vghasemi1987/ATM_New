using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DomainContracts.NotificationAggregate;
using Web.Extensions;
using System.Collections.Generic;
using DomainEntities.NotificationAggregate;

namespace Web.ViewComponents
{
    public class Notification : ViewComponent
    {
        private readonly INotificationRepository _notificationRepository;

        public Notification(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
            //_toDoTaskRepository = toDoTaskRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var taskList = await _toDoTaskRepository.ListByUserIdForNotification(HttpContext.User.GetUserId());

            var notification = await _notificationRepository.GetByUserIdNotificationsAsync(HttpContext.User.GetUserId(), 30);

            return View(new List<NotificationItem>(notification));
        }
    }
}
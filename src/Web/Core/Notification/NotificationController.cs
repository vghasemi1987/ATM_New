using System.ComponentModel;
using System.Threading.Tasks;
using DomainContracts.NotificationAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.Notification
{
    [Authorize]
    [DisplayName("مدیریت اعلانات")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationService notificationService,
            INotificationRepository notificationRepository)
        {
            _notificationService = notificationService;
            _notificationRepository = notificationRepository;
        }

        [Permission]
        [DisplayName("لیست نمایشی")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRecords(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            var list = _notificationRepository.GetByUserIdNotificationsAsync(request, User.GetUserId());
            return Json(list);
        }

        public async Task<IActionResult> SelectedItem(int? id)
        {
            if (!id.HasValue) return NoContent();
            var notiUrl = await _notificationService.ConvertToReadAsync(id.Value);
            return Json(new {Redirect = notiUrl});
        }

        //[AjaxOnly, HttpPost]
        //[Permission("Notification_DeleteRows")]
        //[DisplayName("حذف")]
        //public async Task<IActionResult> DeleteRows(string[] ids)
        //{
        //    if (!ids.Any()) return Json(false);
        //    _agentRepository.Delete(ids.Select(int.Parse).ToList());
        //    await _unitOfWork.SaveAsync();
        //    return Ok();
        //}
    }
}
using System.ComponentModel;
using System.Threading.Tasks;
using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.SettingAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web.Core.Setting.ViewModels;
using Web.Extensions.Attributes;

namespace Web.Core.Setting
{
    [Authorize]
    [DisplayName("تنظیمات")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly ISettingRepository _settingRepository;

        public SettingController(ISettingService settingService,
            IWebHostEnvironment environment,
            IFileService fileService,
            ISettingRepository settingRepository)
        {
            _settingService = settingService;
            _environment = environment;
            _fileService = fileService;
            _settingRepository = settingRepository;
        }
        [Permission]
        [DisplayName("ایمیل")]
        public async Task<IActionResult> Email()
        {
            var setting = await _settingRepository.FindByIdAsync(1);
            var emailModel = new EmailViewModel();
            if (setting != null)
            {
                emailModel = new EmailViewModel
                {
                    Password = setting.EmailPassword,
                    EmailPortNumber = setting.EmailPortNumber.GetValueOrDefault(),
                    EmailSmtpServer = setting.EmailSmtpServer,
                    Username = setting.EmailUsername,
                    EnableSsl = setting.EnableSsl.GetValueOrDefault()
                };
            }
            return View(emailModel);
        }
        [Permission]
        [DisplayName("پیامک")]
        public async Task<IActionResult> Sms()
        {
            var setting = await _settingRepository.FindByIdAsync(1);
            var smsModel = new SmsViewModel();
            if (setting != null)
            {
                smsModel = new SmsViewModel
                {
                    Password = setting.SmsPassword,
                    UserName = setting.SmsUserName,
                    ServiceNumber = setting.SmsServiceNumber
                };
            }
            return View(smsModel);
        }
        [Permission]
        [DisplayName("سامانه")]
        public async Task<IActionResult> Application()
        {
            var setting = await _settingRepository.FindByIdAsync(1);
            var appConfig = new AppViewModel();
            if (setting != null)
            {
                appConfig = new AppViewModel
                {
                    WebSiteTitle = setting.WebSiteTitle                    
                };
            }
            return View(appConfig);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        public async Task<IActionResult> Email(EmailViewModel model)
        {
            var setting = new DomainEntities.SettingAggregate.Setting
            {
                Id = 1,
                EmailPassword = model.Password,
                EmailPortNumber = model.EmailPortNumber,
                EmailSmtpServer = model.EmailSmtpServer,
                EmailUsername = model.Username,
                EnableSsl = model.EnableSsl
            };
            await _settingService.SaveEmailAsync(setting);

            return Json(new
            {
                Message = Message.Show("تنظیمات با موفقیت ثبت شد...", MessageType.Success)
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        public async Task<IActionResult> Sms(SmsViewModel model)
        {
            var setting = new DomainEntities.SettingAggregate.Setting
            {
                Id = 1,
                SmsPassword = model.Password,
                SmsServiceNumber = model.ServiceNumber,
                SmsUserName = model.UserName
            };
            await _settingService.SaveSmsAsync(setting);

            return Json(new
            {
                Message = Message.Show("تنظیمات با موفقیت ثبت شد...", MessageType.Success)
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        public async Task<IActionResult> Application(AppViewModel model)
        {
            var setting = new DomainEntities.SettingAggregate.Setting
            {
                Id = 1,
                WebSiteTitle = model.WebSiteTitle
            };
            await _settingService.SaveAppAsync(setting);
            if (model.LogoFile != null)
            {
                await _fileService.SaveFileToServerAsync($"{_environment.WebRootPath}\\images\\ui-theme\\logo.png",
                    model.LogoFile);
            }

            if (model.FaviconFile != null)
            {
                await _fileService.SaveFileToServerAsync($"{_environment.WebRootPath}\\favicon.ico", model.FaviconFile);
            }

            return Json(new
            {
                Message = Message.Show("تنظیمات با موفقیت ثبت شد...", MessageType.Success)
            });
        }
    }
}
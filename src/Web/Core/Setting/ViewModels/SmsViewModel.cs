using System.ComponentModel.DataAnnotations;

namespace Web.Core.Setting.ViewModels
{
    public class SmsViewModel
    {
        [MaxLength(100), DataType(DataType.PhoneNumber, ErrorMessage = "{0} وارد شده صحیح نمی باشد!"),
         Display(Name = "شماره سرویس پیامک")]
        public string ServiceNumber { get; set; }

        [MaxLength(100), Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [MaxLength(100), Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}
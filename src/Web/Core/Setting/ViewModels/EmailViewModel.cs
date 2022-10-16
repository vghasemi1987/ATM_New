using System.ComponentModel.DataAnnotations;

namespace Web.Core.Setting.ViewModels
{
    public class EmailViewModel
    {
        [MaxLength(100), DataType(DataType.EmailAddress, ErrorMessage = "{0} وارد شده صحیح نمی باشد!"),
         Display(Name = "پست الکترونیکی")]
        public string Username { get; set; }

        [MaxLength(100), Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [MaxLength(100), Display(Name = "سرور SMTP")]
        public string EmailSmtpServer { get; set; }
        [Range(0, 37000, ErrorMessage = "مقدار وارد شده برای {0} صحیح نمی باشد!"), Display(Name = "پورت پست الکترونیکی")]
        public short? EmailPortNumber { get; set; }

        [Display(Name = "وضعیت SSL")]
        public bool? EnableSsl { get; set; }
    }
}
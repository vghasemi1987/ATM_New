using System.ComponentModel.DataAnnotations;

namespace Web.Core.UserManagement.ViewModels
{
    public class SigninViewModel
    {
        [Required(ErrorMessage = "{0} را وارد نمایید")]
        [Display(Name = "نام کاربری", Prompt = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور", Prompt ="رمزعبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}

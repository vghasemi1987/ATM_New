using System.ComponentModel.DataAnnotations;

namespace Web.Core.UserManagement.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "{0} را وارد نمایید"), StringLength(100, ErrorMessage = "فیلد {0} باید حداقل {2} کاراکتر باشد!", MinimumLength = 6),
        DataType(DataType.Password), Display(Name = "رمز عبور فعلی", Prompt = "رمز عبور فعلی")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید"), StringLength(100, ErrorMessage = "فیلد {0} باید حداقل {2} کاراکتر باشد!", MinimumLength = 6),
        DataType(DataType.Password), Display(Name = "رمز عبور جدید", Prompt = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Display(Name = "تکرار رمز عبور", Prompt = "تکرار رمز عبور"),
        Compare("NewPassword", ErrorMessage = "{0} یکسان نیست")]
        public string ConfirmPassword { get; set; }
    }
}

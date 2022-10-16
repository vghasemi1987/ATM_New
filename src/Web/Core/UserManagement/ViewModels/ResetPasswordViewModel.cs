using System.ComponentModel.DataAnnotations;

namespace Web.Core.UserManagement.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "{0} را وارد نمایید"), EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد"),
        Display(Name = "پست الکترونیکی", Prompt = "پست الکترونیکی")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید"), StringLength(100, ErrorMessage = "فیلد {0} باید حداقل {2} کاراکتر باشد!", MinimumLength = 6),
        DataType(DataType.Password), Display(Name = "رمز عبور", Prompt = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "تایید رمز عبور", Prompt = "تایید رمز عبور"),
        Compare("Password", ErrorMessage = "{0} یکسان نیست")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}

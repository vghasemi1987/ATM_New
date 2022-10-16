using System.ComponentModel.DataAnnotations;

namespace Web.Core.UserManagement.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "{0} را وارد نمایید"), EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد"),
        Display(Name = "پست الکترونیکی", Prompt = "پست الکترونیکی")]
        public string Email { get; set; }
    }
}

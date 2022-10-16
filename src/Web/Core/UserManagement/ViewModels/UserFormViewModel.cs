using System.ComponentModel.DataAnnotations;
using ApplicationCommon.WebToolkit.ValidationAttributes;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Core.UserManagement.ViewModels
{
    public class UserFormViewModel
    {
        [HiddenInput]
        public int UserId { set; get; }

        [Required(ErrorMessage = "{0} کاربر را وارد نمایید"),
        Display(Name = "نام"), MaxLength(150, ErrorMessage = "حداکثر {0} کاربر باید {1} کاراکتر باشد")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} کاربر را وارد نمایید"),
         Display(Name = "نام خانوادگی"), MaxLength(150, ErrorMessage = "حداکثر {0} کاربر باید {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید"), Display(Name = "کدپرسنلی"), MaxLength(10, ErrorMessage = "حداکثر {0} کاربر باید {1} کاراکتر باشد")]
        public string PersonnelCode { get; set; }

        //[Required(ErrorMessage = "{0} را تعیین نمایید"), Display(Name = "جنسیت")]
        //public bool? Gender { get; set; }

        //[Required(ErrorMessage = "{0} کاربر را وارد نمایید"),
        //Display(Name = "پست الکترونیکی"), EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
        //public string Email { get; set; }

        [StringLength(15, ErrorMessage = "شماره {0} وارد شده صحیح نمی باشد", MinimumLength = 10), Display(Name = "تلفن همراه"),
        Phone(ErrorMessage = "شماره {0} وارد شده صحیح نمی باشد")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید"),
        StringLength(20, ErrorMessage = "حداقل طول {0} باید {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 4),
        Remote(nameof(UserManagementController.ValidateUserName), "UserManagement", AdditionalFields = nameof(UserId),
            HttpMethod = "Post", ErrorMessage = "{0} وارد شده تکراری است"),
        Display(Name = "نام کاربری"), RegularExpression("^[0-9a-zA-Z_]*$", ErrorMessage = "لطفا تنها از حروف انگلیسی استفاده نمائید")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور"),
        StringLength(15, ErrorMessage = "حداقل طول {0} باید {2} و حداکثر {1} کاراکتر باشد", MinimumLength = 6),
        DataType(DataType.Password)]
        //[Remote("ValidatePassword", "Register",
        //    AdditionalFields = nameof(UserName), HttpMethod = "POST")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} کاربر را انتخاب نمایید"),
        Display(Name = "سمت مربوطه")]
        public short OrganizationChartId { get; set; }
        public SelectList OrganizationChartList { get; set; }

        [Display(Name = "تکرار رمز عبور"), DataType(DataType.Password),
        Compare("Password", ErrorMessage = "{0} یکسان نیست")]
        public string ConfirmPassword { get; set; }
        [HiddenInput]
        public string Picture { get; set; }

        [Display(Name = "تصویر"), ValidateFile(MaxSize = 1000, AllowExtensions = new[] { ".jpg", ".gif", ".png" })]
        public IFormFile PostedFile { get; set; }

        //[Display(Name = "امور شعب")]
        //public int? BranchHeadId { get; set; }

        [Display(Name = "امور شعب/شعبه")]
        public int? BranchId { get; set; }

        [Display(Name = "وضعیت قفل")]
        public bool? LockState { get; set; }

        [Display(Name = "تاریخ غیرفعالسازی")]
        public string DisabledDate { get; set; }

        [Display(Name = "نقش")]
        public string SelectedRole { get; set; }
        //public SelectList BranchHeadList { get; set; }

        public SelectList BranchList { get; set; }
        public SelectList RolesList { get; set; }
    }

    public class UserFormViewModelValidator : AbstractValidator<UserFormViewModel>
    {
        public UserFormViewModelValidator()
        {
            RuleFor(o => o)
                .Must(o => o.Password != null && o.ConfirmPassword != null )
                .When(o => o.UserId <= 0)
                .WithMessage("رمز عبور می بایست وارد شود.");
        }
    }
}

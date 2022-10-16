using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ApplicationCommon.WebToolkit.ValidationAttributes;
using DomainEntities.BranchAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Core.UserManagement.ViewModels
{
    public class UpdateProfileViewModel
    {
        [HiddenInput]
        public int UserId { set; get; }

        [Required(ErrorMessage = "{0} را وارد نمایید"),
        Display(Name = "نام"), MaxLength(150, ErrorMessage = "حداکثر {0} باید {1} کاراکتر باشد")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} را وارد نمایید"),
         Display(Name = "نام خانوادگی"), MaxLength(150, ErrorMessage = "حداکثر {0} باید {1} کاراکتر باشد")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "{0} را تعیین نمایید"), Display(Name = "جنسیت")]
        //public bool? Gender { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید"),
        Display(Name = "پست الکترونیکی"), EmailAddress(ErrorMessage = "{0} وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "شماره {0} وارد شده صحیح نمی باشد", MinimumLength = 10), Display(Name = "تلفن همراه"),
        Phone(ErrorMessage = "شماره {0} وارد شده صحیح نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام کاربری"), ReadOnly(true)]
        public string UserName { get; set; }

        [Display(Name = "شعبه")]
        public int? BranchId { get; set; }
        [Display(Name = "امور شعب")]
        public int? BranchHeadId { get; set; }
        public Branch Branch { get; set; }


        [HiddenInput]
        public string Picture { get; set; }

        [Display(Name = "تصویر"), ValidateFile(MaxSize = 1000, AllowExtensions = new[] { ".jpg", ".gif", ".png" })]
        public IFormFile PostedFile { get; set; }
        //[Display(Name = "چارت سازمانی")]
        //public int? OrganizationId { get; set; }
        //public SelectList OrganizationList { get; set; }
    }
}

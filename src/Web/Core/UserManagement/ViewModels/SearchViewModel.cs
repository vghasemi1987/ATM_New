using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.UserManagement.ViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "از تاریخ")]
        public string FromDate { get; set; }
        [Display(Name = "تا تاریخ")]
        public string ToDate { get; set; }
        [Display(Name = "امور شعب")]
        public int? BranchHeadId { get; set; }

        [Display(Name = "شعبه")]
        public int? BranchId { get; set; }

        public SelectList BranchHeadList { get; set; }

        public SelectList BranchList { get; set; }
        [Display(Name = "وضعیت قفل")]
        public bool? LockState { get; set; }
    }
}

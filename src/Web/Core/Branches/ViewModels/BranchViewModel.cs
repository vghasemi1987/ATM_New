using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Core.Branches.ViewModels
{
    public class BranchViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید")]
        [Display(Name = "عنوان")]   
        public string Title { get; set; }
        [HiddenInput]
        [Display(Name = "عنوان امور شعب")]
        public int? BranchHeadId { get; set; }

        //[Required(ErrorMessage = "{0} را وارد نمایید")]
        //[Display(Name = "عنوان دپارتمان بالادست")]
        //public string ParentDepartmentTitle { get; set; }
        public SelectList BranchHeadSelectList { get; set; }
        [Required(ErrorMessage = "{0} را وارد نمایید")]
        [Display(Name = "کد"), Remote(nameof(BranchesController.ValidateCode), "Branches", AdditionalFields = nameof(Id),
    HttpMethod = "Post", ErrorMessage = "{0} وارد شده تکراری است")]
        public int code { get; set; }
    }
}

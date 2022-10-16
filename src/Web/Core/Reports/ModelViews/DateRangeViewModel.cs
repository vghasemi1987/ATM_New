using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Reports.ModelViews
{
    public class DateRangeViewModel
    {
        [Display(Name = "از تاریخ",Prompt = "از تاریخ")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
        public string FromEntryDate { get; set; }
        [Display(Name = "تا تاریخ", Prompt = "تا تاریخ")]
        [RegularExpression(@"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "{0} وارد شده نامعتبر است.")]
        public string ToEntryDate { get; set; }
        public string Title { get; set; }
        //[Required(ErrorMessage = "{0} را انتخاب نمایید")]
        [Display(Name = "سیستم")]
        public int? BranchHeadId { get; set; }
        public SelectList BranchHeadSelectList { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Core.ReportBoxes.ModelViews
{
    public class FormViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "عنوان"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Title { get; set; }
        [Display(Name = "توضیحات"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Description { get; set; }
        [Display(Name = "آیکن"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Icon { get; set; }
        [Display(Name = "کلید"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Key { get; set; }
        //[Display(Name = "عنوان"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Value { get; set; }
        [Display(Name = "لینک"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public string Link { get; set; }
        [Display(Name = "دسترسی")]
        public List<string> AccessRight { get; set; }
        [Display(Name = "نوع نمایش"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public BoxStatus BoxStatus { get; set; }
        [Display(Name = "دستور Sql"), Required(ErrorMessage = "{0} را وارد نمایید")]
        public string SqlCommand { get; set; }
        public SelectList Roles { get; set; }
    }
    public enum BoxStatus
    {
        Info = 0,
        Danger = 1,
        Striped = 2,
        Success = 3,
        Warning = 4
    }
}

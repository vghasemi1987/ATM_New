using ApplicationCommon.WebToolkit.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.HamrahLoanHeaders.ViewModels
{
    public class HamrahLoanHeaderViewModel
    {
        [Required(ErrorMessage = "{0} را انتخاب نمایید"),
            Display(Name = "عنوان")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} را انتخاب نمایید"),
            Display(Name = "فایل فزونی"),
            ValidateFile(MaxSize = 5000, AllowExtensions = new[] { ".txt" })]
        public IFormFile PostedFile { get; set; }

    }
}

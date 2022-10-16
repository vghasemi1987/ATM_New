using ApplicationCommon.WebToolkit.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Transactions.ViewModels
{
    public class UploadFileViewModel
    {
        [Required(ErrorMessage = "{0} را انتخاب نمایید"),
         Display(Name = "فایل فزونی"),
         ValidateFile(MaxSize = 5000, AllowExtensions = new[] { ".log", ".jrn", ".txt" })]
        public IFormFile PostedFile { get; set; }
    }
}

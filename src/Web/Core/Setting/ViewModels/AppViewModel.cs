using System.ComponentModel.DataAnnotations;
using ApplicationCommon.WebToolkit.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Web.Core.Setting.ViewModels
{
    public class AppViewModel
    {
        [Display(Name = "عنوان وب سایت")]
        public string WebSiteTitle { get; set; }
        [Display(Name = "فایل لوگو"), ValidateFile(MaxSize = 500, AllowExtensions = new[] { ".png" })]
        public IFormFile LogoFile { get; set; }
        [Display(Name = "نمادک"), ValidateFile(MaxSize = 500, AllowExtensions = new[] { ".ico" })]
        public IFormFile FaviconFile { get; set; }
    }
}
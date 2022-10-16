using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ApplicationCommon.WebToolkit.ValidationAttributes
{
    public sealed class ValidateFileAttribute : ValidationAttribute
    {
        public string[] AllowExtensions { get; set; }
        public int MaxSize { get; set; }

        public override bool IsValid(object value)
        {
            var maxContentLength = 1024 * MaxSize; //3 MB
            if (!(value is IFormFile file)) return true;
            if (AllowExtensions != null)
            {
                if (!AllowExtensions.Contains(System.IO.Path.GetExtension(file.FileName.ToLower())))
                {
                    ErrorMessage =
                        $"فایل ارسالی باید دارای یکی از این پسوندها باشد: {string.Join(", ", AllowExtensions)}";
                    return false;
                }
            }

            if (file.Length <= maxContentLength) return true;
            ErrorMessage = $"فایل انتخابی حداکثر باید: {maxContentLength / 1024} کیلوبایت باشد!";
            return false;
        }
    }
}
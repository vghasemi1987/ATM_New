using System.Text;
using ApplicationCommon;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.Extensions.TagHelpers
{
    [HtmlTargetElement("thumbnail")]
    public abstract class ThumbnailTagHelper : TagHelper
    {
        [HtmlAttributeName("for-item")]
        public ModelExpression FileName { get; set; }
        //[HtmlAttributeName("for-designation")]
        //public ModelExpression Designation { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ThumbnailImages";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            if (FileName.Model.ToString().IsImage())
                sb.AppendFormat("<img class='img-circle' width='40' height='40' src='/uploads/user-content/{0}' />", FileName.Model);
            else
                sb.AppendFormat("<img src='/images/file-types/{0}.png' />", System.IO.Path.GetExtension(FileName.Model.ToString()).Remove(0, 1));
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}

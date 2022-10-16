using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.Extensions.TagHelpers
{
    [HtmlTargetElement(Attributes = "policy-action, policy-controller")]
    public class PolicyTagHelper : TagHelper
    {
        private readonly ClaimsPrincipal _principal;

        public PolicyTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _principal = httpContextAccessor.HttpContext.User;
        }

        [HtmlAttributeName("policy-action")]
        public string PolicyAction { get; set; }

        [HtmlAttributeName("policy-controller")]
        public string PolicyControll { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var policy = $"{PolicyControll.Replace("Controller", string.Empty)}_{PolicyAction}";
            if (!_principal.HasClaim("Permission", policy))
                output.SuppressOutput();
            //if (!(await _authService.AuthorizeAsync(_principal, Policy)).Succeeded)
        }
    }
}
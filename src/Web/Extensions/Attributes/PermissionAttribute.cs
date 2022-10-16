using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Web.Extensions.Attributes
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            return HandleRequirementAsync(context, requirement);
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class PermissionAttribute : AuthorizeAttribute
    {
        //public string Id { get; }

        public PermissionAttribute(/*string name*/) : base("Permission")
        {
            //Id = name;
        }
    }
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
    }
    public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var rd = _httpContextAccessor.HttpContext.Request.RouteValues;
            var permissionName = $"{rd["controller"].ToString()}_{rd["action"].ToString()}";

            if (context.User == null || !context.User.HasClaim("Permission", permissionName))
            {
                context.Fail();
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Extensions.Filters
{
    public class DynamicAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!IsProtectedAction(context))
                return Task.CompletedTask;

            if (!IsUserAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return Task.CompletedTask;
            }
            if (!((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttributes<DisplayNameAttribute>().Any())
                return Task.CompletedTask;

            var actionId = GetActionId(context);
            if (context.HttpContext.User.HasClaim("Permission", actionId))
            {
                return Task.CompletedTask;
            }
            context.Result = new ForbidResult();
            return Task.CompletedTask;
        }
        private static bool IsProtectedAction(FilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return false;

            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
            var actionMethodInfo = controllerActionDescriptor.MethodInfo;

            var authorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            authorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            return false;
        }
        private static bool IsUserAuthenticated(ActionContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }
        private static string GetActionId(ActionContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            var controller = controllerActionDescriptor.ControllerName;
            var action = controllerActionDescriptor.ActionName;

            return $"{controller}_{action}";
        }
    }
}
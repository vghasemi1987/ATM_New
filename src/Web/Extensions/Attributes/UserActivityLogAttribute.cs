using DomainContracts.ApplicationUserAggregate;
using DomainEntities.ApplicationUserAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel;
using System.Reflection;

namespace Web.Extensions.Attributes
{
    public class UserActivityLogAttribute : ActionFilterAttribute
    {
        private readonly IApplicationUserActivityService _applicationUserActivityService;

        public UserActivityLogAttribute(IApplicationUserActivityService applicationUserActivityService)
        {
            _applicationUserActivityService = applicationUserActivityService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (!(context.Controller is Controller controller)) return;

            var actionName = string.Empty;
            var controllerName = string.Empty;
            var activityTitle = string.Empty;
            var userId = 0;
            if (context.HttpContext.User.Identity.IsAuthenticated)
                userId = context.HttpContext.User.GetUserId();
            else if (controller.ViewData["UserId"] != null)
                userId = int.Parse(controller.ViewData["UserId"].ToString());

            if (userId == 0)
                return;
            
            if (context?.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                actionName = descriptor.ActionName;
                controllerName = descriptor.ControllerName;
                activityTitle = $"{descriptor.MethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName} _" +
                    $"{descriptor.ControllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName}";
            }

            _applicationUserActivityService.Save(new ApplicationUserActivity
            {
                ActionName = actionName,
                ControllerName = controllerName,
                ActivityTitle = activityTitle,
                UserId = userId
            });
        }
    }
}

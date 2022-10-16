using ApplicationCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Web.Extensions.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var validationErrors = string.Join("<br />", context.ModelState
                .Keys
                .SelectMany(k => context.ModelState[k].Errors)
                .Select(e => e.ErrorMessage));

            var json = new JsonErrorResponse
            {
                Message = Message.Show(validationErrors, MessageType.Warning)
            };

            context.Result = new BadRequestObjectResult(json);
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions.Filters.ErrorHandling
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiError apiError;
            if (context.Exception is ApiException ex)
            {
                // handle explicit 'known' API errors
                context.Exception = null;
                apiError = new ApiError(ex.Message) {errors = ex.Errors};
                context.HttpContext.Response.StatusCode = ex.StatusCode;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;
            }
            else if (context.HttpContext.Response.StatusCode == 403)
            {
                apiError = new ApiError("شما مجوز دسترسی به این بخش را ندارید!");
                context.HttpContext.Response.StatusCode = 403;
            }
            else if (context.Exception is DbUpdateException &&
                context.Exception.InnerException.Message.Contains("DELETE statement conflicted"))
            {
                apiError = new ApiError("رکورد انتخابی دارای اطلاعات وابسته است!");
                context.HttpContext.Response.StatusCode = 500;
            }
            else
            {
                var msg = context.Exception.GetBaseException().Message;
                var stack = context.Exception.StackTrace;

                apiError = new ApiError(msg) {Detail = stack};

                context.HttpContext.Response.StatusCode = 500;
            }
            // always return a JSON result
            context.Result = new JsonResult(apiError);
            base.OnException(context);
        }
    }
}
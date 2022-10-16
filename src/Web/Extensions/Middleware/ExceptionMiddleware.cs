using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Extensions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string result;

            if (exception is UnauthorizedAccessException)
            {
                result = JsonSerializer.Serialize(new ErrorDetails(
                    context.Response.StatusCode,
                    "دسترسی غیر مجاز!",
                    exception.StackTrace
                ));
            }
            else if (context.Response.StatusCode == 403)
            {
                result = JsonSerializer.Serialize(new ErrorDetails(
                    context.Response.StatusCode,
                    "شما مجوز دسترسی به این بخش را ندارید!",
                    exception.StackTrace
                ));
            }
            else if (exception is DbUpdateException && exception.InnerException.Message.Contains("DELETE statement conflicted"))
            {
                result = JsonSerializer.Serialize(new ErrorDetails(
                    context.Response.StatusCode,
                    "رکورد انتخابی دارای اطلاعات وابسته است!",
                    exception.StackTrace
                ));
            }
            else
            {
                result = JsonSerializer.Serialize(new ErrorDetails(
                    context.Response.StatusCode,
                    exception.GetBaseException().Message,
                    exception.StackTrace
                ));
            }
            return context.Response.WriteAsync(result);
        }
    }
}

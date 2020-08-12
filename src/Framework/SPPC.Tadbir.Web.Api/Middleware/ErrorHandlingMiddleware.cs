using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.ExceptionHandling;

namespace SPPC.Tadbir.Web.Api.Middleware
{
    /// <summary>
    /// امکان مدیریت استثنائات ایجاد شده در سرویس را به صورت مرکزی پیاده سازی می کند
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="next">درخواست جاری (بعدی) که باید توسط سرویس پردازش شود</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// درخواست وب جاری را با استفاده از اطلاعات محیطی وب اجرا می کند و روی خطای احتمالی اقدام می کند
        /// </summary>
        /// <param name="context">اطلاعات محیطی وب</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = JsonHelper.From(ErrorFromException(exception), false, GetIgnoredPropertyNames());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)StatusCodeFromException();
            return context.Response.WriteAsync(result);
        }

        private static HttpStatusCode StatusCodeFromException()
        {
            var code = HttpStatusCode.InternalServerError;
            return code;
        }

        private static object ErrorFromException(Exception exception)
        {
            Verify.ArgumentNotNull(exception, "exception");
            return ServiceExceptionFactory.FromException(exception);
        }

        private static string[] GetIgnoredPropertyNames()
        {
            return new string[]
                {
                    "TypeMap", "TargetSite", "ChangeTracker", "ModelBuilder"
                };
        }

        private readonly RequestDelegate _next;
    }
}

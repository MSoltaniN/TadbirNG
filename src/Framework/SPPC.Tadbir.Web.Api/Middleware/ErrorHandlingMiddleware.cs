using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.ExceptionHandling;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Core;

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
        /// <param name="localizer">امکان ترجمه متن های چندزبانه را از روی کلید متنی فراهم می کند</param>
        public ErrorHandlingMiddleware(RequestDelegate next, IStringLocalizer<AppStrings> localizer)
        {
            _next = next;
            _localizer = localizer;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            TryLogException(context, exception);
            string message = _localizer[AppStrings.ErrorOccured];
            var error = new ErrorViewModel(message, ErrorType.RuntimeException);
            var result = JsonHelper.From(error, false);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }

        private void TryLogException(HttpContext context, Exception exception)
        {
            // TODO: Log detail exception and context information to SystemError table in system database...
        }

        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<AppStrings> _localizer;
    }
}

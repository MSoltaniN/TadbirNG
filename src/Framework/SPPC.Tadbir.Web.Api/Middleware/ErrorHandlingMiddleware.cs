using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ExceptionHandling;
using SPPC.Tadbir.Persistence.Query;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
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
        /// <param name="dbConsole">امکان ارسال دستورات مستقیم به دیتابیس را فراهم می کند</param>
        /// <param name="token">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public ErrorHandlingMiddleware(RequestDelegate next, IStringLocalizer<AppStrings> localizer,
            ISqlConsole dbConsole, ITokenService token)
        {
            _next = next;
            _localizer = localizer;
            _dbConsole = dbConsole;
            _token = token;
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

        private static string FromNullableId(int? id)
        {
            return id.HasValue ? id.ToString() : "NULL";
        }

        private ISecurityContext GetSecurityContext(HttpContext httpContext)
        {
            var context = httpContext.Request.Headers[AppConstants.ContextHeaderName];
            if (String.IsNullOrEmpty(context))
            {
                return null;
            }

            return _token.GetSecurityContext(context);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            TryLogException(context, exception);
            string message = _localizer[AppStrings.ErrorOccured];
            var error = new ErrorViewModel(message, ErrorType.RuntimeException);
            var result = JsonHelper.From(error, false);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(result);
        }

        private void TryLogException(HttpContext context, Exception exception)
        {
            var serviceException = ServiceExceptionFactory.FromException(exception);
            int? companyId = null;
            int? fpId = null;
            int? branchId = null;
            var securityContext = GetSecurityContext(context);
            if (securityContext != null)
            {
                companyId = securityContext.User.CompanyId;
                fpId = securityContext.User.FiscalPeriodId;
                branchId = securityContext.User.BranchId;
            }

            var enCulture = new CultureInfo("en");
            var systemError = new SystemErrorViewModel()
            {
                Code = (int)serviceException.ErrorDetail.ErrorCode,
                FaultingMethod = serviceException.ErrorDetail.FaultingMethod,
                FaultType = serviceException.ErrorDetail.FaultType,
                Message = exception.Message.Replace("'", "\""),
                TimestampUtc = DateTime.UtcNow.ToString(AppConstants.TimestampFormat, enCulture),
                StackTrace = serviceException.ErrorDetail.StackTrace
            };

            try
            {
                var query = String.Format(ErrorQuery.Insert, FromNullableId(companyId),
                    FromNullableId(fpId), FromNullableId(branchId), systemError.TimestampUtc,
                    systemError.Code, systemError.Message, systemError.FaultingMethod,
                    systemError.FaultType, systemError.StackTrace);
                _dbConsole.ExecuteNonQuery(query);
            }
            catch
            {
                Debug.WriteLine("WARNING: Could not log system error to database.");
            }
        }

        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<AppStrings> _localizer;
        private readonly ISqlConsole _dbConsole;
        private readonly ITokenService _token;
    }
}

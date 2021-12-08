using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ExceptionHandling;
using SPPC.Tadbir.Persistence.Query;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
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
            ISqlConsole dbConsole, ITokenManager token)
        {
            _next = next;
            _localizer = localizer;
            _dbConsole = dbConsole;
            _tokenManager = token;
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

            return _tokenManager.GetSecurityContext(context);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is SecurityTokenExpiredException _)
            {
                await HandleExceptionAsync(context, AppStrings.SessionIsExpired,
                    ErrorType.ExpiredSession, HttpStatusCode.Unauthorized);
            }
            else
            {
                TryLogException(context, exception);
                await HandleExceptionAsync(context, AppStrings.ErrorOccured,
                    ErrorType.RuntimeException, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string messageKey,
            ErrorType errorType, HttpStatusCode statusCode)
        {
            string message = _localizer[messageKey];
            var error = new ErrorViewModel(message, errorType);
            var result = JsonHelper.From(error, false);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }

        private void TryLogException(HttpContext context, Exception exception)
        {
            var serviceException = ServiceExceptionFactory.FromException(exception);
            int? companyId = null;
            int? fpId = null;
            int? branchId = null;
            try
            {
                var securityContext = GetSecurityContext(context);
                if (securityContext != null)
                {
                    companyId = securityContext.User.CompanyId;
                    fpId = securityContext.User.FiscalPeriodId;
                    branchId = securityContext.User.BranchId;
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("WARNING: Security token is expired. Environment info is not available.");
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
        private readonly ITokenManager _tokenManager;
    }
}

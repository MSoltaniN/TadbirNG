using System;
using Microsoft.AspNetCore.Http;
using SPPC.Framework.Common;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Web.Api.Extensions
{
    /// <summary>
    /// امکانات کمکی مورد نیاز برنامه را برای درخواست های وب پیاده سازی می کند
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// اطلاعات امنیتی جاری را - در صورت وجود - از درخواست وب فعال به دست آورده و برمی گرداند
        /// </summary>
        /// <param name="request">آبجکت درخواست وب</param>
        /// <returns>اطلاعات امنیتی جاری به دست آمده از درخواست
        /// یا - در صورت تنظیم نشدن این اطلاعات - رفرنس بدون مقدار</returns>
        public static SecurityContext CurrentSecurityContext(this HttpRequest request)
        {
            Verify.ArgumentNotNull(request, "request");
            var context = request.Headers[AppConstants.ContextHeaderName];
            if (String.IsNullOrEmpty(context))
            {
                return null;
            }

            var tokenService = new JwtTokenService();
            return tokenService.GetSecurityContext(context) as SecurityContext;
        }
    }
}

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Web.Api.Filters
{
    /// <summary>
    /// امکان رمزگشایی رشته اتصال دیتابیسی را برای استفاده داخلی برنامه پیاده سازی می کند
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DecryptConnectionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public DecryptConnectionAttribute()
        {
            _crypto = new CryptoService();
            _contextEncoder = new Base64Encoder<SecurityContext>();
        }

        /// <summary>
        /// امکان فیلتر درخواست را پیش از اجرای متد کنترلر فراهم می کند
        /// </summary>
        /// <param name="actionContext">اطلاعات جاری مورد نیاز برای فیلتر درخواست</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            string authTicket;
            var headers = actionContext.HttpContext.Request.Headers;
            if (headers[AppConstants.ContextHeaderName] != StringValues.Empty)
            {
                authTicket = headers[AppConstants.ContextHeaderName].First();
                var userContext = _contextEncoder.Decode(authTicket);
                if (!String.IsNullOrEmpty(userContext.User.Connection))
                {
                    userContext.User.Connection = _crypto.Decrypt(userContext.User.Connection);
                    headers.Remove(AppConstants.ContextHeaderName);
                    headers[AppConstants.ContextHeaderName] = _contextEncoder.Encode(userContext);
                }
            }

            base.OnActionExecuting(actionContext);
        }

        private readonly ICryptoService _crypto;
        private readonly ITextEncoder<SecurityContext> _contextEncoder;
    }
}

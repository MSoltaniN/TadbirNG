using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Web.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DecryptConnectionAttribute : ActionFilterAttribute
    {
        public DecryptConnectionAttribute()
        {
            _crypto = new CryptoService();
            _contextEncoder = new Base64Encoder<SecurityContext>();
        }

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

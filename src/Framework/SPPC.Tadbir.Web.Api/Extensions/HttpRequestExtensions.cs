using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Web.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static SecurityContext CurrentSecurityContext(this HttpRequest request)
        {
            Verify.ArgumentNotNull(request, "request");
            var context = request.Headers[AppConstants.ContextHeaderName];
            if (String.IsNullOrEmpty(context))
            {
                return null;
            }

            var json = Encoding.UTF8.GetString(Transform.FromBase64String(context));
            return JsonHelper.To<SecurityContext>(json);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.AppStart;
using BabakSoft.Platform.Common;

namespace SPPC.Tadbir.Web.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthorizeRequestAttribute : ActionFilterAttribute
    {
        public AuthorizeRequestAttribute(string entity, int permission)
        {
            Verify.ArgumentNotNullOrWhitespace(entity, "entity");
            _requiredPermissions = new PermissionBriefViewModel[]
            {
                new PermissionBriefViewModel(entity, permission)
            };

            _contextDecoder = UnityConfig.GetConfiguredContainer()
                .Resolve<ITextEncoder<SecurityContext>>();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Verify.ArgumentNotNull(actionContext, "actionContext");

            string authTicket = null;
            if (!IsValidRequest(actionContext, out authTicket))
            {
                // If custom authorization ticket header is not found in request, return Bad Request (400) response...
                string reason = string.Format(
                    "Authorization ticket header '{0}' could not be found.", Values.Constants.ContextHeaderName);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = reason };
            }
            else if (!IsAuthorized(authTicket))
            {
                // If caller is not authorized, return Unauthorized (401) response...
                string reason = "Caller is not authorized to perform current operation.";
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = reason };
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }

        private static bool IsValidRequest(HttpActionContext actionContext, out string authTicket)
        {
            authTicket = null;
            IEnumerable<string> values = null;
            if (actionContext.Request.Headers.TryGetValues(Values.Constants.ContextHeaderName, out values))
            {
                authTicket = values.First();
            }

            return !String.IsNullOrEmpty(authTicket);
        }

        private bool IsAuthorized(string authTicket)
        {
            var securityContext = _contextDecoder.Decode(authTicket);
            return (securityContext.User.Id == Values.Constants.AdminUserId
                || securityContext.HasPermissions(_requiredPermissions));
        }

        private readonly PermissionBriefViewModel[] _requiredPermissions;
        private readonly ITextEncoder<SecurityContext> _contextDecoder;
    }
}

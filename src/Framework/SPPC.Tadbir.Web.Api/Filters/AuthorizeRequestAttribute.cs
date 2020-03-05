using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SPPC.Framework.Common;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Web.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthorizeRequestAttribute : ActionFilterAttribute
    {
        public AuthorizeRequestAttribute()
        {
            _contextDecoder = new Base64Encoder<SecurityContext>();
        }

        public AuthorizeRequestAttribute(string entity, int permission)
        {
            Verify.ArgumentNotNullOrWhitespace(entity, "entity");
            _contextDecoder = new Base64Encoder<SecurityContext>();
            _requiredPermissions = new PermissionBriefViewModel[]
            {
                new PermissionBriefViewModel(entity, permission)
            };
        }

        public string Entity
        {
            get { return _requiredPermissions?[0].EntityName; }
        }

        public int Permission
        {
            get { return (_requiredPermissions != null) ? _requiredPermissions[0].Flags : 0; }
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Verify.ArgumentNotNull(actionContext, "actionContext");

            string authTicket = null;
            if (!IsValidRequest(actionContext, out authTicket))
            {
                // If custom authorization ticket header is not found in request, return Bad Request (400) response...
                string reason = string.Format(
                    "Authorization ticket header '{0}' could not be found.", AppConstants.ContextHeaderName);
                actionContext.Result = new BadRequestObjectResult(reason);
            }
            else if (!IsAuthorized(authTicket))
            {
                // If caller is not authorized, return Unauthorized (401) response...
                actionContext.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }

        private static bool IsValidRequest(ActionExecutingContext actionContext, out string authTicket)
        {
            authTicket = null;
            var headers = actionContext.HttpContext.Request.Headers;
            if (headers[AppConstants.ContextHeaderName] != StringValues.Empty)
            {
                authTicket = headers[AppConstants.ContextHeaderName].First();
            }

            return !String.IsNullOrEmpty(authTicket);
        }

        private bool IsAuthorized(string authTicket)
        {
            var securityContext = _contextDecoder.Decode(authTicket);
            bool isAuthorized = securityContext.IsInRole(AppConstants.AdminRoleId);
            if (_requiredPermissions != null)
            {
                isAuthorized = isAuthorized ||
                    securityContext.HasPermissions(_requiredPermissions);
            }

            return isAuthorized;
        }

        private readonly PermissionBriefViewModel[] _requiredPermissions;
        private readonly ITextEncoder<SecurityContext> _contextDecoder;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;
using SPPC.Framework.Common;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;
//using SPPC.Tadbir.Web.Api.AppStart;
//using Unity;

namespace SPPC.Tadbir.Web.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthorizeRequestAttribute : ActionFilterAttribute
    {
        public AuthorizeRequestAttribute(string entity, int permission)
        {
            Verify.ArgumentNotNullOrWhitespace(entity, "entity");
            _contextDecoder = new Base64Encoder<SecurityContext>();
            _requiredPermissions = new PermissionBriefViewModel[]
            {
                new PermissionBriefViewModel(entity, permission)
            };
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Verify.ArgumentNotNull(actionContext, "actionContext");

            string authTicket = null;
            if (!IsValidRequest(actionContext, out authTicket))
            {
                // If custom authorization ticket header is not found in request, return Bad Request (400) response...
                string reason = string.Format(
                    "Authorization ticket header '{0}' could not be found.", Values.Constants.ContextHeaderName);
                actionContext.Result = new BadRequestObjectResult(reason);
            }
            else if (!IsAuthorized(authTicket))
            {
                // If caller is not authorized, return Unauthorized (401) response...
                string reason = "Caller is not authorized to perform current operation.";
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
            if (headers[Values.Constants.ContextHeaderName] != StringValues.Empty)
            {
                authTicket = headers[Values.Constants.ContextHeaderName].First();
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

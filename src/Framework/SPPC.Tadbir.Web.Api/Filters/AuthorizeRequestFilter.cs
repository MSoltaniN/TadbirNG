using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Licensing.Local.Persistence;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Web.Api.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class AuthorizeRequestFilter : IActionFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        public AuthorizeRequestFilter(ITokenService token)
        {
            _licenseUtility = LicenseUtility.CreateDefault();
            _tokenService = token;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// امکان فیلتر درخواست را پیش از اجرای متد کنترلر فراهم می کند
        /// </summary>
        /// <param name="actionContext">اطلاعات جاری مورد نیاز برای فیلتر درخواست</param>
        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Verify.ArgumentNotNull(actionContext, nameof(actionContext));
            _requiredPermissions = actionContext.ActionDescriptor.FilterDescriptors
                .Where(desc => desc.Filter.GetType() == typeof(AuthorizeRequestAttribute))
                .Select(desc => desc.Filter as AuthorizeRequestAttribute)
                .Select(att => new PermissionBriefViewModel(att.Entity, att.Permission))
                .ToArray();

            if (_requiredPermissions.Count() == 0)
            {
                return;
            }

            ////if (!CheckLicense(actionContext))
            ////{
            ////    string reason = "Access denied because license is missing, invalid or tampered.";
            ////    actionContext.Result = new BadRequestObjectResult(reason);
            ////}

            if (!IsValidRequest(actionContext, out string authTicket))
            {
                // If custom authorization ticket header is not found in request, return Bad Request (400) response...
                string reason = String.Format(
                    "Authorization ticket header '{0}' could not be found.", AppConstants.ContextHeaderName);
                actionContext.Result = new BadRequestObjectResult(reason);
            }
            else if (!_tokenService.Validate(authTicket))
            {
                // If ticket is invalid or expired, return Unauthorized (401) response...
                actionContext.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            else if (!IsAuthorized(authTicket, actionContext))
            {
                // If caller is not authorized, return Unauthorized (401) response...
                actionContext.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
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

        private bool IsAuthorized(string authTicket, ActionExecutingContext actionContext)
        {
            var securityContext = _tokenService.GetSecurityContext(authTicket);
            bool isAuthorized = securityContext.IsInRole(AppConstants.AdminRoleId);
            if (_requiredPermissions != null)
            {
                isAuthorized = isAuthorized ||
                    securityContext.HasPermissions(_requiredPermissions);

                bool hasView = _requiredPermissions
                    .Any(perm => (perm.Flags & (int)ViewPermissions.View) == (int)ViewPermissions.View);
                if (isAuthorized && hasView)
                {
                    var gridOptions = GetGridOptions(actionContext);
                    isAuthorized = isAuthorized && CheckViewPermissions(securityContext, gridOptions);
                }
            }

            return isAuthorized;
        }

        private bool CheckLicense(ActionExecutingContext actionContext)
        {
            string signature;
            bool validated = false;
            var headers = actionContext.HttpContext.Request.Headers;
            if (headers[AppConstants.LicenseHeaderName] != StringValues.Empty)
            {
                signature = headers[AppConstants.LicenseHeaderName].First();
                string license = File.ReadAllText(_licensePath, Encoding.UTF8);
                _licenseUtility.LicensePath = Path.Combine(_serverRoot, Constants.LicenseFile);
                validated = _licenseUtility.ValidateSignature(license, signature);
            }

            return validated;
        }

        private bool CheckViewPermissions(ISecurityContext securityContext, GridOptions gridOptions)
        {
            bool isAuthorized = true;
            var viewPermissions = _requiredPermissions
                .Where(perm => (perm.Flags & (int)ViewPermissions.View) == (int)ViewPermissions.View)
                .ToArray();
            switch (gridOptions.Operation)
            {
                case (int)OperationId.Filter:
                    Array.ForEach(viewPermissions, perm => perm.Flags |= (int)ViewPermissions.Filter);
                    isAuthorized = securityContext.HasPermissions(viewPermissions);
                    break;
                case (int)OperationId.Print:
                    Array.ForEach(viewPermissions, perm => perm.Flags |= (int)ViewPermissions.Print);
                    isAuthorized = securityContext.HasPermissions(viewPermissions);
                    break;
                case (int)OperationId.Export:
                    Array.ForEach(viewPermissions, perm => perm.Flags |= (int)ViewPermissions.Export);
                    isAuthorized = securityContext.HasPermissions(viewPermissions);
                    break;
                case (int)OperationId.View:
                default:
                    break;
            }

            return isAuthorized;
        }

        private GridOptions GetGridOptions(ActionExecutingContext actionContext)
        {
            var request = actionContext.HttpContext.Request;
            var options = request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return new GridOptions();
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return JsonHelper.To<GridOptions>(json);
        }

        private readonly ILicenseUtility _licenseUtility;
        private readonly ITokenService _tokenService;
        private readonly string _licensePath = @"wwwroot\static\license";
        private readonly string _serverRoot = @"..\SPPC.Licensing.Local.Web\wwwroot";
        private PermissionBriefViewModel[] _requiredPermissions;
    }
}

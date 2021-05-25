using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SPPC.Framework.Common;
using SPPC.Licensing.Local.Persistence;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Web.Api.Filters
{
    /// <summary>
    /// امکانات احراز هویت و مجوزدهی امنیتی را با استفاده از
    /// مکانیزم فیلترهای درخواست پیاده سازی می کند
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class AuthorizeRequestAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AuthorizeRequestAttribute()
        {
            _utility = LicenseUtility.CreateDefault();
            _tokenService = new JwtTokenService();
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس با توجه به موجودیت محافظت شده و دسترسی مورد نیاز می سازد
        /// </summary>
        /// <param name="entity">موجودیت محافظت شده ای که مجوزدهی امنیتی باید برای آن انجام شود</param>
        /// <param name="permission">دسترسی امنیتی مورد نیاز برای ادامه عملیات</param>
        public AuthorizeRequestAttribute(string entity, int permission)
            : this()
        {
            Verify.ArgumentNotNullOrWhitespace(entity, "entity");
            _requiredPermissions = new PermissionBriefViewModel[]
            {
                new PermissionBriefViewModel(entity, permission)
            };
        }

        /// <summary>
        /// موجودیت محافظت شده ای که مجوزدهی امنیتی باید برای آن انجام شود
        /// </summary>
        public string Entity
        {
            get { return _requiredPermissions?[0].EntityName; }
        }

        /// <summary>
        /// دسترسی امنیتی مورد نیاز برای ادامه عملیات
        /// </summary>
        public int Permission
        {
            get { return (_requiredPermissions != null) ? _requiredPermissions[0].Flags : 0; }
        }

        /// <summary>
        /// امکان فیلتر درخواست را پیش از اجرای متد کنترلر فراهم می کند
        /// </summary>
        /// <param name="actionContext">اطلاعات جاری مورد نیاز برای فیلتر درخواست</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Verify.ArgumentNotNull(actionContext, "actionContext");
            ////if (!CheckLicense(actionContext))
            ////{
            ////    string reason = "Access denied because license is missing, invalid or tampered.";
            ////    actionContext.Result = new BadRequestObjectResult(reason);
            ////}

            string authTicket = null;
            if (!IsValidRequest(actionContext, out authTicket))
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
            var securityContext = _tokenService.GetSecurityContext(authTicket);
            ////var securityContext = _contextDecoder.Decode(authTicket);
            bool isAuthorized = securityContext.IsInRole(AppConstants.AdminRoleId);
            if (_requiredPermissions != null)
            {
                isAuthorized = isAuthorized ||
                    securityContext.HasPermissions(_requiredPermissions);
            }

            return isAuthorized;
        }

        private bool CheckLicense(ActionExecutingContext actionContext)
        {
            string signature = null;
            bool validated = false;
            var headers = actionContext.HttpContext.Request.Headers;
            if (headers[AppConstants.LicenseHeaderName] != StringValues.Empty)
            {
                signature = headers[AppConstants.LicenseHeaderName].First();
                string license = File.ReadAllText(_licensePath, Encoding.UTF8);
                _utility.LicensePath = Path.Combine(_serverRoot, Constants.LicenseFile);
                validated = _utility.ValidateSignature(license, signature);
            }

            return validated;
        }

        private readonly PermissionBriefViewModel[] _requiredPermissions;
        private readonly ILicenseUtility _utility;
        private readonly ITokenService _tokenService;
        private readonly string _licensePath = @"wwwroot\static\license";
        private readonly string _serverRoot = @"..\SPPC.Licensing.Local.Web\wwwroot";
    }
}

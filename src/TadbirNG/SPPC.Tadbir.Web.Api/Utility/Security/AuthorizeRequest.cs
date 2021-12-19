using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Framework.Presentation;
using SPPC.Framework.Service;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// عملیات مورد نیاز برای احراز هویت، مجوزدهی و کنترل لایسنس را پیاده سازی می کند
    /// </summary>
    public class AuthorizeRequest : IAuthorizeRequest
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="tokenManager"></param>
        /// <param name="apiClient">امکان تماس با یک سرویس وب را فراهم می کند</param>
        /// <param name="pathProvider">مسیرهای فایل های کاربردی مورد نیاز سرویس وب را فراهم می کند</param>
        public AuthorizeRequest(ITokenManager tokenManager, IApiClient apiClient,
            IApiPathProvider pathProvider)
        {
            _tokenManager = tokenManager;
            _apiClient = apiClient;
            _pathProvider = pathProvider;
        }

        /// <summary>
        /// مجوزهای امنیتی مورد نیاز برای عملیات کنترل دسترسی را تنظیم می کند
        /// </summary>
        /// <param name="permissions">مجوزهای امنیتی مورد نیاز</param>
        public void SetRequiredPermissions(IEnumerable<PermissionBriefViewModel> permissions)
        {
            Verify.ArgumentNotNull(permissions, nameof(permissions));
            _requiredPermissions = permissions;
        }

        /// <summary>
        /// با توجه به محتوای درخواست داده شده، کنترل دسترسی امنیتی را انجام داده و نتیجه را برمی گرداند
        /// </summary>
        /// <param name="httpRequest">درخواست وب جاری</param>
        /// <returns>نتیجه عملیات پس از کنترل لایسنس، احراز هویت و کنترل دسترسی امنیتی</returns>
        public IActionResult GetAuthorizationResult(HttpRequest httpRequest)
        {
            IActionResult result;
            if (!CheckLicense(httpRequest))
            {
                string reason = "Access denied because license is missing, invalid or tampered.";
                result = new BadRequestObjectResult(reason);
            }
            else if (!IsValidRequest(httpRequest, out string authTicket))
            {
                // If custom authorization ticket header is not found in request, return Bad Request (400) response...
                string reason = String.Format(
                    "Authorization ticket header '{0}' could not be found.", AppConstants.ContextHeaderName);
                result = new BadRequestObjectResult(reason);
            }
            else if (!_tokenManager.Validate(authTicket))
            {
                // If ticket is invalid or expired, return Unauthorized (401) response...
                result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            else if (!IsAuthorized(httpRequest, authTicket))
            {
                // If caller is not authorized, return Unauthorized (401) response...
                result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            else
            {
                result = null;
            }

            return result;
        }

        private static bool IsValidRequest(HttpRequest httpRequest, out string authTicket)
        {
            authTicket = null;
            var headers = httpRequest.Headers;
            if (!String.IsNullOrWhiteSpace(headers[AppConstants.ContextHeaderName]))
            {
                authTicket = headers[AppConstants.ContextHeaderName].First();
            }

            return !String.IsNullOrEmpty(authTicket);
        }

        private static GridOptions GetGridOptions(HttpRequest httpRequest)
        {
            var options = httpRequest.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return new GridOptions();
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return JsonHelper.To<GridOptions>(json);
        }

        private bool IsAuthorized(HttpRequest httpRequest, string authTicket)
        {
            var securityContext = _tokenManager.GetSecurityContext(authTicket);
            bool isAuthorized = securityContext.IsInRole(AppConstants.AdminRoleId);
            if (!isAuthorized && _requiredPermissions != null)
            {
                isAuthorized = isAuthorized ||
                    securityContext.HasPermissions(_requiredPermissions.ToArray());

                bool hasView = _requiredPermissions
                    .Any(perm => (perm.Flags & (int)ViewPermissions.View) == (int)ViewPermissions.View);
                if (isAuthorized && hasView)
                {
                    var gridOptions = GetGridOptions(httpRequest);
                    isAuthorized = isAuthorized && CheckViewPermissions(securityContext, gridOptions);
                }
            }

            return isAuthorized;
        }

        private bool CheckLicense(HttpRequest httpRequest)
        {
            string signature;
            bool validated = false;
            if (!String.IsNullOrEmpty(httpRequest.Headers[AppConstants.LicenseHeaderName]))
            {
                signature = httpRequest.Headers[AppConstants.LicenseHeaderName];
                string license = File.ReadAllText(_pathProvider.License, Encoding.UTF8);
                _apiClient.AddHeader(AppConstants.LicenseHeaderName, signature);
                validated = _apiClient.Update<string, bool>(license, LicenseApi.ValidateLicense);
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

        private readonly ITokenManager _tokenManager;
        private readonly IApiClient _apiClient;
        private readonly IApiPathProvider _pathProvider;
        private IEnumerable<PermissionBriefViewModel> _requiredPermissions;
    }
}

using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SPPC.Framework.Common;
using SPPC.Tadbir.Security;
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
        /// <param name="authorize"></param>
        public AuthorizeRequestFilter(IAuthorizeRequest authorize)
        {
            _authorize = authorize;
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
            var requiredPermissions = actionContext.ActionDescriptor.FilterDescriptors
                .Where(desc => desc.Filter.GetType() == typeof(AuthorizeRequestAttribute))
                .Select(desc => desc.Filter as AuthorizeRequestAttribute)
                .Select(att => new PermissionBriefViewModel(att.Entity, att.Permission))
                .ToArray();

            if (requiredPermissions.Count() == 0)
            {
                return;
            }

            _authorize.SetRequiredPermissions(requiredPermissions);
            var result = _authorize.GetAuthorizationResult(actionContext.HttpContext.Request);
            if (result != null)
            {
                actionContext.Result = result;
            }
        }

        private readonly IAuthorizeRequest _authorize;
    }
}

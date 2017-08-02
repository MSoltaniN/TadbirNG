using System;
using System.Web.Mvc;
using BabakSoft.Platform.Common;
using Microsoft.Practices.Unity;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.AppStart;

namespace SPPC.Tadbir.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AppAuthorizeAttribute : ActionFilterAttribute
    {
        public AppAuthorizeAttribute(string entity, int permission)
        {
            Verify.ArgumentNotNullOrWhitespace(entity, "entity");
            _requiredPermissions = new PermissionBriefViewModel[]
            {
                new PermissionBriefViewModel(entity, permission)
            };
        }

        public AppAuthorizeAttribute(string entity1, int permission1, string entity2, int permission2)
        {
            Verify.ArgumentNotNullOrWhitespace(entity1, "entity1");
            Verify.ArgumentNotNullOrWhitespace(entity2, "entity2");
            _requiredPermissions = new PermissionBriefViewModel[]
            {
                new PermissionBriefViewModel(entity1, permission1),
                new PermissionBriefViewModel(entity2, permission2)
            };
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Verify.ArgumentNotNull(filterContext, "filterContext");
            var contextManager = UnityConfig.GetConfiguredContainer().Resolve<ISecurityContextManager>();
            var currentContext = contextManager.CurrentContext;
            if (currentContext != null)
            {
                if (currentContext.HasPermissions(_requiredPermissions))
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/error/denied");
                }
            }
            else
            {
                var redirUrl = String.Format(
                    "~/account/login?ReturnUrl={0}", filterContext.HttpContext.Request.Url.PathAndQuery);
                filterContext.Result = new RedirectResult(redirUrl);
            }
        }

        private readonly PermissionBriefViewModel[] _requiredPermissions;
    }
}

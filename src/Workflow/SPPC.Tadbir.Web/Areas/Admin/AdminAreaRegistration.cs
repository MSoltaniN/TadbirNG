using System.Web.Mvc;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            Verify.ArgumentNotNull(context, "context");
            context.MapRoute(
                "Admin_main",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SPPC.Tadbir.Web.Areas.Admin.Controllers" });
        }
    }
}
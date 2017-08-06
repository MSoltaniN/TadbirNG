using System.Web.Mvc;
using BabakSoft.Platform.Common;

namespace SPPC.Tadbir.Web.Areas.Procurement
{
    public class ProcurementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Procurement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            Verify.ArgumentNotNull(context, "context");
            context.MapRoute(
                "Procurement_main",
                "procurement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SPPC.Tadbir.Web.Areas.Procurement.Controllers" });
        }
    }
}
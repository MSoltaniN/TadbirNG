using System.Web.Mvc;
using BabakSoft.Platform.Common;

namespace SPPC.Tadbir.Web.Areas.Inventory
{
    public class InventoryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Inventory";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            Verify.ArgumentNotNull(context, "context");
            context.MapRoute(
                "Inventory_main",
                "inventory/{controller}/{action}/{id}",
                new { action = "index", id = UrlParameter.Optional },
                new string[] { "SPPC.Tadbir.Web.Areas.Inventory.Controllers" });
        }
    }
}
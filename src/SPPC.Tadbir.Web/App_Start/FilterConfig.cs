using System.Web;
using System.Web.Mvc;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Web
{
    public sealed class FilterConfig
    {
        private FilterConfig()
        {
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Verify.ArgumentNotNull(filters, "filters");
            filters.Add(new HandleErrorAttribute());
        }
    }
}

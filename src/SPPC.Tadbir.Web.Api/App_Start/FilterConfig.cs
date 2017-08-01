using System.Web;
using System.Web.Mvc;
using BabakSoft.Platform.Common;

namespace SPPC.Tadbir.Web.Api
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

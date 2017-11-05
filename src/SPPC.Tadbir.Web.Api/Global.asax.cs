using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BabakSoft.Platform.Persistence;
using SPPC.Tadbir.Web.Api.AppStart;
using Unity;

namespace SPPC.Tadbir.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitNHibernate();
        }

        private static void InitNHibernate()
        {
            // Resolve and initialize NHibernate instances registered for both API service and WF...
            var nhibernate = UnityConfig.GetConfiguredContainer()
                .Resolve<IORMapper>();
            nhibernate.Initialize();
            nhibernate = UnityConfig.GetConfiguredContainer()
                .Resolve<IORMapper>("WF");
            nhibernate.Initialize();
        }
    }
}

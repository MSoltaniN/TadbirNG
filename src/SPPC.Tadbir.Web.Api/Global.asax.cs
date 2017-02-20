using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using SPPC.Tadbir.Web.Api.AppStart;
using SwForAll.Platform.Persistence;

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

        private void InitNHibernate()
        {
            var nhibernate = UnityConfig.GetConfiguredContainer()
                .Resolve<IORMapper>();
            nhibernate.Initialize();
        }
    }
}

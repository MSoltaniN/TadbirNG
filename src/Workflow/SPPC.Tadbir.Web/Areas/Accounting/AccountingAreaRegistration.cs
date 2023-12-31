﻿using System.Web.Mvc;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.Web.Areas.Accounting
{
    public class AccountingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Accounting";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            Verify.ArgumentNotNull(context, "context");
            context.MapRoute(
                "Accounting_main",
                "accounting/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SPPC.Tadbir.Web.Areas.Accounting.Controllers" });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPPC.Tadbir.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error");
        }

        // GET: Error/NotFound
        public ActionResult NotFound()
        {
            return View("NotFound");
        }
    }
}
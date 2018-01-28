using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPPC.Tadbir.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: error
        public ViewResult Index()
        {
            return View("Error");
        }

        // GET: error/notfound
        public ViewResult NotFound()
        {
            return View();
        }

        // GET: error/denied
        public ViewResult Denied()
        {
            return View();
        }
    }
}
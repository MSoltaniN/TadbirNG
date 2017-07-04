using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Web.Areas.Admin.Controllers
{
    public class SettingsController : Controller
    {
        public SettingsController(ISettingsService service)
        {
            _service = service;
        }

        // GET: admin/settings
        public ViewResult Workflows()
        {
            var settings = _service.GetWorkflowSettings();
            return View(settings);
        }

        // POST: admin/settings
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Workflows(WorkflowSettingsViewModel settings)
        {
            if (settings == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            _service.SaveWorkflowSettings(settings);
            return RedirectToAction("index", "home", new { area = String.Empty });
        }

        private ISettingsService _service;
    }
}
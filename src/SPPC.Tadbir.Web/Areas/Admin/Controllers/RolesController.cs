using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Web.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        public RolesController(ISecurityService service)
        {
            _service = service;
        }

        // GET: admin/roles
        public ViewResult Index(int? page = null)
        {
            var roles = _service.GetRoles();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(roles.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/roles/create
        public ViewResult Create()
        {
            var newRole = _service.GetNewRole();
            return View(newRole);
        }

        // POST: admin/roles/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleFullViewModel fullRole)
        {
            if (fullRole == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveRole(fullRole);
                return RedirectToAction("index");
            }

            return View(fullRole);
        }

        // GET: admin/roles/details/id
        public ActionResult Details(int id)
        {
            var role = _service.GetRole(id);
            if (role == null)
            {
                return RedirectToAction("notfound", "error", new { area = "admin" });
            }

            return View(role);
        }

        private ISecurityService _service;
    }
}
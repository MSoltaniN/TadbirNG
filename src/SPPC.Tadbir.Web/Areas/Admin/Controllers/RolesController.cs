using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;

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

        private ISecurityService _service;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public UsersController(ISecurityService service)
        {
            _service = service;
        }

        // GET: admin/users
        public ViewResult Index(int? page = null)
        {
            var accounts = _service.GetUsers();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        private ISecurityService _service;
    }
}
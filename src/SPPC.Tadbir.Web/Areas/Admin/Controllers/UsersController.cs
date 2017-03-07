using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

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

        // GET: admin/users/create
        public ViewResult Create()
        {
            var newUser = new UserViewModel() { IsEnabled = true };
            return View(newUser);
        }

        // POST: admin/users/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            if (user == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveUser(user);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("UserName", response.Message);
                    return View(user);
                }

                return RedirectToAction("index");
            }

            return View(user);
        }

        private ISecurityService _service;
    }
}
using System;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Filters;

namespace SPPC.Tadbir.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(ISecurityService service)
        {
            _service = service;
        }

        // GET: admin/users
        [AppAuthorize(SecureEntity.User, (int)UserPermissions.View)]
        public ViewResult Index(int? page = null)
        {
            var users = _service.GetUsers();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/users/create
        [AppAuthorize(SecureEntity.User, (int)UserPermissions.Create)]
        public ViewResult Create()
        {
            var newUser = new UserViewModel() { IsEnabled = true };
            return View(newUser);
        }

        // POST: admin/users/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.User, (int)UserPermissions.Create)]
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

        // GET: admin/users/edit/id
        [AppAuthorize(SecureEntity.User, (int)UserPermissions.Edit)]
        public ActionResult Edit(int id)
        {
            // Prevent modification of Admin user by directly browsing the Edit page...
            if (id == AppConstants.AdminUserId)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            var user = _service.GetUser(id);
            if (user == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(user);
        }

        // POST: admin/users/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.User, (int)UserPermissions.Edit)]
        public ActionResult Edit(UserViewModel user)
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

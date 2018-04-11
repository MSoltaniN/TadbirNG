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
    public class RolesController : Controller
    {
        public RolesController(ISecurityService service)
        {
            _service = service;
        }

        // GET: admin/roles
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.View)]
        public ViewResult Index(int? page = null)
        {
            var roles = _service.GetRoles();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(roles.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/roles/create
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.Create)]
        public ViewResult Create()
        {
            var newRole = _service.GetNewRole();
            return View(newRole);
        }

        // POST: admin/roles/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.Create)]
        public ActionResult Create(RoleFullViewModel fullRole)
        {
            if (fullRole == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveRole(fullRole);
                return RedirectToAction("index");
            }

            return View(fullRole);
        }

        // GET: admin/roles/edit/id
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.Edit)]
        public ActionResult Edit(int id)
        {
            // Prevent modification of Admin role by directly browsing the Edit page...
            if (id == AppConstants.AdminRoleId)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            var role = _service.GetRole(id);
            if (role == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(role);
        }

        // POST: admin/roles/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.Edit)]
        public ActionResult Edit(RoleFullViewModel fullRole)
        {
            if (fullRole == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveRole(fullRole);
                return RedirectToAction("index");
            }

            return View(fullRole);
        }

        // GET: admin/roles/details/id
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.View)]
        public ActionResult Details(int id)
        {
            var role = _service.GetRoleDetails(id);
            if (role == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(role);
        }

        // GET: admin/roles/delete/id
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.Delete)]
        public ActionResult Delete(int id)
        {
            // Prevent deletion of Admin role by directly browsing the Edit page...
            if (id == AppConstants.AdminRoleId)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            var response = _service.DeleteRole(id);
            if (response.Result == ServiceResult.DeleteFailed)
            {
                return View(response);
            }

            return RedirectToAction("index");
        }

        // GET: admin/roles/branches/id
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public ActionResult Branches(int id)
        {
            var branches = _service.GetRoleBranches(id);
            if (branches == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(branches);
        }

        // POST: admin/roles/branches/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public ActionResult Branches(RoleBranchesViewModel roleBranches)
        {
            if (roleBranches == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveRoleBranches(roleBranches);
                return RedirectToAction("index");
            }

            return View(roleBranches);
        }

        // GET: admin/roles/users/id
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public ActionResult Users(int id)
        {
            var users = _service.GetRoleUsers(id);
            if (users == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(users);
        }

        // POST: admin/roles/users/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public ActionResult Users(RoleUsersViewModel roleUsers)
        {
            if (roleUsers == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveRoleUsers(roleUsers);
                return RedirectToAction("index");
            }

            return View(roleUsers);
        }

        private ISecurityService _service;
    }
}
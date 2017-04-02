using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using SPPC.Framework.Service;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Extensions;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController(ISecurityService service)
        {
            _service = service;
        }

        // GET: account/login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginModel, string returnUrl)
        {
            Verify.ArgumentNotNull(loginModel, "loginModel");
            if (ModelState.IsValid && this.ValidateCaptcha())
            {
                var user = _service.Authenticate(loginModel);
                if (user != null)
                {
                    _service.Login(user);
                    string redir = returnUrl ?? "/";
                    return Redirect(redir);
                }
                else
                {
                    ModelState.AddModelError(String.Empty, Strings.InvalidUserOrPassword);
                }
            }

            return View();
        }

        // GET: account/manage
        public ActionResult Manage()
        {
            var principal = HttpContext.User;
            var identity = principal?.Identity;
            var model = new UserProfileViewModel()
            {
                UserName = identity?.Name
            };
            return View(model);
        }

        // POST: account/manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(UserProfileViewModel profile)
        {
            if (profile == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                var response = _service.ChangePassword(profile);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("OldPassword", response.Message);
                    return View(profile);
                }

                return RedirectToAction("index", "home", new { area = String.Empty });
            }

            return View(profile);
        }

        private ISecurityService _service;
    }
}
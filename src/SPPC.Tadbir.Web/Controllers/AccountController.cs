using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Extensions;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Web.Controllers
{
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

        private ISecurityService _service;
    }
}
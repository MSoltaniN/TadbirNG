using System;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Filters;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    public class AccountsController : Controller
    {
        public AccountsController(IAccountService accountService)
        {
            _service = accountService;
        }

        // GET: accounting/accounts[?page={page}]
        [AppAuthorize(SecureEntity.Account, (int)AccountPermissions.View)]
        public ViewResult Index(int? page = null)
        {
            var accounts = _service.GetAccounts(TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        // GET: accounting/accounts/create
        [AppAuthorize(SecureEntity.Account, (int)AccountPermissions.Create)]
        public ViewResult Create()
        {
            var account = new AccountViewModel()
            {
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId
            };
            return View(account);
        }

        // POST: accounting/accounts/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Account, (int)AccountPermissions.Create)]
        public ActionResult Create(AccountViewModel account)
        {
            if (account == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveAccount(account);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Code", response.Message);
                    return View(account);
                }

                return RedirectToAction("index");
            }

            return View(account);
        }

        // GET: accounting/accounts/edit/id
        [AppAuthorize(SecureEntity.Account, (int)AccountPermissions.Edit)]
        public ActionResult Edit(int id)
        {
            var viewModel = _service.GetAccount(id);
            if (viewModel == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(viewModel);
        }

        // POST: accounting/accounts/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Account, (int)AccountPermissions.Edit)]
        public ActionResult Edit(AccountViewModel account)
        {
            if (account == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveAccount(account);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Code", response.Message);
                    return View(account);
                }

                return RedirectToAction("index");
            }

            return View(account);
        }

        // GET: accounting/accounts/details/id
        [AppAuthorize(SecureEntity.Account, (int)AccountPermissions.View)]
        public ActionResult Details(int id)
        {
            var viewModel = _service.GetDetailAccountInfo(id);
            if (viewModel == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(viewModel);
        }

        // GET: accounting/account/delete/id
        [AppAuthorize(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public ActionResult Delete(int id)
        {
            var response = _service.DeleteAccount(id);
            if (response.Result == ServiceResult.DeleteFailed)
            {
                return View(response);
            }

            return RedirectToAction("index");
        }

        private IAccountService _service;
    }
}

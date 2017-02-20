using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    public class AccountsController : Controller
    {
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: accounting/accounts[?page={page}]
        public ViewResult Index(int? page = null)
        {
            var accounts = _accountService.GetAccounts(TempContext.CurrentFiscalPeriodId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        // GET: accounting/accounts/create
        public ViewResult Create()
        {
            var account = new AccountViewModel() { FiscalPeriodId = TempContext.CurrentFiscalPeriodId };
            return View(account);
        }

        // POST: accounting/accounts/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel account)
        {
            if (account == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                var response = _accountService.SaveAccount(account);
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
        public ActionResult Edit(int id)
        {
            var viewModel = _accountService.GetAccount(id);
            if (viewModel == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(viewModel);
        }

        // POST: accounting/accounts/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountViewModel account)
        {
            if (account == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                var response = _accountService.SaveAccount(account);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Code", response.Message);
                    return View(account);
                }

                return RedirectToAction("index");
            }

            return View(account);
        }

        private IAccountService _accountService;
    }
}

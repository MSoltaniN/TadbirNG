using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
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
        public ActionResult Create(AccountViewModel viewModel)
        {
            if (viewModel == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                _accountService.SaveAccount(viewModel);
                return RedirectToAction("index");
            }

            return View(viewModel);
        }

        private IAccountService _accountService;
    }
}

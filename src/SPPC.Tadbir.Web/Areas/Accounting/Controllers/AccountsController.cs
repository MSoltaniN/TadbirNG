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

        private IAccountService _accountService;
    }
}

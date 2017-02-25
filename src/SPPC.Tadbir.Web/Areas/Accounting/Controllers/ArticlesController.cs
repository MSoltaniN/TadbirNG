using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    public class ArticlesController : Controller
    {
        public ArticlesController(ITransactionService service, ILookupService lookupService)
        {
            _service = service;
            _lookupService = lookupService;
        }

        // GET: accounting/articles/create?transactionId={id}
        public ActionResult Create(int transactionId)
        {
            ViewBag.AccountLookup = _lookupService.LookupAccounts(TempContext.CurrentFiscalPeriodId);
            ViewBag.CurrencyLookup = _lookupService.LookupCurrencies();
            var article = new TransactionLineViewModel() { TransactionId = transactionId };
            return View(article);
        }

        // POST: accounting/articles/create?transactionId={id}
        [HttpPost]
        public ActionResult Create(TransactionLineViewModel article, int transactionId)
        {
            if (article == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                _service.SaveArticle(article);
                return RedirectToAction("edit", "transactions", new { id = transactionId });
            }

            return View(article);
        }

        private ITransactionService _service;
        private ILookupService _lookupService;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPPC.Framework.Service;
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
            InitLookups();
            var article = new TransactionLineViewModel() { TransactionId = transactionId };
            return View(article);
        }

        // POST: accounting/articles/create?transactionId={id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionLineViewModel article, int transactionId)
        {
            if (article == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveArticle(article);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError(String.Empty, response.Message);
                    InitLookups();
                    return View(article);
                }

                return RedirectToAction("edit", "transactions", new { id = transactionId });
            }

            InitLookups();
            return View(article);
        }

        // GET: accounting/articles/edit/id
        public ActionResult Edit(int id)
        {
            var article = _service.GetArticle(id);
            if (article == null)
            {
                return RedirectToAction("notfound", "error");
            }

            InitLookups();
            return View(article);
        }

        // POST: accounting/articles/edit/id?transactionId={tid}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransactionLineViewModel article, int transactionId)
        {
            if (article == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveArticle(article);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError(String.Empty, response.Message);
                    InitLookups();
                    return View(article);
                }

                return RedirectToAction("edit", "transactions", new { id = transactionId });
            }

            InitLookups();
            return View(article);
        }

        // GET: accounting/articles/details/id
        public ActionResult Details(int id)
        {
            var article = _service.GetDetailArticleInfo(id);
            if (article == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(article);
        }

        private void InitLookups()
        {
            ViewBag.AccountLookup = _lookupService.LookupAccounts(TempContext.CurrentFiscalPeriodId);
            ViewBag.CurrencyLookup = _lookupService.LookupCurrencies();
        }

        private ITransactionService _service;
        private ILookupService _lookupService;
    }
}

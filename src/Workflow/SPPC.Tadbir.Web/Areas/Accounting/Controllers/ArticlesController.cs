using System;
using System.Web.Mvc;
using SPPC.Framework.Service;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Filters;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    public class ArticlesController : Controller
    {
        public ArticlesController(IVoucherService service, ILookupService lookupService)
        {
            _service = service;
            _lookupService = lookupService;
        }

        // GET: accounting/articles/create?transactionId={id}
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public ActionResult Create(int transactionId)
        {
            InitLookups();
            var article = new VoucherLineViewModel()
            {
                VoucherId = transactionId,
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId
            };
            return View(article);
        }

        // POST: accounting/articles/create?transactionId={id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public ActionResult Create(VoucherLineViewModel article, int transactionId)
        {
            if (article == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
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
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public ActionResult Edit(int id)
        {
            var article = _service.GetArticle(id);
            if (article == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            InitLookups();
            return View(article);
        }

        // POST: accounting/articles/edit/id?transactionId={tid}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public ActionResult Edit(VoucherLineViewModel article, int transactionId)
        {
            if (article == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
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
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public ActionResult Details(int id)
        {
            var article = _service.GetDetailArticleInfo(id);
            if (article == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(article);
        }

        // GET: accounting/articles/delete/id?transactionId={tid}
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public ActionResult Delete(int id, int transactionId)
        {
            _service.DeleteArticle(id);
            return RedirectToAction("edit", "transactions", new { area = "accounting", id = transactionId });
        }

        private void InitLookups()
        {
            ViewBag.AccountLookup = _lookupService.LookupAccounts(
                TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            ViewBag.CurrencyLookup = _lookupService.LookupCurrencies();
        }

        private IVoucherService _service;
        private ILookupService _lookupService;
    }
}

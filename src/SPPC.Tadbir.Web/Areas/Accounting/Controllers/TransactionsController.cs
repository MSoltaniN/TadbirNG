using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Filters;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    public class TransactionsController : Controller
    {
        public TransactionsController(ITransactionService service, ISecurityContextManager contextManager)
        {
            _service = service;
            _contextManager = contextManager;
        }

        // GET: accounting/transactions[?page={page}]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public ViewResult Index(int? page = null)
        {
            var transactions = _service.GetTransactions(TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(transactions.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IList<SelectedItemViewModel> allItems)
        {
            Verify.ArgumentNotNull(allItems, "allItems");
            for (int i = 0; i < allItems.Count; i++)
            {
                var item = allItems[i];
                item.IsSelected = Request.Form.AllKeys.Contains(String.Format("[{0}].IsSelected", i));
            }

            ActionResult result = null;
            var routeValues = GetGroupOperationRouteValues(allItems);
            routeValues.Add("paraph", Request.Form["paraph"]);
            if (Request.Form.AllKeys.Contains("submit-prepare"))
            {
                result = RedirectToAction("GroupPrepare", routeValues);
            }

            return result;
        }

        // GET: accounting/transactions/create
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public ViewResult Create()
        {
            var currentContext = _contextManager.CurrentContext;
            var transaction = new TransactionViewModel()
            {
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId,
                CreatedById = currentContext.User.Id,
                ModifiedById = currentContext.User.Id,
                Date = JalaliDateTime.Now.ToShortDateString()
            };

            return View(transaction);
        }

        // POST: accounting/transactions/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public ActionResult Create(TransactionViewModel transaction)
        {
            if (transaction == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            JalaliDateTime jalali;
            if (!JalaliDateTime.TryParse(transaction.Date, out jalali))
            {
                ModelState.AddModelError("Date", Strings.InvalidDate);
                return View(transaction);
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveTransaction(transaction);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Date", response.Message);
                    return View(transaction);
                }

                return RedirectToAction("index");
            }

            return View(transaction);
        }

        // GET: accounting/transactions/edit/id
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public ActionResult Edit(int id)
        {
            var transaction = _service.GetDetailTransactionInfo(id);
            if (transaction == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            transaction.Transaction.ModifiedById = _contextManager.CurrentContext.User.Id;
            return View(transaction);
        }

        // POST: accounting/transactions/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public ActionResult Edit(TransactionFullViewModel fullTransaction)
        {
            if (fullTransaction == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            JalaliDateTime jalali;
            if (!JalaliDateTime.TryParse(fullTransaction.Transaction.Date, out jalali))
            {
                ModelState.AddModelError("Transaction.Date", Strings.InvalidDate);
                return View(fullTransaction);
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveTransaction(fullTransaction.Transaction);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Transaction.Date", response.Message);
                    return View(fullTransaction);
                }

                return RedirectToAction("index");
            }

            return View(fullTransaction);
        }

        // GET: accounting/transactions/details/id
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public ActionResult Details(int id)
        {
            var transaction = _service.GetDetailTransactionInfo(id);
            if (transaction == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(transaction);
        }

        // GET: accounting/transactions/delete/id
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
        public ActionResult Delete(int id)
        {
            _service.DeleteTransaction(id);
            return RedirectToAction("index");
        }

        // GET: accounting/transactions/prepare/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Prepare)]
        public ActionResult Prepare(int id, string paraph = null)
        {
            var response = _service.PrepareTransaction(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/review/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Review)]
        public ActionResult Review(int id, string paraph = null)
        {
            var response = _service.ReviewTransaction(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/reject/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Confirm)]
        public ActionResult Reject(int id, string paraph = null)
        {
            var response = _service.RejectTransaction(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/confirm/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Confirm)]
        public ActionResult Confirm(int id, string paraph = null)
        {
            var response = _service.ConfirmTransaction(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/approve/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Approve)]
        public ActionResult Approve(int id, string paraph = null)
        {
            var response = _service.ApproveTransaction(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/prepare?id1={id1}&id2={id2}&...[&paraph={encoded-text}]
        [ActionName("GroupPrepare")]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Prepare)]
        public ActionResult Prepare(string paraph = null)
        {
            var items = GetGroupOperationItems();
            var response = _service.PrepareTransactions(items, paraph);
            return GetNextResult(response);
        }

        private static RouteValueDictionary GetGroupOperationRouteValues(IList<SelectedItemViewModel> allItems)
        {
            var routeValues = new RouteValueDictionary();
            int index = 0;
            foreach (var item in allItems.Where(i => i.IsSelected))
            {
                routeValues.Add(String.Format("id{0}", index), item.Id);
                index++;
            }

            return routeValues;
        }

        private ActionResult GetNextResult(ServiceResponse response)
        {
            ActionResult nextResult = RedirectToAction("index");
            if (!response.Succeeded)
            {
                nextResult = View("error", response);
            }
            else if (Request.QueryString.AllKeys.Contains("returnUrl"))
            {
                nextResult = RedirectToAction("index", "cartable", new { area = String.Empty });
            }

            return nextResult;
        }

        private IEnumerable<int> GetGroupOperationItems()
        {
            var items = new List<int>(Request.QueryString.AllKeys
                .Where(k => k != "paraph")
                .Select(k => Int32.Parse(Request.QueryString[k])));
            return items;
        }

        private ITransactionService _service;
        private ISecurityContextManager _contextManager;
    }
}

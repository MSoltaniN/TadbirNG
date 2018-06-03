using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SPPC.Framework.Common;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Filters;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    public class TransactionsController : Controller
    {
        public TransactionsController(IVoucherService service)
        {
            _service = service;
        }

        #region Transaction CRUD Actions

        // GET: accounting/transactions[?page={page}]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public ViewResult Index(int? page = null)
        {
            var transactions = _service.GetVouchers(TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(transactions.ToPagedList(pageNumber, pageSize));
        }

        // POST: accounting/transactions
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

            var routeValues = GetGroupOperationRouteValues(allItems);
            routeValues.Add("paraph", Request.Form["paraph"]);
            return GetNextResult(routeValues);
        }

        // GET: accounting/transactions/create
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public ViewResult Create()
        {
            var transaction = new VoucherViewModel()
            {
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId,
                Date = DateTime.Now
            };

            return View(transaction);
        }

        // POST: accounting/transactions/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public ActionResult Create(VoucherViewModel transaction)
        {
            if (transaction == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveVoucher(transaction);
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
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public ActionResult Edit(int id)
        {
            var transaction = _service.GetDetailTransactionInfo(id);
            if (transaction == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(transaction);
        }

        // POST: accounting/transactions/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public ActionResult Edit(VoucherFullViewModel fullVoucher)
        {
            if (fullVoucher == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveVoucher(fullVoucher.Voucher);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Voucher.Date", response.Message);
                    return View(fullVoucher);
                }

                return RedirectToAction("index");
            }

            return View(fullVoucher);
        }

        // GET: accounting/transactions/details/id
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public ActionResult Details(int id)
        {
            var transaction = _service.GetDetailVoucherInfo(id);
            if (transaction == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(transaction);
        }

        // GET: accounting/transactions/delete/id
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public ActionResult Delete(int id)
        {
            _service.DeleteVoucher(id);
            return RedirectToAction("index");
        }

        #endregion

        #region Transaction Workflow Actions

        // GET: accounting/transactions/prepare/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Prepare)]
        public ActionResult Prepare(int id, string paraph = null)
        {
            var response = _service.PrepareVoucher(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/review/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Review)]
        public ActionResult Review(int id, string paraph = null)
        {
            var response = _service.ReviewVoucher(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/reject/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public ActionResult Reject(int id, string paraph = null)
        {
            var response = _service.RejectVoucher(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/confirm/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public ActionResult Confirm(int id, string paraph = null)
        {
            var response = _service.ConfirmVoucher(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/approve/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Approve)]
        public ActionResult Approve(int id, string paraph = null)
        {
            var response = _service.ApproveVoucher(id, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/prepare?id0={id0}[&id1={id1}&...&paraph={encoded-text}]
        [ActionName("GroupPrepare")]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Prepare)]
        public ActionResult Prepare(string paraph = null)
        {
            var items = GetGroupOperationItems();
            var response = _service.PrepareVouchers(items, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/review?id0={id0}[&id1={id1}&...&paraph={encoded-text}]
        [ActionName("GroupReview")]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Review)]
        public ActionResult Review(string paraph = null)
        {
            var items = GetGroupOperationItems();
            var response = _service.ReviewVouchers(items, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/reject?id0={id0}[&id1={id1}&...&paraph={encoded-text}]
        [ActionName("GroupReject")]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public ActionResult Reject(string paraph = null)
        {
            var items = GetGroupOperationItems();
            var response = _service.RejectVouchers(items, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/confirm?id0={id0}[&id1={id1}&...&paraph={encoded-text}]
        [ActionName("GroupConfirm")]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public ActionResult Confirm(string paraph = null)
        {
            var items = GetGroupOperationItems();
            var response = _service.ConfirmVouchers(items, paraph);
            return GetNextResult(response);
        }

        // GET: accounting/transactions/approve?id0={id0}[&id1={id1}&...&paraph={encoded-text}]
        [ActionName("GroupApprove")]
        [AppAuthorize(SecureEntity.Voucher, (int)VoucherPermissions.Approve)]
        public ActionResult Approve(string paraph = null)
        {
            var items = GetGroupOperationItems();
            var response = _service.ApproveVouchers(items, paraph);
            return GetNextResult(response);
        }

        #endregion

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

        private ActionResult GetNextResult(RouteValueDictionary routeValues)
        {
            ActionResult result = null;
            if (Request.Form.AllKeys.Contains("submit-prepare"))
            {
                result = RedirectToAction("GroupPrepare", routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-review"))
            {
                result = RedirectToAction("GroupReview", routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-reject"))
            {
                result = RedirectToAction("GroupReject", routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-confirm"))
            {
                result = RedirectToAction("GroupConfirm", routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-approve"))
            {
                result = RedirectToAction("GroupApprove", routeValues);
            }

            return result;
        }

        private IEnumerable<int> GetGroupOperationItems()
        {
            var items = new List<int>(Request.QueryString.AllKeys
                .Where(k => k.StartsWith("id"))
                .Select(k => Int32.Parse(Request.QueryString[k])));
            return items;
        }

        private IVoucherService _service;
    }
}

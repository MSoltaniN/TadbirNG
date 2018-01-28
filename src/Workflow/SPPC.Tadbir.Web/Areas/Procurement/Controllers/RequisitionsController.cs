using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Framework.Values;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Procurement;
using SPPC.Tadbir.Web.Filters;

namespace SPPC.Tadbir.Web.Areas.Procurement.Controllers
{
    public class RequisitionsController : Controller
    {
        public RequisitionsController(IRequisitionService service, ILookupService lookupService)
        {
            _service = service;
            _lookupService = lookupService;
        }

        #region CRUD Actions

        // GET: procurement/requisitions[?page={no}]
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.View)]
        public ActionResult Index(int? page = null)
        {
            var requisitions = _service.GetRequisitions(TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(requisitions.ToPagedList(pageNumber, pageSize));
        }

        // GET: procurement/requisitions/create
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Create)]
        public ViewResult Create()
        {
            InitLookups();
            var voucher = new RequisitionVoucherViewModel()
            {
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId
            };

            return View(voucher);
        }

        // POST: procurement/requisitions/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Create)]
        public ActionResult Create(RequisitionVoucherViewModel voucher)
        {
            if (voucher == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveRequisition(voucher);
                return RedirectToAction("index");
            }

            InitLookups();
            return View(voucher);
        }

        // GET: procurement/requisitions/edit/id
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public ActionResult Edit(int id)
        {
            var requisition = _service.GetDetailRequisitionInfo(id);
            if (requisition == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            InitLookups();
            return View(requisition);
        }

        // POST: procurement/requisitions/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public ActionResult Edit(RequisitionFullViewModel fullRequisition)
        {
            if (fullRequisition == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveRequisition(fullRequisition.Voucher);
                return RedirectToAction("index");
            }

            InitLookups();
            return View(fullRequisition);
        }

        // GET: procurement/requisitions/delete/id
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Delete)]
        public ActionResult Delete(int id)
        {
            _service.DeleteRequisition(id);
            return RedirectToAction("index");
        }

        #endregion

        #region Line CRUD Actions

        // GET: procurement/requisitions/createline/id
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public ViewResult CreateLine(int id)
        {
            ViewBag.Title = String.Format(LocalStrings.CreateNewEntity, Entities.Article);
            InitLineLookups();
            var voucherLine = new RequisitionVoucherLineViewModel()
            {
                VoucherId = id,
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId
            };
            return View("LineEditor", voucherLine);
        }

        // POST: procurement/requisitions/createline/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public ActionResult CreateLine(int id, RequisitionVoucherLineViewModel line)
        {
            if (line == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                line.Id = 0;        // MVC binds value of id route value to Id property, which is wrong.
                _service.SaveRequisitionLine(line);
                return RedirectToAction("edit", new { id = id });
            }

            InitLineLookups();
            return View("LineEditor", line);
        }

        // GET: procurement/requisitions/editline/id?lineId={lineId}
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public ActionResult EditLine(int id, int lineId)
        {
            ViewBag.Title = String.Format(LocalStrings.EditExistingEntity, Entities.Article);
            var voucherLine = _service.GetDetailRequisitionLineInfo(id, lineId);
            if (voucherLine == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            InitLineLookups();
            return View("LineEditor", voucherLine);
        }

        // POST: procurement/requisitions/editline/id?lineId={lineId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public ActionResult EditLine(int id, int lineId, RequisitionVoucherLineViewModel line)
        {
            if (line == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                line.Id = lineId;        // MVC binds value of id route value to Id property, which is wrong.
                _service.SaveRequisitionLine(line);
                return RedirectToAction("edit", new { id = id });
            }

            InitLookups();
            return View("LineEditor", line);
        }

        // GET: procurement/requisitions/deleteline/id?lineId={lineId}
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Delete)]
        public ActionResult DeleteLine(int id, int lineId)
        {
            _service.DeleteRequisitionLine(id, lineId);
            return RedirectToAction("edit", "requisitions", new { area = "procurement", id = id });
        }

        #endregion

        #region Workflow Actions

        // GET: procurement/requisitions/prepare/id[?paraph={encoded-text}]
        [AppAuthorize(SecureEntity.Requisition, (int)RequisitionPermissions.Prepare)]
        public ActionResult Prepare(int id, string paraph = null)
        {
            var response = _service.Prepare(id, paraph);
            return GetNextResult(response);
        }

        #endregion

        private void InitLookups()
        {
            var depends = _lookupService.LookupRequisitionVoucherDepends();
            ViewBag.VoucherTypes = depends.VoucherTypes;
            ViewBag.Accounts = depends.Accounts;
            ViewBag.DetailAccounts = depends.DetailAccounts;
            ViewBag.CostCenters = depends.CostCenters;
            ViewBag.Projects = depends.Projects;
            ViewBag.Partners = depends.Partners;
            ViewBag.Units = depends.Units;
            ViewBag.Warehouses = depends.Warehouses;
        }

        private void InitLineLookups()
        {
            var depends = _lookupService.LookupRequisitionVoucherLineDepends();
            ViewBag.Accounts = depends.Accounts;
            ViewBag.DetailAccounts = depends.DetailAccounts;
            ViewBag.CostCenters = depends.CostCenters;
            ViewBag.Projects = depends.Projects;
            ViewBag.Warehouses = depends.Warehouses;
            ViewBag.Products = depends.Products;
            ViewBag.Units = depends.Units;
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

        private IRequisitionService _service;
        private ILookupService _lookupService;
    }
}

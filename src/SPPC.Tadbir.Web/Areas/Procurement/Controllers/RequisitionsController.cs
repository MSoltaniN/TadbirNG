using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Web.Areas.Procurement.Controllers
{
    public class RequisitionsController : Controller
    {
        public RequisitionsController(IRequisitionService service, ILookupService lookupService)
        {
            _service = service;
            _lookupService = lookupService;
        }

        // GET: procurement/requisitions[?page={no}]
        public ActionResult Index(int? page = null)
        {
            var requisitions = _service.GetRequisitions(TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(requisitions.ToPagedList(pageNumber, pageSize));
        }

        // GET: procurement/requisitions/create
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

        // GET: procurement/requisitions/id/createline
        public ViewResult CreateLine(int id)
        {
            InitLineLookups();
            var voucherLine = new RequisitionVoucherLineViewModel()
            {
                VoucherId = id,
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId
            };
            return View(voucherLine);
        }

        // POST: procurement/requisitions/id/createline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLine(int id, RequisitionVoucherLineViewModel line)
        {
            if (line == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveRequisitionLine(line);
                return RedirectToAction("edit", new { id = id });
            }

            InitLineLookups();
            return View(line);
        }

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

        private IRequisitionService _service;
        private ILookupService _lookupService;
    }
}

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

        private void InitLookups()
        {
            var depends = _lookupService.LookupRequisitionVoucherDepends();
            ViewBag.Accounts = depends.Accounts;
            ViewBag.DetailAccounts = depends.DetailAccounts;
            ViewBag.CostCenters = depends.CostCenters;
            ViewBag.Projects = depends.Projects;
            ViewBag.Partners = depends.Partners;
            ViewBag.Units = depends.Units;
            ViewBag.Warehouses = depends.Warehouses;
        }

        private IRequisitionService _service;
        private ILookupService _lookupService;
    }
}

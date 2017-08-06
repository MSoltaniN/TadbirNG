using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Web.Areas.Procurement.Controllers
{
    public class RequisitionsController : Controller
    {
        public RequisitionsController(IRequisitionService service)
        {
            _service = service;
        }

        // GET: procurement/requisitions
        public ActionResult Index(int? page = null)
        {
            var requisitions = _service.GetRequisitions(TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(requisitions.ToPagedList(pageNumber, pageSize));
        }

        private IRequisitionService _service;
    }
}
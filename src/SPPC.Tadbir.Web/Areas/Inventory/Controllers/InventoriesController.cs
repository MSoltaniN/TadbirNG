using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Web.Areas.Inventory.Controllers
{
    public class InventoriesController : Controller
    {
        public InventoriesController(IInventoryService service)
        {
            _service = service;
        }

        // GET: inventory/inventories
        public ViewResult Index(int? page = null)
        {
            var inventories = _service.GetProductInventories(
                TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageNumber = (page ?? 1);
            return View(inventories.ToPagedList(pageNumber, Constants.DefaultPageSize));
        }

        private IInventoryService _service;
    }
}

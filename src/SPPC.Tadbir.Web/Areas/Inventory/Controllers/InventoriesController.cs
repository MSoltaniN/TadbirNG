using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Values;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.Web.Filters;

namespace SPPC.Tadbir.Web.Areas.Inventory.Controllers
{
    public class InventoriesController : Controller
    {
        public InventoriesController(IInventoryService service, ILookupService lookupService)
        {
            _service = service;
            _lookupService = lookupService;
        }

        // GET: inventory/inventories
        [AppAuthorize(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.View)]
        public ViewResult Index(int? page = null)
        {
            var inventories = _service.GetProductInventories(
                TempContext.CurrentFiscalPeriodId, TempContext.CurrentBranchId);
            int pageNumber = (page ?? 1);
            return View(inventories.ToPagedList(pageNumber, Constants.DefaultPageSize));
        }

        // GET: inventory/inventories/create
        [AppAuthorize(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Create)]
        public ViewResult Create()
        {
            ViewBag.Title = String.Format(LocalStrings.CreateNewEntity, Entities.ProductInventoryAlt);
            InitLookups();
            var inventory = new ProductInventoryViewModel()
            {
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                BranchId = TempContext.CurrentBranchId
            };
            return View("Editor", inventory);
        }

        // POST: inventory/inventories/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Create)]
        public ActionResult Create(ProductInventoryViewModel inventory)
        {
            if (inventory == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveProductInventory(inventory);
                return RedirectToAction("index");
            }

            ViewBag.Title = String.Format(LocalStrings.CreateNewEntity, Entities.ProductInventoryAlt);
            InitLookups();
            return View("Editor", inventory);
        }

        // GET: inventory/inventories/edit/id
        [AppAuthorize(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Edit)]
        public ActionResult Edit(int id)
        {
            var inventory = _service.GetProductInventory(id);
            if (inventory == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            ViewBag.Title = String.Format(LocalStrings.EditExistingEntity, Entities.ProductInventory);
            InitLookups();
            return View("Editor", inventory);
        }

        // POST: inventory/inventories/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Edit)]
        public ActionResult Edit(ProductInventoryViewModel inventory)
        {
            if (inventory == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            if (ModelState.IsValid)
            {
                _service.SaveProductInventory(inventory);
                return RedirectToAction("index");
            }

            ViewBag.Title = String.Format(LocalStrings.EditExistingEntity, Entities.ProductInventory);
            InitLookups();
            return View("Editor", inventory);
        }

        private void InitLookups()
        {
            var depends = _lookupService.LookupProductInventoryDepends();
            ViewBag.Units = depends.Units;
            ViewBag.Products = depends.Products;
            ViewBag.Warehouses = depends.Warehouses;
        }

        private IInventoryService _service;
        private ILookupService _lookupService;
    }
}

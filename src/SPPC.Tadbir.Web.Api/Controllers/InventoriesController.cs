using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class InventoriesController : ApiController
    {
        public InventoriesController(IInventoryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/inventories/fp/{fpId:int}/branch/{branchId:int}
        [Route(InventoryApi.FiscalPeriodBranchInventoriesUrl)]
        [AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.View)]
        public IHttpActionResult GetProductInventories(int fpId, int branchId)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var inventories = _repository.GetProductInventories(fpId, branchId);
            return Json(inventories);
        }

        // GET: api/inventories/{inventoryId:int}
        [Route(InventoryApi.InventoryUrl)]
        [AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.View)]
        public IHttpActionResult GetProductInventory(int inventoryId)
        {
            if (inventoryId <= 0)
            {
                return NotFound();
            }

            var inventory = _repository.GetProductInventory(inventoryId);
            var result = (inventory != null)
                ? Json(inventory)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // POST: api/inventories
        [Route(InventoryApi.InventoriesUrl)]
        [AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Create)]
        public IHttpActionResult PostNewProductInventory([FromBody] ProductInventoryViewModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Could not post new product inventory because a 'null' value was provided");
            }

            _repository.SaveProductInventory(inventory);
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/inventories/{inventoryId:int}
        [Route(InventoryApi.InventoryUrl)]
        [AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Edit)]
        public IHttpActionResult PutModifiedProductInventory(
            int inventoryId, [FromBody] ProductInventoryViewModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Could not put modified product inventory because a 'null' value was provided");
            }

            if (inventory.Id <= 0 || inventoryId <= 0)
            {
                return BadRequest("Could not put modified product inventory because original item could not be found");
            }

            if (inventory.Id != inventoryId)
            {
                return BadRequest("Could not put modified product inventory due to an identity conflict in request");
            }

            _repository.SaveProductInventory(inventory);
            return StatusCode(HttpStatusCode.OK);
        }

        private IInventoryRepository _repository;
    }
}

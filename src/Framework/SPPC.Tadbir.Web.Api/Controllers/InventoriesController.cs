using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class InventoriesController : Controller
    {
        public InventoriesController(IInventoryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/inventories/fp/{fpId:int}/branch/{branchId:int}
        [Route(InventoryApi.FiscalPeriodBranchInventoriesUrl)]
        //[AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.View)]
        public IActionResult GetProductInventories(int fpId, int branchId)
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
        //[AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.View)]
        public IActionResult GetProductInventory(int inventoryId)
        {
            if (inventoryId <= 0)
            {
                return NotFound();
            }

            var inventory = _repository.GetProductInventory(inventoryId);
            var result = (inventory != null)
                ? Json(inventory)
                : NotFound() as IActionResult;
            return result;
        }

        // POST: api/inventories
        [HttpPost, Route(InventoryApi.InventoriesUrl)]
        //[AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Create)]
        public IActionResult PostNewProductInventory([FromBody] ProductInventoryViewModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Could not post new product inventory because a 'null' value was provided");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveProductInventory(inventory);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/inventories/{inventoryId:int}
        [HttpPut, Route(InventoryApi.InventoryUrl)]
        //[AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Edit)]
        public IActionResult PutModifiedProductInventory(
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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveProductInventory(inventory);
            return Ok();
        }

        // DELETE: api/inventories/{inventoryId:int}
        [HttpDelete, Route(InventoryApi.InventoryUrl)]
        //[AuthorizeRequest(SecureEntity.ProductInventory, (int)ProductInventoryPermissions.Delete)]
        public IActionResult DeleteExistingProductInventory(int inventoryId)
        {
            if (inventoryId <= 0)
            {
                return BadRequest("Could not delete product inventory because it does not exist.");
            }

            _repository.DeleteProductInventory(inventoryId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private IInventoryRepository _repository;
    }
}

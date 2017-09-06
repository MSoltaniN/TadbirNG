using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class InventoriesController : ApiController
    {
        public InventoriesController(IInventoryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/inventories/fp/{fpId:int}/branch/{branchId:int}
        [Route(InventoryApi.ProductInventoriesUrl)]
        public IHttpActionResult GetProductInventories(int fpId, int branchId)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var inventories = _repository.GetProductInventories(fpId, branchId);
            return Json(inventories);
        }

        private IInventoryRepository _repository;
    }
}

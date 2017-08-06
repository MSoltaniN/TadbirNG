using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class RequisitionsController : ApiController
    {
        public RequisitionsController(IRequisitionRepository repository)
        {
            _repository = repository;
        }

        // GET: api/requisitions/fp/{fpId:int}/branch/{branchId:int}
        [Route(RequisitionApi.FiscalPeriodBranchRequisitionsUrl)]
        public IHttpActionResult GetRequisitions(int fpId, int branchId)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var requisitions = _repository.GetRequisitions(fpId, branchId);
            return Json(requisitions);
        }

        // POST: api/requisitions
        [Route(RequisitionApi.RequisitionsUrl)]
        public IHttpActionResult PostNewRequisitionVoucher([FromBody] RequisitionVoucherViewModel voucher)
        {
            if (voucher == null)
            {
                return BadRequest("Could not post new requisition because a 'null' value was provided.");
            }

            _repository.SaveRequisition(voucher);
            return StatusCode(HttpStatusCode.Created);
        }

        private IRequisitionRepository _repository;
    }
}

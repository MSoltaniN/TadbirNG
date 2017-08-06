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

        private IRequisitionRepository _repository;
    }
}

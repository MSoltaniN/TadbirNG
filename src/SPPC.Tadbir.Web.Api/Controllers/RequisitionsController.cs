using System;
using System.Net;
using System.Web.Http;
using BabakSoft.Platform.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class RequisitionsController : ApiController
    {
        public RequisitionsController(IRequisitionRepository repository, ISecurityContextManager contextManager)
        {
            Verify.ArgumentNotNull(contextManager, "contextManager");
            _repository = repository;
            _userContext = contextManager.CurrentContext;
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

        // GET: api/requisitions/{voucherId:int}/details
        [Route(RequisitionApi.RequisitionDetailsUrl)]
        public IHttpActionResult GetRequisitionDetails(int voucherId)
        {
            if (voucherId < 0)
            {
                return NotFound();
            }

            var voucher = _repository.GetRequisitionDetails(voucherId);
            var result = (voucher != null)
                ? Json(voucher)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // POST: api/requisitions
        [Route(RequisitionApi.RequisitionsUrl)]
        public IHttpActionResult PostNewRequisitionVoucher([FromBody] RequisitionVoucherViewModel voucher)
        {
            if (voucher == null)
            {
                return BadRequest("Could not post new requisition because a 'null' value was provided.");
            }

            SetVoucherDocument(voucher);
            _repository.SaveRequisition(voucher);
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/requisitions/{voucherId:int}
        [Route(RequisitionApi.RequisitionUrl)]
        public IHttpActionResult PutModifiedRequisition(int voucherId, [FromBody] RequisitionVoucherViewModel voucher)
        {
            if (voucherId < 0)
            {
                return BadRequest("Could not put modified requisition voucher because it does not exist.");
            }

            if (voucher == null || voucher.Id < 0 || voucherId != voucher.Id)
            {
                return BadRequest();
            }

            _repository.SaveRequisition(voucher);
            return Ok();
        }

        private static string GenerateNumber()
        {
            return Guid.NewGuid()
                .ToString()
                .Replace("{", String.Empty)
                .Replace("}", String.Empty)
                .Replace("-", String.Empty)
                .Substring(0, 8);
        }

        private void SetVoucherDocument(RequisitionVoucherViewModel voucher)
        {
            var document = new DocumentViewModel()
            {
                No = GenerateNumber(),
                OperationalStatus = DocumentStatus.Created,
                StatusId = (int)DocumentStatuses.Draft,
                TypeId = (int)DocumentTypes.RequisitionVoucher
            };
            document.Actions.Add(new DocumentActionViewModel()
            {
                CreatedById = _userContext.User.Id,
                ModifiedById = _userContext.User.Id
            });
            voucher.Document = document;
        }

        private IRequisitionRepository _repository;
        private ISecurityContext _userContext;
    }
}

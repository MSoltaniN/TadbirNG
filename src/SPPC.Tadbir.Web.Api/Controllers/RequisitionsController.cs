using System;
using System.Linq;
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
        public IHttpActionResult PostNewRequisition([FromBody] RequisitionVoucherViewModel voucher)
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

            SetVoucherDocument(voucher);
            _repository.SaveRequisition(voucher);
            return Ok();
        }

        // DELETE: api/requisitions/{voucherId:int}
        [Route(RequisitionApi.RequisitionUrl)]
        public IHttpActionResult DeleteExistingRequisition(int voucherId)
        {
            if (voucherId <= 0)
            {
                return BadRequest("Could not delete requisition because it does not exist.");
            }

            _repository.DeleteRequisition(voucherId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/requisitions/{voucherId:int}/lines/{lineId:int}
        [Route(RequisitionApi.RequisitionLineUrl)]
        public IHttpActionResult GetOneRequisitionLine(int voucherId, int lineId)
        {
            if (lineId < 0)
            {
                return NotFound();
            }

            var line = _repository.GetRequisitionLine(lineId);
            var result = (line != null && line.VoucherId == voucherId)
                ? Json(line)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // POST: api/requisitions/{voucherId:int}/lines
        [Route(RequisitionApi.RequisitionLinesUrl)]
        public IHttpActionResult PostNewRequisitionVoucherLine(
            int voucherId, [FromBody] RequisitionVoucherLineViewModel line)
        {
            if (line == null)
            {
                return BadRequest("Could not post new requisition line because a 'null' value was provided.");
            }

            if (voucherId < 0 || line.VoucherId < 0 || line.VoucherId != voucherId)
            {
                return BadRequest();
            }

            SetVoucherLineAction(line);
            _repository.SaveRequisitionLine(line);
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/requisitions/{voucherId:int}/lines/{lineId:int}
        [Route(RequisitionApi.RequisitionLineUrl)]
        public IHttpActionResult PutModifiedRequisitionLine(
            int voucherId, int lineId, [FromBody] RequisitionVoucherLineViewModel line)
        {
            if (voucherId < 0 || lineId < 0)
            {
                return BadRequest("Could not put modified requisition line because it does not exist.");
            }

            if (line == null || line.Id != lineId || line.VoucherId != voucherId)
            {
                return BadRequest();
            }

            SetVoucherLineAction(line);
            _repository.SaveRequisitionLine(line);
            return Ok();
        }

        private void SetVoucherDocument(RequisitionVoucherViewModel voucher)
        {
            if (voucher.Document == null)
            {
                var document = new DocumentViewModel()
                {
                    OperationalStatus = DocumentStatusName.Created,
                    StatusId = (int)DocumentStatusId.Draft,
                    TypeId = (int)DocumentTypeId.RequisitionVoucher
                };
                document.Actions.Add(new DocumentActionViewModel()
                {
                    CreatedById = _userContext.User.Id,
                    ModifiedById = _userContext.User.Id
                });
                voucher.Document = document;
            }
            else
            {
                var mainAction = voucher.Document.Actions.First();
                mainAction.ModifiedById = _userContext.User.Id;
            }
        }

        private void SetVoucherLineAction(RequisitionVoucherLineViewModel line)
        {
            var document = _repository.GetRequisitionDocument(line.VoucherId);
            if (line.Id == 0)
            {
                var action = new DocumentActionViewModel()
                {
                    CreatedById = _userContext.User.Id,
                    ModifiedById = _userContext.User.Id
                };
                line.DocumentAction = action;
                line.DocumentId = document.Id;
            }
            else
            {
                line.DocumentAction.ModifiedById = _userContext.User.Id;
            }
        }

        private IRequisitionRepository _repository;
        private ISecurityContext _userContext;
    }
}

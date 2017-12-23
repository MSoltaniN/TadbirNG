using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Procurement;
using SPPC.Tadbir.ViewModel.Workflow;
using SPPC.Tadbir.Web.Api.Filters;
//using SPPC.Tadbir.Workflow;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class RequisitionsController : Controller
    {
        public RequisitionsController(IRequisitionRepository repository, ISecurityContextManager contextManager)
        {
            Verify.ArgumentNotNull(contextManager, "contextManager");
            //Verify.ArgumentNotNull(workflow, "workflow");
            _repository = repository;
            //_workflow = workflow;
            //_workflow.CurrentContext = contextManager.CurrentContext;
            _userContext = contextManager.CurrentContext;
        }

        // GET: api/requisitions/fp/{fpId:int}/branch/{branchId:int}
        [Route(RequisitionApi.FiscalPeriodBranchRequisitionsUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.View)]
        public IActionResult GetRequisitions(int fpId, int branchId)
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
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.View)]
        public IActionResult GetRequisitionDetails(int voucherId)
        {
            if (voucherId < 0)
            {
                return NotFound();
            }

            var voucher = _repository.GetRequisitionDetails(voucherId);
            var result = (voucher != null)
                ? Json(voucher)
                : NotFound() as IActionResult;
            return result;
        }

        // POST: api/requisitions
        [HttpPost, Route(RequisitionApi.RequisitionsUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.Create)]
        public IActionResult PostNewRequisition([FromBody] RequisitionVoucherViewModel voucher)
        {
            if (voucher == null)
            {
                return BadRequest("Could not post new requisition because a 'null' value was provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SetVoucherDocument(voucher);
            _repository.SaveRequisition(voucher);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/requisitions/{voucherId:int}
        [HttpPut, Route(RequisitionApi.RequisitionUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public IActionResult PutModifiedRequisition(int voucherId, [FromBody] RequisitionVoucherViewModel voucher)
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
        [HttpDelete, Route(RequisitionApi.RequisitionUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.Delete)]
        public IActionResult DeleteExistingRequisition(int voucherId)
        {
            if (voucherId <= 0)
            {
                return BadRequest("Could not delete requisition because it does not exist.");
            }

            _repository.DeleteRequisition(voucherId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/requisitions/{voucherId:int}/lines/{lineId:int}
        [Route(RequisitionApi.RequisitionLineUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.View)]
        public IActionResult GetOneRequisitionLine(int voucherId, int lineId)
        {
            if (lineId < 0)
            {
                return NotFound();
            }

            var line = _repository.GetRequisitionLine(lineId);
            var result = (line != null && line.VoucherId == voucherId)
                ? Json(line)
                : NotFound() as IActionResult;
            return result;
        }

        // POST: api/requisitions/{voucherId:int}/lines
        [HttpPost, Route(RequisitionApi.RequisitionLinesUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public IActionResult PostNewRequisitionLine(int voucherId, [FromBody] RequisitionVoucherLineViewModel line)
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
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/requisitions/{voucherId:int}/lines/{lineId:int}
        [HttpPut, Route(RequisitionApi.RequisitionLineUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.Edit)]
        public IActionResult PutModifiedRequisitionLine(
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

        // DELETE: api/requisitions/{voucherId:int}/lines/{lineId:int}
        [HttpDelete, Route(RequisitionApi.RequisitionLineUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.Delete)]
        public IActionResult DeleteExistingRequisitionLine(int voucherId, int lineId)
        {
            if (voucherId <= 0 || lineId <= 0)
            {
                BadRequest("Could not delete requisition line because it does not exist.");
            }

            _repository.DeleteRequisitionLine(lineId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/requisitions/{voucherId:int}/prepare
        [HttpPut, Route(RequisitionApi.PrepareUrl)]
        [AuthorizeRequest(SecureEntity.Requisition, (int)RequisitionPermissions.Prepare)]
        public IActionResult PutRequisitionAsPrepared(int voucherId, [FromBody] ActionDetailViewModel detail)
        {
            if (voucherId <= 0)
            {
                return BadRequest("Could not put requisition as Prepared because requisition does not exist.");
            }

            var summary = _repository.GetRequisitionSummary(voucherId);
            if (summary == null)
            {
                return BadRequest("Could not put requisition as Prepared because requisition does not exist.");
            }

            string message = ValidateStateOperation(DocumentActionName.Prepare, summary);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            var paraph = detail?.Paraph;
            //_workflow.Prepare(summary.Id, summary.DocumentId, DocumentTypeName.RequisitionVoucher, paraph);
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
                    TypeId = (int)DocumentTypeId.RequisitionVoucher,
                    EntityNo = voucher.No
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

        private string ValidateStateOperation(string operation, VoucherSummaryViewModel summary)
        {
            string result = String.Empty;
            //if (!_workflow.ValidateAction(DocumentTypeName.RequisitionVoucher, summary.DocumentOperationalStatus, operation))
            //{
            //    result = String.Format(
            //        Strings.InvalidDocumentOperation,
            //        Entities.RequisitionVoucherAlt,
            //        summary.No,
            //        DocumentActionName.ToLocalValue(operation),
            //        DocumentStatusName.ToLocalValue(summary.DocumentOperationalStatus));
            //}

            return result;
        }

        private IRequisitionRepository _repository;
        //private IDocumentWorkflow _workflow;
        private ISecurityContext _userContext;
    }
}

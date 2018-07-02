using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Workflow;
using SPPC.Workflow.Web.Api.Filters;

namespace SPPC.Workflow.Web.Api.Controllers
{
    public class VouchersController : ApiController
    {
        public VouchersController(IVoucherRepository repository, IVoucherWorkflow workflow)
        {
            _repository = repository;
            _workflow = workflow;
        }

        // PUT: api/vouchers/{voucherId:int}/prepare
        [Route(VoucherApi.PrepareVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Prepare)]
        public async Task<IHttpActionResult> PutVoucherAsPreparedAsync(int voucherId, [FromBody] ActionDetailViewModel detail)
        {
            var result = await BasicValidationResultAsync(voucherId, DocumentActionName.Prepare);
            if (result is BadRequestErrorMessageResult)
            {
                return result;
            }

            var voucher = (result as OkNegotiatedContentResult<VoucherViewModel>).Content;
            var paraph = detail?.Paraph;
            _workflow.Prepare(voucher.Id, voucher.Document.Id, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/review
        [Route(VoucherApi.ReviewVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Review)]
        public async Task<IHttpActionResult> PutTransactionAsReviewedAsync(int voucherId, [FromBody] ActionDetailViewModel detail)
        {
            var result = await BasicValidationResultAsync(voucherId, DocumentActionName.Review);
            if (result is BadRequestErrorMessageResult)
            {
                return result;
            }

            var voucher = (result as OkNegotiatedContentResult<VoucherViewModel>).Content;
            var paraph = detail?.Paraph;
            _workflow.Review(voucher.Id, voucher.Document.Id, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/reject
        [Route(VoucherApi.RejectVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public async Task<IHttpActionResult> PutTransactionAsRejectedAsync(int voucherId, [FromBody] ActionDetailViewModel detail)
        {
            var result = await BasicValidationResultAsync(voucherId, DocumentActionName.Reject);
            if (result is BadRequestErrorMessageResult)
            {
                return result;
            }

            var voucher = (result as OkNegotiatedContentResult<VoucherViewModel>).Content;
            var paraph = detail?.Paraph;
            _workflow.Reject(voucher.Id, voucher.Document.Id, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/confirm
        [Route(VoucherApi.ConfirmVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public async Task<IHttpActionResult> PutTransactionAsConfirmedAsync(int voucherId, [FromBody] ActionDetailViewModel detail)
        {
            var result = await BasicValidationResultAsync(voucherId, DocumentActionName.Confirm);
            if (result is BadRequestErrorMessageResult)
            {
                return result;
            }

            var voucher = (result as OkNegotiatedContentResult<VoucherViewModel>).Content;
            var paraph = detail?.Paraph;
            _workflow.Confirm(voucher.Id, voucher.Document.Id, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/approve
        [Route(VoucherApi.ApproveVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Approve)]
        public async Task<IHttpActionResult> PutTransactionAsApprovedAsync(int voucherId, [FromBody] ActionDetailViewModel detail)
        {
            var result = await BasicValidationResultAsync(voucherId, DocumentActionName.Approve);
            if (result is BadRequestErrorMessageResult)
            {
                return result;
            }

            var voucher = (result as OkNegotiatedContentResult<VoucherViewModel>).Content;
            var paraph = detail?.Paraph;
            _workflow.Approve(voucher.Id, voucher.Document.Id, paraph);
            return Ok();
        }

        private async Task<IHttpActionResult> BasicValidationResultAsync(int voucherId, string operation)
        {
            string message = String.Format("Operation '{0}' did not succeed because the voucher does not exist.", operation);
            if (voucherId <= 0)
            {
                return BadRequest(message);
            }

            var voucher = await _repository.GetVoucherAsync(voucherId);
            if (voucher == null)
            {
                return BadRequest(message);
            }

            message = ValidateStateOperation(operation, voucher);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            return Ok(voucher);
        }

        private string ValidateStateOperation(string operation, VoucherViewModel summary)
        {
            string result = String.Empty;
            if (!_workflow.ValidateAction(DocumentTypeName.Voucher, summary.Document.OperationalStatus, operation))
            {
                result = String.Format(
                    Strings.InvalidDocumentOperation,
                    Entities.VoucherLongName,
                    summary.No,
                    DocumentActionName.ToLocalValue(operation),
                    DocumentStatusName.ToLocalValue(summary.Document.OperationalStatus));
            }

            return result;
        }

        private readonly IVoucherRepository _repository;
        private readonly IVoucherWorkflow _workflow;
    }
}

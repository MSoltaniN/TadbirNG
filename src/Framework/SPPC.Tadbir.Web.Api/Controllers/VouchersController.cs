using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class VouchersController : ValidatingController<VoucherViewModel>
    {
        public VouchersController(
            IVoucherRepository repository,
            IVoucherLineRepository lineRepository,
            IRelationRepository relationRepository,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _lineRepository = lineRepository;
            _relationRepository = relationRepository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Voucher; }
        }

        #region Voucher Operations

        // GET: api/vouchers
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVouchersAsync()
        {
            var vouchers = await _repository.GetVouchersAsync(GridOptions);
            Localize(vouchers.Items);
            return JsonListResult(vouchers);
        }

        // GET: api/vouchers/{voucherId:int}
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherAsync(int voucherId)
        {
            var voucher = await _repository.GetVoucherAsync(voucherId);
            Localize(voucher);
            return JsonReadResult(voucher);
        }

        // GET: api/vouchers/new
        [Route(VoucherApi.NewVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public async Task<IActionResult> GetNewVoucherAsync()
        {
            var newVoucher = await _repository.GetNewVoucherAsync();
            return Json(newVoucher);
        }

        // GET: api/vouchers/by-no
        [Route(VoucherApi.VoucherByNoUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherByNoAsync(int voucherNo)
        {
            var voucherByNo = await _repository.GetVoucherByNoAsync(voucherNo);
            return JsonReadResult(voucherByNo);
        }

        // GET: api/vouchers/range
        [Route(VoucherApi.EnvironmentItemRangeUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVoucherRangeAsync()
        {
            var range = await _repository.GetVoucherRangeInfoAsync();
            return Json(range);
        }

        // GET: api/vouchers/first
        [Route(VoucherApi.FirstVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetFirstVoucherAsync()
        {
            var first = await _repository.GetFirstVoucherAsync();
            return JsonReadResult(first);
        }

        // GET: api/vouchers/{voucherNo:min(1)}/previous
        [Route(VoucherApi.PreviousVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousVoucherAsync(int voucherNo)
        {
            var previous = await _repository.GetPreviousVoucherAsync(voucherNo);
            return JsonReadResult(previous);
        }

        // GET: api/vouchers/{voucherNo:min(1)}/next
        [Route(VoucherApi.NextVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetNextVoucherAsync(int voucherNo)
        {
            var next = await _repository.GetNextVoucherAsync(voucherNo);
            return JsonReadResult(next);
        }

        // GET: api/vouchers/last
        [Route(VoucherApi.LastVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetLastVoucherAsync()
        {
            var lastVoucher = await _repository.GetLastVoucherAsync();
            return JsonReadResult(lastVoucher);
        }

        // GET: api/vouchers/count/by-status
        [Route(VoucherApi.VoucherCountByStatusUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVouchersCountByStatusIdAsync()
        {
            int itemCount = await _repository.GetCountAsync<VoucherViewModel>(GridOptions);

            return Ok(itemCount);
        }

        // GET: api/vouchers/no-article
        [Route(VoucherApi.VoucherWithNoArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVouchersWithNoArticleAsync(DateTime from, DateTime to)
        {
            var vouchers = await _repository.GetVouchersWithNoArticleAsync(GridOptions, from, to);
            Localize(vouchers.Items);
            return JsonListResult(vouchers);
        }

        // GET: api/vouchers/unbalanced
        [Route(VoucherApi.UnbalancedVouchers)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetUnbalancedVouchersAsync(DateTime from, DateTime to)
        {
            var vouchers = await _repository.GetUnbalancedVouchersAsync(GridOptions, from, to);
            Localize(vouchers.Items);
            return JsonListResult(vouchers);
        }

        // GET: api/vouchers/miss-number
        [Route(VoucherApi.MissingVoucherNumberUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetMissingVoucherNumbersAsync(DateTime from, DateTime to)
        {
            var (voucherNumbers, itemCount) = await _repository.GetMissingVoucherNumbersAsync(GridOptions, from, to);
            SetItemCount(itemCount);
            SetRowNumbers(voucherNumbers);
            return Json(voucherNumbers);
        }

        // POST: api/vouchers
        [HttpPost]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public async Task<IActionResult> PostNewVoucherAsync([FromBody] VoucherViewModel voucher)
        {
            var result = await VoucherValidationResultAsync(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputVoucher = await _repository.SaveVoucherAsync(voucher);
            return StatusCode(StatusCodes.Status201Created, outputVoucher);
        }

        // PUT: api/vouchers/{voucherId:int}
        [HttpPut]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public async Task<IActionResult> PutModifiedVoucherAsync(
            int voucherId, [FromBody] VoucherViewModel voucher)
        {
            var result = await VoucherValidationResultAsync(voucher, voucherId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = CheckedValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (voucher.SaveCount == 0)
            {
                await _repository.SetVoucherDailyNoAsync(voucher);
            }

            var outputVoucher = await _repository.SaveVoucherAsync(voucher);
            result = (outputVoucher != null)
                ? Ok(outputVoucher)
                : NotFound() as IActionResult;
            return result;
        }

        // PUT: api/vouchers/{voucherId:int}/check
        [HttpPut]
        [Route(VoucherApi.CheckVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Check)]
        public async Task<IActionResult> PutExistingVoucherAsChecked(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, VoucherAction.Check);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusValue.Checked);
            return Ok();
        }

        // PUT: api/vouchers/{voucherId:int}/check/undo
        [HttpPut]
        [Route(VoucherApi.UndoCheckVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoCheck)]
        public async Task<IActionResult> PutExistingVoucherAsUnchecked(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, VoucherAction.UndoCheck);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusValue.Draft);
            return Ok();
        }

        // PUT: api/vouchers/{voucherId:int}/confirm
        [HttpPut]
        [Route(VoucherApi.ConfirmVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public async Task<IActionResult> PutExistingVoucherAsConfirmed(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, VoucherAction.Confirm);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherConfirmationAsync(voucherId, true);
            return Ok();
        }

        // PUT: api/vouchers/{voucherId:int}/confirm/undo
        [HttpPut]
        [Route(VoucherApi.UndoConfirmVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingVoucherAsUnconfirmed(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, VoucherAction.UndoConfirm);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherConfirmationAsync(voucherId, false);
            return Ok();
        }

        // PUT: api/vouchers/{voucherId:int}/approve
        [HttpPut]
        [Route(VoucherApi.ApproveVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Approve)]
        public async Task<IActionResult> PutExistingVoucherAsApproved(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, VoucherAction.Approve);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherApprovalAsync(voucherId, true);
            return Ok();
        }

        // PUT: api/vouchers/{voucherId:int}/approve/undo
        [HttpPut]
        [Route(VoucherApi.UndoApproveVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingVoucherAsUnapproved(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, VoucherAction.UndoApprove);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherApprovalAsync(voucherId, false);
            return Ok();
        }

        // PUT: api/vouchers/{voucherId:int}/finalize
        [HttpPut]
        [Route(VoucherApi.FinalizeVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Finalize)]
        public async Task<IActionResult> PutExistingVoucherAsFinalized(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, VoucherAction.Finalize);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusValue.Finalized);
            return Ok();
        }

        // PUT: api/vouchers/{voucherId:int}/finalize/undo
        [HttpPut]
        [Route(VoucherApi.UndoFinalizeVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoFinalize)]
        public IActionResult PutExistingVoucherAsUnfinalized(int voucherId)
        {
            // NOTE: This operation is formally ILLEGAL, so it's currently disabled.
            int id = voucherId; // Prevent unused argument warning
            return Unauthorized();
        }

        // DELETE: api/vouchers/{voucherId:int}
        [HttpDelete]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingVoucherAsync(int voucherId)
        {
            string result = await ValidateDeleteAsync(voucherId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteVoucherAsync(voucherId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/vouchers
        [HttpPut]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> PutExistingVouchersAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await ValidateGroupDeleteAsync(actionDetail.Items);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            await _repository.DeleteVouchersAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/vouchers/opening
        [Route(VoucherApi.OpeningVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueOpeningVoucherAsync()
        {
            // TODO: Perform required validation

            // Rule 1 : Current fiscal period MUST have required account collection associations
            var openingVoucher = await _repository.GetOpeningVoucherAsync();
            return Json(openingVoucher);
        }

        // GET: api/vouchers/closing-tmp
        [Route(VoucherApi.ClosingAccountsVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueClosingAccountsVoucherAsync()
        {
            // TODO: Perform required validation
            var closingAccountsVoucher = await _repository.GetClosingTempAccountsVoucherAsync();
            return Json(closingAccountsVoucher);
        }

        // GET: api/vouchers/closing
        [Route(VoucherApi.ClosingVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueClosingVoucherAsync()
        {
            // TODO: Perform required validation

            // Rule 1 : Current fiscal period MUST NOT have any unchecked vouchers

            // Rule 2 : Current fiscal period MUST have the closing temp accounts voucher
            var closingVoucher = await _repository.GetClosingVoucherAsync();
            return Json(closingVoucher);
        }

        #endregion

        #region Article Operations

        // GET: api/vouchers/{voucherId:min(1)}/articles
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticlesAsync(int voucherId)
        {
            var articles = await _lineRepository.GetArticlesAsync(voucherId, GridOptions);
            SetItemCount(articles.TotalCount);
            foreach (var article in articles.Items)
            {
                article.CurrencyName = _strings[article.CurrencyName];
            }

            return Json(articles.Items);
        }

        // GET: api/vouchers/articles/{articleId:min(1)}
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticleAsync(int articleId)
        {
            var article = await _lineRepository.GetArticleAsync(articleId);
            return JsonReadResult(article);
        }

        // GET: api/vouchers/articles/count
        [Route(VoucherApi.VoucherArticlesCountUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticlesCountAsync()
        {
            int itemsCount = await _lineRepository.GetAllArticlesCountAsync();
            return Ok(itemsCount);
        }

        // GET: api/vouchers/articles/sys-issue/{issueType}
        [Route(VoucherApi.SystemIssueArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetSystemIssueArticlesAsync(string issueType, DateTime from, DateTime to)
        {
            var articles = await _lineRepository.GetSystemIssueArticlesAsync(GridOptions, issueType, from, to);
            SetItemCount(articles.TotalCount);
            if (issueType != "invalid-acc")
            {
                SetRowNumbers(articles.Items);
            }

            return Json(articles.Items);
        }

        // POST: api/vouchers/{voucherId:min(1)}/articles
        [HttpPost]
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public async Task<IActionResult> PostNewArticleAsync(
            int voucherId, [FromBody] VoucherLineViewModel article)
        {
            int id = voucherId; // Prevent unused argument warning
            var result = VoucherLineValidationResultAsync(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await FullAccountValidationResult(article.FullAccount, _relationRepository);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputLine = await _lineRepository.SaveArticleAsync(article);
            return StatusCode(StatusCodes.Status201Created, outputLine);
        }

        // PUT: api/vouchers/articles/{articleId:min(1)}
        [HttpPut]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public async Task<IActionResult> PutModifiedArticleAsync(
            int articleId, [FromBody] VoucherLineViewModel article)
        {
            var result = VoucherLineValidationResultAsync(article, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputLine = await _lineRepository.SaveArticleAsync(article);
            result = (outputLine != null)
                ? Ok(outputLine)
                : NotFound() as IActionResult;
            return result;
        }

        // PUT: api/vouchers/articles/{articleId:min(1)}/mark
        [HttpPut]
        [Route(VoucherApi.VoucherArticleMarkUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.Mark)]
        public async Task<IActionResult> PutModifiedArticleMarkAsync(
            int articleId, [FromBody] VoucherLineMarkViewModel mark)
        {
            var result = BasicValidationResult(mark, AppStrings.VoucherLineMark, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _lineRepository.SaveArticleMarkAsync(mark);
            return Ok();
        }

        // DELETE: api/vouchers/articles/{articleId:min(1)}
        [HttpDelete]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingArticleAsync(int articleId)
        {
            string result = await ValidateLineDeleteAsync(articleId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _lineRepository.DeleteArticleAsync(articleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/vouchers/articles
        [HttpPut]
        [Route(VoucherApi.AllVoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> PutExistingVoucherLinesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await ValidateGroupLineDeleteAsync(actionDetail.Items);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            await _lineRepository.DeleteArticlesAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        protected override async Task<string> ValidateDeleteAsync(int voucherId)
        {
            string message = String.Empty;
            var voucher = await _repository.GetVoucherAsync(voucherId);
            if (voucher == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Voucher), voucherId);
            }

            var result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult branchError)
            {
                message = branchError.Value.ToString();
            }

            result = CheckedValidationResult(voucher);
            if (result is BadRequestObjectResult statusError)
            {
                message = statusError.Value.ToString();
            }

            return message;
        }

        private static bool IsUnbalancedVoucher(VoucherViewModel voucher)
        {
            return (voucher.DebitSum == 0.0M && voucher.CreditSum == 0.0M)
                || Math.Abs(voucher.DebitSum - voucher.CreditSum) >= 1.0M;
        }

        private static bool IsVoucherMainAction(string action)
        {
            return action == VoucherAction.Check
                || action == VoucherAction.Confirm
                || action == VoucherAction.Approve
                || action == VoucherAction.Finalize;
        }

        private IActionResult BasicValidationResult<TModel>(TModel model, string modelType, int modelId = 0)
        {
            if (model == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, modelType));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = (int)Reflector.GetProperty(model, "Id");
            if (modelId != id)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, modelType));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherValidationResultAsync(VoucherViewModel voucher, int voucherId = 0)
        {
            var result = BasicValidationResult(voucher, AppStrings.Voucher, voucherId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateVoucherNoAsync(voucher))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.VoucherNo));
            }

            if (await _repository.IsDuplicateVoucherDailyNoAsync(voucher))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.DailyNo));
            }

            var fiscalPeriod = await _repository.GetVoucherFiscalPeriodAsync(voucher);
            if (fiscalPeriod == null
                || voucher.Date < fiscalPeriod.StartDate
                || voucher.Date > fiscalPeriod.EndDate)
            {
                return BadRequest(_strings.Format(AppStrings.OutOfFiscalPeriodDate));
            }

            return Ok();
        }

        private IActionResult VoucherLineValidationResultAsync(
            VoucherLineViewModel article, int articleId = 0)
        {
            var result = BasicValidationResult(article, AppStrings.VoucherLine, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(_strings.Format(AppStrings.DebitAndCreditNotAllowed));
            }

            return Ok();
        }

        private async Task<string> ValidateLineDeleteAsync(int articleId)
        {
            string message = String.Empty;
            var voucherLine = await _lineRepository.GetArticleAsync(articleId);
            if (voucherLine == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.VoucherLine), articleId);
            }
            else
            {
                var result = BranchValidationResult(voucherLine);
                if (result is BadRequestObjectResult errorResult)
                {
                    message = errorResult.Value.ToString();
                }
            }

            return message;
        }

        private IActionResult CheckedValidationResult(VoucherViewModel voucher)
        {
            if (voucher.StatusId != (int)DocumentStatusValue.Draft)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifyCheckedDocument, AppStrings.Voucher));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherActionValidationResultAsync(int voucherId, string action)
        {
            var error = await _repository.ValidateVoucherActionAsync(voucherId, action);
            if (!String.IsNullOrEmpty(error))
            {
                return BadRequest(_strings.Format(AppStrings.InvalidVoucherAction, action, error));
            }

            if (IsVoucherMainAction(action))
            {
                int lineCount = await _lineRepository.GetArticleCountAsync<VoucherLineViewModel>(voucherId);
                if (lineCount == 0)
                {
                    return BadRequest(_strings.Format(AppStrings.InvalidEmptyVoucherAction, action));
                }
            }

            return Ok();
        }

        private async Task<IEnumerable<string>> ValidateGroupLineDeleteAsync(IEnumerable<int> items)
        {
            var messages = new List<string>();
            foreach (int item in items)
            {
                messages.Add(await ValidateLineDeleteAsync(item));
            }

            return messages
                .Where(msg => !String.IsNullOrEmpty(msg));
        }

        private void Localize(IEnumerable<VoucherViewModel> vouchers)
        {
            foreach (var voucher in vouchers)
            {
                Localize(voucher);
            }
        }

        private void Localize(VoucherViewModel voucher)
        {
            if (voucher != null)
            {
                voucher.StatusName = _strings.Format(voucher.StatusName);
            }
        }

        private readonly IVoucherRepository _repository;
        private readonly IVoucherLineRepository _lineRepository;
        private readonly IRelationRepository _relationRepository;
    }
}

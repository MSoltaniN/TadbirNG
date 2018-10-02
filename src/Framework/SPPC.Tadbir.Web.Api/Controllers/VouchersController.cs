using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class VouchersController : ValidatingController<VoucherViewModel>
    {
        public VouchersController(
            IVoucherRepository repository,
            IVoucherLineRepository lineRepository,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _lineRepository = lineRepository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Voucher; }
        }

        #region Voucher CRUD Operations

        // GET: api/vouchers
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVouchersAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            int itemCount = await _repository.GetCountAsync(GridOptions);
            SetItemCount(itemCount);
            var vouchers = await _repository.GetVouchersAsync(GridOptions);
            return Json(vouchers);
        }

        // GET: api/vouchers/{voucherId:int}
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherAsync(int voucherId)
        {
            var voucher = await _repository.GetVoucherAsync(voucherId);
            return JsonReadResult(voucher);
        }

        // GET: api/vouchers/metadata
        [Route(VoucherApi.VoucherMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherMetadataAsync()
        {
            var metadata = await _repository.GetVoucherMetadataAsync();
            return JsonReadResult(metadata);
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

            _repository.SetCurrentContext(SecurityContext.User);
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

            _repository.SetCurrentContext(SecurityContext.User);
            var outputVoucher = await _repository.SaveVoucherAsync(voucher);
            result = (outputVoucher != null)
                ? Ok(outputVoucher)
                : NotFound() as IActionResult;
            return result;
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

            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.DeleteVoucherAsync(voucherId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #region Article CRUD Operations

        // GET: api/vouchers/{voucherId:min(1)}/articles
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticlesAsync(int voucherId)
        {
            _lineRepository.SetCurrentContext(SecurityContext.User);
            int itemCount = await _lineRepository.GetArticleCountAsync(SecurityContext.User, voucherId, GridOptions);
            SetItemCount(itemCount);
            var articles = await _lineRepository.GetArticlesAsync(SecurityContext.User, voucherId, GridOptions);
            return Json(articles);
        }

        // GET: api/vouchers/articles/{articleId:min(1)}
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticleAsync(int articleId)
        {
            var article = await _lineRepository.GetArticleAsync(articleId);
            return JsonReadResult(article);
        }

        // GET: api/vouchers/articles/metadata
        [Route(VoucherApi.VoucherArticleMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherArticleMetadataAsync()
        {
            var metadata = await _lineRepository.GetVoucherLineMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/vouchers/{voucherId:min(1)}/articles
        [HttpPost]
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public async Task<IActionResult> PostNewArticleAsync(
            int voucherId, [FromBody] VoucherLineViewModel article)
        {
            var result = await VoucherLineValidationResultAsync(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _lineRepository.SetCurrentContext(SecurityContext.User);
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
            var result = await VoucherLineValidationResultAsync(article, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _lineRepository.SetCurrentContext(SecurityContext.User);
            var outputLine = await _lineRepository.SaveArticleAsync(article);
            result = (outputLine != null)
                ? Ok(outputLine)
                : NotFound() as IActionResult;
            return result;
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

            _lineRepository.SetCurrentContext(SecurityContext.User);
            await _lineRepository.DeleteArticleAsync(articleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

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

            var fiscalPeriod = await _repository.GetVoucherFiscalPeriodAsync(voucher);
            if (fiscalPeriod == null
                || voucher.Date < fiscalPeriod.StartDate
                || voucher.Date > fiscalPeriod.EndDate)
            {
                return BadRequest(_strings.Format(AppStrings.OutOfFiscalPeriodDate));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherLineValidationResultAsync(
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

            var account = await _lineRepository.GetArticleAccountAsync(article.FullAccount.AccountId.Value);
            if (account.ChildCount > 0)
            {
                string accountInfo = String.Format("{0} ({1})", account.Name, account.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.Account), accountInfo);
                return BadRequest(message);
            }

            var detailAccount = await _lineRepository.GetArticleDetailAccountAsync(article.FullAccount.DetailId ?? 0);
            if (detailAccount != null && detailAccount.ChildCount > 0)
            {
                string detailInfo = String.Format("{0} ({1})", detailAccount.Name, detailAccount.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.DetailAccount), detailInfo);
                return BadRequest(message);
            }

            var costCenter = await _lineRepository.GetArticleCostCenterAsync(article.FullAccount.CostCenterId ?? 0);
            if (costCenter != null && costCenter.ChildCount > 0)
            {
                string costCenterInfo = String.Format("{0} ({1})", costCenter.Name, costCenter.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.CostCenter), costCenterInfo);
                return BadRequest(message);
            }

            var project = await _lineRepository.GetArticleProjectAsync(article.FullAccount.ProjectId ?? 0);
            if (project != null && project.ChildCount > 0)
            {
                string projectInfo = String.Format("{0} ({1})", project.Name, project.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.Project), projectInfo);
                return BadRequest(message);
            }

            return Ok();
        }

        private async Task<string> ValidateDeleteAsync(int voucherId)
        {
            string message = String.Empty;
            var voucher = await _repository.GetVoucherAsync(voucherId);
            if (voucher == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Voucher), voucherId);
            }

            var result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult errorResult)
            {
                message = errorResult.Value.ToString();
            }

            return message;
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

            var result = BranchValidationResult(voucherLine);
            if (result is BadRequestObjectResult errorResult)
            {
                message = errorResult.Value.ToString();
            }

            return message;
        }

        private readonly IVoucherRepository _repository;
        private readonly IVoucherLineRepository _lineRepository;
    }
}

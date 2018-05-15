using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class VouchersController : ApiControllerBase<VoucherViewModel>
    {
        public VouchersController(
            IVoucherRepository repository,
            ISecurityContextManager contextManager,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            Verify.ArgumentNotNull(contextManager, "contextManager");
            _repository = repository;
            _contextManager = contextManager;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Voucher; }
        }

        #region Voucher CRUD Operations

        // GET: api/vouchers/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(VoucherApi.FiscalPeriodBranchVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVouchersAsync(int fpId, int branchId)
        {
            var gridOptions = GetGridOptions();
            int itemCount = await _repository.GetCountAsync(fpId, branchId, gridOptions);
            SetItemCount(itemCount);
            var vouchers = await _repository.GetVouchersAsync(fpId, branchId, gridOptions);
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
        [Route(VoucherApi.VouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public async Task<IActionResult> PostNewVoucherAsync([FromBody] VoucherViewModel voucher)
        {
            var result = await VoucherValidationResultAsync(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            SetDocument(voucher);
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

            SetDocument(voucher);
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
            var gridOptions = GetGridOptions();
            int itemCount = await _repository.GetArticleCountAsync(voucherId, gridOptions);
            SetItemCount(itemCount);
            var articles = await _repository.GetArticlesAsync(voucherId, gridOptions);
            return Json(articles);
        }

        // GET: api/vouchers/articles/{articleId:min(1)}
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticleAsync(int articleId)
        {
            var article = await _repository.GetArticleAsync(articleId);
            return JsonReadResult(article);
        }

        // GET: api/vouchers/articles/metadata
        [Route(VoucherApi.VoucherArticleMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherArticleMetadataAsync()
        {
            var metadata = await _repository.GetVoucherLineMetadataAsync();
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

            var outputLine = await _repository.SaveArticleAsync(article);
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

            var outputLine = await _repository.SaveArticleAsync(article);
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
            var article = _repository.GetArticle(articleId);
            if (article == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.VoucherLine));
            }

            await _repository.DeleteArticleAsync(articleId);
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

            var account = await _repository.GetArticleAccountAsync(article.FullAccount.AccountId.Value);
            if (account.ChildCount > 0)
            {
                string accountInfo = String.Format("{0} ({1})", account.Name, account.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.Account), accountInfo);
                return BadRequest(message);
            }

            var detailAccount = await _repository.GetArticleDetailAccountAsync(article.FullAccount.DetailId ?? 0);
            if (detailAccount != null && detailAccount.ChildCount > 0)
            {
                string detailInfo = String.Format("{0} ({1})", detailAccount.Name, detailAccount.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.DetailAccount), detailInfo);
                return BadRequest(message);
            }

            var costCenter = await _repository.GetArticleCostCenterAsync(article.FullAccount.CostCenterId ?? 0);
            if (costCenter != null && costCenter.ChildCount > 0)
            {
                string costCenterInfo = String.Format("{0} ({1})", costCenter.Name, costCenter.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.CostCenter), costCenterInfo);
                return BadRequest(message);
            }

            var project = await _repository.GetArticleProjectAsync(article.FullAccount.ProjectId ?? 0);
            if (project != null && project.ChildCount > 0)
            {
                string projectInfo = String.Format("{0} ({1})", project.Name, project.FullCode);
                string message = String.Format(
                    _strings.Format(AppStrings.CannotUseNonLeafItem), _strings.Format(AppStrings.Project), projectInfo);
                return BadRequest(message);
            }

            return Ok();
        }

        private void SetDocument(VoucherViewModel voucher)
        {
            if (voucher.Document.Id == 0)
            {
                var document = new DocumentViewModel()
                {
                    OperationalStatus = DocumentStatusName.Created,
                    StatusId = (int)DocumentStatusId.Draft,
                    TypeId = (int)DocumentTypeId.Voucher,
                    EntityNo = voucher.No
                };
                document.Actions.Add(new DocumentActionViewModel()
                {
                    CreatedById = _contextManager.CurrentContext.User.Id,
                    ModifiedById = _contextManager.CurrentContext.User.Id
                });
                voucher.Document = document;
            }
            else
            {
                var mainAction = voucher.Document.Actions.First();
                mainAction.ModifiedById = _contextManager.CurrentContext.User.Id;
            }
        }

        private IVoucherRepository _repository;
        private ISecurityContextManager _contextManager;
    }
}

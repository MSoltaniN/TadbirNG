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
    public class TransactionsController : ApiControllerBase<TransactionViewModel>
    {
        public TransactionsController(
            ITransactionRepository repository,
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

        #region Transaction CRUD Operations

        // GET: api/transactions/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(TransactionApi.FiscalPeriodBranchTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetTransactionsAsync(int fpId, int branchId)
        {
            var gridOptions = GetGridOptions();
            int itemCount = await _repository.GetCountAsync(fpId, branchId, gridOptions);
            SetItemCount(itemCount);
            var transactions = await _repository.GetTransactionsAsync(fpId, branchId, gridOptions);
            return Json(transactions);
        }

        // GET: api/transactions/{transactionId:int}
        [Route(TransactionApi.TransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetTransactionAsync(int transactionId)
        {
            var transaction = await _repository.GetTransactionAsync(transactionId);
            return JsonReadResult(transaction);
        }

        // GET: api/transactions/metadata
        [Route(TransactionApi.TransactionMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetTransactionMetadataAsync()
        {
            var metadata = await _repository.GetTransactionMetadataAsync();
            return JsonReadResult(metadata);
        }

        // GET: api/transactions/fp/{fpId:min(1)}/branch/{branchId:min(1)}/count
        [Route(TransactionApi.FiscalPeriodBranchItemCountUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetItemCountAsync(int fpId, int branchId)
        {
            var gridOptions = GetGridOptions();
            int count = await _repository.GetCountAsync(fpId, branchId, gridOptions);
            return Json(count);
        }

        // POST: api/transactions
        [HttpPost]
        [Route(TransactionApi.TransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public async Task<IActionResult> PostNewTransactionAsync([FromBody] TransactionViewModel transaction)
        {
            var result = BasicValidationResult(transaction, AppStrings.Voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!await _repository.IsValidTransactionAsync(transaction))
            {
                return BadRequest(_strings.Format(AppStrings.OutOfFiscalPeriodDate));
            }

            SetDocument(transaction);
            var outputTransaction = await _repository.SaveTransactionAsync(transaction);
            return StatusCode(StatusCodes.Status201Created, outputTransaction);
        }

        // PUT: api/transactions/{transactionId:int}
        [HttpPut]
        [Route(TransactionApi.TransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public async Task<IActionResult> PutModifiedTransactionAsync(
            int transactionId, [FromBody] TransactionViewModel transaction)
        {
            var result = BasicValidationResult(transaction, AppStrings.Voucher, transactionId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!await _repository.IsValidTransactionAsync(transaction))
            {
                return BadRequest(_strings.Format(AppStrings.OutOfFiscalPeriodDate));
            }

            SetDocument(transaction);
            var outputTransaction = await _repository.SaveTransactionAsync(transaction);
            result = (outputTransaction != null)
                ? Ok(outputTransaction)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/transactions/{transactionId:int}
        [HttpDelete]
        [Route(TransactionApi.TransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingTransactionAsync(int transactionId)
        {
            await _repository.DeleteTransactionAsync(transactionId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #region Article CRUD Operations

        // GET: api/transactions/{transactionId:min(1)}/articles
        [Route(TransactionApi.TransactionArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetArticlesAsync(int transactionId)
        {
            var gridOptions = GetGridOptions();
            int itemCount = await _repository.GetArticleCountAsync(transactionId, gridOptions);
            SetItemCount(itemCount);
            var articles = await _repository.GetArticlesAsync(transactionId, gridOptions);
            return Json(articles);
        }

        // GET: api/transactions/articles/{articleId:min(1)}
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetArticleAsync(int articleId)
        {
            var article = await _repository.GetArticleAsync(articleId);
            return JsonReadResult(article);
        }

        // GET: api/transactions/articles/{articleId:min(1)}/details
        [Route(TransactionApi.TransactionArticleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetArticleDetailsAsync(int articleId)
        {
            var article = await _repository.GetArticleDetailsAsync(articleId);
            return JsonReadResult(article);
        }

        // GET: api/transactions/articles/metadata
        [Route(TransactionApi.TransactionArticleMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetTransactionArticleMetadataAsync()
        {
            var metadata = await _repository.GetTransactionLineMetadataAsync();
            return JsonReadResult(metadata);
        }

        // GET: api/transactions/{transactionId:min(1)}/articles/count
        [Route(TransactionApi.TransactionArticleCountUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetTransactionArticleCountAsync(int transactionId)
        {
            var gridOptions = GetGridOptions();
            int count = await _repository.GetArticleCountAsync(transactionId, gridOptions);
            return Json(count);
        }

        // POST: api/transactions/{transactionId:min(1)}/articles
        [HttpPost]
        [Route(TransactionApi.TransactionArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public async Task<IActionResult> PostNewArticleAsync(
            int transactionId, [FromBody] TransactionLineViewModel article)
        {
            var result = BasicValidationResult(article, AppStrings.VoucherLine);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (article.TransactionId != transactionId)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.VoucherLine));
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(_strings.Format(AppStrings.DebitAndCreditNotAllowed));
            }

            var outputLine = await _repository.SaveArticleAsync(article);
            return StatusCode(StatusCodes.Status201Created, outputLine);
        }

        // PUT: api/transactions/articles/{articleId:min(1)}
        [HttpPut]
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public async Task<IActionResult> PutModifiedArticleAsync(
            int articleId, [FromBody] TransactionLineViewModel article)
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

            var outputLine = await _repository.SaveArticleAsync(article);
            result = (outputLine != null)
                ? Ok(outputLine)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/transactions/articles/{articleId:min(1)}
        [HttpDelete]
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
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

        private string ValidateGroupStateOperation(string operation, IEnumerable<TransactionSummaryViewModel> summaries)
        {
            var messages = summaries
                .Where(summary => summary != null)
                .Select(summary => ValidateStateOperation(operation, summary))
                .Where(msg => !String.IsNullOrEmpty(msg))
                .ToArray();
            return String.Join(Environment.NewLine, messages);
        }

        private string ValidateStateOperation(string operation, TransactionSummaryViewModel summary)
        {
            return String.Empty;
        }

        private void SetDocument(TransactionViewModel transaction)
        {
            if (transaction.Document.Id == 0)
            {
                var document = new DocumentViewModel()
                {
                    OperationalStatus = DocumentStatusName.Created,
                    StatusId = (int)DocumentStatusId.Draft,
                    TypeId = (int)DocumentTypeId.Transaction,
                    EntityNo = transaction.No
                };
                document.Actions.Add(new DocumentActionViewModel()
                {
                    CreatedById = _contextManager.CurrentContext.User.Id,
                    ModifiedById = _contextManager.CurrentContext.User.Id
                });
                transaction.Document = document;
            }
            else
            {
                var mainAction = transaction.Document.Actions.First();
                mainAction.ModifiedById = _contextManager.CurrentContext.User.Id;
            }
        }

        private ITransactionRepository _repository;
        private ISecurityContextManager _contextManager;
    }
}

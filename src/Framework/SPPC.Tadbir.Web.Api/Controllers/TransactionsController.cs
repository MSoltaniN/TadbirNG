using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class TransactionsController : Controller
    {
        public TransactionsController(ITransactionRepository repository, ISecurityContextManager contextManager)
        {
            Verify.ArgumentNotNull(contextManager, "contextManager");
            _repository = repository;
            _contextManager = contextManager;
        }

        #region Transaction CRUD Operations

        #region Asynchronous Methods

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
            var result = BasicValidationResult(transaction, Entities.Transaction);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!await _repository.IsValidTransactionAsync(transaction))
            {
                return BadRequest(Strings.OutOfFiscalPeriodDate);
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
            var result = BasicValidationResult(transaction, Entities.Transaction, transactionId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!await _repository.IsValidTransactionAsync(transaction))
            {
                return BadRequest(Strings.OutOfFiscalPeriodDate);
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

        #region Synchronous Methods (May be removed in the future)

        // GET: api/transactions/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync
        [Route(TransactionApi.FiscalPeriodBranchTransactionsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetTransactions(int fpId, int branchId)
        {
            var gridOptions = GetGridOptions();
            int itemCount = _repository.GetCount(fpId, branchId, gridOptions);
            SetItemCount(itemCount);
            var transactions = _repository.GetTransactions(fpId, branchId, gridOptions);
            return Json(transactions);
        }

        // GET: api/transactions/{transactionId:int}/sync
        [Route(TransactionApi.TransactionSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetTransaction(int transactionId)
        {
            var transaction = _repository.GetTransaction(transactionId);
            return JsonReadResult(transaction);
        }

        // POST: api/transactions/sync
        [HttpPost]
        [Route(TransactionApi.TransactionsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public IActionResult PostNewTransaction([FromBody] TransactionViewModel transaction)
        {
            var result = BasicValidationResult(transaction, Entities.Transaction);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!_repository.IsValidTransaction(transaction))
            {
                return BadRequest(Strings.OutOfFiscalPeriodDate);
            }

            SetDocument(transaction);
            _repository.SaveTransaction(transaction);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/transactions/{transactionId:min(1)}/sync
        [HttpPut]
        [Route(TransactionApi.TransactionSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IActionResult PutModifiedTransaction(int transactionId, [FromBody] TransactionViewModel transaction)
        {
            var result = BasicValidationResult(transaction, Entities.Transaction, transactionId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!_repository.IsValidTransaction(transaction))
            {
                return BadRequest(Strings.OutOfFiscalPeriodDate);
            }

            SetDocument(transaction);
            _repository.SaveTransaction(transaction);
            return Ok();
        }

        // DELETE: api/transactions/{transactionId:int}/sync
        [HttpDelete]
        [Route(TransactionApi.TransactionSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
        public IActionResult DeleteExistingTransaction(int transactionId)
        {
            _repository.DeleteTransaction(transactionId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #endregion

        #region Article CRUD Operations

        #region Asynchronous Methods

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
            var result = BasicValidationResult(article, Entities.Article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (article.TransactionId != transactionId)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, Entities.Article);
                return BadRequest(message);
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(Strings.DebitAndCreditNotAllowed);
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
            var result = BasicValidationResult(article, Entities.Article, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(Strings.DebitAndCreditNotAllowed);
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
                var message = String.Format(ValidationMessages.ItemNotFound, Entities.Article);
                return BadRequest(message);
            }

            await _repository.DeleteArticleAsync(articleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        // GET: api/transactions/{transactionId:min(1)}/articles/sync
        [Route(TransactionApi.TransactionArticlesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetArticles(int transactionId)
        {
            var gridOptions = GetGridOptions();
            int itemCount = _repository.GetArticleCount(transactionId, gridOptions);
            SetItemCount(itemCount);
            var articles = _repository.GetArticles(transactionId, gridOptions);
            return Json(articles);
        }

        // GET: api/transactions/articles/{articleId:min(1)}/sync
        [Route(TransactionApi.TransactionArticleSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetArticle(int articleId)
        {
            var article = _repository.GetArticle(articleId);
            return JsonReadResult(article);
        }

        // GET: api/transactions/articles/{articleId:min(1)}/details/sync
        [Route(TransactionApi.TransactionArticleDetailsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetArticleDetails(int articleId)
        {
            var article = _repository.GetArticleDetails(articleId);
            return JsonReadResult(article);
        }

        // POST: api/transactions/{transactionId:min(1)}/articles/sync
        [HttpPost]
        [Route(TransactionApi.TransactionArticlesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IActionResult PostNewArticle(int transactionId, [FromBody] TransactionLineViewModel article)
        {
            var result = BasicValidationResult(article, Entities.Article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (article.TransactionId != transactionId)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, Entities.Article);
                return BadRequest(message);
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(Strings.DebitAndCreditNotAllowed);
            }

            _repository.SaveArticle(article);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/transactions/articles/{articleId:min(1)}/sync
        [HttpPut]
        [Route(TransactionApi.TransactionArticleSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IActionResult PutModifiedArticle(int articleId, [FromBody] TransactionLineViewModel article)
        {
            var result = BasicValidationResult(article, Entities.Article, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(Strings.DebitAndCreditNotAllowed);
            }

            _repository.SaveArticle(article);
            return Ok();
        }

        // DELETE: api/transactions/articles/{articleId:int}/sync
        [HttpDelete]
        [Route(TransactionApi.TransactionArticleSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
        public IActionResult DeleteExistingArticle(int articleId)
        {
            if (articleId <= 0)
            {
                return BadRequest("Could not delete article because it does not exist.");
            }

            var article = _repository.GetArticle(articleId);
            if (article == null)
            {
                return BadRequest("Could not delete article because it does not exist.");
            }

            _repository.DeleteArticle(articleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #endregion

        private IActionResult JsonReadResult<TData>(TData data)
        {
            var result = (data != null)
                ? Json(data)
                : NotFound() as IActionResult;

            return result;
        }

        private IActionResult BasicValidationResult<TModel>(TModel model, string modelType, int modelId = 0)
        {
            if (model == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, modelType);
                return BadRequest(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = (int)Reflector.GetProperty(model, "Id");
            if (modelId != id)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, modelType);
                return BadRequest(message);
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

        private GridOptions GetGridOptions()
        {
            var options = Request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return null;
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return Framework.Helpers.Json.To<GridOptions>(json);
        }

        private void SetItemCount(int count)
        {
            Response.Headers.Add(AppConstants.TotalCountHeaderName, count.ToString());
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

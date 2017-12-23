using System;
using System.Collections.Generic;
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
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Workflow;
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

        // GET: api/transactions/fp/{fpId:int}/branch/{branchId:int}
        [Route(TransactionApi.FiscalPeriodBranchTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetTransactions(int fpId, int branchId)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var transactions = _repository.GetTransactions(fpId, branchId);
            return Json(transactions);
        }

        // GET: api/transactions/{transactionId:int}/details
        [Route(TransactionApi.TransactionDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetTransactionDetail(int transactionId)
        {
            if (transactionId <= 0)
            {
                return NotFound();
            }

            var transaction = _repository.GetTransactionDetail(transactionId);
            var result = (transaction != null)
                ? Json(transaction)
                : NotFound() as IActionResult;

            return result;
        }

        // POST: api/transactions
        [Route(TransactionApi.TransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public IActionResult PostNewTransaction([FromBody] TransactionViewModel transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Could not post new transaction because a 'null' value was provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.IsValidTransaction(transaction))
            {
                return BadRequest(Strings.OutOfFiscalPeriodDate);
            }

            SetDocument(transaction);
            _repository.SaveTransaction(transaction);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/transactions/{transactionId:int}
        [Route(TransactionApi.TransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IActionResult PutModifiedTransaction(int transactionId, [FromBody] TransactionViewModel transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Could not put modified transaction because a 'null' value was provided.");
            }

            if (transactionId <= 0 || transaction.Id <= 0)
            {
                return BadRequest("Could not put modified transaction because original transaction does not exist.");
            }

            if (transactionId != transaction.Id)
            {
                return BadRequest("Could not put modified transaction because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.IsValidTransaction(transaction))
            {
                return BadRequest(Strings.OutOfFiscalPeriodDate);
            }

            SetDocument(transaction);
            _repository.SaveTransaction(transaction);
            return Ok();
        }

        // DELETE: api/transactions/{transactionId:int}
        [Route(TransactionApi.TransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
        public IActionResult DeleteExistingTransaction(int transactionId)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not delete transaction because it does not exist.");
            }

            bool deleted = _repository.DeleteTransaction(transactionId);
            var result = deleted
                ? StatusCode(StatusCodes.Status204NoContent)
                : BadRequest("Could not delete transaction because it does not exist.") as IActionResult;
            return result;
        }

        #endregion

        #region Article CRUD Operations

        // GET: api/transactions/articles/{articleId:int}
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetArticle(int articleId)
        {
            if (articleId <= 0)
            {
                return NotFound();
            }

            var article = _repository.GetArticle(articleId);
            var result = (article != null)
                ? Json(article)
                : NotFound() as IActionResult;
            return result;
        }

        // GET: api/transactions/articles/{articleId:int}/details
        [Route(TransactionApi.TransactionArticleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetArticleDetails(int articleId)
        {
            if (articleId <= 0)
            {
                return NotFound();
            }

            var article = _repository.GetArticleDetails(articleId);
            var result = (article != null)
                ? Json(article)
                : NotFound() as IActionResult;
            return result;
        }

        // POST: api/transactions/{transactionId:int}/articles
        [Route(TransactionApi.TransactionArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IActionResult PostNewArticle(int transactionId, [FromBody] TransactionLineViewModel article)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not post new article because the parent transaction could not be found.");
            }

            if (article == null)
            {
                return BadRequest("Could not post new article because a 'null' value was provided.");
            }

            if (article.TransactionId != transactionId)
            {
                return BadRequest("Could not post new article because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(Strings.DebitAndCreditNotAllowed);
            }

            _repository.SaveArticle(article);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/transactions/articles/{articleId:int}
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IActionResult PutModifiedArticle(int articleId, [FromBody] TransactionLineViewModel article)
        {
            if (article == null)
            {
                return BadRequest("Could not put modified article because a 'null' value was provided.");
            }

            if (articleId <= 0 || article.Id <= 0)
            {
                return BadRequest("Could not put modified article because original transaction does not exist.");
            }

            if (articleId != article.Id)
            {
                return BadRequest("Could not put modified article because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(Strings.DebitAndCreditNotAllowed);
            }

            _repository.SaveArticle(article);
            return Ok();
        }

        // DELETE: api/transactions/articles/{articleId:int}
        [Route(TransactionApi.TransactionArticleUrl)]
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

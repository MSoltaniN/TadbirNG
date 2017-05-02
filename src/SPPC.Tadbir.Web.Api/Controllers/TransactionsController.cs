using System;
using System.Net;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.AppStart;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Workflow;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class TransactionsController : ApiController
    {
        public TransactionsController(
            ITransactionRepository repository, ITransactionWorkflow workflow, ISecurityContextManager contextManager)
        {
            Verify.ArgumentNotNull(workflow, "workflow");
            _repository = repository;
            _workflow = workflow;
            _workflow.TypeContainer = UnityConfig.GetConfiguredContainer();
            _workflow.ContextManager = contextManager;
        }

        #region Transaction CRUD Operations

        // GET: api/transactions/fp/{fpId:int}/branch/{branchId:int}
        [Route(TransactionApi.FiscalPeriodBranchTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IHttpActionResult GetTransactions(int fpId, int branchId)
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
        public IHttpActionResult GetTransactionDetail(int transactionId)
        {
            if (transactionId <= 0)
            {
                return NotFound();
            }

            var transaction = _repository.GetTransactionDetail(transactionId);
            var result = (transaction != null)
                ? Json(transaction)
                : NotFound() as IHttpActionResult;

            return result;
        }

        // POST: api/transactions
        [Route(TransactionApi.TransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public IHttpActionResult PostNewTransaction([FromBody] TransactionViewModel transaction)
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
                return BadRequest(Transactions.InvalidDate);
            }

            _repository.SaveTransaction(transaction);
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/transactions/{transactionId:int}
        [Route(TransactionApi.TransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IHttpActionResult PutModifiedTransaction(int transactionId, [FromBody] TransactionViewModel transaction)
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
                return BadRequest(Transactions.InvalidDate);
            }

            _repository.SaveTransaction(transaction);
            return Ok();
        }

        // DELETE: api/transactions/{transactionId:int}
        [Route(TransactionApi.TransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
        public IHttpActionResult DeleteExistingTransaction(int transactionId)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not delete transaction because it does not exist.");
            }

            bool deleted = _repository.DeleteTransaction(transactionId);
            var result = deleted
                ? StatusCode(HttpStatusCode.NoContent)
                : BadRequest("Could not delete transaction because it does not exist.") as IHttpActionResult;
            return result;
        }

        #endregion

        #region Article CRUD Operations

        // GET: api/transactions/articles/{articleId:int}
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IHttpActionResult GetArticle(int articleId)
        {
            if (articleId <= 0)
            {
                return NotFound();
            }

            var article = _repository.GetArticle(articleId);
            var result = (article != null)
                ? Json(article)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // GET: api/transactions/articles/{articleId:int}/details
        [Route(TransactionApi.TransactionArticleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IHttpActionResult GetArticleDetails(int articleId)
        {
            if (articleId <= 0)
            {
                return NotFound();
            }

            var article = _repository.GetArticleDetails(articleId);
            var result = (article != null)
                ? Json(article)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // POST: api/transactions/{transactionId:int}/articles
        [Route(TransactionApi.TransactionArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IHttpActionResult PostNewArticle(int transactionId, [FromBody] TransactionLineViewModel article)
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
                return BadRequest(TransactionLines.DebitAndCreditNotAllowed);
            }

            _repository.SaveArticle(article);
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/transactions/articles/{articleId:int}
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IHttpActionResult PutModifiedArticle(int articleId, [FromBody] TransactionLineViewModel article)
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
                return BadRequest(TransactionLines.DebitAndCreditNotAllowed);
            }

            _repository.SaveArticle(article);
            return Ok();
        }

        // DELETE: api/transactions/articles/{articleId:int}
        [Route(TransactionApi.TransactionArticleUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public IHttpActionResult DeleteExistingArticle(int articleId)
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
            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Transaction Workflow Operations

        // PUT: api/transactions/{transactionId:int}/prepare
        [Route(TransactionApi.PrepareTransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Prepare)]
        public IHttpActionResult PutTransactionAsPrepared(int transactionId)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not put transaction as Prepared because transaction does not exist.");
            }

            _workflow.Prepare(transactionId);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/review
        [Route(TransactionApi.ReviewTransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Review)]
        public IHttpActionResult PutTransactionAsReviewed(int transactionId)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not put transaction as Reviewed because transaction does not exist.");
            }

            _workflow.Review(transactionId);
            return Ok();
        }

        #endregion

        private ITransactionRepository _repository;
        private ITransactionWorkflow _workflow;
    }
}

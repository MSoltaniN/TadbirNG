using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.Practices.Unity;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Workflow;
using SPPC.Tadbir.Web.Api.AppStart;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Workflow;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class TransactionsController : ApiController
    {
        public TransactionsController(ITransactionRepository repository, IWorkflowTracker tracker,
            ISecurityContextManager contextManager)
        {
            _repository = repository;
            _tracker = tracker;
            _contextManager = contextManager;
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
                return BadRequest(Strings.OutOfFiscalPeriodDate);
            }

            SetDocument(transaction);
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
                return BadRequest(Strings.OutOfFiscalPeriodDate);
            }

            SetDocument(transaction);
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
                return BadRequest(Strings.DebitAndCreditNotAllowed);
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
                return BadRequest(Strings.DebitAndCreditNotAllowed);
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
        public IHttpActionResult PutTransactionAsPrepared(int transactionId, [FromBody] ActionDetailViewModel detail)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not put transaction as Prepared because transaction does not exist.");
            }

            string message = ValidateStateOperation(DocumentAction.Prepare, transactionId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            var paraph = detail?.Paraph;
            var workflow = GetWorkflow(transactionId, _contextManager);
            workflow.Prepare(transactionId, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/review
        [Route(TransactionApi.ReviewTransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Review)]
        public IHttpActionResult PutTransactionAsReviewed(int transactionId, [FromBody] ActionDetailViewModel detail)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not put transaction as Reviewed because transaction does not exist.");
            }

            string message = ValidateStateOperation(DocumentAction.Review, transactionId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            var paraph = detail?.Paraph;
            var workflow = GetWorkflow(transactionId, _contextManager);
            workflow.Review(transactionId, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/reject
        [Route(TransactionApi.RejectTransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Confirm)]
        public IHttpActionResult PutTransactionAsRejected(int transactionId, [FromBody] ActionDetailViewModel detail)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not put transaction as Rejected because transaction does not exist.");
            }

            string message = ValidateStateOperation(DocumentAction.Reject, transactionId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            var paraph = detail?.Paraph;
            var workflow = GetWorkflow(transactionId, _contextManager);
            workflow.RejectReviewed(transactionId, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/confirm
        [Route(TransactionApi.ConfirmTransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Confirm)]
        public IHttpActionResult PutTransactionAsConfirmed(int transactionId, [FromBody] ActionDetailViewModel detail)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not put transaction as Confirmed because transaction does not exist.");
            }

            string message = ValidateStateOperation(DocumentAction.Confirm, transactionId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            var paraph = detail?.Paraph;
            var workflow = GetWorkflow(transactionId, _contextManager);
            workflow.Confirm(transactionId, paraph);
            return Ok();
        }

        // PUT: api/transactions/{transactionId:int}/approve
        [Route(TransactionApi.ApproveTransactionUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Approve)]
        public IHttpActionResult PutTransactionAsApproved(int transactionId, [FromBody] ActionDetailViewModel detail)
        {
            if (transactionId <= 0)
            {
                return BadRequest("Could not put transaction as Approved because transaction does not exist.");
            }

            string message = ValidateStateOperation(DocumentAction.Approve, transactionId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            var paraph = detail?.Paraph;
            var workflow = GetWorkflow(transactionId, _contextManager);
            workflow.Approve(transactionId, paraph);
            return Ok();
        }

        // PUT: api/transactions/prepare
        [Route(TransactionApi.PrepareTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Prepare)]
        public IHttpActionResult PutTransactionsAsPrepared([FromBody] ActionDetailViewModel detail)
        {
            if (detail == null)
            {
                return BadRequest("Could not put transaction as Prepared because operation detail is null.");
            }

            string message = ValidateGroupStateOperation(DocumentAction.Prepare, detail.Items.ToArray());
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            foreach (int transactionId in detail.Items)
            {
                var workflow = GetWorkflow(transactionId, _contextManager);
                workflow.Prepare(transactionId, detail.Paraph);
            }

            return Ok();
        }

        // PUT: api/transactions/review
        [Route(TransactionApi.ReviewTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Review)]
        public IHttpActionResult PutTransactionsAsReviewed([FromBody] ActionDetailViewModel detail)
        {
            if (detail == null)
            {
                return BadRequest("Could not put transaction as Reviewed because operation detail is null.");
            }

            string message = ValidateGroupStateOperation(DocumentAction.Review, detail.Items.ToArray());
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            foreach (int transactionId in detail.Items)
            {
                var workflow = GetWorkflow(transactionId, _contextManager);
                workflow.Review(transactionId, detail.Paraph);
            }

            return Ok();
        }

        // PUT: api/transactions/reject
        [Route(TransactionApi.RejectTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Confirm)]
        public IHttpActionResult PutTransactionsAsRejected([FromBody] ActionDetailViewModel detail)
        {
            if (detail == null)
            {
                return BadRequest("Could not put transaction as Rejected because operation detail is null.");
            }

            string message = ValidateGroupStateOperation(DocumentAction.Reject, detail.Items.ToArray());
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            foreach (int transactionId in detail.Items)
            {
                var workflow = GetWorkflow(transactionId, _contextManager);
                workflow.RejectReviewed(transactionId, detail.Paraph);
            }

            return Ok();
        }

        // PUT: api/transactions/confirm
        [Route(TransactionApi.ConfirmTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Confirm)]
        public IHttpActionResult PutTransactionsAsConfirmed([FromBody] ActionDetailViewModel detail)
        {
            if (detail == null)
            {
                return BadRequest("Could not put transaction as Confirmed because operation detail is null.");
            }

            string message = ValidateGroupStateOperation(DocumentAction.Confirm, detail.Items.ToArray());
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            foreach (int transactionId in detail.Items)
            {
                var workflow = GetWorkflow(transactionId, _contextManager);
                workflow.Confirm(transactionId, detail.Paraph);
            }

            return Ok();
        }

        // PUT: api/transactions/approve
        [Route(TransactionApi.ApproveTransactionsUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.Approve)]
        public IHttpActionResult PutTransactionsAsApproved([FromBody] ActionDetailViewModel detail)
        {
            if (detail == null)
            {
                return BadRequest("Could not put transaction as Approved because operation detail is null.");
            }

            string message = ValidateGroupStateOperation(DocumentAction.Approve, detail.Items.ToArray());
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            foreach (int transactionId in detail.Items)
            {
                var workflow = GetWorkflow(transactionId, _contextManager);
                workflow.Approve(transactionId, detail.Paraph);
            }

            return Ok();
        }

        #endregion

        private ITransactionWorkflow GetWorkflow(int transactionId, ISecurityContextManager contextManager)
        {
            var edition = _tracker.TrackDocumentWorkflowEdition(transactionId, DocumentType.Transaction);
            var workflow = UnityConfig.GetConfiguredContainer()
                .Resolve<ITransactionWorkflow>(edition);
            workflow.ContextManager = contextManager;
            return workflow;
        }

        private string ValidateGroupStateOperation(string operation, int[] transactions)
        {
            var messages = transactions
                .Select(item => ValidateStateOperation(operation, item))
                .Where(msg => !String.IsNullOrEmpty(msg))
                .ToArray();
            return String.Join(Environment.NewLine, messages);
        }

        // !!! WARNING : Broken functionality during refactoring
        private string ValidateStateOperation(string operation, int transactionId)
        {
            string result = String.Empty;
            var summary = _repository.GetTransactionSummary(transactionId);
            if (summary == null)
            {
                result = String.Format(ValidationMessages.ItemNotFound, Entities.TransactionLongName);
            }
            else if (!StateOperationValidator.Validate(operation, DocumentStatus.Created))
            {
                result = String.Format(
                    Strings.InvalidDocumentOperation,
                    Entities.TransactionLongName,
                    summary.No,
                    DocumentAction.ToLocalValue(operation),
                    DocumentStatus.ToLocalValue(DocumentStatus.Created));
            }

            return result;
        }

        private void SetDocument(TransactionViewModel transaction)
        {
            if (transaction.Document == null)
            {
                var document = new DocumentViewModel()
                {
                    OperationalStatus = DocumentStatus.Created,
                    StatusId = (int)DocumentStatuses.Draft,
                    TypeId = (int)DocumentTypes.Transaction
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
        private IWorkflowTracker _tracker;
        private ISecurityContextManager _contextManager;
    }
}

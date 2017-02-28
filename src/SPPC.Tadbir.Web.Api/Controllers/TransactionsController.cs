using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class TransactionsController : ApiController
    {
        public TransactionsController(ITransactionRepository repository)
        {
            _repository = repository;
        }

        // GET: api/transactions/fp/{fpId:int}
        [Route(TransactionApi.FiscalPeriodTransactionsUrl)]
        public IHttpActionResult GetTransactions(int fpId)
        {
            if (fpId <= 0)
            {
                return NotFound();
            }

            var transactions = _repository.GetTransactions(fpId);
            return Json(transactions);
        }

        // GET: api/transactions/{transactionId:int}/details
        [Route(TransactionApi.TransactionDetailsUrl)]
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

        // GET: api/transactions/articles/{articleId:int}
        [Route(TransactionApi.TransactionArticleUrl)]
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

        private ITransactionRepository _repository;
    }
}

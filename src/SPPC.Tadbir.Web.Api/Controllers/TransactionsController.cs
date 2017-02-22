using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Framework.Values;
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
        [Route("transactions/fp/{fpId:int}")]
        public IHttpActionResult GetTransactions(int fpId)
        {
            if (fpId <= 0)
            {
                return NotFound();
            }

            var transactions = _repository.GetTransactions(fpId);
            return Json(transactions);
        }

        // POST: api/transactions
        [Route("transactions")]
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
                var message = String.Format(Transactions.InvalidDate);
                return BadRequest(message);
            }

            _repository.SaveTransaction(transaction);
            return StatusCode(HttpStatusCode.Created);
        }

        private ITransactionRepository _repository;
    }
}

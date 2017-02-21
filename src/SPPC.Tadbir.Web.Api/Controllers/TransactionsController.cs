using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.NHibernate;

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

        private ITransactionRepository _repository;
    }
}

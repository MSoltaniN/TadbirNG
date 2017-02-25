using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.NHibernate;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class LookupController : ApiController
    {
        public LookupController(ILookupRepository repository)
        {
            _repository = repository;
        }

        // GET: api/lookup/accounts/{fpId:int}
        [Route("lookup/accounts/{fpId:int}")]
        public IHttpActionResult GetAccountsLookup(int fpId)
        {
            if (fpId <= 0)
            {
                return NotFound();
            }

            var accountLookup = _repository.GetAccounts(fpId);
            return Json(accountLookup);
        }

        // GET: api/lookup/currencies
        [Route("lookup/currencies")]
        public IHttpActionResult GetCurrenciesLookup()
        {
            var currencyLookup = _repository.GetCurrencies();
            return Json(currencyLookup);
        }

        private ILookupRepository _repository;
    }
}

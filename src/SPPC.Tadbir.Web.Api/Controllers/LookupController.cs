using System;
using System.Collections.Generic;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class LookupController : ApiController
    {
        public LookupController(ILookupRepository repository)
        {
            _repository = repository;
        }

        // GET: api/lookup/accounts/fp/{fpId:int}/branch/{branchId:int}
        [Route(LookupApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IHttpActionResult GetAccountsLookup(int fpId, int branchId)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var accountLookup = _repository.GetAccounts(fpId, branchId);
            return Json(accountLookup);
        }

        // GET: api/lookup/currencies
        [Route(LookupApi.CurrenciesUrl)]
        public IHttpActionResult GetCurrenciesLookup()
        {
            var currencyLookup = _repository.GetCurrencies();
            return Json(currencyLookup);
        }

        private ILookupRepository _repository;
    }
}

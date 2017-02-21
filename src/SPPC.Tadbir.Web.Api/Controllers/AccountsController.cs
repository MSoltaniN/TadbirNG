using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public partial class AccountsController
    {
        // GET: api/accounts/{accountId:int}/detail
        [Route("accounts/{accountId:int}/detail")]
        public IHttpActionResult GetAccountDetail(int accountId)
        {
            if (accountId <= 0)
            {
                return NotFound();
            }

            var account = _repository.GetAccountDetail(accountId);
            var result = (account != null)
                ? Json(account)
                : NotFound() as IHttpActionResult;

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public partial class AccountsController
    {
        // GET: api/accounts/{accountId:int}/details
        [Route(AccountApi.AccountDetailsUrl)]
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

        // DELETE: api/accounts/{accountId:int}
        [Route(AccountApi.AccountUrl)]
        public IHttpActionResult DeleteExistingAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest("Could not delete account because it does not exist.");
            }

            var account = _repository.GetAccount(accountId);
            if (account == null)
            {
                return BadRequest("Could not delete account because it does not exist.");
            }

            if (_repository.IsUsedAccount(accountId))
            {
                var accountInfo = String.Format("'{0} ({1})'", account.Name, account.Code);
                var message = String.Format(Accounts.CannotDeleteUsedAccount, accountInfo);
                return BadRequest(message);
            }

            _repository.DeleteAccount(accountId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Values;
using System.Net;

using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.UI;
using SPPC.Framework.Values;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tadbir.GridViewCRUD.Controllers
{
    


    public class AccountController : Controller
    {
        private IAccountRepository _repository;

        public AccountController(IAccountRepository Repo)
        {
            _repository = Repo;
        }



        [HttpGet, Produces("application/json")]
        [Route("accounts/GetTotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {

            //var data = await _repository.GetAllAccount();
            return Json(new { result = 50 });


        }


        //[HttpPost, Produces("application/json")]
        //[Route("/Account/GetLazyAccounts/{id}")]

        // DELETE: api/accounts/{accountId:int}
        [Route(AccountApi.AccountUrl)]
        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            //var data = await AccountRepo.DeleteAccount(id);
            //return Json(new { result = data });

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
                var message = String.Format(Strings.CannotDeleteUsedAccount, accountInfo);
                return BadRequest(message);
            }

            _repository.DeleteAccount(accountId);
            return StatusCode((int)HttpStatusCode.NoContent);
        }


        //[HttpPost, Produces("application/json")]
        //[Route("/Account/SaveAccount")]

        // POST: api/accounts
        [Route(AccountApi.AccountsUrl)]
        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        public async Task<IActionResult> PostNewAccount([FromBody] AccountViewModel account)
        {
            //var data = await AccountRepo.SaveAccount(account);
            //return Json(new { result = data });

            if (account == null)
            {
                return BadRequest("Could not post new account because a 'null' value was provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repository.IsDuplicateAccount(account))
            {
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.AccountCodeField);
                return BadRequest(message);
            }

            _repository.SaveAccount(account);
            return StatusCode((int)HttpStatusCode.Created);

        }


        // PUT: api/accounts/{accountId:int}
        [Route(AccountApi.AccountUrl)]
        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Edit)]
        public async Task<IActionResult> PutModifiedAccount(int accountId, [FromBody] AccountViewModel account)
        {
            //var data = await AccountRepo.SaveAccount(account);
            //return Json(new { result = data });

            if (account == null)
            {
                return BadRequest("Could not put modified account because a 'null' value was provided.");
            }

            if (accountId <= 0 || account.Id <= 0)
            {
                return BadRequest("Could not put modified account because original account does not exist.");
            }

            if (accountId != account.Id)
            {
                return BadRequest("Could not put modified account because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repository.IsDuplicateAccount(account))
            {
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.AccountCodeField);
                return BadRequest(message);
            }

            _repository.SaveAccount(account);
            return Ok();
        }



        //[HttpPost, Produces("application/json")]       
        //[Route("/Account/GetLazyAccounts")]

        // GET: api/accounts/fp/{fpId:int}/branch/{branchId:int}
        [Route(AccountApi.FiscalPeriodBranchAccountsUrl)]
        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccounts(int fpId, int branchId, [FromBody] GridOptions options = null)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var accounts = _repository.GetAccounts(fpId, branchId);
            return Json(accounts);
        }
    }
}

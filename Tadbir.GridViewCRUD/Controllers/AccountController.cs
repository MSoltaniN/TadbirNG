using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SPPC.Tadbir.Business;
using System.Net;
using SPPC.Tadbir.DataAccess;

//using SPPC.Tadbir.Api;
//using SPPC.Tadbir.NHibernate;
//using SPPC.Tadbir.Values;
//using System.Net;

//using SPPC.Tadbir.ViewModel.Finance;
//using SPPC.Tadbir.ViewModel.UI;
//using SPPC.Framework.Values;

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
        [Route("/Account/Count")]
        public IActionResult Count()
        {

            //var data = await _repository.GetAllAccount();
            return Json(new { result = 50 });


        }


        //[HttpPost, Produces("application/json")]
        //[Route("/Account/GetLazyAccounts/{id}")]
        
        
        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]

        [HttpPost, Produces("application/json")]
        [Route("/Account/Delete/{id}")]
        public IActionResult Delete(int accountId)
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

            //if (_repository.IsUsedAccount(accountId))
            //{
            //    var accountInfo = String.Format("'{0} ({1})'", account.Name, account.Code);
            //    var message = String.Format(Strings.CannotDeleteUsedAccount, accountInfo);
            //    return BadRequest(message);
            //}

            _repository.DeleteAccount(accountId);
            return StatusCode((int)HttpStatusCode.NoContent);
        }


       
        // POST: api/accounts
        [Route("/Account/Edit")]
        [HttpPost, Produces("application/json")]        
        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        public IActionResult Edit([FromBody] AccountViewModel account)
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

            //if (_repository.IsDuplicateAccount(account))
            //{
            //    var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.AccountCodeField);
            //    return BadRequest(message);
            //}

            _repository.SaveAccount(account);
            return StatusCode((int)HttpStatusCode.Created);

        }


        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        [HttpPost, Produces("application/json")]
        [Route("/Account/Insert/{id}")]
        public IActionResult Insert([FromBody] AccountViewModel account)
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

            //if (_repository.IsDuplicateAccount(account))
            //{
            //    var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.AccountCodeField);
            //    return BadRequest(message);
            //}

            _repository.InsertAccount(account);
            return StatusCode((int)HttpStatusCode.Created);

        }





        
        // GET: api/accounts/fp/{fpId:int}/branch/{branchId:int}
        [Route("/Account/fp/{fpId}/branch/{branchId}")]
        [HttpPost, Produces("application/json")]
        public  IActionResult List(int fpId, int branchId, [FromBody] GridOption options = null)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var accounts = _repository.GetAccounts(fpId, branchId,options);
            return Json(accounts);
        }
    }
}

﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Presentation;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountsController : Controller
    {
        public AccountsController(IAccountRepository repository)
        {
            _repository = repository;
        }

        // GET: api/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync
        [Route(AccountApi.FiscalPeriodBranchAccountsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccounts(int fpId, int branchId, [FromBody] GridOptions gridOptions = null)
        {
            var accounts = _repository.GetAccounts(fpId, branchId, gridOptions);
            return Json(accounts);
        }

        // GET: api/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(AccountApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsAsync(
            int fpId, int branchId, [FromBody] GridOptions gridOptions = null)
        {
            var accounts = await _repository.GetAccountsAsync(fpId, branchId, gridOptions);
            return Json(accounts);
        }

        // GET: api/accounts/{accountId:min(1)}/sync
        [Route(AccountApi.AccountSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccount(int accountId)
        {
            var account = _repository.GetAccount(accountId);
            return JsonReadResult(account);
        }

        // GET: api/accounts/{accountId:min(1)}
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountAsync(int accountId)
        {
            var account = await _repository.GetAccountAsync(accountId);
            return JsonReadResult(account);
        }

        // POST: api/accounts/sync
        [HttpPost]
        [Route(AccountApi.AccountsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        public IActionResult PostNewAccount([FromBody] AccountViewModel account)
        {
            var result = ValidationResult(account);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SaveAccount(account);
            return StatusCode(StatusCodes.Status201Created);
        }

        // POST: api/accounts
        [HttpPost]
        [Route(AccountApi.AccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        public async Task<IActionResult> PostNewAccountAsync([FromBody] AccountViewModel account)
        {
            var result = await ValidationResultAsync(account);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveAccountAsync(account);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/accounts/{accountId:min(1)}/sync
        [HttpPut]
        [Route(AccountApi.AccountSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Edit)]
        public IActionResult PutModifiedAccount(int accountId, [FromBody] AccountViewModel account)
        {
            var result = ValidationResult(account, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SaveAccount(account);
            return Ok();
        }

        // PUT: api/accounts/{accountId:min(1)}
        [HttpPut]
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Edit)]
        public async Task<IActionResult> PutModifiedAccountAsync(int accountId, [FromBody] AccountViewModel account)
        {
            var result = await ValidationResultAsync(account, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveAccountAsync(account);
            return Ok();
        }

        // GET: api/accounts/{accountId:min(1)}/details/sync
        [Route(AccountApi.AccountDetailsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccountDetail(int accountId)
        {
            var account = _repository.GetAccountDetail(accountId);
            return JsonReadResult(account);
        }

        // GET: api/accounts/{accountId:min(1)}/details
        [Route(AccountApi.AccountDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountDetailAsync(int accountId)
        {
            var account = await _repository.GetAccountDetailAsync(accountId);
            return JsonReadResult(account);
        }

        // GET: api/accounts/{accountId:min(1)}/articles/sync
        [Route(AccountApi.AccountArticlesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetAccountArticles(int accountId, [FromBody] GridOptions gridOptions = null)
        {
            var articles = _repository.GetAccountArticles(accountId, gridOptions);
            return JsonReadResult(articles);
        }

        // GET: api/accounts/{accountId:min(1)}/articles
        [Route(AccountApi.AccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetAccountArticlesAsync(
            int accountId, [FromBody] GridOptions gridOptions = null)
        {
            var articles = await _repository.GetAccountArticlesAsync(accountId, gridOptions);
            return JsonReadResult(articles);
        }

        // GET: api/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}/count
        [Route(AccountApi.FiscalPeriodBranchItemCountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountItemCount(
            int fpId, int branchId, [FromBody] GridOptions gridOptions = null)
        {
            int count = await _repository.GetCountAsync(fpId, branchId, gridOptions);
            return Json(count);
        }

        // DELETE: api/accounts/{accountId:min(1)}/sync
        [HttpDelete]
        [Route(AccountApi.AccountSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public IActionResult DeleteExistingAccount(int accountId)
        {
            var account = _repository.GetAccount(accountId);
            if (account == null)
            {
                return NotFound();
            }

            if (_repository.IsUsedAccount(accountId))
            {
                var accountInfo = String.Format("'{0} ({1})'", account.Name, account.Code);
                var message = String.Format(Strings.CannotDeleteUsedAccount, accountInfo);
                return BadRequest(message);
            }

            _repository.DeleteAccount(accountId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // DELETE: api/accounts/{accountId:min(1)}
        [HttpDelete]
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingAccountAsync(int accountId)
        {
            var account = await _repository.GetAccountAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            if (await _repository.IsUsedAccountAsync(accountId))
            {
                var accountInfo = String.Format("'{0} ({1})'", account.Name, account.Code);
                var message = String.Format(Strings.CannotDeleteUsedAccount, accountInfo);
                return BadRequest(message);
            }

            await _repository.DeleteAccountAsync(accountId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private IActionResult JsonReadResult<TData>(TData data)
        {
            var result = (data != null)
                ? Json(data)
                : NotFound() as IActionResult;

            return result;
        }

        private IActionResult BasicValidationResult(AccountViewModel account, int accountId)
        {
            if (account == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.Account);
                return BadRequest(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (accountId != account.Id)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, Entities.Account);
                return BadRequest(message);
            }

            return Ok();
        }

        private IActionResult ValidationResult(AccountViewModel account, int accountId = 0)
        {
            var result = BasicValidationResult(account, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (_repository.IsDuplicateAccount(account))
            {
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.AccountCodeField);
                return BadRequest(message);
            }

            return Ok();
        }

        private async Task<IActionResult> ValidationResultAsync(AccountViewModel account, int accountId = 0)
        {
            var result = BasicValidationResult(account, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateAccountAsync(account))
            {
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.AccountCodeField);
                return BadRequest(message);
            }

            return Ok();
        }

        private IAccountRepository _repository;
    }
}
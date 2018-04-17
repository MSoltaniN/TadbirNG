﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountsController : Controller
    {
        public AccountsController(
            IAccountRepository repository,
            IStringLocalizer<Messages> messages = null,
            IStringLocalizer<EntityNames> entities = null)
        {
            _repository = repository;
            _messages = messages;
            _entities = entities;
        }

        #region Asynchronous Methods

        // GET: api/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(AccountApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsAsync(int fpId, int branchId)
        {
            var gridOptions = GetGridOptions();
            int itemCount = await _repository.GetCountAsync(fpId, branchId, gridOptions);
            SetItemCount(itemCount);
            var accounts = await _repository.GetAccountsAsync(fpId, branchId, gridOptions);
            return Json(accounts);
        }

        // GET: api/accounts/{accountId:min(1)}
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountAsync(int accountId)
        {
            var account = await _repository.GetAccountAsync(accountId);
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

        // GET: api/accounts/metadata
        [Route(AccountApi.AccountMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountMetadataAsync()
        {
            var metadata = await _repository.GetAccountMetadataAsync();
            return JsonReadResult(metadata);
        }

        // GET: api/accounts/{accountId:min(1)}/articles
        [Route(AccountApi.AccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public async Task<IActionResult> GetAccountArticlesAsync(int accountId)
        {
            var gridOptions = GetGridOptions();
            var articles = await _repository.GetAccountArticlesAsync(accountId, gridOptions);
            return JsonReadResult(articles);
        }

        // GET: api/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}/count
        [Route(AccountApi.FiscalPeriodBranchItemCountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetItemCountAsync(int fpId, int branchId)
        {
            var gridOptions = GetGridOptions();
            int count = await _repository.GetCountAsync(fpId, branchId, gridOptions);
            return Json(count);
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

            var outputAccount = await _repository.SaveAccountAsync(account);
            return StatusCode(StatusCodes.Status201Created, outputAccount);
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

            var outputAccount = await _repository.SaveAccountAsync(account);
            result = (outputAccount != null)
                ? Ok(outputAccount)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/accounts/{accountId:min(1)}
        [HttpDelete]
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingAccountAsync(int accountId)
        {
            string result = await ValidateDeleteAsync(accountId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteAccountAsync(accountId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/accounts
        [HttpPut]
        [Route(AccountApi.AccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public async Task<IActionResult> PutExistingAccountsAsDeletedAsync([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.GroupAction);
                return BadRequest(message);
            }

            var result = await ValidateGroupDeleteAsync(actionDetail.Items);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            foreach (int accountId in actionDetail.Items)
            {
                await _repository.DeleteAccountAsync(accountId);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        // GET: api/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync
        [Route(AccountApi.FiscalPeriodBranchAccountsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccounts(int fpId, int branchId)
        {
            var gridOptions = GetGridOptions();
            int itemCount = _repository.GetCount(fpId, branchId, gridOptions);
            SetItemCount(itemCount);
            var accounts = _repository.GetAccounts(fpId, branchId, gridOptions);
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

        // GET: api/accounts/{accountId:min(1)}/details/sync
        [Route(AccountApi.AccountDetailsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccountDetail(int accountId)
        {
            var account = _repository.GetAccountDetail(accountId);
            return JsonReadResult(account);
        }

        // GET: api/accounts/{accountId:min(1)}/articles/sync
        [Route(AccountApi.AccountArticlesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public IActionResult GetAccountArticles(int accountId)
        {
            var gridOptions = GetGridOptions();
            var articles = _repository.GetAccountArticles(accountId, gridOptions);
            return JsonReadResult(articles);
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

        #endregion

        private GridOptions GetGridOptions()
        {
            var options = Request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return null;
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return Framework.Helpers.Json.To<GridOptions>(json);
        }

        private void SetItemCount(int count)
        {
            Response.Headers.Add(AppConstants.TotalCountHeaderName, count.ToString());
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
                var message = _messages["RequestFailedNoData", _entities["Account"]];
                return BadRequest(message.Value);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (accountId != account.Id)
            {
                var message = _messages["RequestFailedConflict", _entities["Account"]];
                return BadRequest(message.Value);
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

        private async Task<IEnumerable<string>> ValidateGroupDeleteAsync(IEnumerable<int> items)
        {
            var messages = new List<string>();
            foreach (int item in items)
            {
                messages.Add(await ValidateDeleteAsync(item));
            }

            return messages
                .Where(msg => !String.IsNullOrEmpty(msg));
        }

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var accountItem = await _repository.GetAccountAsync(item);
            if (accountItem == null)
            {
                message = String.Format(Strings.ItemByIdNotFound, Entities.Account, item);
            }

            if (await _repository.IsUsedAccountAsync(item))
            {
                var accountInfo = String.Format("'{0} ({1})'", accountItem.Item.Name, accountItem.Item.Code);
                message = String.Format(Strings.CannotDeleteUsedAccount, accountInfo);
            }

            return message;
        }

        private IAccountRepository _repository;
        private IStringLocalizer<Messages> _messages;
        private IStringLocalizer<EntityNames> _entities;
    }
}
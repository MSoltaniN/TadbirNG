using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountsController : ValidatingController<AccountViewModel>
    {
        public AccountsController(IAccountRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Account; }
        }

        // GET: api/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(AccountApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsAsync(int fpId, int branchId)
        {
            int itemCount = await _repository.GetCountAsync(UserAccess, fpId, branchId, GridOptions);
            SetItemCount(itemCount);
            var accounts = await _repository.GetAccountsAsync(UserAccess, fpId, branchId, GridOptions);
            return Json(accounts);
        }

        // GET: api/accounts/lookup/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(AccountApi.FiscalPeriodBranchAccountsLookupUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetAccountsLookupAsync(UserAccess, fpId, branchId, GridOptions);
            return Json(lookup);
        }

        // GET: api/accounts/{accountId:min(1)}
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountAsync(int accountId)
        {
            var account = await _repository.GetAccountAsync(accountId);
            return JsonReadResult(account);
        }

        // GET: api/accounts/{accountId:min(1)}/children
        [Route(AccountApi.AccountChildrenUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountChildrenAsync(int accountId)
        {
            var children = await _repository.GetAccountChildrenAsync(accountId);
            return JsonReadResult(children);
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
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetAccountArticlesAsync(int accountId)
        {
            var articles = await _repository.GetAccountArticlesAsync(accountId, GridOptions);
            return JsonReadResult(articles);
        }

        // GET: api/accounts/fullcode/{parentId}
        [HttpGet]
        [Route(AccountApi.AccountFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create | (int)AccountPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int parentId)
        {
            if (parentId <= 0)
            {
                return Ok(string.Empty);
            }

            string fullCode = await _repository.GetAccountFullCodeAsync(parentId);

            return Ok(fullCode);
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
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

        private async Task<IActionResult> ValidationResultAsync(AccountViewModel account, int accountId = 0)
        {
            var result = BasicValidationResult(account, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateAccountAsync(account))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateCodeValue, AppStrings.Account, account.FullCode));
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
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Account), item);
            }

            string accountInfo = String.Format("'{0} ({1})'", accountItem.Name, accountItem.Code);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteNonLeafItem], _strings[AppStrings.Account], accountInfo);
            }
            else if (await _repository.IsUsedAccountAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteUsedItem], _strings[AppStrings.Account], accountInfo);
            }
            else if (await _repository.IsRelatedAccountAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteRelatedItem], _strings[AppStrings.Account], accountInfo);
            }

            return message;
        }

        private IAccountRepository _repository;
    }
}
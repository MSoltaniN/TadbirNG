using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Utility;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountGroupsController : Controller
    {
        public AccountGroupsController(IAccountGroupRepository repository, IApiValidation apiUtility)
        {
            _repository = repository;
            _apiUtility = apiUtility;
            _apiUtility.EntityNameKey = AppStrings.AccountGroup;
        }

        // GET: api/accgroups
        [HttpGet]
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsAsync()
        {
            var accountGroups = await _repository.GetAccountGroupsAsync(_apiUtility.GridOptions);
            Localize(accountGroups.Items);
            return _apiUtility.JsonListResult(accountGroups);
        }

        // GET: api/accgroups/{groupId:min(1)}
        [HttpGet]
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupAsync(int groupId)
        {
            var accountGroup = await _repository.GetAccountGroupAsync(groupId);
            if (accountGroup != null)
            {
                Localize(accountGroup);
            }

            return _apiUtility.JsonReadResult(accountGroup);
        }

        // GET: api/accgroups/{groupId:min(1)}/accounts
        [HttpGet]
        [Route(AccountGroupApi.GroupLedgerAccountsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetGroupLedgerAccountsAsync(int groupId)
        {
            var accounts = await _repository.GetGroupLedgerAccountsAsync(groupId, _apiUtility.GridOptions);
            return _apiUtility.JsonListResult(accounts);
        }

        // GET: api/accgroups/brief
        [HttpGet]
        [Route(AccountGroupApi.AccountGroupBriefUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsBriefAsync()
        {
            var accGroups = await _repository.GetAccountGroupsBriefAsync();
            return _apiUtility.JsonReadResult(accGroups);
        }

        // POST: api/accgroups
        [HttpPost]
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Create)]
        public async Task<IActionResult> PostNewAccountGroupAsync(
            [FromBody] AccountGroupViewModel accountGroup)
        {
            var result = _apiUtility.BasicValidationResult(accountGroup, ModelState);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveAccountGroupAsync(accountGroup);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/accgroups/{groupId:min(1)}
        [HttpPut]
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Edit)]
        public async Task<IActionResult> PutModifiedAccountGroupAsync(
            int groupId, [FromBody] AccountGroupViewModel accountGroup)
        {
            var result = _apiUtility.BasicValidationResult(accountGroup, ModelState, groupId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveAccountGroupAsync(accountGroup);
            return _apiUtility.OkReadResult(outputItem);
        }

        // DELETE: api/accgroups/{groupId:min(1)}
        [HttpDelete]
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingAccountGroupAsync(int groupId)
        {
            string message = await ValidateDeleteAsync(groupId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            await _repository.DeleteAccountGroupAsync(groupId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/accgroups
        [HttpPut]
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Delete)]
        public async Task<IActionResult> PutExistingAccountGroupsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_apiUtility.Strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await _apiUtility.ValidateGroupDeleteAsync(actionDetail.Items, ValidateDeleteAsync);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            await _repository.DeleteAccountGroupsAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var accountGroup = await _repository.GetAccountGroupAsync(item);
            if (accountGroup == null)
            {
                message = _apiUtility.Strings.Format(
                    AppStrings.ItemByIdNotFound, AppStrings.AccountGroup, item.ToString());
            }
            else
            {
                bool canDelete = await _repository.CanDeleteAccountGroupAsync(item);
                if (!canDelete)
                {
                    message = _apiUtility.Strings.Format(
                        AppStrings.CantDeleteUsedAccountGroup, accountGroup.Name);
                }
            }

            return message;
        }

        private void Localize(IEnumerable<AccountGroupViewModel> accountGroups)
        {
            foreach (var accountGroup in accountGroups)
            {
                Localize(accountGroup);
            }
        }

        private void Localize(AccountGroupViewModel accountGroup)
        {
            accountGroup.Category = _apiUtility.Strings[accountGroup.Category];
        }

        private readonly IAccountGroupRepository _repository;
        private readonly IApiValidation _apiUtility;
    }
}
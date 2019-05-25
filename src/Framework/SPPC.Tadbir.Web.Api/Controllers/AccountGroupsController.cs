using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountGroupsController : ValidatingController<AccountGroupViewModel>
    {
        public AccountGroupsController(IAccountGroupRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.AccountGroup; }
        }

        // GET: api/accgroups
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsAsync()
        {
            int itemCount = await _repository.GetCountAsync(GridOptions);
            SetItemCount(itemCount);
            var accountGroups = await _repository.GetAccountGroupsAsync(GridOptions);
            Localize(accountGroups.ToArray());
            return Json(accountGroups);
        }

        // GET: api/accgroups/{groupId:min(1)}
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupAsync(int groupId)
        {
            var accountGroup = await _repository.GetAccountGroupAsync(groupId);
            if (accountGroup != null)
            {
                Localize(accountGroup);
            }

            return JsonReadResult(accountGroup);
        }

        // GET: api/accgroups/{groupId:min(1)}/accounts
        [Route(AccountGroupApi.GroupLedgerAccountsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetGroupLedgerAccountsAsync(int groupId)
        {
            _repository.SetCurrentContext(SecurityContext.User);
            int itemCount = await _repository.GetSubItemCountAsync(groupId, GridOptions);
            SetItemCount(itemCount);
            var accounts = await _repository.GetGroupLedgerAccountsAsync(groupId, GridOptions);
            return Json(accounts);
        }

        // GET: api/accgroups/brief
        [Route(AccountGroupApi.AccountGroupBriefUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsBriefAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var accGroups = await _repository.GetAccountGroupsBriefAsync();
            return JsonReadResult(accGroups);
        }

        // POST: api/accgroups
        [HttpPost]
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Create)]
        public async Task<IActionResult> PostNewAccountGroupAsync(
            [FromBody] AccountGroupViewModel accountGroup)
        {
            var result = BasicValidationResult(accountGroup);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SetCurrentContext(SecurityContext.User);
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
            var result = BasicValidationResult(accountGroup, groupId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SetCurrentContext(SecurityContext.User);
            var outputItem = await _repository.SaveAccountGroupAsync(accountGroup);
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/accgroups/{groupId:min(1)}
        [HttpDelete]
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingAccountGroupAsync(int groupId)
        {
            var accountGroup = await _repository.GetAccountGroupAsync(groupId);
            if (accountGroup == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.AccountGroup));
            }

            bool canDelete = await _repository.CanDeleteAccountGroupAsync(groupId);
            if (!canDelete)
            {
                return BadRequest(_strings.Format(AppStrings.CantDeleteUsedAccountGroup));
            }

            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.DeleteAccountGroupAsync(groupId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private void Localize(params AccountGroupViewModel[] accountGroups)
        {
            Array.ForEach(accountGroups, grp => grp.Category = _strings[grp.Category]);
        }

        private readonly IAccountGroupRepository _repository;
    }
}
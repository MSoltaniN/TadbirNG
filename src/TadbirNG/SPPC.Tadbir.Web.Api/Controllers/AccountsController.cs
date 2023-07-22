using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با سرفصل های حسابداری را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class AccountsController : ValidatingController<AccountViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات سرفصل های حسابداری در دیتابیس را فراهم می کند</param>
        /// <param name="config">امکان خواندن اطلاعات پیکربندی برنامه را فراهم می کند</param>
        /// <param name="checkEdition"></param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        public AccountsController(
            IAccountRepository repository, IConfigRepository config, ICheckEdition checkEdition,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewId.Account).Result;
            _checkEdition = checkEdition;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام سرفصل حسابداری
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Account; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سرفصل های حسابداری قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده سرفصل های حسابداری</returns>
        // GET: api/accounts
        [HttpGet]
        [Route(AccountApi.EnvironmentAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetEnvironmentAccountsAsync()
        {
            var accounts = await _repository.GetAccountsAsync(GridOptions);
            return JsonListResult(accounts);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی سرفصل حسابداری مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر</param>
        /// <returns>اطلاعات نمایشی سرفصل حسابداری</returns>
        // GET: api/accounts/{accountId:min(1)}
        [HttpGet]
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountAsync(int accountId)
        {
            var account = await _repository.GetAccountAsync(accountId);
            return JsonReadResult(account);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل سرفصل حسابداری مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر</param>
        /// <returns>اطلاعات کامل سرفصل حسابداری شامل حساب و سایر مشخصات حساب</returns>
        // GET: api/accounts/{accountId:min(1)}/fulldata
        [HttpGet]
        [Route(AccountApi.AccountFullDataUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountFullDataAsync(int accountId)
        {
            var account = await _repository.GetAccountFullDataAsync(accountId);
            return JsonReadResult(account);
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل حسابداری جدیدی زیرمجموعه سرفصل والد داده شده برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل والد</param>
        /// <returns>اطلاعات کامل پیشنهادی برای سرفصل حسابداری جدید</returns>
        // GET: api/accounts/{accountId:int}/children/new
        [HttpGet]
        [Route(AccountApi.NewChildAccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        public async Task<IActionResult> GetNewAccountAsync(int accountId)
        {
            var newAccountFull = await _repository.GetNewChildAccountAsync(
                accountId > 0 ? accountId : null);
            if (newAccountFull == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ParentItemNotFound, AppStrings.Account));
            }

            if (newAccountFull.Account.Level == -1)
            {
                return BadRequestResult(_strings.Format(AppStrings.ChildItemsNotAllowed, AppStrings.Account));
            }

            if (accountId > 0 && await _repository.IsUsedAccountAsync(accountId))
            {
                var parent = await _repository.GetAccountAsync(accountId);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.Account, parentInfo));
            }

            return Json(newAccountFull);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سرفصل های حسابداری در سطح کل را برمی گرداند
        /// </summary>
        /// <returns>لیست اطلاعات خلاصه سرفصل های حسابداری در سطح کل</returns>
        // GET: api/accounts/ledger
        [HttpGet]
        [Route(AccountApi.LedgerAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetLedgerAccountsAsync()
        {
            var accounts = await _repository.GetLedgerAccountsAsync();
            return Json(accounts);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سرفصل های حسابداری کل تعریف شده برای گروه حساب داده شده را برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر</param>
        /// <returns>لیست اطلاعات خلاصه سرفصل های حسابداری در سطح کل</returns>
        // GET: api/accounts/ledger/{groupId:min(1)}
        [HttpGet]
        [Route(AccountApi.LedgerAccountsByGroupIdUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetLedgerAccountsByGroupIdAsync(int groupId)
        {
            var accounts = await _repository.GetLedgerAccountsByGroupIdAsync(groupId);
            return Json(accounts);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه سرفصل های حسابداری زیرمجموعه سرفصل داده شده را برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری والد</param>
        /// <returns>لیست اطلاعات خلاصه سرفصل های حسابداری زیرمجموعه</returns>
        // GET: api/accounts/{accountId:min(1)}/children
        [HttpGet]
        [Route(AccountApi.AccountChildrenUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountChildrenAsync(int accountId)
        {
            var children = await _repository.GetAccountChildrenAsync(accountId);
            return Json(children);
        }

        /// <summary>
        /// به روش آسنکرون، کد کامل سرفصل حسابداری مشخص شده با شناسه را برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر</param>
        /// <returns>کد کامل سرفصل حسابداری</returns>
        // GET: api/accounts/{accountId:int}/fullcode
        [HttpGet]
        [Route(AccountApi.AccountFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create | (int)AccountPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int accountId)
        {
            if (accountId <= 0)
            {
                return Ok(String.Empty);
            }

            string fullCode = await _repository.GetAccountFullCodeAsync(accountId);
            return Ok(fullCode);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل سرفصل های حسابداری را برمی گرداند
        /// </summary>
        /// <returns>تعداد کل سرفصل های حسابداری</returns>
        // GET: api/accounts/count
        [HttpGet]
        [Route(AccountApi.AccountsCount)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsCountAsync()
        {
            int itemsCount = await _repository.GetAllAccountsCountAsync();
            return Ok(itemsCount);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/accounts/lookup
        [HttpGet]
        [Route(AccountApi.AccountsLookupUrl)]
        public async Task<IActionResult> GetAccountsLookupAsync()
        {
            var accounts = await _repository.GetAccountsLookupAsync(GridOptions);
            Localize(accounts.Items);
            return JsonListResult(accounts);
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل حسابداری داده شده را ایجاد می کند
        /// </summary>
        /// <param name="account">اطلاعات کامل سرفصل حسابداری جدید</param>
        /// <returns>اطلاعات سرفصل حسابداری بعد از ایجاد در دیتابیس</returns>
        // POST: api/accounts
        [HttpPost]
        [Route(AccountApi.EnvironmentAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        public async Task<IActionResult> PostNewAccountAsync([FromBody] AccountFullDataViewModel account)
        {
            Verify.ArgumentNotNull(account, nameof(account));
            var message = _checkEdition.ValidateNewModel(account.Account, EditionLimit.AccountDepth);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            var result = await ValidationResultAsync(account.Account);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputAccount = await _repository.SaveAccountAsync(account);
            return StatusCode(StatusCodes.Status201Created, outputAccount);
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل حسابداری مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر برای اصلاح</param>
        /// <param name="account">اطلاعات اصلاح شده سرفصل حسابداری</param>
        /// <returns>اطلاعات سرفصل حسابداری بعد از اصلاح در دیتابیس</returns>
        // PUT: api/accounts/{accountId:min(1)}
        [HttpPut]
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Edit)]
        public async Task<IActionResult> PutModifiedAccountAsync(
            int accountId, [FromBody] AccountFullDataViewModel account)
        {
            var result = await ValidationResultAsync(account.Account, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputAccount = await _repository.SaveAccountAsync(account);
            return OkReadResult(outputAccount);
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل حسابداری مشخص شده با شناسه دیتابیسی را غیرفعال می کند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر برای غیرفعال کردن</param>
        // PUT: api/accounts/{accountId:min(1)}/deactivate
        [HttpPut]
        [Route(AccountApi.DeactivateAccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Deactivate)]
        public async Task<IActionResult> PutAccountAsDeactivated(int accountId)
        {
            return await UpdateActiveStateAsync(accountId, false);
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل حسابداری مشخص شده با شناسه دیتابیسی را فعال می کند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر برای فعال کردن</param>
        // PUT: api/accounts/{accountId:min(1)}/reactivate
        [HttpPut]
        [Route(AccountApi.ReactivateAccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Reactivate)]
        public async Task<IActionResult> PutAccountAsReactivated(int accountId)
        {
            return await UpdateActiveStateAsync(accountId, true);
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل حسابداری مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/accounts/{accountId:min(1)}
        [HttpDelete]
        [Route(AccountApi.AccountUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingAccountAsync(int accountId)
        {
            var result = await ValidateDeleteResultAsync(accountId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteAccountAsync(accountId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/accounts
        [HttpPut]
        [Route(AccountApi.EnvironmentAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        public async Task<IActionResult> PutExistingAccountsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteAccountsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی سرفصل حسابداری مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رفرنس بدون مقدار در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var account = await _repository.GetAccountAsync(item);
            if (account == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Account, item.ToString());
                return GetGroupActionResult(message, account);
            }

            var result = BranchValidationResult(account);
            if (result is BadRequestObjectResult errorResult)
            {
                return GetGroupActionResult(errorResult.Value.ToString(), account);
            }

            string accountInfo = String.Format("'{0} ({1})'", account.Name, account.FullCode);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = _strings.Format(AppStrings.CantDeleteNonLeafItem, AppStrings.Account, accountInfo);
            }
            else if (await _repository.IsUsedAccountAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteUsedItem, AppStrings.Account, accountInfo);
            }
            else if (await _repository.IsRelatedAccountAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteRelatedItem, AppStrings.Account, accountInfo);
            }
            else if (await _repository.IsUsedInAccountCollectionAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteUsedInAccountCollection, accountInfo);
            }

            return GetGroupActionResult(message, account);
        }

        private async Task<IActionResult> ValidationResultAsync(AccountViewModel account, int accountId = 0)
        {
            var result = BasicValidationResult(account, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (accountId == 0 && account.ParentId.HasValue)
            {
                var parent = await _repository.GetAccountAsync(account.ParentId.Value);
                if (!parent.IsActive)
                {
                    string message = _strings.Format(AppStrings.InactiveAccountCantHaveChildren, parent.Name);
                    return BadRequestResult(message);
                }
            }

            if (accountId > 0 && account.ChildCount > 0 && !account.IsActive)
            {
                string message = _strings.Format(AppStrings.ParentAccountCantBeInactive, account.Name);
                return BadRequestResult(message);
            }

            if (account.Level == 0 && !account.GroupId.HasValue)
            {
                return BadRequestResult(_strings.Format(AppStrings.AccountGroupIsRequired));
            }

            if (await _repository.IsAssociationChildAccountAsync(account))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.CantCreateAssociationChild, AppStrings.Account, account.Name));
            }

            if (await _repository.IsDuplicateFullCodeAsync(account))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateCodeValue, AppStrings.Account, account.FullCode));
            }

            if (await _repository.IsDuplicateNameAsync(account))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateNameValue, AppStrings.Account, account.Name));
            }

            if (account.ParentId.HasValue && await _repository.IsUsedAccountAsync(account.ParentId.Value))
            {
                var parent = await _repository.GetAccountAsync(account.ParentId.Value);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.Account, parentInfo));
            }

            if (account.ParentId.HasValue && !await _repository.CanHaveChildrenAsync(account.ParentId.Value))
            {
                return BadRequestResult(_strings.Format(AppStrings.CantInsertLeafAccount));
            }

            result = BranchValidationResult(account);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = ConfigValidationResult(account, _treeConfig.Current);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            return Ok();
        }

        private async Task<IActionResult> UpdateActiveStateAsync(int accountId, bool isActive)
        {
            var account = await _repository.GetAccountAsync(accountId);
            if (account == null)
            {
                string message = _strings.Format(
                    AppStrings.ItemByIdNotFound, EntityNameKey, accountId.ToString());
                return BadRequestResult(message);
            }

            var result = ActiveStateValidationResult(account);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = _repository as IActiveStateRepository<AccountViewModel>;
            await repository.SetActiveStatusAsync(account, isActive);
            return Ok();
        }

        private void Localize(IEnumerable<AccountViewModel> accounts)
        {
            foreach (var account in accounts)
            {
                account.TurnoverMode = _strings[account.TurnoverMode];
            }
        }

        private readonly IAccountRepository _repository;
        private readonly IConfigRepository _config;
        private readonly ViewTreeFullConfig _treeConfig;
        private readonly ICheckEdition _checkEdition;
    }
}
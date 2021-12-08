using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با گروه های حساب را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class AccountGroupsController : ValidatingController<AccountGroupViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات گروه های حساب در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        public AccountGroupsController(IAccountGroupRepository repository,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام موجودیت گروه حساب
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.AccountGroup; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه اطلاعات گروه های حساب تعریف شده را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده گروه های حساب</returns>
        // GET: api/accgroups
        [HttpGet]
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsAsync()
        {
            var accountGroups = await _repository.GetAccountGroupsAsync(GridOptions);
            return JsonListResult(accountGroups);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی گروه حساب مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر</param>
        /// <returns>اطلاعات نمایشی گروه حساب</returns>
        // GET: api/accgroups/{groupId:min(1)}
        [HttpGet]
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupAsync(int groupId)
        {
            var accountGroup = await _repository.GetAccountGroupAsync(groupId);
            return JsonReadResult(accountGroup);
        }

        /// <summary>
        /// به روش آسنکرون، حساب های کل زیرمجموعه گروه حساب داده شده را برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده حساب های کل زیرمجموعه</returns>
        // GET: api/accgroups/{groupId:min(1)}/accounts
        [HttpGet]
        [Route(AccountGroupApi.GroupLedgerAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetGroupLedgerAccountsAsync(int groupId)
        {
            var accounts = await _repository.GetGroupLedgerAccountsAsync(groupId, GridOptions);
            return JsonListResult(accounts);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه گروه های حساب تعریف شده را برمی گرداند
        /// </summary>
        /// <returns>فهرست گروه های حساب تعریف شده</returns>
        // GET: api/accgroups/brief
        [HttpGet]
        [Route(AccountGroupApi.AccountGroupBriefUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsBriefAsync()
        {
            var accGroups = await _repository.GetAccountGroupsBriefAsync();
            return JsonReadResult(accGroups);
        }

        /// <summary>
        /// به روش آسنکرون، گروه حساب داده شده را ایجاد می کند
        /// </summary>
        /// <param name="accountGroup">اطلاعات کامل گروه حساب جدید</param>
        /// <returns>اطلاعات گروه حساب بعد از ایجاد در دیتابیس</returns>
        // POST: api/accgroups
        [HttpPost]
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Create)]
        public async Task<IActionResult> PostNewAccountGroupAsync(
            [FromBody] AccountGroupViewModel accountGroup)
        {
            var result = await ValidationResultAsync(accountGroup);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveAccountGroupAsync(accountGroup);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، گروه حساب مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر برای اصلاح</param>
        /// <param name="accountGroup">اطلاعات اصلاح شده گروه حساب</param>
        /// <returns>اطلاعات گروه حساب بعد از اصلاح در دیتابیس</returns>
        // PUT: api/accgroups/{groupId:min(1)}
        [HttpPut]
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Edit)]
        public async Task<IActionResult> PutModifiedAccountGroupAsync(
            int groupId, [FromBody] AccountGroupViewModel accountGroup)
        {
            var result = await ValidationResultAsync(accountGroup, groupId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveAccountGroupAsync(accountGroup);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، گروه حساب مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/accgroups/{groupId:min(1)}
        [HttpDelete]
        [Route(AccountGroupApi.AccountGroupUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingAccountGroupAsync(int groupId)
        {
            var result = await ValidateDeleteResultAsync(groupId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteAccountGroupAsync(groupId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، گروه های حساب داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/accgroups
        [HttpPut]
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.Delete)]
        public async Task<IActionResult> PutExistingAccountGroupsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteAccountGroupsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی گروه حساب مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رفرنس بدون مقدار در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var accountGroup = await _repository.GetAccountGroupAsync(item);
            if (accountGroup == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.AccountGroup, item.ToString());
            }
            else
            {
                bool canDelete = await _repository.CanDeleteAccountGroupAsync(item);
                if (!canDelete)
                {
                    message = _strings.Format(AppStrings.CantDeleteUsedAccountGroup, accountGroup.Name);
                }
            }

            return GetGroupActionResult(message, accountGroup);
        }

        private async Task<IActionResult> ValidationResultAsync(AccountGroupViewModel accountGroup, int groupId = 0)
        {
            var result = BasicValidationResult(accountGroup, groupId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            bool isDuplicate = await _repository.IsDuplicateGroupAsync(accountGroup);
            if (isDuplicate)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.DuplicateNameValue, AppStrings.AccountGroup, accountGroup.Name));
            }

            return Ok();
        }

        private readonly IAccountGroupRepository _repository;
    }
}
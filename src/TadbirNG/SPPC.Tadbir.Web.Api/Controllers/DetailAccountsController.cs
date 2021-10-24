﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با تفصیلی های شناور را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class DetailAccountsController : ValidatingController<DetailAccountViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات تفصیلی های شناور در دیتابیس را فراهم می کند</param>
        /// <param name="config">امکان خواندن اطلاعات پیکربندی برنامه را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenService"></param>
        public DetailAccountsController(
            IDetailAccountRepository repository, IConfigRepository config,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewId.DetailAccount).Result;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام تفصیلی شناور
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.DetailAccount; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناور قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده تفصیلی های شناور</returns>
        // GET: api/faccounts
        [HttpGet]
        [Route(DetailAccountApi.EnvironmentDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetEnvironmentDetailAccountsAsync()
        {
            var detailAccounts = await _repository.GetDetailAccountsAsync(GridOptions);
            return JsonListResult(detailAccounts);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی تفصیلی شناور مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <returns>اطلاعات نمایشی تفصیلی شناور</returns>
        // GET: api/faccounts/{faccountId:min(1)}
        [HttpGet]
        [Route(DetailAccountApi.DetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountAsync(int faccountId)
        {
            var detailAccount = await _repository.GetDetailAccountAsync(faccountId);
            return JsonReadResult(detailAccount);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناور در بالاترین سطح را برمی گرداند
        /// </summary>
        /// <returns>لیست اطلاعات خلاصه تفصیلی های شناور در بالاترین سطح</returns>
        // GET: api/faccounts/root
        [HttpGet]
        [Route(DetailAccountApi.RootDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetRootDetailAccountsAsync()
        {
            var detailAccounts = await _repository.GetRootDetailAccountsAsync();
            return JsonReadResult(detailAccounts);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناور زیرمجموعه سرفصل داده شده را برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور والد</param>
        /// <returns>لیست اطلاعات خلاصه تفصیلی های شناور زیرمجموعه</returns>
        // GET: api/faccounts/{faccountId:min(1)}/children
        [HttpGet]
        [Route(DetailAccountApi.DetailAccountChildrenUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountChildrenAsync(int faccountId)
        {
            var children = await _repository.GetDetailAccountChildrenAsync(faccountId);
            return JsonReadResult(children);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور جدیدی زیرمجموعه شناور والد داده شده برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی شناور والد</param>
        /// <returns>اطلاعات پیشنهادی برای تفصیلی شناور جدید</returns>
        // GET: api/faccounts/{faccountId:int}/children/new
        [HttpGet]
        [Route(DetailAccountApi.NewChildDetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Create)]
        public async Task<IActionResult> GetNewDetailAccountAsync(int faccountId)
        {
            var newDetail = await _repository.GetNewChildDetailAccountAsync(
                faccountId > 0 ? faccountId : (int?)null);
            if (newDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ParentItemNotFound, AppStrings.DetailAccount));
            }

            if (newDetail.Level == -1)
            {
                return BadRequestResult(_strings.Format(AppStrings.ChildItemsNotAllowed, AppStrings.DetailAccount));
            }

            if (faccountId > 0 && await _repository.IsUsedDetailAccountAsync(faccountId))
            {
                var parent = await _repository.GetDetailAccountAsync(faccountId);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.DetailAccount, parentInfo));
            }

            return Json(newDetail);
        }

        /// <summary>
        /// به روش آسنکرون، کد کامل تفصیلی شناور مشخص شده با شناسه را برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <returns>کد کامل تفصیلی شناور</returns>
        // GET: api/faccounts/{faccountId:int}/fullcode
        [HttpGet]
        [Route(DetailAccountApi.DetailAccountFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Create | (int)DetailAccountPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int faccountId)
        {
            if (faccountId <= 0)
            {
                return Ok(String.Empty);
            }

            string fullCode = await _repository.GetDetailAccountFullCodeAsync(faccountId);

            return Ok(fullCode);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور داده شده را ایجاد می کند
        /// </summary>
        /// <param name="detailAccount">اطلاعات کامل تفصیلی شناور جدید</param>
        /// <returns>اطلاعات تفصیلی شناور بعد از ایجاد در دیتابیس</returns>
        // POST: api/faccounts
        [HttpPost]
        [Route(DetailAccountApi.EnvironmentDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Create)]
        public async Task<IActionResult> PostNewDetailAccountAsync([FromBody] DetailAccountViewModel detailAccount)
        {
            var result = await ValidationResultAsync(detailAccount);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveDetailAccountAsync(detailAccount);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر برای اصلاح</param>
        /// <param name="detailAccount">اطلاعات اصلاح شده تفصیلی شناور</param>
        /// <returns>اطلاعات تفصیلی شناور بعد از اصلاح در دیتابیس</returns>
        // PUT: api/faccounts/{faccountId:min(1)}
        [HttpPut]
        [Route(DetailAccountApi.DetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Edit)]
        public async Task<IActionResult> PutModifiedDetailAccountAsync(
            int faccountId, [FromBody] DetailAccountViewModel detailAccount)
        {
            var result = await ValidationResultAsync(detailAccount, faccountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveDetailAccountAsync(detailAccount);
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/faccounts/{faccountId:min(1)}
        [HttpDelete]
        [Route(DetailAccountApi.DetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingDetailAccountAsync(int faccountId)
        {
            var result = await ValidateDeleteResultAsync(faccountId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteDetailAccountAsync(faccountId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/faccounts
        [HttpPut]
        [Route(DetailAccountApi.EnvironmentDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Delete)]
        public async Task<IActionResult> PutExistingDetailAccountsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteDetailAccountsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی تفصیلی شناور مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رفرنس بدون مقدار در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var detailAccount = await _repository.GetDetailAccountAsync(item);
            if (detailAccount == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.DetailAccount, item.ToString());
                return GetGroupActionResult(message, detailAccount);
            }

            var result = BranchValidationResult(detailAccount);
            if (result is BadRequestObjectResult errorResult)
            {
                return GetGroupActionResult(errorResult.Value.ToString(), detailAccount);
            }

            var detailInfo = String.Format("'{0} ({1})'", detailAccount.Name, detailAccount.FullCode);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                    _strings[AppStrings.CantDeleteNonLeafItem], _strings[AppStrings.DetailAccount], detailInfo);
            }
            else if (await _repository.IsUsedDetailAccountAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CantDeleteUsedItem], _strings[AppStrings.DetailAccount], detailInfo);
            }
            else if (await _repository.IsRelatedDetailAccountAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CantDeleteRelatedItem], _strings[AppStrings.DetailAccount], detailInfo);
            }

            return GetGroupActionResult(message, detailAccount);
        }

        private async Task<IActionResult> ValidationResultAsync(DetailAccountViewModel detailAccount, int faccountId = 0)
        {
            var result = BasicValidationResult(detailAccount, faccountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateFullCodeAsync(detailAccount))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.DuplicateCodeValue, AppStrings.DetailAccount, detailAccount.FullCode));
            }

            if (await _repository.IsDuplicateNameAsync(detailAccount))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.DuplicateNameValue, AppStrings.DetailAccount, detailAccount.Name));
            }

            if (detailAccount.ParentId.HasValue
                && await _repository.IsUsedDetailAccountAsync(detailAccount.ParentId.Value))
            {
                var parent = await _repository.GetDetailAccountAsync(detailAccount.ParentId.Value);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.DetailAccount, parentInfo));
            }

            result = BranchValidationResult(detailAccount);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = ConfigValidationResult(detailAccount, _treeConfig.Current);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            return Ok();
        }

        private readonly IDetailAccountRepository _repository;
        private readonly IConfigRepository _config;
        private readonly ViewTreeFullConfig _treeConfig;
    }
}
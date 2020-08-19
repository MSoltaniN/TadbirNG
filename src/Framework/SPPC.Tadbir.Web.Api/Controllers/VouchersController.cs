﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با اسناد و آرتیکل های مالی را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class VouchersController : ValidatingController<VoucherViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات اسناد مالی را فراهم می کند</param>
        /// <param name="lineRepository">امکان مدیریت اطلاعات آرتیکل های مالی را فراهم می کند</param>
        /// <param name="relationRepository">امکان خواندن ارتباطات موجود در  بردار حساب را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        public VouchersController(
            IVoucherRepository repository,
            IVoucherLineRepository lineRepository,
            IRelationRepository relationRepository,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _lineRepository = lineRepository;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام اسناد مالی
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Voucher; }
        }

        #region Voucher Operations

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده اسناد مالی</returns>
        // GET: api/vouchers
        [HttpGet]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ManageVouchersPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVouchersAsync()
        {
            var vouchers = await _repository.GetVouchersAsync(GridOptions);
            Localize(vouchers.Items);
            return JsonListResult(vouchers);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی سند مالی مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر</param>
        /// <returns>اطلاعات نمایشی سند مالی</returns>
        // GET: api/vouchers/{voucherId:int}
        [HttpGet]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherAsync(int voucherId)
        {
            var voucher = await _repository.GetVoucherAsync(voucherId);
            Localize(voucher);
            return JsonReadResult(voucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی جدیدی با مقادیر پیشنهادی در دیتابیس ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند مالی جدید با مقادیر پیشنهادی</returns>
        // GET: api/vouchers/new
        [HttpGet]
        [Route(VoucherApi.NewVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public async Task<IActionResult> GetNewVoucherAsync()
        {
            bool isChecked = await _repository.IsCurrentSpecialVoucherCheckedAsync(
                VoucherOriginValue.ClosingVoucher);
            if (isChecked)
            {
                return BadRequest(_strings[AppStrings.CurrentClosingVoucherIsChecked]);
            }

            var newVoucher = await _repository.GetNewVoucherAsync();
            return Json(newVoucher);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی مورد نظر</param>
        /// <returns>اطلاعات نمایشی سند مالی مورد نظر</returns>
        // GET: api/vouchers/by-no
        [HttpGet]
        [Route(VoucherApi.VoucherByNoUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherByNoAsync(int voucherNo)
        {
            var voucherByNo = await _repository.GetVoucherByNoAsync(voucherNo);
            Localize(voucherByNo);
            return JsonReadResult(voucherByNo);
        }

        /// <summary>
        /// به روش آسنکرون، محدوده شماره سندهای قابل دسترسی توسط کاربر جاری را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات محدوده شماره سندها</returns>
        // GET: api/vouchers/range
        [HttpGet]
        [Route(VoucherApi.EnvironmentItemRangeUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVoucherRangeAsync()
        {
            var range = await _repository.GetVoucherRangeInfoAsync();
            return Json(range);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین سند مالی قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی اولین سند مالی قابل دسترسی</returns>
        // GET: api/vouchers/first
        [HttpGet]
        [Route(VoucherApi.FirstVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetFirstVoucherAsync()
        {
            var first = await _repository.GetFirstVoucherAsync();
            Localize(first);
            return JsonReadResult(first);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی پیش از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی فعلی</param>
        /// <returns>اطلاعات نمایشی سند مالی قابل دسترسی قبلی</returns>
        // GET: api/vouchers/{voucherNo:min(1)}/previous
        [HttpGet]
        [Route(VoucherApi.PreviousVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousVoucherAsync(int voucherNo)
        {
            var previous = await _repository.GetPreviousVoucherAsync(voucherNo);
            Localize(previous);
            return JsonReadResult(previous);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعد از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی فعلی</param>
        /// <returns>اطلاعات نمایشی سند مالی قابل دسترسی بعدی</returns>
        // GET: api/vouchers/{voucherNo:min(1)}/next
        [HttpGet]
        [Route(VoucherApi.NextVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetNextVoucherAsync(int voucherNo)
        {
            var next = await _repository.GetNextVoucherAsync(voucherNo);
            Localize(next);
            return JsonReadResult(next);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین سند مالی قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی آخرین سند مالی قابل دسترسی</returns>
        // GET: api/vouchers/last
        [HttpGet]
        [Route(VoucherApi.LastVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetLastVoucherAsync()
        {
            var last = await _repository.GetLastVoucherAsync();
            Localize(last);
            return JsonReadResult(last);
        }

        // GET: api/vouchers/count/by-status
        [HttpGet]
        [Route(VoucherApi.VoucherCountByStatusUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetVouchersCountByStatusIdAsync()
        {
            int itemCount = await _repository.GetCountAsync<VoucherViewModel>(GridOptions);

            return Ok(itemCount);
        }

        // GET: api/vouchers/no-article
        [HttpGet]
        [Route(VoucherApi.VoucherWithNoArticleUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetVouchersWithNoArticleAsync(DateTime from, DateTime to)
        {
            var (vouchers, itemCount) = await _repository.GetVouchersWithNoArticleAsync(GridOptions, from, to);
            SetItemCount(itemCount);
            Localize(vouchers.ToArray());
            SetRowNumbers(vouchers);
            return Json(vouchers);
        }

        // GET: api/vouchers/unbalanced
        [HttpGet]
        [Route(VoucherApi.UnbalancedVouchers)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetUnbalancedVouchersAsync(DateTime from, DateTime to)
        {
            var (vouchers, itemCount) = await _repository.GetUnbalancedVouchersAsync(GridOptions, from, to);
            SetItemCount(itemCount);
            Localize(vouchers.ToArray());
            SetRowNumbers(vouchers);
            return Json(vouchers);
        }

        // GET: api/vouchers/miss-number
        [HttpGet]
        [Route(VoucherApi.MissingVoucherNumberUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetMissingVoucherNumbersAsync(DateTime from, DateTime to)
        {
            var (voucherNumbers, itemCount) = await _repository.GetMissingVoucherNumbersAsync(GridOptions, from, to);
            SetItemCount(itemCount);
            SetRowNumbers(voucherNumbers);
            return Json(voucherNumbers);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="voucher">اطلاعات کامل سند مالی جدید</param>
        /// <returns>اطلاعات سند مالی بعد از ایجاد در دیتابیس</returns>
        // POST: api/vouchers
        [HttpPost]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public async Task<IActionResult> PostNewVoucherAsync([FromBody] VoucherViewModel voucher)
        {
            var result = await VoucherValidationResultAsync(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputVoucher = await _repository.SaveVoucherAsync(voucher);
            return StatusCode(StatusCodes.Status201Created, outputVoucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر برای اصلاح</param>
        /// <param name="voucher">اطلاعات اصلاح شده سند مالی</param>
        /// <returns>اطلاعات سند مالی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/vouchers/{voucherId:int}
        [HttpPut]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public async Task<IActionResult> PutModifiedVoucherAsync(
            int voucherId, [FromBody] VoucherViewModel voucher)
        {
            var result = await VoucherValidationResultAsync(voucher, voucherId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = CheckedValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (voucher.SaveCount == 0)
            {
                await _repository.SetVoucherDailyNoAsync(voucher);
            }

            var outputVoucher = await _repository.SaveVoucherAsync(voucher);
            result = (outputVoucher != null)
                ? Ok(outputVoucher)
                : NotFound() as IActionResult;
            return result;
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت ثبت سند مشخص شده را به ثبت عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای ثبت</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/check
        [HttpPut]
        [Route(VoucherApi.CheckVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Check)]
        public async Task<IActionResult> PutExistingVoucherAsChecked(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Check);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusValue.Checked);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند ثبت عادی را برگشت داده و وضعیتش را به ثبت نشده تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از ثبت</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/check/undo
        [HttpPut]
        [Route(VoucherApi.UndoCheckVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoCheck)]
        public async Task<IActionResult> PutExistingVoucherAsUnchecked(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoCheck);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusValue.NotChecked);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را در حالت تأییدشده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/confirm
        [HttpPut]
        [Route(VoucherApi.ConfirmVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public async Task<IActionResult> PutExistingVoucherAsConfirmed(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Confirm);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherConfirmationAsync(voucherId, true);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را برگشت از تأیید کرده و وضعیتش را در حالت تأییدنشده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/confirm/undo
        [HttpPut]
        [Route(VoucherApi.UndoConfirmVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingVoucherAsUnconfirmed(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoConfirm);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherConfirmationAsync(voucherId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را در حالت تصویب شده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/approve
        [HttpPut]
        [Route(VoucherApi.ApproveVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Approve)]
        public async Task<IActionResult> PutExistingVoucherAsApproved(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Approve);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherApprovalAsync(voucherId, true);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را برگشت از تصویب کرده و وضعیتش را در حالت تصویب نشده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/approve/undo
        [HttpPut]
        [Route(VoucherApi.UndoApproveVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingVoucherAsUnapproved(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoApprove);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherApprovalAsync(voucherId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت ثبت سند مشخص شده را به ثبت قطعی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای ثبت قطعی</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/finalize
        [HttpPut]
        [Route(VoucherApi.FinalizeVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Finalize)]
        public async Task<IActionResult> PutExistingVoucherAsFinalized(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Finalize);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusValue.Finalized);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند ثبت قطعی را برگشت داده و وضعیتش را به ثبت عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از ثبت قطعی</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/finalize/undo
        [HttpPut]
        [Route(VoucherApi.UndoFinalizeVoucherUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutExistingVoucherAsUnfinalized(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoFinalize);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusValue.Checked);
            return Ok();
        }

        /// <summary>
        /// ثبت گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده </param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.CheckVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ManageVouchersPermissions.GroupCheck)]
        public async Task<IActionResult> PutExistingVouchersAsChecked([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.Check, DocumentStatusValue.Checked);
        }

        /// <summary>
        /// برگشت از ثبت گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.UndoCheckVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ManageVouchersPermissions.GroupUndoCheck)]
        public async Task<IActionResult> PutExistingVouchersAsUnChecked([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.UndoCheck, DocumentStatusValue.NotChecked);
        }

        /// <summary>
        /// تایید گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        // PUT: api/vouchers/confirm
        [HttpPut]
        [Route(VoucherApi.ConfirmVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ManageVouchersPermissions.GroupConfirm)]
        public async Task<IActionResult> PutExistingVouchersAsConfirmed([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupConfirmApproveResultAsync(
                actionDetail.Items, AppStrings.GroupConfirmApprove);
        }

        /// <summary>
        /// برگشت از تایید گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        // PUT: api/vouchers/confirm/undo
        [HttpPut]
        [Route(VoucherApi.UndoConfirmVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ManageVouchersPermissions.GroupUndoConfirm)]
        public async Task<IActionResult> PutExistingVouchersAsUnConfirmed([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupConfirmApproveResultAsync(
                actionDetail.Items, AppStrings.GroupUndoConfirmApprove);
        }

        /// <summary>
        /// ثبت قطعی گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.FinalizeVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ManageVouchersPermissions.GroupFinalize)]
        public async Task<IActionResult> PutExistingVouchersAsFinalized([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.Finalize, DocumentStatusValue.Finalized);
        }

        /// <summary>
        /// برگشت از ثبت قطعی گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.UndoFinalizeVouchersUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutExistingVouchersAsUnfinalized([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.UndoFinalize, DocumentStatusValue.Checked);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/vouchers/{voucherId:int}
        [HttpDelete]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingVoucherAsync(int voucherId)
        {
            var result = await ValidateDeleteResultAsync(voucherId);
            if (result != null)
            {
                return BadRequest(result.ErrorMessage);
            }

            await _repository.DeleteVoucherAsync(voucherId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، اسناد مالی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/vouchers
        [HttpPut]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> PutExistingVouchersAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteVouchersAsync);
        }

        // GET: api/vouchers/opening/query
        [HttpGet]
        [Route(VoucherApi.OpeningVoucherQueryUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOpeningVoucherAsync()
        {
            var openingVoucher = await _repository.GetOpeningVoucherAsync(true);
            if (openingVoucher != null)
            {
                Localize(openingVoucher);
                return Json(openingVoucher);
            }
            else
            {
                bool hasPrevious = await _repository.HasPreviousClosingVoucherAsync();
                bool needsPrompt = !hasPrevious;
                return Json(needsPrompt);
            }
        }

        // GET: api/vouchers/opening?isDefault={bool}
        [HttpGet]
        [Route(VoucherApi.OpeningVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueOpeningVoucherAsync(bool? isDefault)
        {
            var result = await SpecialVoucherValidationResultAsync(AppStrings.OpeningVoucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            bool isDefaultVoucher = isDefault ?? true;
            var openingVoucher = await _repository.GetOpeningVoucherAsync(false, isDefaultVoucher);
            Localize(openingVoucher);
            return Json(openingVoucher);
        }

        // GET: api/vouchers/closing-tmp
        [HttpGet]
        [Route(VoucherApi.ClosingAccountsVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueClosingAccountsVoucherAsync()
        {
            var result = await SpecialVoucherValidationResultAsync(AppStrings.ClosingTempAccounts);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var closingAccountsVoucher = await _repository.GetClosingTempAccountsVoucherAsync();
            Localize(closingAccountsVoucher);
            return Json(closingAccountsVoucher);
        }

        // PUT: api/vouchers/closing-tmp
        [HttpPut]
        [Route(VoucherApi.ClosingAccountsVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> PutOrIssueClosingAccountsVoucherAsync(
            [FromBody] IList<AccountBalanceViewModel> balanceItems)
        {
            var result = await SpecialVoucherValidationResultAsync(AppStrings.ClosingTempAccounts);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var closingAccountsVoucher = await _repository.GetPeriodicClosingTempAccountsVoucherAsync(balanceItems);
            Localize(closingAccountsVoucher);
            return Json(closingAccountsVoucher);
        }

        // GET: api/vouchers/closing
        [HttpGet]
        [Route(VoucherApi.ClosingVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueClosingVoucherAsync()
        {
            var result = await ClosingVoucherValidationResultAsync();
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var closingVoucher = await _repository.GetClosingVoucherAsync();
            Localize(closingVoucher);
            return Json(closingVoucher);
        }

        #endregion

        #region Article Operations

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های سند داده شده را برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های سند</returns>
        // GET: api/vouchers/{voucherId:min(1)}/articles
        [HttpGet]
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticlesAsync(int voucherId)
        {
            var articles = await _lineRepository.GetArticlesAsync(voucherId, GridOptions);
            SetItemCount(articles.TotalCount);
            Localize(articles.Items);
            return Json(articles.Items);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آرتیکل مالی مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر</param>
        /// <returns>اطلاعات نمایشی آرتیکل مالی</returns>
        // GET: api/vouchers/articles/{articleId:min(1)}
        [HttpGet]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticleAsync(int articleId)
        {
            var article = await _lineRepository.GetArticleAsync(articleId);
            Localize(article);
            return JsonReadResult(article);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل آرتیکل های موجود را برمی گرداند
        /// </summary>
        /// <returns>تعداد کل آرتیکل های موجود</returns>
        // GET: api/vouchers/articles/count
        [HttpGet]
        [Route(VoucherApi.VoucherArticlesCountUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticlesCountAsync()
        {
            int itemsCount = await _lineRepository.GetAllArticlesCountAsync();
            return Ok(itemsCount);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی دارای اشکال داده شده را برمی گرداند
        /// </summary>
        /// <param name="issueType">نوع اشکال سیستمی مورد نظر برای آرتیکل های مالی</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <returns>اطلاعات نمایشی آرتیکل های مالی دارای مشکل داده شده</returns>
        // GET: api/vouchers/articles/sys-issue/{issueType}
        [HttpGet]
        [Route(VoucherApi.SystemIssueArticlesUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetSystemIssueArticlesAsync(string issueType, DateTime from, DateTime to)
        {
            var (articles, itemCount) = await _lineRepository.GetSystemIssueArticlesAsync(
                GridOptions, issueType, from, to);
            SetItemCount(itemCount);
            if (issueType != "invalid-acc")
            {
                SetRowNumbers(articles);
            }

            return Json(articles);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی</param>
        /// <param name="article">اطلاعات کامل آرتیکل مالی جدید</param>
        /// <returns>اطلاعات آرتیکل مالی بعد از ایجاد در دیتابیس</returns>
        // POST: api/vouchers/{voucherId:min(1)}/articles
        [HttpPost]
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.CreateLine))]
        public async Task<IActionResult> PostNewArticleAsync(
            int voucherId, [FromBody] VoucherLineViewModel article)
        {
            int id = voucherId; // Prevent unused argument warning
            var result = VoucherLineValidationResultAsync(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await FullAccountValidationResult(article.FullAccount, _relationRepository);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputLine = await _lineRepository.SaveArticleAsync(article);
            return StatusCode(StatusCodes.Status201Created, outputLine);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر برای اصلاح</param>
        /// <param name="article">اطلاعات اصلاح شده آرتیکل مالی</param>
        /// <returns>اطلاعات آرتیکل مالی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/vouchers/articles/{articleId:min(1)}
        [HttpPut]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.EditLine))]
        public async Task<IActionResult> PutModifiedArticleAsync(
            int articleId, [FromBody] VoucherLineViewModel article)
        {
            var result = VoucherLineValidationResultAsync(article, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputLine = await _lineRepository.SaveArticleAsync(article);
            result = (outputLine != null)
                ? Ok(outputLine)
                : NotFound() as IActionResult;
            return result;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات علامتگذاری آرتیکل مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی</param>
        /// <param name="mark">اطلاعات اصلاح شده علامتگذاری آرتیکل مالی</param>
        /// <returns>در صورت بروز خطا، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
        // PUT: api/vouchers/articles/{articleId:min(1)}/mark
        [HttpPut]
        [Route(VoucherApi.VoucherArticleMarkUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.Mark)]
        public async Task<IActionResult> PutModifiedArticleMarkAsync(
            int articleId, [FromBody] VoucherLineMarkViewModel mark)
        {
            var result = BasicValidationResult(mark, AppStrings.VoucherLineMark, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _lineRepository.SaveArticleMarkAsync(mark);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/vouchers/articles/{articleId:min(1)}
        [HttpDelete]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.DeleteLine))]
        public async Task<IActionResult> DeleteExistingArticleAsync(int articleId)
        {
            var result = await ValidateLineDeleteResultAsync(articleId);
            if (result != null)
            {
                return BadRequest(result.ErrorMessage);
            }

            await _lineRepository.DeleteArticleAsync(articleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/vouchers/articles
        [HttpPut]
        [Route(VoucherApi.AllVoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.DeleteLine))]
        public async Task<IActionResult> PutExistingArticlesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteLineResultAsync(actionDetail, _lineRepository.DeleteArticlesAsync);
        }

        #endregion

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سند مالی مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>پیغام خطای به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int voucherId)
        {
            string message = String.Empty;
            var voucher = await _repository.GetVoucherAsync(voucherId);
            if (voucher == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Voucher), voucherId);
                return GetGroupActionResult(message, voucher);
            }

            var result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult branchError)
            {
                message = branchError.Value.ToString();
                return GetGroupActionResult(message, voucher);
            }

            result = CheckedValidationResult(voucher);
            if (result is BadRequestObjectResult statusError)
            {
                message = statusError.Value.ToString();
            }

            return GetGroupActionResult(message, voucher);
        }

        private static bool IsVoucherMainAction(string action)
        {
            return action == AppStrings.Check
                || action == AppStrings.Confirm
                || action == AppStrings.Approve
                || action == AppStrings.Finalize
                || action == AppStrings.UndoFinalize;
        }

        private async Task<IActionResult> GroupStatusChangeResultAsync(
            IEnumerable<int> items, string action, DocumentStatusValue status)
        {
            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            foreach (int item in items)
            {
                var result = await _repository.ValidateVoucherActionAsync(item, action);
                if (result == null)
                {
                    validated.Add(item);
                }
                else
                {
                    notValidated.Add(result);
                }
            }

            await _repository.SetVouchersStatusAsync(validated, status);
            return Ok(notValidated);
        }

        private async Task<IActionResult> GroupConfirmApproveResultAsync(
            IEnumerable<int> items, string action)
        {
            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            bool isConfirmed = (action == AppStrings.GroupConfirmApprove);
            foreach (int item in items)
            {
                var result = await _repository.ValidateVoucherActionAsync(item, action);
                if (result == null)
                {
                    validated.Add(item);
                }
                else
                {
                    notValidated.Add(result);
                }
            }

            await _repository.SetVouchersConfirmApproveStatusAsync(validated, isConfirmed);
            return Ok(notValidated);
        }

        private IActionResult BasicValidationResult<TModel>(TModel model, string modelType, int modelId = 0)
        {
            if (model == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, modelType));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = (int)Reflector.GetProperty(model, "Id");
            if (modelId != id)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, modelType));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherValidationResultAsync(VoucherViewModel voucher, int voucherId = 0)
        {
            var result = BasicValidationResult(voucher, AppStrings.Voucher, voucherId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateVoucherNoAsync(voucher))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.VoucherNo));
            }

            if (await _repository.IsDuplicateVoucherDailyNoAsync(voucher))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.DailyNo));
            }

            var fiscalPeriod = await _repository.GetVoucherFiscalPeriodAsync(voucher);
            if (fiscalPeriod == null
                || voucher.Date < fiscalPeriod.StartDate
                || voucher.Date > fiscalPeriod.EndDate)
            {
                return BadRequest(_strings.Format(AppStrings.OutOfFiscalPeriodDate));
            }

            return Ok();
        }

        private IActionResult VoucherLineValidationResultAsync(
            VoucherLineViewModel article, int articleId = 0)
        {
            var result = BasicValidationResult(article, AppStrings.VoucherLine, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(_strings.Format(AppStrings.DebitAndCreditNotAllowed));
            }

            return Ok();
        }

        private async Task<IActionResult> ClosingVoucherValidationResultAsync()
        {
            var result = await SpecialVoucherValidationResultAsync(AppStrings.ClosingVoucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            // Current fiscal period MUST have the closing temp accounts voucher
            if (!await _repository.IsCurrentSpecialVoucherCheckedAsync(
                VoucherOriginValue.ClosingTempAccounts))
            {
                return BadRequest(_strings[AppStrings.ClosingAccountsVoucherNotIssuedOrChecked]);
            }

            return Ok();
        }

        private async Task<IActionResult> SpecialVoucherValidationResultAsync(string typeKey)
        {
            // Rule 1 : Current branch MUST be the highest-level branch in hierarchy...
            if (!await _repository.CanIssueSpecialVoucherAsync(SecurityContext.User.BranchId))
            {
                string message = _strings.Format(AppStrings.CantIssueVoucherFromLowerBranch, typeKey);
                return BadRequest(message);
            }

            if (typeKey == AppStrings.ClosingTempAccounts)
            {
                // Rule 2 : Current fiscal period MUST NOT have any unchecked vouchers
                int uncheckedCount = await _repository.GetCountByStatusAsync(DocumentStatusValue.NotChecked);
                if (uncheckedCount > 0)
                {
                    return BadRequest(_strings[AppStrings.CantIssueClosingVoucherWithUncheckedVouchers]);
                }
            }

            return Ok();
        }

        private async Task<GroupActionResultViewModel> ValidateLineDeleteResultAsync(int articleId)
        {
            string message = String.Empty;
            var voucherLine = await _lineRepository.GetArticleAsync(articleId);
            if (voucherLine == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.VoucherLine), articleId);
            }
            else
            {
                var result = BranchValidationResult(voucherLine);
                if (result is BadRequestObjectResult errorResult)
                {
                    message = errorResult.Value.ToString();
                }
            }

            return GetGroupActionResult(message, voucherLine);
        }

        private IActionResult CheckedValidationResult(VoucherViewModel voucher)
        {
            if (voucher.StatusId != (int)DocumentStatusValue.NotChecked)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifyCheckedDocument, AppStrings.Voucher));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherActionValidationResultAsync(int voucherId, string action)
        {
            var error = await _repository.ValidateVoucherActionAsync(voucherId, action);
            if (error != null)
            {
                return BadRequest(error.ErrorMessage);
            }

            if (IsVoucherMainAction(action))
            {
                int lineCount = await _lineRepository.GetArticleCountAsync<VoucherLineViewModel>(voucherId);
                if (lineCount == 0)
                {
                    return BadRequest(_strings.Format(AppStrings.InvalidEmptyVoucherAction, action));
                }
            }

            return Ok();
        }

        private async Task<IActionResult> GroupDeleteLineResultAsync(
            ActionDetailViewModel actionDetail, GroupDeleteAsyncDelegate groupDelete)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            foreach (int item in actionDetail.Items)
            {
                var result = await ValidateLineDeleteResultAsync(item);
                if (result == null)
                {
                    validated.Add(item);
                }
                else
                {
                    notValidated.Add(result);
                }
            }

            if (validated.Count > 0)
            {
                await groupDelete(validated);
            }

            return Ok(notValidated);
        }

        private void Localize(IEnumerable<VoucherViewModel> vouchers)
        {
            foreach (var voucher in vouchers)
            {
                Localize(voucher);
            }
        }

        private void Localize(VoucherViewModel voucher)
        {
            if (voucher != null)
            {
                voucher.StatusName = _strings[voucher.StatusName ?? String.Empty];
                voucher.OriginName = _strings[voucher.OriginName ?? String.Empty];
                voucher.Description = _strings[voucher.Description ?? String.Empty];
            }
        }

        private void Localize(IEnumerable<VoucherLineViewModel> voucherLines)
        {
            foreach (var voucherLine in voucherLines)
            {
                Localize(voucherLine);
            }
        }

        private void Localize(VoucherLineViewModel voucherLine)
        {
            if (voucherLine != null)
            {
                voucherLine.CurrencyName = _strings[voucherLine.CurrencyName ?? String.Empty];
                voucherLine.Description = _strings[voucherLine.Description ?? String.Empty];
            }
        }

        private readonly IVoucherRepository _repository;
        private readonly IVoucherLineRepository _lineRepository;
        private readonly IRelationRepository _relationRepository;
    }
}

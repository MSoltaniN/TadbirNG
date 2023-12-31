﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// امکانات مورد نیاز برای اعتبارسنجی عملیات سرویس وب را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TViewModel">نوع مدل نمایشی اصلی مورد نیاز برای عملیات سرویس</typeparam>
    public abstract class ValidatingController<TViewModel> : ApiControllerBase
        where TViewModel : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        protected ValidatingController(IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام موجودیت اصلی مدیریت شده در سرویس
        /// </summary>
        protected abstract string EntityNameKey
        {
            get;
        }

        /// <summary>
        /// قواعد اعتبارسنجی پایه ای را روی آبجکت داده شده بررسی می کند
        /// و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <param name="item">آبجکت داده شده برای اعتبارسنجی</param>
        /// <param name="itemId">شناسه دیتابیسی آبجکت مشخص شده در آدرس وب درخواست</param>
        /// <returns>در صورت نبود خطای اعتبارسنجی کد وضعیتی 200 و در غیر این صورت
        /// متن خطا را با کد وضعیتی 400 برای درخواست نامعتبر برمی گرداند</returns>
        protected virtual IActionResult BasicValidationResult(TViewModel item, int itemId = 0)
        {
            return BasicValidationResult(item, itemId, null);
        }

        /// <summary>
        /// قواعد اعتبارسنجی پایه ای را روی آبجکت داده شده بررسی می کند
        /// و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <param name="item">آبجکت داده شده برای اعتبارسنجی</param>
        /// <param name="itemId">شناسه دیتابیسی آبجکت مشخص شده در آدرس وب درخواست</param>
        /// <param name="entityKey">عنوان انتیتی که پیش فرض با پراپرتی نام موجودیت پر می شود</param>
        /// <returns>در صورت نبود خطای اعتبارسنجی کد وضعیتی 200 و در غیر این صورت
        /// متن خطا را با کد وضعیتی 400 برای درخواست نامعتبر برمی گرداند</returns>
        protected virtual IActionResult BasicValidationResult(TViewModel item, int itemId, string entityKey = null)
        {
            return GetBasicValidationResult(item, itemId, entityKey);
        }

        /// <summary>
        /// قواعد اعتبارسنجی پایه ای را روی آبجکت داده شده بررسی می کند
        /// و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <typeparam name="TOtherModel">نوع مدل نمایشی ثانویه مدیریت شده توسط سرویس</typeparam>
        /// <param name="item">آبجکت داده شده برای اعتبارسنجی</param>
        /// <param name="itemId">شناسه دیتابیسی آبجکت مشخص شده در آدرس وب درخواست</param>
        /// <returns>در صورت نبود خطای اعتبارسنجی کد وضعیتی 200 و در غیر این صورت
        /// متن خطا را با کد وضعیتی 400 برای درخواست نامعتبر برمی گرداند</returns>
        protected virtual IActionResult BasicValidationResult<TOtherModel>(TOtherModel item, int itemId = 0)
        {
            return GetBasicValidationResult(item, itemId);
        }

        /// <summary>
        /// قواعد اعتبارسنجی دسترسی شعبه را روی آبجکت داده شده بررسی می کند
        /// و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <typeparam name="TFiscalView">نوع مدل نمایشی مالی مدیریت شده توسط سرویس</typeparam>
        /// <param name="item">آبجکت داده شده برای اعتبارسنجی</param>
        /// <returns>در صورت نبود خطای اعتبارسنجی کد وضعیتی 200 و در غیر این صورت
        /// متن خطا را با کد وضعیتی 400 برای درخواست نامعتبر برمی گرداند</returns>
        protected IActionResult BranchValidationResult<TFiscalView>(TFiscalView item)
            where TFiscalView : class, IFiscalEntity
        {
            Verify.ArgumentNotNull(item, nameof(item));
            var currentContext = SecurityContext.User;
            if (item.BranchId != currentContext.BranchId)
            {
                return BadRequestResult(_strings[AppStrings.OtherBranchEditNotAllowed]);
            }

            return Ok();
        }

        /// <summary>
        /// قواعد اعتبارسنجی ساختارهای درختی را روی آبجکت داده شده بررسی می کند
        /// و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <typeparam name="TTreeView">نوع مدل نمایشی درختی مدیریت شده توسط سرویس وب</typeparam>
        /// <param name="item">آبجکت داده شده برای اعتبارسنجی</param>
        /// <param name="treeConfig">اطلاعات تنظیمات ساختار درختی</param>
        /// <returns>در صورت نبود خطای اعتبارسنجی کد وضعیتی 200 و در غیر این صورت
        /// متن خطا را با کد وضعیتی 400 برای درخواست نامعتبر برمی گرداند</returns>
        protected IActionResult ConfigValidationResult<TTreeView>(TTreeView item, ViewTreeConfig treeConfig)
            where TTreeView : class, ITreeEntityView
        {
            Verify.ArgumentNotNull(treeConfig, nameof(treeConfig));
            if (item.Level == treeConfig.MaxDepth)
            {
                string message = String.Format(_strings[AppStrings.TreeLevelsAreTooDeep],
                    treeConfig.MaxDepth, _strings[EntityNameKey]);
                return BadRequestResult(message);
            }

            var levelConfig = treeConfig.Levels[item.Level];
            int codeLength = levelConfig.CodeLength;
            if (item.Code.Length != codeLength)
            {
                string message = String.Format(_strings[AppStrings.LevelCodeLengthIsIncorrect],
                    _strings[EntityNameKey], levelConfig.Name, levelConfig.CodeLength);
                return BadRequestResult(message);
            }

            var invalidCode = new string('0', codeLength);
            if (item.Code == invalidCode)
            {
                string message = String.Format(_strings[AppStrings.InvalidLevelCode], item.Code);
                return BadRequestResult(message);
            }

            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، قواعد اعتبارسنجی بردارهای حساب را روی آبجکت داده شده بررسی می کند
        /// و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <param name="fullAccount">بردار حساب داده شده برای اعتبارسنجی</param>
        /// <param name="repository">امکان خواندن وضعیت بردارهای حساب را فراهم می کند</param>
        /// <returns>در صورت نبود خطای اعتبارسنجی کد وضعیتی 200 و در غیر این صورت
        /// متن خطا را با کد وضعیتی 400 برای درخواست نامعتبر برمی گرداند</returns>
        protected async Task<IActionResult> FullAccountValidationResultAsync(
            FullAccountViewModel fullAccount, IRelationRepository repository)
        {
            Verify.ArgumentNotNull(repository, nameof(repository));
            var lookupResult = await repository.LookupFullAccountAsync(fullAccount);
            if (!String.IsNullOrEmpty(lookupResult))
            {
                return BadRequestResult(_strings.Format(lookupResult));
            }

            return Ok();
        }

        /// <summary>
        /// قواعد اعتبارسنجی مربوط به فعال یا غیرفعال کردن را روی آبجکت داده شده بررسی می کند
        /// و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <typeparam name="TFiscalView">نوع مدل نمایشی برای یکی از موجودیت های پایه</typeparam>
        /// <param name="item">آبجکت داده شده برای اعتبارسنجی</param>
        /// <returns>در صورت نبود خطای اعتبارسنجی کد وضعیتی 200 و در غیر این صورت
        /// متن خطا را با کد وضعیتی 400 برای درخواست نامعتبر برمی گرداند</returns>
        protected IActionResult ActiveStateValidationResult<TFiscalView>(TFiscalView item)
            where TFiscalView : class, IFiscalEntity
        {
            Verify.ArgumentNotNull(item, nameof(item));
            if (item.BranchId != SecurityContext.User.BranchId)
            {
                var message = _strings.Format(AppStrings.ActiveStateBranchError, EntityNameKey);
                return BadRequestResult(message);
            }

            if (item.FiscalPeriodId > SecurityContext.User.FiscalPeriodId)
            {
                var message = _strings.Format(AppStrings.ActiveStateFPeriodError, EntityNameKey);
                return BadRequestResult(message);
            }

            if (item is ITreeEntityView treeItem)
            {
                if (treeItem.ChildCount > 0)
                {
                    var message = _strings.Format(AppStrings.ActiveStateChildError, EntityNameKey);
                    return BadRequestResult(message);
                }
            }

            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionDetail"></param>
        /// <param name="groupDelete"></param>
        /// <returns></returns>
        protected async Task<IActionResult> GroupDeleteResultAsync(
            ActionDetailViewModel actionDetail, GroupDeleteAsyncDelegate groupDelete)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            foreach (int item in actionDetail.Items)
            {
                var result = await ValidateDeleteResultAsync(item);
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

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="error"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected GroupActionResultViewModel GetGroupActionResult<TModel>(string error, TModel model)
            where TModel : class, new()
        {
            var result = String.IsNullOrEmpty(error)
                ? null
                : new GroupActionResultViewModel() { ErrorMessage = error };
            if (result != null && model != null)
            {
                object value = Reflector.GetSimpleProperty(model, AppStrings.Id, false);
                result.Id = (value != null) ? Int32.Parse(value.ToString()) : 0;
                value = Reflector.GetSimpleProperty(model, AppStrings.Name, false);
                result.Name = _strings[value?.ToString() ?? String.Empty];
                value = Reflector.GetSimpleProperty(model, AppStrings.FullCode, false);
                result.FullCode = value?.ToString();
                value = Reflector.GetSimpleProperty(model, AppStrings.No, false);
                result.No = (value != null) ? Int32.Parse(value.ToString()) : (int?)null;
                value = Reflector.GetSimpleProperty(model, AppStrings.Date, false);
                result.Date = (value != null) ? DateTime.Parse(value.ToString()) : (DateTime?)null;
            }

            return result;
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>پیغام خطای به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected virtual async Task<string> ValidateDeleteAsync(int item)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return null;
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <param name="entityNameKey">عنوان کلیدی انتیتی برای فرم های با چند حالت انتیتی</param> 
        /// <returns>پیغام خطای به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected virtual async Task<string> ValidateDeleteAsync(int item, string entityNameKey)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return null;
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected virtual async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return null;
        }

        private IActionResult GetBasicValidationResult(object item, int itemId, string entityKey = null)
        {
            string entityNameKey = entityKey ?? EntityNameKey;
            if (item == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, entityNameKey));
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            int id = Int32.Parse(Reflector.GetProperty(item, "Id").ToString());
            if (itemId != id)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, entityNameKey));
            }

            return Ok();
        }
    }

    /// <summary>
    /// شکل متد مورد نیاز برای حذف گروهی موجودیت ها را تعریف می کند
    /// </summary>
    /// <param name="items">مجموعه شناسه های دیتابیسی موجودیت های انتخاب شده برای حذف گروهی</param>
    public delegate Task GroupDeleteAsyncDelegate(IList<int> items);

    /// <summary>
    /// شکل متد مورد نیاز برای حذف گروهی نوع مشخص شده از موجودیت ها را تعریف می کند.
    /// </summary>
    /// <param name="items">مجموعه شناسه های دیتابیسی موجودیت های انتخاب شده برای حذف گروهی</param>
    /// <param name="entityTypeId">شناسه نوع موجودیت که در فرم‌های تک حالته مقدار پیش‌فرض دارد</param> 
    public delegate Task GroupDeleteSpecialAsyncDelegate(IList<int> items, int entityTypeId);
}
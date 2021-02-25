using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;

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
        protected ValidatingController(IStringLocalizer<AppStrings> strings)
            : base(strings)
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
            return GetBasicValidationResult(item, itemId);
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
            where TFiscalView : class, IFiscalEntityView
        {
            var currentContext = SecurityContext.User;
            if (item.BranchId != currentContext.BranchId)
            {
                return BadRequest(_strings.Format(AppStrings.OtherBranchEditNotAllowed));
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
            Verify.ArgumentNotNull(treeConfig, "treeConfig");
            if (item.Level == treeConfig.MaxDepth)
            {
                string message = String.Format(_strings[AppStrings.TreeLevelsAreTooDeep],
                    treeConfig.MaxDepth, _strings[EntityNameKey]);
                return BadRequest(message);
            }

            var levelConfig = treeConfig.Levels[item.Level];
            int codeLen = levelConfig.CodeLength;
            if (item.Code.Length != codeLen)
            {
                string message = String.Format(_strings[AppStrings.LevelCodeLengthIsIncorrect],
                    _strings[EntityNameKey], levelConfig.Name, levelConfig.CodeLength);
                return BadRequest(message);
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
        protected async Task<IActionResult> FullAccountValidationResult(
            FullAccountViewModel fullAccount, IRelationRepository repository)
        {
            Verify.ArgumentNotNull(repository, nameof(repository));
            var lookupResult = await repository.LookupFullAccountAsync(fullAccount);
            if (!String.IsNullOrEmpty(lookupResult))
            {
                return BadRequest(_strings.Format(lookupResult));
            }

            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف گروهی را برای سطرهای مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>مجموعه ای از پیغام های خطای اعتبارسنجی</returns>
        protected async Task<IEnumerable<string>> ValidateGroupDeleteAsync(IEnumerable<int> items)
        {
            var messages = new List<string>();
            foreach (int item in items)
            {
                messages.Add(await ValidateDeleteAsync(item));
            }

            return messages
                .Where(msg => !String.IsNullOrEmpty(msg));
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
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
                result.Name = value?.ToString();
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
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected virtual async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return null;
        }

        private IActionResult GetBasicValidationResult(object item, int itemId)
        {
            if (item == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, EntityNameKey));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = Int32.Parse(Reflector.GetProperty(item, "Id").ToString());
            if (itemId != id)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, EntityNameKey));
            }

            return Ok();
        }
    }

    /// <summary>
    /// شکل متد مورد نیاز برای حذف گروهی موجودیت ها را تعریف می کند
    /// </summary>
    /// <param name="items">مجموعه شناسه های دیتابیسی موجودیت های انتخاب شده برای حذف گروهی</param>
    public delegate Task GroupDeleteAsyncDelegate(IList<int> items);
}
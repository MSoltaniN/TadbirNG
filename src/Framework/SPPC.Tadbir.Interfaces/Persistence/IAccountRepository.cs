﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات سرفصل های حسابداری را تعریف می کند.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه جاری برنامه تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه جاری برنامه</returns>
        Task<IList<AccountViewModel>> GetAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<IList<KeyValue>> GetAccountsLookupAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، حساب با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی</returns>
        Task<AccountViewModel> GetAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری در سطح کل را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از سرفصل های حسابداری در سطح کل</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountsLedgerAsync();

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری زیرمجموعه یک سرفصل حسابداری مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکی از سرفصل های حسابداری موجود</param>
        /// <returns>مجموعه ای از سرفصل های حسابداری زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountChildrenAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای حساب را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای حساب</returns>
        Task<ViewViewModel> GetAccountMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های تعریف شده در دوره مالی و شعبه جاری برنامه را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات از آن استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های تعریف شده در دوره مالی و شعبه جاری برنامه</returns>
        Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="account">حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی حساب ایجاد یا اصلاح شده</returns>
        Task<AccountViewModel> SaveAccountAsync(AccountViewModel account);

        /// <summary>
        /// به روش آسنکرون، حساب مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountId">شناسه عددی حساب مورد نظر برای حذف</param>
        Task DeleteAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، حساب های مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountIds">مجموعه ای از شناسه های عددی حساب های مورد نظر برای حذف</param>
        Task DeleteAccountsAsync(IList<int> accountIds);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد حساب مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="accountViewModel">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر کد حساب تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        /// <remarks>اگر کد حساب در حسابی با شناسه یکتای همین حساب به کار رفته باشد (مثلاً در حالتی که
        /// یک حساب در حالت ویرایش است) در این صورت مقدار "نادرست" را برمی گرداند</remarks>
        Task<bool> IsDuplicateAccountAsync(AccountViewModel accountViewModel);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRelatedAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده دارای حساب زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده دارای حساب زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر حساب را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر حساب</param>
        /// <returns>اگر حساب والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        Task<string> GetAccountFullCodeAsync(int parentId);

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}

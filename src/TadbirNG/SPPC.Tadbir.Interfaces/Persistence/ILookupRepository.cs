using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن لیست موجودیت ها به صورت مجموعه ای از کلید و مقدار را تعریف می کند.
    /// کلید برابر شناسه دیتابیسی موجودیت و مقدار برابر نام موجودیت خواهد بود
    /// </summary>
    public interface ILookupRepository
    {
        #region Finance Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetAccountsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetCostCentersAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetProjectsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه اسناد مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetVouchersAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه آرتیکل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetVoucherLinesAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، ارزهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        Task<IEnumerable<KeyValue>> GetCurrenciesAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلی ارزهای تعریف شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="withRate">مشخص می کند که آیا آخرین نرخ ثبت شده برای ارز مورد نیاز است یا نه؟</param>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        Task<IEnumerable<CurrencyInfoViewModel>> GetCurrenciesInfoAsync(bool withRate);

        /// <summary>
        /// به روش آسنکرون، شرکت های تعریف شده و قابل دسترسی توسط کاربر مشخص شده را به صورت مجموعه ای
        /// از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از شرکت های قابل دسترسی</returns>
        Task<IList<KeyValue>> GetUserAccessibleCompaniesAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، دوره های مالی تعریف شده در یک شرکت و قابل دسترسی توسط یک کاربر را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه دوره های مالی تعریف شده در یک شرکت مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetUserAccessibleFiscalPeriodsAsync(int companyId, int userId);

        /// <summary>
        /// به روش آسنکرون، شعب سازمانی تعریف شده در یک شرکت و قابل دسترسی توسط یک کاربر را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه شعب سازمانی تعریف شده در یک شرکت مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetUserAccessibleBranchesAsync(int companyId, int userId);

        /// <summary>
        /// ماهیت های قابل استفاده در تعریف گروه های حساب را
        /// به صورت مجموعه ای از متن های چندزبانه برمی گرداند
        /// </summary>
        /// <returns>مجموعه ماهیت های قابل استفاده در تعریف گروه های حساب</returns>
        IList<KeyValue> GetAccountGroupCategories();

        /// <summary>
        /// به روش آسنکرون، گروه های حساب تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه گروه های حساب تعریف شده</returns>
        Task<IEnumerable<KeyValue>> GetAccountGroupsAsync();

        /// <summary>
        /// انواع سیستمی تعریف شده برای سند را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>انواع سیستمی تعریف شده برای سند</returns>
        IEnumerable<KeyValue> GetVoucherTypes();

        /// <summary>
        /// انواع تعریف شده برای آرتیکل سند را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>انواع تعریف شده برای آرتیکل سند</returns>
        IEnumerable<KeyValue> GetVoucherLineTypes();

        /// <summary>
        /// محدودیت های ثبت قابل استفاده در تعریف حساب را به صورت مجموعه ای از متن های چندزبانه برمی گرداند
        /// </summary>
        /// <returns>محدودیت های ثبت قابل استفاده در تعریف حساب</returns>
        IList<KeyValue> GetAccountTurnoverModes();

        /// <summary>
        /// به روش آسنکرون، مولفه های بردار حساب را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه مولفه های بردار حساب</returns>
        Task<IEnumerable<ViewSummaryViewModel>> GetFullAccountPartsLookupAsync();

        /// <summary>
        /// به روش آسنکرون، سطوح قابل استفاده برای دفتر حساب را از تنظیمات درختی خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns></returns>
        Task<IList<AccountLevelViewModel>> GetAccountBookLevelsAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با مجموعه حساب موجودی کالا را
        /// برای کلیه شعب خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از حساب های موجودی کالا</returns>
        Task<IList<AccountViewModel>> GetInventoryAccountsAsync();

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از رفرنس های استفاده شده در سندهای مالی را برمی گرداند
        /// </summary>
        /// <returns>مجموعه رفرنس های استفاده شده در سندهای مالی</returns>
        Task<IEnumerable<string>> GetVoucherReferencesAsync();

        /// <summary>
        ///  به روش آسنکرون، لیست خلاصه سندهای باز موجود در تاریخ عملیاتی داده شده را برمی گرداند
        /// </summary>
        /// <param name="date">تاریخ مورد نظر</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست خلاصه اسناد</returns>
        Task<IEnumerable<VoucherSummaryViewModel>> GetVouchersByOperationalDateAsync(
            DateTime date, GridOptions gridOptions);

        #endregion

        #region Security Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، نقش های امنیتی تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه نقش های امنیتی تعریف شده</returns>
        Task<IList<KeyValue>> GetRolesAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، کلیه کاربران را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه کاربران</returns>
        Task<IList<KeyValue>> GetUsersAsync();

        #endregion

        #region Metadata Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، موجودیت های پایه تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های پایه تعریف شده</returns>
        Task<IList<ViewSummaryViewModel>> GetBaseEntityViewsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، موجودیت تعریف شده را به صورت کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>یک موجودیت های پایه تعریف شده</returns>
        Task<IList<ViewSummaryViewModel>> GetEntityViewAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، موجودیت های تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های تعریف شده</returns>
        Task<IList<KeyValue>> GetEntityViewsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، موجودیت های سلسله مراتبی (درختی) را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های درختی</returns>
        Task<IList<KeyValue>> GetTreeViewsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، موجودیت های برنامه را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های برنامه</returns>
        Task<IList<SourceEntityViewModel>> GetEntityTypesAsync();

        /// <summary>
        /// به روش آسنکرون، موجودیت های سیستمی را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های سیستمی</returns>
        Task<IList<SourceEntityViewModel>> GetSystemEntityTypesAsync();

        /// <summary>
        /// به روش آسنکرون، لیست استان ها را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>لیست استان ها</returns>
        Task<IList<KeyValue>> GetProvincesAsync();

        /// <summary>
        /// به روش آسنکرون، لیست شهرهای یک استان را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="provinceCode">کد یکتای استان</param>
        /// <returns>لیست شهرهای یک استان</returns>
        Task<IList<KeyValue>> GetCitiesAsync(string provinceCode);

        /// <summary>
        /// به روش آسنکرون، لیست دسترسی های سطری مجاز برای یک موجودیت را به صورت مجموعه ای از
        /// کلیدهای متنی چندزبانه برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی موجودیت مورد نظر</param>
        /// <returns>لیست دسترسی های سطری مجاز برای یک موجودیت</returns>
        Task<IList<string>> GetValidRowPermissionsAsync(int viewId);

        #endregion

        #region Treasury Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، موجودیت های پایه تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های پایه تعریف شده</returns>
        Task<IList<KeyValue>> GetSourceApps(int type);

        #endregion Treasury Subsystem lookup
    }
}

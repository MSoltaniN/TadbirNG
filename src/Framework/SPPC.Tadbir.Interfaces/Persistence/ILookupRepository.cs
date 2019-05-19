using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن لیست موجودیت ها به صورت مجموعه ای از کلید و مقدار را تعریف می کند.
    /// کلید برابر شناسه دیتابیسی موجودیت و مقدار برابر نام موجودیت خواهد بود
    /// </summary>
    public interface ILookupRepository
    {
        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را تنظیم می کند
        /// </summary>
        /// <param name="currentContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        void SetCurrentContext(UserContextViewModel currentContext);

        #region Finance Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetCostCentersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetProjectsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه اسناد مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetVouchersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه آرتیکل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetVoucherLinesAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، ارزهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        Task<IEnumerable<KeyValue>> GetCurrenciesAsync();

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

        #endregion

        #region Security Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، اطلاعات اشخاص تعریف شده در برنامه را به صورت یک دیکشنری
        /// که بر حسب شناسه دیتابیسی کاربر ایندکس شده برمی گرداند
        /// </summary>
        /// <returns>مجموعه اطلاعات کاربران موجود به صورت دیکشنری</returns>
        Task<IDictionary<int, string>> GetUserPersonsAsync();

        /// <summary>
        /// به روش آسنکرون، نقش های امنیتی تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه نقش های امنیتی تعریف شده</returns>
        Task<IList<KeyValue>> GetRolesAsync(GridOptions gridOptions = null);

        #endregion

        #region Metadata Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، موجودیت های تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های تعریف شده</returns>
        Task<IList<KeyValue>> GetEntityViewsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، موجودیت های سلسله مراتبی (درختی) را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های درختی</returns>
        Task<IList<KeyValue>> GetTreeViewsAsync(GridOptions gridOptions = null);

        #endregion
    }
}

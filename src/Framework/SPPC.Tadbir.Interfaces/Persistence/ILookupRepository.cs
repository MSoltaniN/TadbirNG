﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;

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
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetAccountsAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetCostCentersAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetProjectsAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه اسناد مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetVouchersAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه آرتیکل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetVoucherLinesAsync(int fpId, int branchId, GridOptions gridOptions = null);

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
        /// به روش آسنکرون، ماهیت های قابل استفاده در تعریف گروه های حساب را
        /// به صورت مجموعه ای از متن های چندزبانه برمی گرداند
        /// </summary>
        /// <returns>مجموعه ماهیت های قابل استفاده در تعریف گروه های حساب</returns>
        IList<string> GetAccountGroupCategoriesAsync();

        /// <summary>
        /// به روش آسنکرون، گروه های حساب تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه گروه های حساب تعریف شده</returns>
        Task<IEnumerable<KeyValue>> GetAccountGroupsAsync();

        #endregion

        #region Security Subsystem lookup

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

using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن مجموعه ای از اطلاعات موجود به صورت کد و نام را پیاده سازی می کند.
    /// </summary>
    public class LookupService : ILookupService
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="apiClient">پیاده سازی اینترفیس مربوط به کار با سرویس</param>
        public LookupService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// حساب های موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه حساب های یک دوره مالی و یک شعبه سازمانی</returns>
        public IEnumerable<KeyValue> LookupAccounts(int fpId, int branchId)
        {
            var accountLookup = _apiClient.Get<IEnumerable<KeyValue>>(
                LookupApi.FiscalPeriodBranchAccounts, fpId, branchId);
            return accountLookup;
        }

        /// <summary>
        /// تفصیلی های شناور موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه تفصیلی های شناور یک دوره مالی و یک شعبه سازمانی</returns>
        public IEnumerable<KeyValue> LookupDetailAccounts(int fpId, int branchId)
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(
                LookupApi.FiscalPeriodBranchDetailAccounts, fpId, branchId);
            return lookup;
        }

        /// <summary>
        /// مراکز هزینه موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه مراکز هزینه یک دوره مالی و یک شعبه سازمانی</returns>
        public IEnumerable<KeyValue> LookupCostCenters(int fpId, int branchId)
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(
                LookupApi.FiscalPeriodBranchCostCenters, fpId, branchId);
            return lookup;
        }

        /// <summary>
        /// پروژه های موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه پروژه های یک دوره مالی و یک شعبه سازمانی</returns>
        public IEnumerable<KeyValue> LookupProjects(int fpId, int branchId)
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(
                LookupApi.FiscalPeriodBranchProjects, fpId, branchId);
            return lookup;
        }

        /// <summary>
        /// دوره های مالی موجود در یک شرکت را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه دوره های مالی موجود در یک شرکت</returns>
        public IEnumerable<KeyValue> LookupFiscalPeriods(int companyId)
        {
            var fiscalperiodLookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.UserAccessibleCompanyFiscalPeriods, companyId);
            return fiscalperiodLookup;
        }

        /// <summary>
        /// ارزهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارزهای موجود</returns>
        public IEnumerable<KeyValue> LookupCurrencies()
        {
            var currencyLookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Currencies);
            return currencyLookup;
        }

        private IApiClient _apiClient;
    }
}

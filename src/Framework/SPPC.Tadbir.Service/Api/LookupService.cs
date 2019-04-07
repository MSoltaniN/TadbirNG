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
        /// حساب های موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه حساب های محیط جاری</returns>
        public IEnumerable<KeyValue> LookupAccounts()
        {
            var accountLookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.EnvironmentAccounts);
            return accountLookup;
        }

        /// <summary>
        /// تفصیلی های شناور موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه تفصیلی های شناور محیط جاری</returns>
        public IEnumerable<KeyValue> LookupDetailAccounts()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.EnvironmentDetailAccounts);
            return lookup;
        }

        /// <summary>
        /// مراکز هزینه موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه مراکز هزینه محیط جاری</returns>
        public IEnumerable<KeyValue> LookupCostCenters()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.EnvironmentCostCenters);
            return lookup;
        }

        /// <summary>
        /// پروژه های موجود در محیط جاری را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه پروژه های محیط جاری</returns>
        public IEnumerable<KeyValue> LookupProjects()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.EnvironmentProjects);
            return lookup;
        }

        /// <summary>
        /// دوره های مالی موجود در یک شرکت را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه دوره های مالی موجود در یک شرکت</returns>
        public IEnumerable<KeyValue> LookupFiscalPeriods(int companyId)
        {
            var fiscalperiodLookup = _apiClient.Get<IEnumerable<KeyValue>>(
                LookupApi.UserAccessibleCompanyFiscalPeriods, companyId);
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

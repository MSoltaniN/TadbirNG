using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides operations for retrieving existing items as key/value collections (lookups).
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
        /// Retrieves existing accounts in the specified fiscal period and branch as a lookup collection.
        /// </summary>
        /// <param name="fpId">Unique identifier of the fiscal period to look for accounts</param>
        /// <param name="branchId">Unique identifier of the branch to look for accounts</param>
        /// <returns>Lookup collection of existing accounts in the fiscal period</returns>
        public IEnumerable<KeyValue> LookupAccounts(int fpId, int branchId)
        {
            var accountLookup = _apiClient.Get<IEnumerable<KeyValue>>(
                LookupApi.FiscalPeriodBranchAccounts, fpId, branchId);
            return accountLookup;
        }

        /// <summary>
        /// تفصیلی های شناور موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه تفصیلی های شناور موجود</returns>
        public IEnumerable<KeyValue> LookupDetailAccounts()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.DetailAccounts);
            return lookup;
        }

        /// <summary>
        /// مراکز هزینه موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه مراکز هزینه موجود</returns>
        public IEnumerable<KeyValue> LookupCostCenters()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.CostCenters);
            return lookup;
        }

        /// <summary>
        /// پروژه های موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه پروژه های موجود</returns>
        public IEnumerable<KeyValue> LookupProjects()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Projects);
            return lookup;
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

        /// <summary>
        /// شرکای تجاری موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه شرکای تجاری موجود</returns>
        public IEnumerable<KeyValue> LookupPartners()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Partners);
            return lookup;
        }

        /// <summary>
        /// واحد های سازمانی موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه واحد های سازمانی موجود</returns>
        public IEnumerable<KeyValue> LookupBusinessUnits()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Units);
            return lookup;
        }

        /// <summary>
        /// انبارهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه انبارهای موجود</returns>
        public IEnumerable<KeyValue> LookupWarehouses()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Warehouses);
            return lookup;
        }

        /// <summary>
        /// کالاهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه کالاهای موجود</returns>
        public IEnumerable<KeyValue> LookupProducts()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.Products);
            return lookup;
        }

        /// <summary>
        /// واحدهای اندازه گیری موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه واحدهای اندازه گیری موجود</returns>
        public IEnumerable<KeyValue> LookupUnitsOfMeasurement()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.UnitsOfMeasurement);
            return lookup;
        }

        /// <summary>
        /// انواع درخواست کالاهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه انواع درخواست کالاهای موجود</returns>
        public IEnumerable<KeyValue> LookupRequisitionVoucherTypes()
        {
            var lookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.RequisitionVoucherTypes);
            return lookup;
        }

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات درخواست کار را خوانده و برمی گرداند 
        /// </summary>
        /// <returns>وابستگی های مورد نیاز درخواست کار</returns>
        public VoucherDependsViewModel LookupRequisitionVoucherDepends()
        {
            var voucherDepends = _apiClient.Get<VoucherDependsViewModel>(LookupApi.RequisitionVoucherDepends);
            return voucherDepends;
        }

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات سطر درخواست کار را خوانده و برمی گرداند 
        /// </summary>
        /// <returns>وابستگی های مورد نیاز سطر درخواست کار</returns>
        public VoucherLineDependsViewModel LookupRequisitionVoucherLineDepends()
        {
            var lineDepends = _apiClient.Get<VoucherLineDependsViewModel>(LookupApi.RequisitionVoucherLineDepends);
            return lineDepends;
        }

        private IApiClient _apiClient;
    }
}

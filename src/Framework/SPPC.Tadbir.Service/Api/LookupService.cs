using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.ViewModel.Procurement;

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
            var fiscalperiodLookup = _apiClient.Get<IEnumerable<KeyValue>>(LookupApi.CompanyFiscalPeriods, companyId);
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

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات سطر موجودی کالا را خوانده و برمی گرداند
        /// </summary>
        /// <returns>وابستگی های مورد نیاز سطر موجودی کالا</returns>
        public InventoryDependsViewModel LookupProductInventoryDepends()
        {
            var depends = _apiClient.Get<InventoryDependsViewModel>(LookupApi.ProductInventoryDepends);
            return depends;
        }

        private IApiClient _apiClient;
    }
}

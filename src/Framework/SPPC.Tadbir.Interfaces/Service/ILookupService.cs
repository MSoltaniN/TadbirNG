using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن مجموعه ای از اطلاعات موجود به صورت کد و نام را تعریف می کند.
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// حساب های موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه حساب های یک دوره مالی و یک شعبه سازمانی</returns>
        IEnumerable<KeyValue> LookupAccounts(int fpId, int branchId);

        /// <summary>
        /// تفصیلی های شناور موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه تفصیلی های شناور یک دوره مالی و یک شعبه سازمانی</returns>
        IEnumerable<KeyValue> LookupDetailAccounts(int fpId, int branchId);

        /// <summary>
        /// مراکز هزینه موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه مراکز هزینه یک دوره مالی و یک شعبه سازمانی</returns>
        IEnumerable<KeyValue> LookupCostCenters(int fpId, int branchId);

        /// <summary>
        /// پروژه های موجود در یک دوره مالی و یک شعبه را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب سازمانی موجود</param>
        /// <returns>مجموعه پروژه های یک دوره مالی و یک شعبه سازمانی</returns>
        IEnumerable<KeyValue> LookupProjects(int fpId, int branchId);

        /// <summary>
        /// دوره های مالی موجود در یک شرکت را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه دوره های مالی موجود در یک شرکت</returns>
        IEnumerable<KeyValue> LookupFiscalPeriods(int companyId);

        /// <summary>
        /// ارزهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارزهای موجود</returns>
        IEnumerable<KeyValue> LookupCurrencies();

        /// <summary>
        /// شرکای تجاری موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه شرکای تجاری موجود</returns>
        IEnumerable<KeyValue> LookupPartners();

        /// <summary>
        /// واحد های سازمانی موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه واحد های سازمانی موجود</returns>
        IEnumerable<KeyValue> LookupBusinessUnits();

        /// <summary>
        /// انبارهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه انبارهای موجود</returns>
        IEnumerable<KeyValue> LookupWarehouses();

        /// <summary>
        /// کالاهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه کالاهای موجود</returns>
        IEnumerable<KeyValue> LookupProducts();

        /// <summary>
        /// واحدهای اندازه گیری موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه واحدهای اندازه گیری موجود</returns>
        IEnumerable<KeyValue> LookupUnitsOfMeasurement();

        /// <summary>
        /// انواع درخواست کالاهای موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه انواع درخواست کالاهای موجود</returns>
        IEnumerable<KeyValue> LookupRequisitionVoucherTypes();

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات درخواست کالا را خوانده و برمی گرداند
        /// </summary>
        /// <returns>وابستگی های مورد نیاز درخواست کار</returns>
        VoucherDependsViewModel LookupRequisitionVoucherDepends();

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات سطر درخواست کالا را خوانده و برمی گرداند
        /// </summary>
        /// <returns>وابستگی های مورد نیاز سطر درخواست کار</returns>
        VoucherLineDependsViewModel LookupRequisitionVoucherLineDepends();

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات سطر موجودی کالا را خوانده و برمی گرداند
        /// </summary>
        /// <returns>وابستگی های مورد نیاز سطر موجودی کالا</returns>
        InventoryDependsViewModel LookupProductInventoryDepends();
    }
}

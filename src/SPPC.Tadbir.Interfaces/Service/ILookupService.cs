using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides operations for retrieving existing items as key/value collections (lookups).
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// Retrieves existing accounts in the specified fiscal period and branch as a lookup collection.
        /// </summary>
        /// <param name="fpId">Unique identifier of the fiscal period to look for accounts</param>
        /// <param name="branchId">Unique identifier of the branch to look for accounts</param>
        /// <returns>Lookup collection of existing accounts in the fiscal period</returns>
        IEnumerable<KeyValue> LookupAccounts(int fpId, int branchId);

        /// <summary>
        /// تفصیلی های شناور موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه تفصیلی های شناور موجود</returns>
        IEnumerable<KeyValue> LookupDetailAccounts();

        /// <summary>
        /// مراکز هزینه موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه مراکز هزینه موجود</returns>
        IEnumerable<KeyValue> LookupCostCenters();

        /// <summary>
        /// پروژه های موجود را به صورت مجموعه ای از کد و نام خوانده و برمی گرداند 
        /// </summary>
        /// <returns>مجموعه پروژه های موجود</returns>
        IEnumerable<KeyValue> LookupProjects();

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
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات درخواست کار را خوانده و برمی گرداند 
        /// </summary>
        /// <returns>وابستگی های مورد نیاز درخواست کار</returns>
        VoucherDependsViewModel LookupRequisitionVoucherDepends();

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات سطر درخواست کار را خوانده و برمی گرداند 
        /// </summary>
        /// <returns>وابستگی های مورد نیاز سطر درخواست کار</returns>
        VoucherLineDependsViewModel LookupRequisitionVoucherLineDepends();
    }
}

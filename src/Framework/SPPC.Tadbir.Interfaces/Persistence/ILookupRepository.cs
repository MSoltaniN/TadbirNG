using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن لیست موجودیت ها به صورت مجموعه ای از کلید و مقدار را تعریف می کند.
    /// کلید برابر شناسه دیتابیسی موجودیت و مقدار برابر نام موجودیت خواهد بود
    /// </summary>
    public interface ILookupRepository
    {
        #region Finance Subsystem lookup

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetAccountsAsync(int fpId, int branchId);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(int fpId, int branchId);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetCostCentersAsync(int fpId, int branchId);

        /// <summary>
        /// به روش آسنکرون، پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        Task<IEnumerable<KeyValue>> GetProjectsAsync(int fpId, int branchId);

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

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// سرفصل های حسابداری تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        IEnumerable<KeyValue> GetAccounts(int fpId, int branchId);

        /// <summary>
        /// تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        IEnumerable<KeyValue> GetDetailAccounts(int fpId, int branchId);

        /// <summary>
        /// مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        IEnumerable<KeyValue> GetCostCenters(int fpId, int branchId);

        /// <summary>
        /// پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        IEnumerable<KeyValue> GetProjects(int fpId, int branchId);

        /// <summary>
        /// ارزهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        IEnumerable<KeyValue> GetCurrencies();

        /// <summary>
        /// دوره های مالی تعریف شده در یک شرکت مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه دوره های مالی تعریف شده در یک شرکت مشخص شده</returns>
        IEnumerable<KeyValue> GetFiscalPeriods(int companyId);

        #endregion

        #endregion

        #region Inventory Subsystem lookup

        /// <summary>
        /// انبارهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه انبارهای تعریف شده</returns>
        IEnumerable<KeyValue> GetWarehouses();

        /// <summary>
        /// کالاهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه کالاهای تعریف شده</returns>
        IEnumerable<KeyValue> GetProducts();

        /// <summary>
        /// واحدهای شمارش تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه واحدهای شمارش تعریف شده</returns>
        IEnumerable<KeyValue> GetUnitsOfMeasurement();

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک سطر موجودی کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات پایه مورد نیاز سطر موجودی کالا</returns>
        InventoryDependsViewModel GetInventoryDepends();

        #endregion

        #region Procurement Subsystem lookup

        /// <summary>
        /// انواع درخواست کالای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه انواع درخواست کالای تعریف شده</returns>
        IEnumerable<KeyValue> GetRequisitionVoucherTypes();

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات پایه مورد نیاز درخواست کالا</returns>
        VoucherDependsViewModel GetRequisitionDepends();

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک سطر درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات پایه مورد نیاز سطر درخواست کالا</returns>
        VoucherLineDependsViewModel GetRequisitionLineDepends();

        #endregion

        /// <summary>
        /// شرکای کاری تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه شرکای کاری تعریف شده</returns>
        IEnumerable<KeyValue> GetPartners();

        /// <summary>
        /// واحدهای سازمانی تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه واحدهای سازمانی تعریف شده</returns>
        IEnumerable<KeyValue> GetBusinessUnits();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    ///
    /// </summary>
    public interface IReportDirectUtility
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        IEnumerable<int> GetChildTree(int branchId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        IEnumerable<int> GetParentTree(int branchId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        int GetLevelCodeLength(int level);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        int GetLevelCodeLength(int viewId, int level);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        T ValueOrDefault<T>(DataRow row, string field);

        /// <summary>
        ///
        /// </summary>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        string ValueOrDefault(DataRow row, string field);

        /// <summary>
        ///
        /// </summary>
        /// <param name="gridOptions"></param>
        /// <param name="fiscalPeriodId"></param>
        /// <returns></returns>
        string GetEnvironmentFilters(GridOptions gridOptions, int fiscalPeriodId);

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح قابل استفاده برای گزارشگیری را
        /// برای مولفه حساب داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح قابل استفاده</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync();

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId);

        /// <summary>
        /// سطرهای بدون مانده و گردش را با توجه به سطرهای داده شده برای گزارش خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب مورد نظر</param>
        /// <param name="items">سطرهای داده شده برای گزارش</param>
        /// <param name="level">سطح گزارشگیری مورد نظر</param>
        /// <returns>سطرهای بدون مانده و گردش</returns>
        Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            int viewId, IEnumerable<TestBalanceItemViewModel> items, int level);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        ReportQuery GetItemLookupQuery(int viewId, int length);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<TreeEntity> GetItemAsync(int viewId, int itemId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="fpId"></param>
        /// <returns></returns>
        Task<DateTime> GetFiscalPeriodEndAsync(int fpId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="fpId"></param>
        /// <returns></returns>
        Task<int> GetFirstVoucherNoAsync(int fpId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="fiscalPeriodId"></param>
        /// <param name="originId"></param>
        /// <returns></returns>
        Task<bool> HasSpecialVoucherAsync(int fiscalPeriodId, VoucherOriginId originId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="collectionId"></param>
        /// <param name="withRelations"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        IEnumerable<AccountItemBriefViewModel> GetUsableAccountsAsync(
            AccountCollectionId collectionId, bool withRelations = false, int? branchId = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="collectionId"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        IEnumerable<AccountItemBriefViewModel> GetInheritedAccountsAsync(
            AccountCollectionId collectionId, int branchId);
    }
}

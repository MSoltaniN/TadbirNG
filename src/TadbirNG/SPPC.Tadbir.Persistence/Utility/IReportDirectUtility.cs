using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// <param name="viewId"></param>
        /// <returns></returns>
        string GetItemName(int viewId);

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
        /// <param name="branchId"></param>
        /// <param name="noDraft"></param>
        /// <returns></returns>
        string GetEnvironmentFilters(GridOptions gridOptions = null, int? fiscalPeriodId = null,
            int? branchId = null, bool noDraft = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="gridOptions"></param>
        /// <returns></returns>
        string GetColumnFilters(GridOptions gridOptions);

        /// <summary>
        ///
        /// </summary>
        /// <param name="gridOptions"></param>
        /// <returns></returns>
        string GetColumnSorting(GridOptions gridOptions);

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
        Task<DateTime> GetFiscalPeriodStartAsync(int fpId);

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
        IEnumerable<AccountItemBriefViewModel> GetUsableAccounts(
            AccountCollectionId collectionId, bool withRelations = false, int? branchId = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="collectionId"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        IEnumerable<AccountItemBriefViewModel> GetInheritedAccounts(
            AccountCollectionId collectionId, int branchId);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<Voucher> GetOpeningVoucherAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="openingVoucher"></param>
        /// <returns></returns>
        bool MustApplyOpeningOption(FinanceReportOptions options, Voucher openingVoucher);
    }
}

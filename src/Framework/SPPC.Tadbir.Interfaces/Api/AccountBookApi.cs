using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for account book reports.
    /// </summary>
    public sealed class AccountBookApi
    {
        private AccountBookApi()
        {
        }

        #region Account Books

        /// <summary>
        /// API client URL for Book report by row for an account specified by unique identifier
        /// </summary>
        public const string AccountBookByRow = "accbook/account/{0}/by-row";

        /// <summary>
        /// API server route URL for Book report by row for an account specified by unique identifier
        /// </summary>
        public const string AccountBookByRowUrl = "accbook/account/{accountId:min(1)}/by-row";

        /// <summary>
        /// API client URL for Book report by voucher sum for an account specified by unique identifier
        /// </summary>
        public const string AccountBookVoucherSum = "accbook/account/{0}/voucher-sum";

        /// <summary>
        /// API server route URL for Book report by voucher sum for an account specified by unique identifier
        /// </summary>
        public const string AccountBookVoucherSumUrl = "accbook/account/{accountId:min(1)}/voucher-sum";

        /// <summary>
        /// API client URL for Book report by daily sum for an account specified by unique identifier
        /// </summary>
        public const string AccountBookDailySum = "accbook/account/{0}/daily-sum";

        /// <summary>
        /// API server route URL for Book report by daily sum for an account specified by unique identifier
        /// </summary>
        public const string AccountBookDailySumUrl = "accbook/account/{accountId:min(1)}/daily-sum";

        /// <summary>
        /// API client URL for Book report by monthly sum for an account specified by unique identifier
        /// </summary>
        public const string AccountBookMonthlySum = "accbook/account/{0}/monthly-sum";

        /// <summary>
        /// API server route URL for Book report by monthly sum for an account specified by unique identifier
        /// </summary>
        public const string AccountBookMonthlySumUrl = "accbook/account/{accountId:min(1)}/monthly-sum";

        /// <summary>
        /// API client URL for Book report by row by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookByRowByBranch = "accbook/account/{0}/by-row/by-branch";

        /// <summary>
        /// API server route URL for Book report by row by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookByRowByBranchUrl = "accbook/account/{accountId:min(1)}/by-row/by-branch";

        /// <summary>
        /// API client URL for Book report by voucher sum by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookVoucherSumByBranch = "accbook/account/{0}/voucher-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by voucher sum by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookVoucherSumByBranchUrl = "accbook/account/{accountId:min(1)}/voucher-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by daily sum by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookDailySumByBranch = "accbook/account/{0}/daily-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by daily sum by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookDailySumByBranchUrl = "accbook/account/{accountId:min(1)}/daily-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by monthly sum by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookMonthlySumByBranch = "accbook/account/{0}/monthly-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by monthly sum by branch for an account specified by unique identifier
        /// </summary>
        public const string AccountBookMonthlySumByBranchUrl = "accbook/account/{accountId:min(1)}/monthly-sum/by-branch";

        #endregion

        #region DetailAccount Books

        /// <summary>
        /// API client URL for Book report by row for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookByRow = "accbook/faccount/{0}/by-row";

        /// <summary>
        /// API server route URL for Book report by row for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookByRowUrl = "accbook/faccount/{faccountId:min(1)}/by-row";

        /// <summary>
        /// API client URL for Book report by voucher sum for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookVoucherSum = "accbook/faccount/{0}/voucher-sum";

        /// <summary>
        /// API server route URL for Book report by voucher sum for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookVoucherSumUrl = "accbook/faccount/{faccountId:min(1)}/voucher-sum";

        /// <summary>
        /// API client URL for Book report by daily sum for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookDailySum = "accbook/faccount/{0}/daily-sum";

        /// <summary>
        /// API server route URL for Book report by daily sum for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookDailySumUrl = "accbook/faccount/{faccountId:min(1)}/daily-sum";

        /// <summary>
        /// API client URL for Book report by monthly sum for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookMonthlySum = "accbook/faccount/{0}/monthly-sum";

        /// <summary>
        /// API server route URL for Book report by monthly sum for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookMonthlySumUrl = "accbook/faccount/{faccountId:min(1)}/monthly-sum";

        /// <summary>
        /// API client URL for Book report by row by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookByRowByBranch = "accbook/faccount/{0}/by-row/by-branch";

        /// <summary>
        /// API server route URL for Book report by row by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookByRowByBranchUrl = "accbook/faccount/{faccountId:min(1)}/by-row/by-branch";

        /// <summary>
        /// API client URL for Book report by voucher sum by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookVoucherSumByBranch = "accbook/faccount/{0}/voucher-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by voucher sum by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookVoucherSumByBranchUrl = "accbook/faccount/{faccountId:min(1)}/voucher-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by daily sum by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookDailySumByBranch = "accbook/faccount/{0}/daily-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by daily sum by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookDailySumByBranchUrl = "accbook/faccount/{faccountId:min(1)}/daily-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by monthly sum by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookMonthlySumByBranch = "accbook/faccount/{0}/monthly-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by monthly sum by branch for a detail account specified by unique identifier
        /// </summary>
        public const string DetailAccountBookMonthlySumByBranchUrl = "accbook/faccount/{faccountId:min(1)}/monthly-sum/by-branch";

        #endregion

        #region CostCenter Books

        /// <summary>
        /// API client URL for Book report by row for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookByRow = "accbook/ccenter/{0}/by-row";

        /// <summary>
        /// API server route URL for Book report by row for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookByRowUrl = "accbook/ccenter/{ccenterId:min(1)}/by-row";

        /// <summary>
        /// API client URL for Book report by voucher sum for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookVoucherSum = "accbook/ccenter/{0}/voucher-sum";

        /// <summary>
        /// API server route URL for Book report by voucher sum for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookVoucherSumUrl = "accbook/ccenter/{ccenterId:min(1)}/voucher-sum";

        /// <summary>
        /// API client URL for Book report by daily sum for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookDailySum = "accbook/ccenter/{0}/daily-sum";

        /// <summary>
        /// API server route URL for Book report by daily sum for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookDailySumUrl = "accbook/ccenter/{ccenterId:min(1)}/daily-sum";

        /// <summary>
        /// API client URL for Book report by monthly sum for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookMonthlySum = "accbook/ccenter/{0}/monthly-sum";

        /// <summary>
        /// API server route URL for Book report by monthly sum for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookMonthlySumUrl = "accbook/ccenter/{ccenterId:min(1)}/monthly-sum";

        /// <summary>
        /// API client URL for Book report by row by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookByRowByBranch = "accbook/ccenter/{0}/by-row/by-branch";

        /// <summary>
        /// API server route URL for Book report by row by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookByRowByBranchUrl = "accbook/ccenter/{ccenterId:min(1)}/by-row/by-branch";

        /// <summary>
        /// API client URL for Book report by voucher sum by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookVoucherSumByBranch = "accbook/ccenter/{0}/voucher-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by voucher sum by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookVoucherSumByBranchUrl = "accbook/ccenter/{ccenterId:min(1)}/voucher-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by daily sum by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookDailySumByBranch = "accbook/ccenter/{0}/daily-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by daily sum by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookDailySumByBranchUrl = "accbook/ccenter/{ccenterId:min(1)}/daily-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by monthly sum by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookMonthlySumByBranch = "accbook/ccenter/{0}/monthly-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by monthly sum by branch for a cost center specified by unique identifier
        /// </summary>
        public const string CostCenterBookMonthlySumByBranchUrl = "accbook/ccenter/{ccenterId:min(1)}/monthly-sum/by-branch";

        #endregion

        #region Project Books

        /// <summary>
        /// API client URL for Book report by row for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookByRow = "accbook/project/{0}/by-row";

        /// <summary>
        /// API server route URL for Book report by row for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookByRowUrl = "accbook/project/{projectId:min(1)}/by-row";

        /// <summary>
        /// API client URL for Book report by voucher sum for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookVoucherSum = "accbook/project/{0}/voucher-sum";

        /// <summary>
        /// API server route URL for Book report by voucher sum for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookVoucherSumUrl = "accbook/project/{projectId:min(1)}/voucher-sum";

        /// <summary>
        /// API client URL for Book report by daily sum for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookDailySum = "accbook/project/{0}/daily-sum";

        /// <summary>
        /// API server route URL for Book report by daily sum for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookDailySumUrl = "accbook/project/{projectId:min(1)}/daily-sum";

        /// <summary>
        /// API client URL for Book report by monthly sum for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookMonthlySum = "accbook/project/{0}/monthly-sum";

        /// <summary>
        /// API server route URL for Book report by monthly sum for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookMonthlySumUrl = "accbook/project/{projectId:min(1)}/monthly-sum";

        /// <summary>
        /// API client URL for Book report by row by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookByRowByBranch = "accbook/project/{0}/by-row/by-branch";

        /// <summary>
        /// API server route URL for Book report by row by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookByRowByBranchUrl = "accbook/project/{projectId:min(1)}/by-row/by-branch";

        /// <summary>
        /// API client URL for Book report by voucher sum by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookVoucherSumByBranch = "accbook/project/{0}/voucher-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by voucher sum by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookVoucherSumByBranchUrl = "accbook/project/{projectId:min(1)}/voucher-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by daily sum by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookDailySumByBranch = "accbook/project/{0}/daily-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by daily sum by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookDailySumByBranchUrl = "accbook/project/{projectId:min(1)}/daily-sum/by-branch";

        /// <summary>
        /// API client URL for Book report by monthly sum by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookMonthlySumByBranch = "accbook/project/{0}/monthly-sum/by-branch";

        /// <summary>
        /// API server route URL for Book report by monthly sum by branch for a project specified by unique identifier
        /// </summary>
        public const string ProjectBookMonthlySumByBranchUrl = "accbook/project/{projectId:min(1)}/monthly-sum/by-branch";

        #endregion

        #region Account Item Navigation

        /// <summary>
        /// API client URL for previous accessible account item relative to the item specified by unique identifier
        /// </summary>
        public const string PreviousEnvironmentItem = "accbook/view/{0}/item/{1}/prev";

        /// <summary>
        /// API server route URL for previous accessible account item relative to the item specified by unique identifier
        /// </summary>
        public const string PreviousEnvironmentItemUrl = "accbook/view/{viewId:min(1)}/item/{itemId:min(1)}/prev";

        /// <summary>
        /// API client URL for next accessible account item relative to the item specified by unique identifier
        /// </summary>
        public const string NextEnvironmentItem = "accbook/view/{0}/item/{1}/next";

        /// <summary>
        /// API server route URL for next accessible account item relative to the item specified by unique identifier
        /// </summary>
        public const string NextEnvironmentItemUrl = "accbook/view/{viewId:min(1)}/item/{itemId:min(1)}/next";

        #endregion
    }
}

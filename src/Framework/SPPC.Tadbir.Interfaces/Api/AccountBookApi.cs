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

        #endregion
    }
}

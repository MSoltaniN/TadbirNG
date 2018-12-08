using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for application reports.
    /// </summary>
    public sealed class ReportApi
    {
        private ReportApi()
        {
        }

        /// <summary>
        /// API client URL for Voucher Summary by Date report
        /// </summary>
        public const string EnvironmentVoucherSummaryByDate = "reports/voucher/sum-by-date";

        /// <summary>
        /// API server route URL for Voucher Summary by Date report
        /// </summary>
        public const string EnvironmentVoucherSummaryByDateUrl = "reports/voucher/sum-by-date";

        /// <summary>
        /// API client URL for Voucher Standard Form report
        /// </summary>
        public const string VoucherStandardForm = "reports/voucher/std-form/{0}";

        /// <summary>
        /// API server route URL for Voucher Standard Form report
        /// </summary>
        public const string VoucherStandardFormUrl = "reports/voucher/std-form/{voucherId:min(1)}";

        /// <summary>
        /// API client URL for Voucher Standard Form With Detail report
        /// </summary>
        public const string VoucherStandardFormWithDetail = "reports/voucher/std-form-detail/{0}";

        /// <summary>
        /// API server route URL for Voucher Standard Form With Detail report
        /// </summary>
        public const string VoucherStandardFormWithDetailUrl = "reports/voucher/std-form-detail/{voucherId:min(1)}";
    }
}

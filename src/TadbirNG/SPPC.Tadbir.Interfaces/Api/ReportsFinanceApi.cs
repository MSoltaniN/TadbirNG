using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client URLs for Finance subsystem reports
    /// </summary>
    public sealed class ReportsFinanceApi
    {
        private ReportsFinanceApi()
        {
        }

        /// <summary>
        /// API client URL for Voucher Summary by Date report
        /// </summary>
        public const string EnvironmentVoucherSummaryByDate = "reports/finance/vouchers/sum-by-date";

        /// <summary>
        /// API server route URL for Voucher Summary by Date report
        /// </summary>
        public const string EnvironmentVoucherSummaryByDateUrl = "reports/finance/vouchers/sum-by-date";

        /// <summary>
        /// API client URL for Voucher Standard Form report
        /// </summary>
        public const string VoucherStandardForm = "reports/finance/voucher-by-no/{0}/std-form";

        /// <summary>
        /// API server route URL for Voucher Standard Form report
        /// </summary>
        public const string VoucherStandardFormUrl = "reports/finance/voucher-by-no/{voucherNo:min(1)}/std-form";

        /// <summary>
        /// API client URL for Voucher Standard Form With Detail report
        /// </summary>
        public const string VoucherStandardFormWithDetail = "reports/finance/voucher-by-no/{0}/std-form-detail";

        /// <summary>
        /// API server route URL for Voucher Standard Form With Detail report
        /// </summary>
        public const string VoucherStandardFormWithDetailUrl = "reports/finance/voucher-by-no/{voucherNo:min(1)}/std-form-detail";
    }
}

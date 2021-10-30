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
        /// API client URL for Voucher Summary report
        /// </summary>
        public const string VoucherSummary = "reports/finance/vouchers/summary";

        /// <summary>
        /// API server route URL for Voucher Summary report
        /// </summary>
        public const string VoucherSummaryUrl = "reports/finance/vouchers/summary";

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
        public const string VoucherStandardFormWithDetailUrl =
            "reports/finance/voucher-by-no/{voucherNo:min(1)}/std-form-detail";

        /// <summary>
        /// API client URL for "Voucher Simple View - By Detail" report
        /// </summary>
        public const string VoucherByDetail = "reports/finance/voucher-by-no/{0}/by-detail";

        /// <summary>
        /// API server route URL for "Voucher Simple View - By Detail" report
        /// </summary>
        public const string VoucherByDetailUrl =
            "reports/finance/voucher-by-no/{voucherNo:min(1)}/by-detail";

        /// <summary>
        /// API client URL for "Voucher Aggregate View - By Ledger" report
        /// </summary>
        public const string VoucherByLedger = "reports/finance/voucher-by-no/{0}/by-ledger";

        /// <summary>
        /// API server route URL for "Voucher Aggregate View - By Ledger" report
        /// </summary>
        public const string VoucherByLedgerUrl =
            "reports/finance/voucher-by-no/{voucherNo:min(1)}/by-ledger";

        /// <summary>
        /// API client URL for "Voucher Aggregate View - By Subsidiary" report
        /// </summary>
        public const string VoucherBySubsidiary = "reports/finance/voucher-by-no/{0}/by-subsid";

        /// <summary>
        /// API server route URL for "Voucher Aggregate View - By Subsidiary" report
        /// </summary>
        public const string VoucherBySubsidiaryUrl =
            "reports/finance/voucher-by-no/{voucherNo:min(1)}/by-subsid";

        /// <summary>
        /// API client URL for Book By Balance report
        /// </summary>
        /// <remarks>گزارش دفتر حساب با تفکیک مانده</remarks>
        public const string BookByBalance = "reports/finance/book-by-bal";

        /// <summary>
        /// API server route URL for Book By Balance report
        /// </summary>
        public const string BookByBalanceUrl = "reports/finance/book-by-bal";

        /// <summary>
        /// API client URL for Detail Account Book By Account report
        /// </summary>
        /// <remarks>گزارش دفتر تفصیلی - گروه بندی روی حسابها</remarks>
        public const string DetailAccountBookByAccount = "reports/finance/faccount/{0}/book-by-acc";

        /// <summary>
        /// API server route URL for Detail Account Book By Account report
        /// </summary>
        public const string DetailAccountBookByAccountUrl = "reports/finance/faccount/{detailAccountId:min(1)}/book-by-acc";

        /// <summary>
        /// API client URL for Cost Center Book By Account report
        /// </summary>
        /// <remarks>گزارش دفتر مرکز هزینه - گروه بندی روی حسابها</remarks>
        public const string CostCenterBookByAccount = "reports/finance/ccenter/{0}/book-by-acc";

        /// <summary>
        /// API server route URL for Cost Center Book By Account report
        /// </summary>
        public const string CostCenterBookByAccountUrl = "reports/finance/ccenter/{costCenterId:min(1)}/book-by-acc";

        /// <summary>
        /// API client URL for Project Book By Account report
        /// </summary>
        /// <remarks>گزارش دفتر پروژه - گروه بندی روی حسابها</remarks>
        public const string ProjectBookByAccount = "reports/finance/project/{0}/book-by-acc";

        /// <summary>
        /// API server route URL for Project Book By Account report
        /// </summary>
        public const string ProjectBookByAccountUrl = "reports/finance/project/{projectId:min(1)}/book-by-acc";
    }
}

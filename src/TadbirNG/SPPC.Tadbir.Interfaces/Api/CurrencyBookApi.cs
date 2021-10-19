namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for currency book reports.
    /// </summary>
    public sealed class CurrencyBookApi
    {
        /// <summary>
        /// API client URL for Book report by row
        /// </summary>
        public const string CurrencyBookByRow = "currbook/by-row/by-branch/{0}";

        /// <summary>
        /// API server route URL for Book report by row
        /// </summary>
        public const string CurrencyBookByRowUrl = "currbook/by-row/by-branch/{byBranch}";

        /// <summary>
        /// API client URL for Book report by voucher sum
        /// </summary>
        public const string CurrencyBookVoucherSum = "currbook/voucher-sum/by-branch/{0}";

        /// <summary>
        /// API server route URL for Book report by voucher sum
        /// </summary>
        public const string CurrencyBookVoucherSumUrl = "currbook/voucher-sum/by-branch/{byBranch}";

        /// <summary>
        /// API client URL for Book report by daily sum
        /// </summary>
        public const string CurrencyBookDailySum = "currbook/daily-sum/by-branch/{0}";

        /// <summary>
        /// API server route URL for Book report by daily sum
        /// </summary>
        public const string CurrencyBookDailySumUrl = "currbook/daily-sum/by-branch/{byBranch}";

        /// <summary>
        /// API client URL for Book report by monthly sum
        /// </summary>
        public const string CurrencyBookMonthlySum = "currbook/monthly-sum/by-branch/{0}";

        /// <summary>
        /// API server route URL for Book report by monthly sum
        /// </summary>
        public const string CurrencyBookMonthlySumUrl = "currbook/monthly-sum/by-branch/{byBranch}";

        /// <summary>
        /// API client URL for Book report by all currencies
        /// </summary>
        public const string CurrencyBookAllCurrencies = "currbook/all-currencies/{0}";

        /// <summary>
        /// API server route URL for Book report by all currencies
        /// </summary>
        public const string CurrencyBookAllCurrenciesUrl = "currbook/all-currencies/{currFree}";
    }
}

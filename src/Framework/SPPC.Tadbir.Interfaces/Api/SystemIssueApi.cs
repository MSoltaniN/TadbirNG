namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with system issue.
    /// </summary>
    public sealed class SystemIssueApi
    {
        /// <summary>
        /// API client URL for system issue
        /// </summary>
        public const string SystemIssues = "sys-issues";

        /// <summary>
        /// API server route URL for system issue
        /// </summary>
        public const string SystemIssuesUrl = "sys-issues";

        /// <summary>
        /// API client URL for unbalanced vouchers
        /// </summary>
        public const string UnbalancedVouchers = "sys-issues/vouchers/unbalanced";

        /// <summary>
        /// API server route URL for unbalanced vouchers
        /// </summary>
        public const string UnbalancedVouchersUrl = "sys-issues/vouchers/unbalanced";

        /// <summary>
        /// API client URL for vouchers with no article
        /// </summary>
        public const string VouchersWithNoArticle = "sys-issues/vouchers/no-article";

        /// <summary>
        /// API server route URL for vouchers with no article
        /// </summary>
        public const string VouchersWithNoArticleUrl = "sys-issues/vouchers/no-article";

        /// <summary>
        /// API client URL for unbalanced vouchers
        /// </summary>
        public const string MissingVoucherNumbers = "sys-issues/vouchers/miss-number";

        /// <summary>
        /// API server route URL for unbalanced vouchers
        /// </summary>
        public const string MissingVoucherNumbersUrl = "sys-issues/vouchers/miss-number";

        /// <summary>
        /// API client URL for system issue articles
        /// </summary>
        public const string SystemIssueArticles = "sys-issues/articles/{0}";

        /// <summary>
        /// API server route URL for system issue articles
        /// </summary>
        public const string SystemIssueArticlesUrl = "sys-issues/articles/{issueType}";
    }
}

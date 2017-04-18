using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with financial transactions and articles.
    /// </summary>
    public sealed class TransactionApi
    {
        private TransactionApi()
        {
        }

        /// <summary>
        /// API client URL for all transactions defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchTransactions = "transactions/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for all transactions defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchTransactionsUrl = "transactions/fp/{fpId:int}/branch/{branchId:int}";

        /// <summary>
        /// API client URL for all transactions
        /// </summary>
        public const string Transactions = "transactions";

        /// <summary>
        /// API server route URL for all transactions
        /// </summary>
        public const string TransactionsUrl = "transactions";

        /// <summary>
        /// API client URL for a single transaction specified by identifier
        /// </summary>
        public const string Transaction = "transactions/{0}";

        /// <summary>
        /// API server route URL for a single transaction specified by identifier
        /// </summary>
        public const string TransactionUrl = "transactions/{transactionId:int}";

        /// <summary>
        /// API client URL for details of a single transaction specified by identifier
        /// </summary>
        public const string TransactionDetails = "transactions/{0}/details";

        /// <summary>
        /// API server route URL for details of a single transaction specified by identifier
        /// </summary>
        public const string TransactionDetailsUrl = "transactions/{transactionId:int}/details";

        /// <summary>
        /// API client URL for all articles in a single transaction specified by identifier
        /// </summary>
        public const string TransactionArticles = "transactions/{0}/articles";

        /// <summary>
        /// API server route URL for all articles in a single transaction specified by identifier
        /// </summary>
        public const string TransactionArticlesUrl = "transactions/{transactionId:int}/articles";

        /// <summary>
        /// API client URL for a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticle = "transactions/articles/{0}";

        /// <summary>
        /// API server route URL for a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleUrl = "transactions/articles/{articleId:int}";

        /// <summary>
        /// API client URL for details of a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleDetails = "transactions/articles/{0}/details";

        /// <summary>
        /// API server route URL for details of a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleDetailsUrl = "transactions/articles/{articleId:int}/details";
    }
}

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
        public const string FiscalPeriodBranchTransactionsUrl = "transactions/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API server route URL for all transactions defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchTransactionsSyncUrl =
            "transactions/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync";

        /// <summary>
        /// API client URL for count of all transactions defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchItemCount = "transactions/fp/{0}/branch/{1}/count";

        /// <summary>
        /// API server route URL for count of all transactions defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchItemCountUrl =
            "transactions/fp/{fpId:min(1)}/branch/{branchId:min(1)}/count";

        /// <summary>
        /// API client URL for all transactions
        /// </summary>
        public const string Transactions = "transactions";

        /// <summary>
        /// API server route URL for all transactions
        /// </summary>
        public const string TransactionsUrl = "transactions";

        /// <summary>
        /// API server route URL for all transactions
        /// </summary>
        public const string TransactionsSyncUrl = "transactions/sync";

        /// <summary>
        /// API client URL for a single transaction specified by identifier
        /// </summary>
        public const string Transaction = "transactions/{0}";

        /// <summary>
        /// API server route URL for a single transaction specified by identifier
        /// </summary>
        public const string TransactionUrl = "transactions/{transactionId:min(1)}";

        /// <summary>
        /// API server route URL for a single transaction specified by identifier
        /// </summary>
        public const string TransactionSyncUrl = "transactions/{transactionId:min(1)}/sync";

        /// <summary>
        /// API client URL for transaction metadata
        /// </summary>
        public const string TransactionMetadata = "transactions/metadata";

        /// <summary>
        /// API server route URL for transaction metadata
        /// </summary>
        public const string TransactionMetadataUrl = "transactions/metadata";

        /// <summary>
        /// API client URL for preparing a single transaction specified by identifier
        /// </summary>
        public const string PrepareTransaction = "transactions/{0}/prepare";

        /// <summary>
        /// API server route URL for preparing a single transaction specified by identifier
        /// </summary>
        public const string PrepareTransactionUrl = "transactions/{transactionId:int}/prepare";

        /// <summary>
        /// API client URL for preparing multiple transactions
        /// </summary>
        public const string PrepareTransactions = "transactions/prepare";

        /// <summary>
        /// API server route URL for preparing multiple transactions
        /// </summary>
        public const string PrepareTransactionsUrl = "transactions/prepare";

        /// <summary>
        /// API client URL for reviewing multiple transactions
        /// </summary>
        public const string ReviewTransactions = "transactions/review";

        /// <summary>
        /// API server route URL for reviewing multiple transactions
        /// </summary>
        public const string ReviewTransactionsUrl = "transactions/review";

        /// <summary>
        /// API client URL for rejecting multiple transactions
        /// </summary>
        public const string RejectTransactions = "transactions/reject";

        /// <summary>
        /// API server route URL for rejecting multiple transactions
        /// </summary>
        public const string RejectTransactionsUrl = "transactions/reject";

        /// <summary>
        /// API client URL for confirming multiple transactions
        /// </summary>
        public const string ConfirmTransactions = "transactions/confirm";

        /// <summary>
        /// API server route URL for confirming multiple transactions
        /// </summary>
        public const string ConfirmTransactionsUrl = "transactions/confirm";

        /// <summary>
        /// API client URL for approving multiple transactions
        /// </summary>
        public const string ApproveTransactions = "transactions/approve";

        /// <summary>
        /// API server route URL for approving multiple transactions
        /// </summary>
        public const string ApproveTransactionsUrl = "transactions/approve";

        /// <summary>
        /// API client URL for reviewing a single transaction specified by identifier
        /// </summary>
        public const string ReviewTransaction = "transactions/{0}/review";

        /// <summary>
        /// API server route URL for reviewing a single transaction specified by identifier
        /// </summary>
        public const string ReviewTransactionUrl = "transactions/{transactionId:min(1)}/review";

        /// <summary>
        /// API client URL for rejecting a reviewed transaction specified by identifier
        /// </summary>
        public const string RejectTransaction = "transactions/{0}/reject";

        /// <summary>
        /// API server route URL for rejecting a reviewed transaction specified by identifier
        /// </summary>
        public const string RejectTransactionUrl = "transactions/{transactionId:min(1)}/reject";

        /// <summary>
        /// API client URL for confirming a single transaction specified by identifier
        /// </summary>
        public const string ConfirmTransaction = "transactions/{0}/confirm";

        /// <summary>
        /// API server route URL for confirming a single transaction specified by identifier
        /// </summary>
        public const string ConfirmTransactionUrl = "transactions/{transactionId:min(1)}/confirm";

        /// <summary>
        /// API client URL for approving a single transaction specified by identifier
        /// </summary>
        public const string ApproveTransaction = "transactions/{0}/approve";

        /// <summary>
        /// API server route URL for approving a single transaction specified by identifier
        /// </summary>
        public const string ApproveTransactionUrl = "transactions/{transactionId:min(1)}/approve";

        /// <summary>
        /// API client URL for all articles in a single transaction specified by identifier
        /// </summary>
        public const string TransactionArticles = "transactions/{0}/articles";

        /// <summary>
        /// API server route URL for all articles in a single transaction specified by identifier
        /// </summary>
        public const string TransactionArticlesUrl = "transactions/{transactionId:min(1)}/articles";

        /// <summary>
        /// API server route URL for all articles in a single transaction specified by identifier
        /// </summary>
        public const string TransactionArticlesSyncUrl = "transactions/{transactionId:min(1)}/articles/sync";

        /// <summary>
        /// API client URL for count of all articles in a transaction specified by identifier
        /// </summary>
        public const string TransactionArticleCount = "transactions/{0}/articles/count";

        /// <summary>
        /// API server route URL for count of all articles in a transaction specified by identifier
        /// </summary>
        public const string TransactionArticleCountUrl = "transactions/{transactionId:min(1)}/articles/count";

        /// <summary>
        /// API client URL for a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticle = "transactions/articles/{0}";

        /// <summary>
        /// API server route URL for a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleUrl = "transactions/articles/{articleId:min(1)}";

        /// <summary>
        /// API server route URL for a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleSyncUrl = "transactions/articles/{articleId:min(1)}/sync";

        /// <summary>
        /// API client URL for details of a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleDetails = "transactions/articles/{0}/details";

        /// <summary>
        /// API server route URL for details of a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleDetailsUrl = "transactions/articles/{articleId:min(1)}/details";

        /// <summary>
        /// API server route URL for details of a single transaction article specified by identifier
        /// </summary>
        public const string TransactionArticleDetailsSyncUrl = "transactions/articles/{articleId:min(1)}/details/sync";

        /// <summary>
        /// API client URL for transaction article metadata
        /// </summary>
        public const string TransactionArticleMetadata = "transactions/articles/metadata";

        /// <summary>
        /// API server route URL for transaction article metadata
        /// </summary>
        public const string TransactionArticleMetadataUrl = "transactions/articles/metadata";
    }
}

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with financial accounts.
    /// </summary>
    public sealed class AccountApi
    {
        private AccountApi()
        {
        }

        /// <summary>
        /// API client URL for accounts defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccounts = "accounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for accounts defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsUrl = "accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API server route URL for accounts defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsSyncUrl = "accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync";

        /// <summary>
        /// API client URL for count of all accounts defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchItemCount = "accounts/fp/{0}/branch/{1}/count";

        /// <summary>
        /// API server route URL for count of all accounts defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchItemCountUrl = "accounts/fp/{fpId:int}/branch/{branchId:int}/count";

        /// <summary>
        /// API client URL for a single account specified by identifier
        /// </summary>
        public const string Account = "accounts/{0}";

        /// <summary>
        /// API server route URL for a single account specified by identifier
        /// </summary>
        public const string AccountUrl = "accounts/{accountId:min(1)}";

        /// <summary>
        /// API server route URL for a single account specified by identifier
        /// </summary>
        public const string AccountSyncUrl = "accounts/{accountId:min(1)}/sync";

        /// <summary>
        /// API client URL for all accounts
        /// </summary>
        public const string Accounts = "accounts";

        /// <summary>
        /// API server route URL for all accounts
        /// </summary>
        public const string AccountsUrl = "accounts";

        /// <summary>
        /// API server route URL for all accounts
        /// </summary>
        public const string AccountsSyncUrl = "accounts/sync";

        /// <summary>
        /// API client URL for details of an account specified by identifier
        /// </summary>
        public const string AccountDetails = "accounts/{0}/details";

        /// <summary>
        /// API server route URL for details of an account specified by identifier
        /// </summary>
        public const string AccountDetailsUrl = "accounts/{accountId:min(1)}/details";

        /// <summary>
        /// API server route URL for details of an account specified by identifier
        /// </summary>
        public const string AccountDetailsSyncUrl = "accounts/{accountId:min(1)}/details/sync";

        /// <summary>
        /// API client URL for transaction articles that reference an account specified by identifier
        /// </summary>
        public const string AccountArticles = "accounts/{0}/articles";

        /// <summary>
        /// API server route URL for transaction articles that reference an account specified by identifier
        /// </summary>
        public const string AccountArticlesUrl = "accounts/{accountId:min(1)}/articles";
    }
}

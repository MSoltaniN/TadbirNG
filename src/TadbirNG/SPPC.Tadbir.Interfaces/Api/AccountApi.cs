using System;

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
        /// API client URL for all active account items
        /// </summary>
        public const string Accounts = "accounts";

        /// <summary>
        /// API server route URL for all active account items
        /// </summary>
        public const string AccountsUrl = "accounts";

        /// <summary>
        /// API client URL for all inactive account items
        /// </summary>
        public const string InactiveAccounts = "accounts/inactive";

        /// <summary>
        /// API server route URL for all inactive account items
        /// </summary>
        public const string InactiveAccountsUrl = "accounts/inactive";

        /// <summary>
        /// API client URL for all account items
        /// </summary>
        public const string AllAccounts = "accounts/all";

        /// <summary>
        /// API server route URL for all account items
        /// </summary>
        public const string AllAccountsUrl = "accounts/all";

        /// <summary>
        /// API client URL for ledger accounts defined in current environment
        /// </summary>
        public const string LedgerAccounts = "accounts/ledger";

        /// <summary>
        /// API server route URL for ledger accounts defined in current environment
        /// </summary>
        public const string LedgerAccountsUrl = "accounts/ledger";

        /// <summary>
        /// API client URL for ledger accounts by group id defined in current environment
        /// </summary>
        public const string LedgerAccountsByGroupId = "accounts/ledger/{0}";

        /// <summary>
        /// API server route URL for ledger accounts by group id defined in current environment
        /// </summary>
        public const string LedgerAccountsByGroupIdUrl = "accounts/ledger/{groupId:min(1)}";

        /// <summary>
        /// API client URL for a single account specified by identifier
        /// </summary>
        public const string Account = "accounts/{0}";

        /// <summary>
        /// API server route URL for a single account specified by identifier
        /// </summary>
        public const string AccountUrl = "accounts/{accountId:min(1)}";

        /// <summary>
        /// API client URL for all child accounts under a specific account in hierarchy
        /// </summary>
        public const string AccountChildren = "accounts/{0}/children";

        /// <summary>
        /// API server route URL for all child accounts under a specific account in hierarchy
        /// </summary>
        public const string AccountChildrenUrl = "accounts/{accountId:min(1)}/children";

        /// <summary>
        /// API client URL for a new child for a parent account specified by unique identifier
        /// </summary>
        public const string NewChildAccount = "accounts/{0}/children/new";

        /// <summary>
        /// API server route URL for a new child for a parent account specified by unique identifier
        /// </summary>
        public const string NewChildAccountUrl = "accounts/{accountId:int}/children/new";

        /// <summary>
        /// API client URL for account full code
        /// </summary>
        public const string AccountFullCode = "accounts/{0}/fullcode";

        /// <summary>
        /// API server route URL for account full code
        /// </summary>
        public const string AccountFullCodeUrl = "accounts/{accountId:int}/fullcode";

        /// <summary>
        /// API client URL for accounts count
        /// </summary>
        public const string AccountsCount = "accounts/count";

        /// <summary>
        /// API server route URL for accounts count
        /// </summary>
        public const string AccountsCountUrl = "accounts/count";

        /// <summary>
        /// API client URL for all selectable accounts
        /// </summary>
        public const string AccountsLookup = "accounts/lookup";

        /// <summary>
        /// API server route URL for all selectable accounts
        /// </summary>
        public const string AccountsLookupUrl = "accounts/lookup";

        /// <summary>
        /// API client URL for full data of a single account specified by identifier
        /// </summary>
        public const string AccountFullData = "accounts/{0}/fulldata";

        /// <summary>
        /// API server route URL for full data of a single account specified by identifier
        /// </summary>
        public const string AccountFullDataUrl = "accounts/{accountId:min(1)}/fulldata";

        /// <summary>
        /// API client URL for marking an active account as inactive
        /// </summary>
        public const string DeactivateAccount = "accounts/{0}/deactivate";

        /// <summary>
        /// API server route URL for marking an active account as inactive
        /// </summary>
        public const string DeactivateAccountUrl = "accounts/{accountId:min(1)}/deactivate";

        /// <summary>
        /// API client URL for marking an inactive account as active
        /// </summary>
        public const string ReactivateAccount = "accounts/{0}/reactivate";

        /// <summary>
        /// API server route URL for marking an inactive account as active
        /// </summary>
        public const string ReactivateAccountUrl = "accounts/{accountId:min(1)}/reactivate";
    }
}

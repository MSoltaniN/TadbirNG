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
        /// API client URL for accounts defined in current environment
        /// </summary>
        public const string EnvironmentAccounts = "accounts";

        /// <summary>
        /// API server route URL for accounts defined in current environment
        /// </summary>
        public const string EnvironmentAccountsUrl = "accounts";

        /// <summary>
        /// API client URL for accounts lookup defined in current environment
        /// </summary>
        public const string EnvironmentAccountsLookup = "accounts/lookup";

        /// <summary>
        /// API client URL for accounts ledger defined in current environment
        /// </summary>
        public const string EnvironmentAccountsLookupUrl = "accounts/lookup";

        /// <summary>
        /// API client URL for ledger accounts defined in current environment
        /// </summary>
        public const string EnvironmentLedgerAccounts = "accounts/ledger";

        /// <summary>
        /// API server route URL for ledger accounts defined in current environment
        /// </summary>
        public const string EnvironmentLedgerAccountsUrl = "accounts/ledger";

        /// <summary>
        /// API client URL for ledger accounts by group id defined in current environment
        /// </summary>
        public const string EnvironmentLedgerAccountsByGroupId = "accounts/ledger/{0}";

        /// <summary>
        /// API server route URL for ledger accounts by group id defined in current environment
        /// </summary>
        public const string EnvironmentLedgerAccountsByGroupIdUrl = "accounts/ledger/{groupId:min(1)}";

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
        public const string EnvironmentNewChildAccount = "accounts/{0}/children/new";

        /// <summary>
        /// API server route URL for a new child for a parent account specified by unique identifier
        /// </summary>
        public const string EnvironmentNewChildAccountUrl = "accounts/{accountId:int}/children/new";

        /// <summary>
        /// API client URL for account full code
        /// </summary>
        public const string AccountFullCode = "accounts/fullcode/{0}";

        /// <summary>
        /// API server route URL for account full code
        /// </summary>
        public const string AccountFullCodeUrl = "accounts/fullcode/{parentId}";
    }
}

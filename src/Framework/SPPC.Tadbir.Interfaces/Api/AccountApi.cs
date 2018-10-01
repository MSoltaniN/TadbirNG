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
        /// API server route URL for accounts lookup defined in current environment
        /// </summary>
        public const string EnvironmentAccountsLookupUrl = "accounts/lookup";

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
        /// API client URL for details of an account specified by identifier
        /// </summary>
        public const string AccountDetails = "accounts/{0}/details";

        /// <summary>
        /// API server route URL for details of an account specified by identifier
        /// </summary>
        public const string AccountDetailsUrl = "accounts/{accountId:min(1)}/details";

        /// <summary>
        /// API client URL for account metadata
        /// </summary>
        public const string AccountMetadata = "accounts/metadata";

        /// <summary>
        /// API server route URL for account metadata
        /// </summary>
        public const string AccountMetadataUrl = "accounts/metadata";

        /// <summary>
        /// API client URL for voucher articles that reference an account specified by identifier
        /// </summary>
        public const string AccountArticles = "accounts/{0}/articles";

        /// <summary>
        /// API server route URL for voucher articles that reference an account specified by identifier
        /// </summary>
        public const string AccountArticlesUrl = "accounts/{accountId:min(1)}/articles";

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

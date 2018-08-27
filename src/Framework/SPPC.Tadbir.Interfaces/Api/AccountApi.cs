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
        /// API client URL for accounts lookup defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsLookup = "accounts/lookup/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for accounts lookup defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsLookupUrl = "accounts/lookup/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

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
        /// API client URL for all accounts
        /// </summary>
        public const string Accounts = "accounts";

        /// <summary>
        /// API server route URL for all accounts
        /// </summary>
        public const string AccountsUrl = "accounts";

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
        public const string AccountFullCode = "accounts/fullcode/{parentId}";

        /// <summary>
        /// API server route URL for account full code
        /// </summary>
        public const string AccountFullCodeUrl = "accounts/fullcode/{parentId}";
    }
}

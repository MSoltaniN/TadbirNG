using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with detail accounts.
    /// </summary>
    public sealed class DetailAccountApi
    {
        private DetailAccountApi()
        {
        }

        /// <summary>
        /// API client URL for detail accounts defined in a current environment
        /// </summary>
        public const string EnvironmentDetailAccounts = "faccounts";

        /// <summary>
        /// API server route URL for detail accounts defined in a current environment
        /// </summary>
        public const string EnvironmentDetailAccountsUrl = "faccounts";

        /// <summary>
        /// API client URL for detail account lookups defined in a current environment
        /// </summary>
        public const string EnvironmentDetailAccountsLookup = "faccounts/lookup";

        /// <summary>
        /// API server route URL for detail account lookups defined in a current environment
        /// </summary>
        public const string EnvironmentDetailAccountsLookupUrl = "faccounts/lookup";

        /// <summary>
        /// API client URL for detail accounts ledger defined in current environment
        /// </summary>
        public const string EnvironmentDetailAccountsLedger = "faccounts/ledger";

        /// <summary>
        /// API server route URL for detail accounts ledger defined in current environment
        /// </summary>
        public const string EnvironmentDetailAccountsLedgerUrl = "faccounts/ledger";

        /// <summary>
        /// API client URL for a detail account item specified by unique identifier
        /// </summary>
        public const string DetailAccount = "faccounts/{0}";

        /// <summary>
        /// API server route URL for a detail account item specified by unique identifier
        /// </summary>
        public const string DetailAccountUrl = "faccounts/{faccountId:min(1)}";

        /// <summary>
        /// API client URL for all child detail accounts under a specific detail account in hierarchy
        /// </summary>
        public const string DetailAccountChildren = "faccounts/{0}/children";

        /// <summary>
        /// API server route URL for all child detail accounts under a specific detail account in hierarchy
        /// </summary>
        public const string DetailAccountChildrenUrl = "faccounts/{faccountId:min(1)}/children";

        /// <summary>
        /// API client URL for a new child for a parent detail account specified by unique identifier
        /// </summary>
        public const string EnvironmentNewChildDetailAccount = "faccounts/{0}/children/new";

        /// <summary>
        /// API server route URL for a new child for a parent detail account specified by unique identifier
        /// </summary>
        public const string EnvironmentNewChildDetailAccountUrl = "faccounts/{faccountId:int}/children/new";

        /// <summary>
        /// API client URL for detail account full code
        /// </summary>
        public const string DetailAccountFullCode = "faccounts/fullcode/{0}";

        /// <summary>
        /// API server route URL for detail account full code
        /// </summary>
        public const string DetailAccountFullCodeUrl = "faccounts/fullcode/{parentId}";
    }
}

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
        /// API client URL for detail accounts defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchDetailAccounts = "faccounts/fp/{0}/branch/{0}";

        /// <summary>
        /// API server route URL for detail accounts defined in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchDetailAccountsUrl = "faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for all detail account items
        /// </summary>
        public const string DetailAccounts = "faccounts";

        /// <summary>
        /// API server route URL for all detail account items
        /// </summary>
        public const string DetailAccountsUrl = "faccounts";

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
        /// API client URL for detail account metadata
        /// </summary>
        public const string DetailAccountMetadata = "faccounts/metadata";

        /// <summary>
        /// API server route URL for detail account metadata
        /// </summary>
        public const string DetailAccountMetadataUrl = "faccounts/metadata";
    }
}

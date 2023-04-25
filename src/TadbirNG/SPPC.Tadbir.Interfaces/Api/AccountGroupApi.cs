using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with account groups.
    /// </summary>
    public sealed class AccountGroupApi
    {
        private AccountGroupApi()
        {
        }

        /// <summary>
        /// API client URL for all account groups
        /// </summary>
        public const string AccountGroups = "acc-groups";

        /// <summary>
        /// API server route URL for all account groups
        /// </summary>
        public const string AccountGroupsUrl = "acc-groups";

        /// <summary>
        /// API client URL for a single account group specified by unique identifier
        /// </summary>
        public const string AccountGroup = "acc-groups/{0}";

        /// <summary>
        /// API server route URL for a single account group specified by unique identifier
        /// </summary>
        public const string AccountGroupUrl = "acc-groups/{groupId:min(1)}";

        /// <summary>
        /// API client URL for ledger-level accounts for an account group specified by unique identifier
        /// </summary>
        public const string GroupLedgerAccounts = "acc-groups/{0}/accounts";

        /// <summary>
        /// API server route URL for ledger-level accounts for an account group specified by unique identifier
        /// </summary>
        public const string GroupLedgerAccountsUrl = "acc-groups/{groupId:min(1)}/accounts";

        /// <summary>
        /// API client URL for account group brief
        /// </summary>
        public const string AccountGroupBrief = "acc-groups/brief";

        /// <summary>
        /// API server route URL for account group brief
        /// </summary>
        public const string AccountGroupBriefUrl = "acc-groups/brief";
    }
}

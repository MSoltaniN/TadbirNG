using System;
using System.Collections.Generic;
using System.Text;

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
        public const string AccountGroups = "accgroups";

        /// <summary>
        /// API server route URL for all account groups
        /// </summary>
        public const string AccountGroupsUrl = "accgroups";

        /// <summary>
        /// API client URL for a single account group specified by unique identifier
        /// </summary>
        public const string AccountGroup = "accgroups/{0}";

        /// <summary>
        /// API server route URL for a single account group specified by unique identifier
        /// </summary>
        public const string AccountGroupUrl = "accgroups/{groupId:min(1)}";

        /// <summary>
        /// API client URL for account group metadata
        /// </summary>
        public const string AccountGroupMetadata = "accgroups/metadata";

        /// <summary>
        /// API server route URL for account group metadata
        /// </summary>
        public const string AccountGroupMetadataUrl = "accgroups/metadata";
    }
}

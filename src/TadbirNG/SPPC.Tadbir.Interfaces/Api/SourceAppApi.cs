using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with sources/applications
    /// </summary>
    public sealed class SourceAppApi
    {
        /// <summary>
        /// API client URL for all active source/application items
        /// </summary>
        public const string SourceApps = "source-apps";

        /// <summary>
        /// API server route URL for all active source/application items
        /// </summary>
        public const string SourceAppsUrl = "source-apps";

        /// <summary>
        /// API client URL for all inactive source/application items
        /// </summary>
        public const string InactiveSourceApps = "source-apps/inactive";

        /// <summary>
        /// API server route URL for all inactive source/application items
        /// </summary>
        public const string InactiveSourceAppsUrl = "source-apps/inactive";

        /// <summary>
        /// API client URL for all source/application items
        /// </summary>
        public const string AllSourceApps = "source-apps/all";

        /// <summary>
        /// API server route URL for all source/application items
        /// </summary>
        public const string AllSourceAppsUrl = "source-apps/all";

        /// <summary>
        /// API client URL for a source/application item specified by unique identifier
        /// </summary>
        public const string SourceApp = "source-apps/{0}";

        /// <summary>
        /// API server route URL for a source/application item specified by unique identifier
        /// </summary>
        public const string SourceAppUrl = "source-apps/{sourceAppId:min(1)}";

        /// <summary>
        /// API client URL for a newly initialized and saved source/application
        /// </summary>
        public const string NewSourceApp = "source-apps/new";

        /// <summary>
        /// API server route URL for a newly initialized and saved source/application
        /// </summary>
        public const string NewSourceAppUrl = "source-apps/new";

        /// <summary>
        /// API client URL for marking an active source/application as inactive
        /// </summary>
        public const string DeactivateSourceApp = "source-apps/{0}/deactivate";

        /// <summary>
        /// API server route URL for marking an active source/application as inactive
        /// </summary>
        public const string DeactivateSourceAppUrl = "source-apps/{sourceAppId:min(1)}/deactivate";

        /// <summary>
        /// API client URL for marking an inactive source/application as active
        /// </summary>
        public const string ReactivateSourceApp = "source-apps/{0}/reactivate";

        /// <summary>
        /// API server route URL for marking an inactive source/application as active
        /// </summary>
        public const string ReactivateSourceAppUrl = "source-apps/{sourceAppId:min(1)}/reactivate";
    }
}

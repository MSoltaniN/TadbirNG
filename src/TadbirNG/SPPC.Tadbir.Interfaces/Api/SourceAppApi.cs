using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with sources/applications
    /// </summary>
    public sealed class SourceAppApi
    {
        /// <summary>
        /// API client URL for all source/application items
        /// </summary>
        public const string SourceApps = "source-apps";

        /// <summary>
        /// API server route URL for all source/application items
        /// </summary>
        public const string SourceAppsUrl = "source-apps";

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
    }
}

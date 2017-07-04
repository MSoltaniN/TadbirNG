using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with application settings.
    /// </summary>
    public sealed class SettingsApi
    {
        private SettingsApi()
        {
        }

        /// <summary>
        /// API server route URL for workflow settings
        /// </summary>
        public const string WorkflowSettings = "settings/workflows";

        /// <summary>
        /// API client URL for workflow settings
        /// </summary>
        public const string WorkflowSettingsUrl = "settings/workflows";
    }
}

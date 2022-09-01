using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with application dashboards.
    /// </summary>
    public static class DashboardApi
    {
        /// <summary>
        /// API client URL for summary values in the main dashboard
        /// </summary>
        public const string Summaries = "dashboard/summaries";

        /// <summary>
        /// API server route URL for summary values in the main dashboard
        /// </summary>
        public const string SummariesUrl = "dashboard/summaries";

        /// <summary>
        /// API client URL for general license information
        /// </summary>
        public const string LicenseInfo = "dashboard/license";

        /// <summary>
        /// API server route URL for general license information
        /// </summary>
        public const string LicenseInfoUrl = "dashboard/license";

        /// <summary>
        /// API client URL for all widgets currently defined by all users
        /// </summary>
        public const string Widgets = "dashboard/widgets";

        /// <summary>
        /// API server route URL for all widgets currently defined by all users
        /// </summary>
        public const string WidgetsUrl = "dashboard/widgets";

        /// <summary>
        /// API client URL for a widget specified by unique identifier
        /// </summary>
        public const string Widget = "dashboard/widgets/{0}";

        /// <summary>
        /// API server route URL for a widget specified by unique identifier
        /// </summary>
        public const string WidgetUrl = "dashboard/widgets/{widgetId:min(1)}";
    }
}

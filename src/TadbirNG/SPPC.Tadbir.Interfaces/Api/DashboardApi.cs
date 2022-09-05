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
        /// API client URL for the dashboard created by (or assigned to) current user
        /// </summary>
        public const string CurrentDashboard = "dashboard/current";

        /// <summary>
        /// API server route URL for the dashboard created by (or assigned to) current user
        /// </summary>
        public const string CurrentDashboardUrl = "dashboard/current";

        /// <summary>
        /// API client URL for all usable widget functions
        /// </summary>
        public const string WidgetFunctionsLookup = "dashboard/lookup/functions";

        /// <summary>
        /// API server route URL for all usable widget functions
        /// </summary>
        public const string WidgetFunctionsLookupUrl = "dashboard/lookup/functions";

        /// <summary>
        /// API client URL for all usable widget types
        /// </summary>
        public const string WidgetTypesLookup = "dashboard/lookup/wtypes";

        /// <summary>
        /// API server route URL for all usable widget types
        /// </summary>
        public const string WidgetTypesLookupUrl = "dashboard/lookup/wtypes";

        /// <summary>
        /// API client URL for all usable widgets
        /// </summary>
        public const string WidgetsLookup = "dashboard/lookup/widgets";

        /// <summary>
        /// API server route URL for all usable widgets
        /// </summary>
        public const string WidgetsLookupUrl = "dashboard/lookup/widgets";

        /// <summary>
        /// API client URL for calculated data of one or more account vectors in a specific widget
        /// </summary>
        public const string WidgetData = "dashboard/widgets/{0}/data";

        /// <summary>
        /// API server route URL for calculated data of one or more account vectors in a specific widget
        /// </summary>
        public const string WidgetDataUrl = "dashboard/widgets/{widgetId:min(1)}/data";
    }
}

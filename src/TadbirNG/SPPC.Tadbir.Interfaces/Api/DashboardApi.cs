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
        /// API client URL for all charts currently defined in dashboard
        /// </summary>
        public const string Charts = "dashboard/charts";

        /// <summary>
        /// API server route URL for all charts currently defined in dashboard
        /// </summary>
        public const string ChartsUrl = "dashboard/charts";

        /// <summary>
        /// API client URL for a charts specified by unique identifier
        /// </summary>
        public const string Chart = "dashboard/charts/{chartId:min(1)}";

        /// <summary>
        /// API server route URL for a charts specified by unique identifier
        /// </summary>
        public const string ChartUrl = "dashboard/charts/{0}";
    }
}

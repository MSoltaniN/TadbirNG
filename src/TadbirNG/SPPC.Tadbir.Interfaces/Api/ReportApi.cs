using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for application reports.
    /// </summary>
    public sealed class ReportApi
    {
        private ReportApi()
        {
        }

        /// <summary>
        /// API client URL for report hierarchy for Report Manager UI
        /// </summary>
        public const string ReportsHierarchy = "reports/tree";

        /// <summary>
        /// API server route URL for report hierarchy for Report Manager UI
        /// </summary>
        public const string ReportsHierarchyUrl = "reports/tree";

        /// <summary>
        /// API client URL for all reports accessible to Report Manager UI
        /// </summary>
        public const string Reports = "reports";

        /// <summary>
        /// API server route URL for all reports accessible to Report Manager UI
        /// </summary>
        public const string ReportsUrl = "reports";

        /// <summary>
        /// API client URL for all reports associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByView = "reports/view/{0}";

        /// <summary>
        /// API server route URL for all reports associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewUrl = "reports/view/{viewId:min(1)}";

        /// <summary>
        /// API client URL for default report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewDefault = "reports/view/{0}/default";

        /// <summary>
        /// API server route URL for default report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewDefaultUrl = "reports/view/{viewId:min(1)}/default";

        /// <summary>
        /// API client URL for quick report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewQuickReport = "reports/view/{0}/quickreport";

        /// <summary>
        /// API server route URL for quick report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewQuickReportUrl = "reports/view/{viewId:min(1)}/quickreport";

        /// <summary>
        /// API client URL for all reports associated with a subsystem specified by unique identifier
        /// </summary>
        public const string ReportsBySubsystem = "reports/subsys/{0}";

        /// <summary>
        /// API server route URL for all reports associated with a subsystem specified by unique identifier
        /// </summary>
        public const string ReportsBySubsystemUrl = "reports/subsys/{subsysId:min(1)}";

        /// <summary>
        /// API client URL for a single report specified by unique identifier
        /// </summary>
        public const string Report = "reports/{0}";

        /// <summary>
        /// API server route URL for a single report specified by unique identifier
        /// </summary>
        public const string ReportUrl = "reports/{reportId:min(1)}";

        /// <summary>
        /// API client URL for default status of a single report specified by unique identifier
        /// </summary>
        public const string ReportDefault = "reports/{0}/default";

        /// <summary>
        /// API server route URL for default status of a single report specified by unique identifier
        /// </summary>
        public const string ReportDefaultUrl = "reports/{reportId:min(1)}/default";

        /// <summary>
        /// API client URL for design data for a single report specified by unique identifier
        /// </summary>
        public const string ReportDesign = "reports/{0}/design";

        /// <summary>
        /// API server route URL for design data for a single report specified by unique identifier
        /// </summary>
        public const string ReportDesignUrl = "reports/{reportId:min(1)}/design";

        /// <summary>
        /// API client URL for caption or title of a single report specified by unique identifier
        /// </summary>
        public const string ReportCaption = "reports/{0}/caption";

        /// <summary>
        /// API server route URL for caption or title of a single report specified by unique identifier
        /// </summary>
        public const string ReportCaptionUrl = "reports/{reportId:min(1)}/caption";

        /// <summary>
        /// API client URL for report metadata for a view specified by unique identifier
        /// </summary>
        public const string ReportMetadataByView = "reports/metadata/{0}";

        /// <summary>
        /// API server route URL for report metadata for a view specified by unique identifier
        /// </summary>
        public const string ReportMetadataByViewUrl = "reports/metadata/{viewId:min(1)}";

        /// <summary>
        /// API client URL for a quick report invoked by environment user
        /// </summary>
        public const string EnvironmentQuickReport = "reports/quickreport";

        /// <summary>
        /// API server route URL for a quick report invoked by environment user
        /// </summary>
        public const string EnvironmentQuickReportUrl = "reports/quickreport/{unit:min(1)}";
    }
}

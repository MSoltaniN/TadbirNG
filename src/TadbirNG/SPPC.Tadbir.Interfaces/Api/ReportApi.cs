﻿using System;
using System.Collections.Generic;

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

        #region Report System API

        /// <summary>
        /// API client URL for report hierarchy for Report Manager UI
        /// </summary>
        public const string ReportsHierarchy = "reports/sys/tree";

        /// <summary>
        /// API server route URL for report hierarchy for Report Manager UI
        /// </summary>
        public const string ReportsHierarchyUrl = "reports/sys/tree";

        /// <summary>
        /// API client URL for all reports accessible to Report Manager UI
        /// </summary>
        public const string Reports = "reports/sys";

        /// <summary>
        /// API server route URL for all reports accessible to Report Manager UI
        /// </summary>
        public const string ReportsUrl = "reports/sys";

        /// <summary>
        /// API client URL for all reports associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByView = "reports/sys/view/{0}";

        /// <summary>
        /// API server route URL for all reports associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewUrl = "reports/sys/view/{viewId:min(1)}";

        /// <summary>
        /// API client URL for default report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewDefault = "reports/sys/view/{0}/default";

        /// <summary>
        /// API server route URL for default report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewDefaultUrl = "reports/sys/view/{viewId:min(1)}/default";

        /// <summary>
        /// API client URL for quick report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewQuickReport = "reports/sys/view/{0}/quickreport";

        /// <summary>
        /// API server route URL for quick report associated with a view specified by unique identifier
        /// </summary>
        public const string ReportsByViewQuickReportUrl = "reports/sys/view/{viewId:min(1)}/quickreport";

        /// <summary>
        /// API client URL for all reports associated with a subsystem specified by unique identifier
        /// </summary>
        public const string ReportsBySubsystem = "reports/sys/subsys/{0}";

        /// <summary>
        /// API server route URL for all reports associated with a subsystem specified by unique identifier
        /// </summary>
        public const string ReportsBySubsystemUrl = "reports/sys/subsys/{subsysId:min(1)}";

        /// <summary>
        /// API client URL for a single report specified by unique identifier
        /// </summary>
        public const string Report = "reports/sys/{0}";

        /// <summary>
        /// API server route URL for a single report specified by unique identifier
        /// </summary>
        public const string ReportUrl = "reports/sys/{reportId:min(1)}";

        /// <summary>
        /// API client URL for default status of a single report specified by unique identifier
        /// </summary>
        public const string ReportDefault = "reports/sys/{0}/default";

        /// <summary>
        /// API server route URL for default status of a single report specified by unique identifier
        /// </summary>
        public const string ReportDefaultUrl = "reports/sys/{reportId:min(1)}/default";

        /// <summary>
        /// API client URL for design data for a single report specified by unique identifier
        /// </summary>
        public const string ReportDesign = "reports/sys/{0}/design";

        /// <summary>
        /// API server route URL for design data for a single report specified by unique identifier
        /// </summary>
        public const string ReportDesignUrl = "reports/sys/{reportId:min(1)}/design";

        /// <summary>
        /// API client URL for caption or title of a single report specified by unique identifier
        /// </summary>
        public const string ReportCaption = "reports/sys/{0}/caption";

        /// <summary>
        /// API server route URL for caption or title of a single report specified by unique identifier
        /// </summary>
        public const string ReportCaptionUrl = "reports/sys/{reportId:min(1)}/caption";

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
        public const string EnvironmentQuickReport = "reports/sys/quickreport";

        /// <summary>
        /// API server route URL for a quick report invoked by environment user
        /// </summary>
        public const string EnvironmentQuickReportUrl = "reports/sys/quickreport/{unit:min(1)}";

        #endregion

        #region Business Reports API

        /// <summary>
        /// API client URL for Voucher Summary by Date report
        /// </summary>
        public const string EnvironmentVoucherSummaryByDate = "reports/voucher/sum-by-date";

        /// <summary>
        /// API server route URL for Voucher Summary by Date report
        /// </summary>
        public const string EnvironmentVoucherSummaryByDateUrl = "reports/voucher/sum-by-date";

        /// <summary>
        /// API client URL for Voucher Standard Form report
        /// </summary>
        public const string VoucherStandardForm = "reports/voucher/{0}/std-form";

        /// <summary>
        /// API server route URL for Voucher Standard Form report
        /// </summary>
        public const string VoucherStandardFormUrl = "reports/voucher/{voucherNo:min(1)}/std-form";

        /// <summary>
        /// API client URL for Voucher Standard Form With Detail report
        /// </summary>
        public const string VoucherStandardFormWithDetail = "reports/voucher/{0}/std-form-detail";

        /// <summary>
        /// API server route URL for Voucher Standard Form With Detail report
        /// </summary>
        public const string VoucherStandardFormWithDetailUrl = "reports/voucher/{voucherNo:min(1)}/std-form-detail";

        #endregion
    }
}
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
        /// API client URL for debit turnover function calculated for one or more account vectors
        /// </summary>
        public const string DebitTurnoverFunction = "dashboard/functions/debit-to";

        /// <summary>
        /// API server route URL for debit turnover function calculated for one or more account vectors
        /// </summary>
        public const string DebitTurnoverFunctionUrl = "dashboard/functions/debit-to";

        /// <summary>
        /// API client URL for credit turnover function calculated for one or more account vectors
        /// </summary>
        public const string CreditTurnoverFunction = "dashboard/functions/credit-to";

        /// <summary>
        /// API server route URL for credit turnover function calculated for one or more account vectors
        /// </summary>
        public const string CreditTurnoverFunctionUrl = "dashboard/functions/credit-to";

        /// <summary>
        /// API client URL for net turnover function calculated for one or more account vectors
        /// </summary>
        public const string NetTurnoverFunction = "dashboard/functions/net-to";

        /// <summary>
        /// API server route URL for net turnover function calculated for one or more account vectors
        /// </summary>
        public const string NetTurnoverFunctionUrl = "dashboard/functions/net-to";

        /// <summary>
        /// API client URL for balance function calculated for one or more account vectors
        /// </summary>
        public const string BalanceFunction = "dashboard/functions/balance";

        /// <summary>
        /// API server route URL for balance function calculated for one or more account vectors
        /// </summary>
        public const string BalanceFunctionUrl = "dashboard/functions/balance";
    }
}

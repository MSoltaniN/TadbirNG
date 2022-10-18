using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with application dashboards.
    /// </summary>
    public static class DashboardApi
    {
        #region Dashboard Management

        /// <summary>
        /// API client URL for the dashboard created by (or assigned to) current user
        /// </summary>
        public const string CurrentDashboard = "dashboard/current";

        /// <summary>
        /// API server route URL for the dashboard created by (or assigned to) current user
        /// </summary>
        public const string CurrentDashboardUrl = "dashboard/current";

        /// <summary>
        /// API client URL for a new dashboard created by (or assigned to) current user
        /// </summary>
        public const string NewDashboard = "dashboard/new";

        /// <summary>
        /// API server route URL for a new dashboard created by (or assigned to) current user
        /// </summary>
        public const string NewDashboardUrl = "dashboard/new";

        /// <summary>
        /// API client URL for all tabs added to current dashboard
        /// </summary>
        public const string DashboardTabs = "dashboard/tabs";

        /// <summary>
        /// API server route URL for all tabs added to current dashboard
        /// </summary>
        public const string DashboardTabsUrl = "dashboard/tabs";

        /// <summary>
        /// API client URL for a specific tab added to a dashboard
        /// </summary>
        public const string DashboardTab = "dashboard/tabs/{0}";

        /// <summary>
        /// API server route URL for a specific tab added to a dashboard
        /// </summary>
        public const string DashboardTabUrl = "dashboard/tabs/{tabId:min(1)}";

        /// <summary>
        /// API client URL for all widgets in all dashboard tabs
        /// </summary>
        public const string AllTabWidgets = "dashboard/tabs/widgets";

        /// <summary>
        /// API server route URL for all widgets in all dashboard tabs
        /// </summary>
        public const string AllTabWidgetsUrl = "dashboard/tabs/widgets";

        /// <summary>
        /// API client URL for all widgets added to a dashboard tab
        /// </summary>
        public const string TabWidgets = "dashboard/tabs/{0}/widgets";

        /// <summary>
        /// API server route URL for all widgets added to a dashboard tab
        /// </summary>
        public const string TabWidgetsUrl = "dashboard/tabs/{tabId:min(1)}/widgets";

        /// <summary>
        /// API client URL for a specific widget added to a dashboard tab
        /// </summary>
        public const string TabWidget = "dashboard/tabs/{0}/widgets/{1}";

        /// <summary>
        /// API server route URL for a specific widget added to a dashboard tab
        /// </summary>
        public const string TabWidgetUrl = "dashboard/tabs/{tabId:min(1)}/widgets/{widgetId:min(1)}";

        #endregion

        #region Widget Management

        /// <summary>
        /// API client URL for all widgets created by current user
        /// </summary>
        public const string Widgets = "dashboard/widgets";

        /// <summary>
        /// API server route URL for all widgets created by current user
        /// </summary>
        public const string WidgetsUrl = "dashboard/widgets";

        /// <summary>
        /// API client URL for all widgets accessible to current user
        /// </summary>
        public const string AllWidgets = "dashboard/widgets/all";

        /// <summary>
        /// API server route URL for all widgets accessible to current user
        /// </summary>
        public const string AllWidgetsUrl = "dashboard/widgets/all";

        /// <summary>
        /// API client URL for a widget specified by unique identifier
        /// </summary>
        public const string Widget = "dashboard/widgets/{0}";

        /// <summary>
        /// API server route URL for a widget specified by unique identifier
        /// </summary>
        public const string WidgetUrl = "dashboard/widgets/{widgetId:min(1)}";

        /// <summary>
        /// API client URL for a widget specified by unique identifier
        /// </summary>
        public const string WidgetUsage = "dashboard/widgets/{0}/usage";

        /// <summary>
        /// API server route URL for a widget specified by unique identifier
        /// </summary>
        public const string WidgetUsageUrl = "dashboard/widgets/{widgetId:min(1)}/usage";

        /// <summary>
        /// API client URL for calculated data of one or more account vectors in a specific widget
        /// </summary>
        public const string WidgetData = "dashboard/widgets/{0}/data";

        /// <summary>
        /// API server route URL for calculated data of one or more account vectors in a specific widget
        /// </summary>
        public const string WidgetDataUrl = "dashboard/widgets/{widgetId:min(1)}/data";

        /// <summary>
        /// API client URL for all parameters required by a specific function
        /// </summary>
        public const string FunctionParameters = "dashboard/functions/{0}/params";

        /// <summary>
        /// API server route URL for all parameters required by a specific function
        /// </summary>
        public const string FunctionParametersUrl = "dashboard/functions/{functionId:min(1)}/params";

        #endregion

        #region Data Lookup

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

        #endregion

        /// <summary>
        /// API client URL for general license information
        /// </summary>
        public const string LicenseInfo = "dashboard/license";

        /// <summary>
        /// API server route URL for general license information
        /// </summary>
        public const string LicenseInfoUrl = "dashboard/license";
    }
}

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
        /// API client URL for all application settings
        /// </summary>
        public const string AllSettings = "settings";

        /// <summary>
        /// API server route URL for all application settings
        /// </summary>
        public const string AllSettingsUrl = "settings";

        /// <summary>
        /// API client URL for a single application settings specified by unique identifier
        /// </summary>
        public const string Setting = "settings/{0}";

        /// <summary>
        /// API server route URL for a single application settings specified by unique identifier
        /// </summary>
        public const string SettingUrl = "settings/{settingId:min(1)}";

        /// <summary>
        /// API client URL for all list form view settings for a user specified by unique identifier
        /// </summary>
        public const string ListSettingsByUser = "settings/list/users/{0}";

        /// <summary>
        /// API server route URL for all list form view settings for a user specified by unique identifier
        /// </summary>
        public const string ListSettingsByUserUrl = "settings/list/users/{userId:min(1)}";

        /// <summary>
        /// API client URL for a single view settings for a user specified by unique identifier
        /// </summary>
        public const string ListSettingsByUserAndView = "settings/list/users/{0}/views/{1}";

        /// <summary>
        /// API server route URL for a single view settings for a user specified by unique identifier
        /// </summary>
        public const string ListSettingsByUserAndViewUrl = "settings/list/users/{userId:min(1)}/views/{viewId:min(1)}";

        /// <summary>
        /// API client URL for all quick search settings for a user specified by unique identifier
        /// </summary>
        public const string QuickSearchSettingsByUser = "settings/qsearch/users/{0}";

        /// <summary>
        /// API server route URL for all quick search settings for a user specified by unique identifier
        /// </summary>
        public const string QuickSearchSettingsByUserUrl = "settings/qsearch/users/{userId:min(1)}";

        /// <summary>
        /// API client URL for quick search settings for a user and a view
        /// (both specified by unique identifier)
        /// </summary>
        public const string QuickSearchSettingsByUserAndView = "settings/qsearch/users/{0}/views/{1}";

        /// <summary>
        /// API server route URL for quick search settings for a user and a view
        /// (both specified by unique identifier)
        /// </summary>
        public const string QuickSearchSettingsByUserAndViewUrl = "settings/qsearch/users/{userId:min(1)}/views/{viewId:min(1)}";

        /// <summary>
        /// API client URL for all quick report settings for a user specified by unique identifier
        /// </summary>
        public const string QuickReportSettingsByUser = "settings/qreport/users/{0}";

        /// <summary>
        /// API server route URL for all quick report settings for a user specified by unique identifier
        /// </summary>
        public const string QuickReportSettingsByUserUrl = "settings/qreport/users/{userId:min(1)}";

        /// <summary>
        /// API client URL for quick report settings for a user and a view
        /// (both specified by unique identifier)
        /// </summary>
        public const string QuickReportSettingsByUserAndView = "settings/qreport/users/{0}/views/{1}";

        /// <summary>
        /// API server route URL for quick report settings for a user and a view
        /// (both specified by unique identifier)
        /// </summary>
        public const string QuickReportSettingsByUserAndViewUrl = "settings/qreport/users/{userId:min(1)}/views/{viewId:min(1)}";

        /// <summary>
        /// API client URL for workflow settings
        /// </summary>
        public const string WorkflowSettings = "settings/workflows";

        /// <summary>
        /// API server route URL for workflow settings
        /// </summary>
        public const string WorkflowSettingsUrl = "settings/workflows";

        /// <summary>
        /// API client URL for settings of all hierarchy views
        /// </summary>
        public const string ViewTreeSettings = "settings/views/tree";

        /// <summary>
        /// API server route URL for settings of all hierarchy views
        /// </summary>
        public const string ViewTreeSettingsUrl = "settings/views/tree";

        /// <summary>
        /// API client URL for hierarcy settings of a view specified by unique identifier
        /// </summary>
        public const string ViewTreeSettingsByView = "settings/views/{0}/tree";

        /// <summary>
        /// API server route URL for hierarcy settings of a view specified by unique identifier
        /// </summary>
        public const string ViewTreeSettingsByViewUrl = "settings/views/{viewId:min(1)}/tree";

        /// <summary>
        /// API client URL for system config
        /// </summary>
        public const string SystemConfig = "settings/sysconfig";

        /// <summary>
        /// API server route URL for system config
        /// </summary>
        public const string SystemConfigUrl = "settings/sysconfig";

        /// <summary>
        /// API client URL for customized labels in a form specified by unique identifier
        /// </summary>
        public const string FormLabelsConfig = "settings/labels/forms/{0}";

        /// <summary>
        /// API server route URL for customized labels in a form specified by unique identifier
        /// </summary>
        public const string FormLabelsConfigUrl = "settings/labels/forms/{formId:min(1)}";

        #region Log Settings API

        /// <summary>
        /// API client URL for all application log settings
        /// </summary>
        public const string LogSettings = "settings/log";

        /// <summary>
        /// API server route URL for all application log settings
        /// </summary>
        public const string LogSettingsUrl = "settings/log";

        /// <summary>
        /// API client URL for all system log settings
        /// </summary>
        public const string SystemLogSettings = "settings/sys/log";

        /// <summary>
        /// API server route URL for all system log settings
        /// </summary>
        public const string SystemLogSettingsUrl = "settings/sys/log";

        #endregion
    }
}

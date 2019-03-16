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
        /// API client URL for default date range settings
        /// </summary>
        public const string DateRangeSettings = "settings/date-range";

        /// <summary>
        /// API server route URL for default date range settings
        /// </summary>
        public const string DateRangeSettingsUrl = "settings/date-range";

        /// <summary>
        /// API client URL for number display settings
        /// </summary>
        public const string NumberDisplaySettings = "settings/num-display";

        /// <summary>
        /// API server route URL for default date range settings
        /// </summary>
        public const string NumberDisplaySettingsUrl = "settings/num-display";

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
    }
}

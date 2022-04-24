using System;

namespace SPPC.Framework.Licensing
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with licenses.
    /// </summary>
    public sealed class LicenseApi
    {
        private LicenseApi()
        {
        }

        /// <summary>
        /// API client URL for the application license requested from a licensing service
        /// </summary>
        public const string License = "license";

        /// <summary>
        /// API server route URL for the application license requested from a licensing service
        /// </summary>
        public const string LicenseUrl = "license";

        /// <summary>
        /// API client URL for the application license requested from a licensing service
        /// </summary>
        public const string UserLicense = "license/users/{0}";

        /// <summary>
        /// API server route URL for the application license requested from a licensing service
        /// </summary>
        public const string UserLicenseUrl = "license/users/{userId:min(1)}";

        /// <summary>
        /// API client URL for the application license requested from online licensing service
        /// </summary>
        public const string OnlineUserLicense = "license/users/{0}/online";

        /// <summary>
        /// API server route URL for the application license requested from online licensing service
        /// </summary>
        public const string OnlineUserLicenseUrl = "license/users/{userId:min(1)}/online";

        /// <summary>
        /// API client URL for activating the application license (online server only)
        /// </summary>
        public const string ActivateLicense = "license/activate";

        /// <summary>
        /// API server route URL for activating the application license (online server only)
        /// </summary>
        public const string ActivateLicenseUrl = "license/activate";

        /// <summary>
        /// API client URL for validating the application license
        /// </summary>
        public const string ValidateLicense = "license/validate";

        /// <summary>
        /// API server route URL for validating the application license
        /// </summary>
        public const string ValidateLicenseUrl = "license/validate";

        /// <summary>
        /// API client URL for all open sessions in the application
        /// </summary>
        public const string OpenSessions = "sessions";

        /// <summary>
        /// API server route URL for all open sessions in the application
        /// </summary>
        public const string OpenSessionsUrl = "sessions";

        /// <summary>
        /// API client URL for all open sessions for a specific user in the application
        /// </summary>
        public const string OpenSessionsByUser = "sessions/users/{0}";

        /// <summary>
        /// API server route URL for all open sessions for a specific user in the application
        /// </summary>
        public const string OpenSessionsByUserUrl = "sessions/users/{userId:min(1)}";

        /// <summary>
        /// API client URL for current session in the application
        /// </summary>
        public const string CurrentSession = "sessions/current";

        /// <summary>
        /// API server route URL for current session in the application
        /// </summary>
        public const string CurrentSessionUrl = "sessions/current";

        /// <summary>
        /// API client URL for keeping current application session in active state
        /// </summary>
        public const string SetCurrentSessionAsActive = "sessions/current/active";

        /// <summary>
        /// API server route URL for keeping current application session in active state
        /// </summary>
        public const string SetCurrentSessionAsActiveUrl = "sessions/current/active";

        /// <summary>
        /// Api client URL for current database server configuration
        /// </summary>
        public const string DbServerConfig = "config/dbserver";

        /// <summary>
        /// Api server route URL for current database server configuration
        /// </summary>
        public const string DbServerConfigUrl = "config/dbserver";
    }
}

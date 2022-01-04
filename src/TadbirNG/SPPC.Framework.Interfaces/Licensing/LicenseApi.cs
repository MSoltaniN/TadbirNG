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
        /// API client URL for the application license requested from online licensing service
        /// </summary>
        public const string OnlineLicense = "license/online";

        /// <summary>
        /// API server route URL for the application license requested from online licensing service
        /// </summary>
        public const string OnlineLicenseUrl = "license/online";

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
    }
}

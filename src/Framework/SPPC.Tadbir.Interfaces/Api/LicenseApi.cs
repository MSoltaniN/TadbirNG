using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
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

        public const string Licenses = "licenses";

        public const string LicensesUrl = "licenses";
    }
}

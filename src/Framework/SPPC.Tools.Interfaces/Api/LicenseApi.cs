using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Api
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
        /// API client URL for the application license specified by instance key
        /// </summary>
        public const string LicenseByKey = "license/{0}";

        /// <summary>
        /// API server route URL for the application license specified by instance key
        /// </summary>
        public const string LicenseByKeyUrl = "license/{instanceKey}";

        /// <summary>
        /// API client URL for activating the application license (online server only)
        /// </summary>
        public const string ActivateLicense = "license/activate";

        /// <summary>
        /// API server route URL for activating the application license (online server only)
        /// </summary>
        public const string ActivateLicenseUrl = "license/activate";

        /// <summary>
        /// API client URL for all application licenses
        /// </summary>
        public const string Licenses = "licenses";

        /// <summary>
        /// API server route URL for all application licenses
        /// </summary>
        public const string LicensesUrl = "licenses";
    }
}

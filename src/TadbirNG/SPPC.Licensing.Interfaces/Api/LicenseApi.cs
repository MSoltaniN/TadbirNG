using System;

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
        public const string LicenseQuery = "license";

        /// <summary>
        /// API server route URL for the application license requested from a licensing service
        /// </summary>
        public const string LicenseQueryUrl = "license";

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

        /// <summary>
        /// API client URL for an application license specified by unique identifier
        /// </summary>
        public const string License = "licenses/{0}";

        /// <summary>
        /// API server route URL for an application license specified by unique identifier
        /// </summary>
        public const string LicenseUrl = "licenses/{licenseId:min(1)}";

        /// <summary>
        /// API client URL for all application licenses issued to a specific customer
        /// </summary>
        public const string LicensesByCustomer = "licenses/by-customer/{0}";

        /// <summary>
        /// API server route URL for all application licenses issued to a specific customer
        /// </summary>
        public const string LicensesByCustomerUrl = "licenses/by-customer/{customerId:min(1)}";
    }
}

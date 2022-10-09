using System;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// Defines API server route URLs for updating an installed instance
    /// </summary>
    public sealed class UpdateApi
    {
        private UpdateApi()
        {
        }

        /// <summary>
        /// API server route URL for information about latest version
        /// </summary>
        public const string LatestVersionInfoUrl = "versions/latest";

        /// <summary>
        /// API server route URL for downloading Docker image for License Server
        /// </summary>
        public const string LicenseServerImageUrl = "services/license-server";

        /// <summary>
        /// API server route URL for downloading Docker image for Api Server
        /// </summary>
        public const string ApiServerImageUrl = "services/api-server";

        /// <summary>
        /// API server route URL for downloading Docker image for Db Server
        /// </summary>
        public const string DbServerImageUrl = "services/db-server";

        /// <summary>
        /// API server route URL for downloading Docker image for Web App
        /// </summary>
        public const string WebAppImageUrl = "services/web-app";
    }
}

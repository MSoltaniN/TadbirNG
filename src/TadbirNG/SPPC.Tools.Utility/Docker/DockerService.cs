using System;

namespace SPPC.Tools.Utility
{
    public sealed class DockerService
    {
        private DockerService()
        {
        }

        public const string LicenseServer = "LicenseServer";

        public const string LicenseServerImage = "license-server";

        public const string ApiServer = "ApiServer";

        public const string ApiServerImage = "api-server";

        public const string DbServer = "DbServer";

        public const string DbServerImage = "db-server";

        public const string WebApp = "WebApp";

        public const string WebAppImage = "web-app";
    }
}

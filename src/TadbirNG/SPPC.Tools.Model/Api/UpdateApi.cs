using System;

namespace SPPC.Tools.Api
{
    public sealed class UpdateApi
    {
        private UpdateApi()
        {
        }

        public const string LatestVersionInfoUrl = "versions/latest";

        public const string LicenseServerImageUrl = "services/license-server";

        public const string ApiServerImageUrl = "services/api-server";

        public const string DbServerImageUrl = "services/db-server";

        public const string WebAppImageUrl = "services/web-app";
    }
}

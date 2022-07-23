using System;

namespace SPPC.Tools.Api
{
    public sealed class UpdateApi
    {
        private UpdateApi()
        {
        }

        public const string LatestVersionInfoUrl = "update/versions/latest";

        public const string LicenseServerImageUrl = "update/services/license-server";

        public const string ApiServerImageUrl = "update/services/api-server";

        public const string DbServerImageUrl = "update/services/db-server";

        public const string WebAppImageUrl = "update/services/web-app";
    }
}

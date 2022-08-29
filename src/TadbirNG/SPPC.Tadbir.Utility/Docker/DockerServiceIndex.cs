using System;

namespace SPPC.Tadbir.Utility.Docker
{
    public sealed class DockerServiceIndex
    {
        private DockerServiceIndex()
        {
        }

        public const int LicenseServer = 0;

        public const int ApiServer = 1;

        public const int DbServer = 2;

        public const int WebApp = 3;
    }
}

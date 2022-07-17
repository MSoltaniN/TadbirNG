using System;

namespace SPPC.Tools.Model
{
    public class VersionInfo
    {
        public VersionInfo()
        {
            Services = new ServiceInfo[4];
        }

        public string Version { get; set; }

        public ServiceInfo[] Services { get; }
    }
}

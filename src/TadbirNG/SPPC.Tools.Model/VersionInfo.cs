using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    public class VersionInfo
    {
        public VersionInfo()
        {
            Services = new List<ServiceInfo>();
        }

        public string Version { get; set; }

        public string Edition { get; set; }

        public List<ServiceInfo> Services { get; }
    }
}

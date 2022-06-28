using System;

namespace SPPC.Tools.Utility
{
    public class DockerLayerConfig
    {
        public string Id { get; set; }

        public DockerContainerConfig Container_config { get; set; }
    }
}

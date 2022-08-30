using System;

namespace SPPC.Tadbir.Utility.Docker
{
    public class DockerLayerConfig
    {
        public string Id { get; set; }

        public DockerConfig Config { get; set; }

        public DockerContainerConfig Container_config { get; set; }
    }
}

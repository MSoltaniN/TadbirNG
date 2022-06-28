using System;

namespace SPPC.Tools.Utility
{
    public class DockerContainer
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ContainerState State { get; set; }

        public class ContainerState
        {
            public bool Running { get; set; }
        }
    }
}

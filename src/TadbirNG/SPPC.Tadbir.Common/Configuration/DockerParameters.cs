using System;

namespace SPPC.Tadbir.Configuration
{
    public class DockerParameters
    {
        public string HubHandle { get; set; }

        public DockerServiceParameters License { get; set; }

        public DockerServiceParameters Api { get; set; }

        public DockerServiceParameters Db { get; set; }

        public DockerServiceParameters App { get; set; }
    }
}

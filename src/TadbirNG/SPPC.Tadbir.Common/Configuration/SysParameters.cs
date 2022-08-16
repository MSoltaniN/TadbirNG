using System;

namespace SPPC.Tadbir.Configuration
{
    public class SysParameters
    {
        public DbParameters Db { get; set; }

        public DockerParameters Docker { get; set; }
    }
}

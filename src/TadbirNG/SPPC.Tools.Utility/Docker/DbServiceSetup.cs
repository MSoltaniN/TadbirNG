using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;

namespace SPPC.Tools.Utility
{
    public class DbServiceSetup : DockerServiceSetup
    {
        public DbServiceSetup(IBuildSettings settings)
            : base(settings)
        {
        }

        protected override string ServiceName => "db-server";

        protected override ITextTemplate SettingsTemplate => null;

        protected override void ConfigureAppLayer(string layerId)
        {
        }
    }
}

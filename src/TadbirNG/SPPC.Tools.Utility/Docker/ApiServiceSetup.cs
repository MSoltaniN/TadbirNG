using System;
using System.IO;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.Utility
{
    public class ApiServiceSetup : DockerServiceSetup
    {
        public ApiServiceSetup(IBuildSettings settings)
            : base(settings)
        {
        }

        protected override ITextTemplate SettingsTemplate => new WebApiSettings(_settings);

        protected override void ConfigureAppLayer(string layerId)
        {
            base.ConfigureAppLayer(layerId);
            var root = Path.Combine(Environment.CurrentDirectory, layerId, "app", "wwwroot");
        }
    }
}

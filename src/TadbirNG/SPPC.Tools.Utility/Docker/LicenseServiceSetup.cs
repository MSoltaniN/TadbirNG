using SPPC.Tadbir.Configuration;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.Utility
{
    public class LicenseServiceSetup : DockerServiceSetup
    {
        public LicenseServiceSetup(IBuildSettings settings)
            : base(settings)
        {
        }

        protected override string ServiceName => SysParameterUtility.LicenseServer.ImageName;

        protected override ITextTemplate SettingsTemplate => new LocalLicenseApiSettings(_settings);
    }
}

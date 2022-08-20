using SPPC.Framework.Utility;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Utility.Templates;

namespace SPPC.Tadbir.Utility.Docker
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

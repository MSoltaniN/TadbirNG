using SPPC.Framework.Utility;

namespace SPPC.Tadbir.Utility.Templates
{
    public partial class LocalLicenseApiSettings : ITextTemplate
    {
        public LocalLicenseApiSettings(IBuildSettings settings)
        {
            _settings = settings;
        }

        private readonly IBuildSettings _settings;
    }
}

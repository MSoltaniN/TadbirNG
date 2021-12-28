using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
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

using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class LocalLicenseApiSettings : ITextTemplate
    {
        public LocalLicenseApiSettings(EnvSetupWizardModel model)
        {
            _model = model;
        }

        private readonly EnvSetupWizardModel _model;
    }
}

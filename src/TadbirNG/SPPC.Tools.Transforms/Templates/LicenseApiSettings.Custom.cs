using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class LicenseApiSettings : ITextTemplate
    {
        public LicenseApiSettings(EnvSetupWizardModel model)
        {
            _model = model;
        }

        private readonly EnvSetupWizardModel _model;
    }
}

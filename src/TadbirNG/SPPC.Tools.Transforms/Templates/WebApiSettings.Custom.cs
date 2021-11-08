using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class WebApiSettings : ITextTemplate
    {
        public WebApiSettings(EnvSetupWizardModel model)
        {
            _model = model;
        }

        private readonly EnvSetupWizardModel _model;
    }
}

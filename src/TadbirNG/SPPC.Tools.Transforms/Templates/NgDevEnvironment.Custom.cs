using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class NgDevEnvironment : ITextTemplate
    {
        public NgDevEnvironment(EnvSetupWizardModel model)
        {
            _model = model;
        }

        private readonly EnvSetupWizardModel _model;
    }
}

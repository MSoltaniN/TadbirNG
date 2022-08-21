using System;

namespace SPPC.Tadbir.Setup
{
    public interface ISetupWizardPage
    {
        SetupWizardModel WizardModel { get; set; }

        Func<bool> PageValidator { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Setup
{
    public interface ISetupWizardPage
    {
        SetupWizardModel WizardModel { get; set; }

        Func<bool> PageValidator { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Setup
{
    public class SetupWizardModel
    {
        public string InstallPath { get; set; }

        public bool CreateShortcut { get; set; }

        public bool IsGlobal { get; set; }

        public bool IsLocal { get; set; }

        public string Domain { get; set; }
    }
}

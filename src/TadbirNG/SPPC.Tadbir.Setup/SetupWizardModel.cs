using System;

namespace SPPC.Tadbir.Setup
{
    public class SetupWizardModel
    {
        public string InstallPath { get; set; }

        public bool CreateShortcut { get; set; }

        public bool IsGlobal { get; set; }

        public bool IsLocal { get; set; }

        public string Domain { get; set; }

        public string DbServer { get; set; }

        public string DbLogin { get; set; }

        public string DbPassword { get; set; }
    }
}

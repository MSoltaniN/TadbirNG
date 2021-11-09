using System;

namespace SPPC.Tools.Model
{
    public class EnvSetupWizardModel
    {
        public EnvSetupWizardModel()
        {
            DbUserName = "sa";
        }

        public string DbServerName { get; set; }

        public string DbUserName { get; set; }

        public string DbPassword { get; set; }

        public string WinUserName { get; set; }

        public string WinPassword { get; set; }

        public string InstanceKey { get; set; }

        public string RootFolder { get; set; }

        public string LicenseeFirstName { get; set; }

        public string LicenseeLastName { get; set; }
    }
}

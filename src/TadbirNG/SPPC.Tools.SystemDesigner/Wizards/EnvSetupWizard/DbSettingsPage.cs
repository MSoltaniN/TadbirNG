using System;
using System.Windows.Forms;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Wizards.EnvSetupWizard
{
    public partial class DbSettingsPage : UserControl
    {
        public DbSettingsPage()
        {
            InitializeComponent();
            Info = "Database settings (required for creating TadbirNG databases)";
        }

        public EnvSetupWizardModel WizardModel { get; set; }

        public string Info { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            chkShowPass.Checked = true;
            SetupBindings();
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtLoginPassword.PasswordChar = chkShowPass.Checked ? '\0' : '*';
        }

        private void SetupBindings()
        {
            txtDbServer.DataBindings.Add("Text", WizardModel, "DbServerName",
                false, DataSourceUpdateMode.OnPropertyChanged);
            txtLoginName.DataBindings.Add("Text", WizardModel, "DbUserName",
                false, DataSourceUpdateMode.OnPropertyChanged);
            txtLoginPassword.DataBindings.Add("Text", WizardModel, "DbPassword",
                false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}

using System;
using System.Windows.Forms;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Wizards.EnvSetupWizard
{
    public partial class GeneralSettingsPage : UserControl
    {
        public GeneralSettingsPage()
        {
            InitializeComponent();
            Info = "General settings and information";
            chkShowPass.Checked = true;
        }

        public EnvSetupWizardModel WizardModel { get; set; }

        public string Info { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.DesktopDirectory,
                Description = "Browse root folder of the cloned git repo",
                UseDescriptionForTitle = true
            };
            if (browser.ShowDialog(this) == DialogResult.OK)
            {
                txtRootFolder.Text = browser.SelectedPath;
            }
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtWinPassword.PasswordChar = chkShowPass.Checked ? '\0' : '*';
        }

        private void SetupBindings()
        {
            txtRootFolder.DataBindings.Add("Text", WizardModel, "RootFolder",
                false, DataSourceUpdateMode.OnPropertyChanged);
            txtFirstName.DataBindings.Add("Text", WizardModel, "LicenseeFirstName",
                false, DataSourceUpdateMode.OnPropertyChanged);
            txtLastName.DataBindings.Add("Text", WizardModel, "LicenseeLastName",
                false, DataSourceUpdateMode.OnPropertyChanged);
            txtWinUser.DataBindings.Add("Text", WizardModel, "WinUserName",
                false, DataSourceUpdateMode.OnPropertyChanged);
            txtWinPassword.DataBindings.Add("Text", WizardModel, "WinPassword",
                false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}

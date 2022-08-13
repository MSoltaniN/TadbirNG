using System;
using System.Windows.Forms;

namespace SPPC.Tadbir.Setup
{
    public partial class SettingsPage : UserControl, ISetupWizardPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public SetupWizardModel WizardModel { get; set; }

        public Func<bool> PageValidator
        {
            get { return ValidateSettings; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            var browser = new FolderBrowserDialog()
            {
                Description = "پوشه نصب برنامه را انتخاب کنید",
                RootFolder = Environment.SpecialFolder.Desktop,
                ShowNewFolderButton = true,
                UseDescriptionForTitle = true
            };
            if (browser.ShowDialog(this) == DialogResult.OK)
            {
                txtInstallPath.Text = browser.SelectedPath;
            }
        }

        private void SetupBindings()
        {
            txtInstallPath.DataBindings.Add(
                "Text", WizardModel, "InstallPath", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCreateShortcut.DataBindings.Add(
                "Checked", WizardModel, "CreateShortcut", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private bool ValidateSettings()
        {
            if (String.IsNullOrWhiteSpace(txtInstallPath.Text))
            {
                MessageBox.Show("لطفاً مسیر نصب برنامه را انتخاب کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }
    }
}

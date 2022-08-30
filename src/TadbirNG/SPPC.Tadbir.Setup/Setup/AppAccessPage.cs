using System;
using System.Windows.Forms;

namespace SPPC.Tadbir.Setup
{
    public partial class AppAccessPage : UserControl, ISetupWizardPage
    {
        public AppAccessPage()
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
            radLocal.Checked = WizardModel.IsLocal;
            radGlobal.Checked = WizardModel.IsGlobal;
        }

        private void Global_CheckedChanged(object sender, EventArgs e)
        {
            txtDomain.Enabled = radGlobal.Checked;
        }

        private void SetupBindings()
        {
            txtDomain.DataBindings.Add(
                "Text", WizardModel, "Domain", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private bool ValidateSettings()
        {
            WizardModel.IsLocal = radLocal.Checked;
            WizardModel.IsGlobal = radGlobal.Checked;
            if (WizardModel.IsGlobal && String.IsNullOrWhiteSpace(WizardModel.Domain))
            {
                MessageBox.Show("لطفاً آدرس سرور را به صورت دامنه یا آی پی ثابت وارد کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return false;
            }

            return true;
        }
    }
}

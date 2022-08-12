using System;
using System.Windows.Forms;

namespace SPPC.Tadbir.Setup
{
    public partial class AppAccessPage : UserControl
    {
        public AppAccessPage()
        {
            InitializeComponent();
        }

        public SetupWizardModel WizardModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
        }

        private void Global_CheckedChanged(object sender, EventArgs e)
        {
            txtDomain.Enabled = radGlobal.Checked;
        }

        private void SetupBindings()
        {
            radGlobal.DataBindings.Add(
                "Checked", WizardModel, "IsGlobal", false, DataSourceUpdateMode.OnPropertyChanged);
            radLocal.DataBindings.Add(
                "Checked", WizardModel, "IsLocal", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDomain.DataBindings.Add(
                "Text", WizardModel, "Domain", false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}

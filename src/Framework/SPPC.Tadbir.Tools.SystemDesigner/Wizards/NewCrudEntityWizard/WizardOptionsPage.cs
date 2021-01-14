using System;
using System.Windows.Forms;
using SPPC.Tools.Model;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.NewCrudEntityWizard
{
    public partial class WizardOptionsPage : UserControl
    {
        public WizardOptionsPage()
        {
            InitializeComponent();
            Info = "Select generated items";
        }

        public CrudWizardOptionsModel Options { get; set; }

        public string Info { get; }

        public event EventHandler<PermissionCheckedEventArgs> PermissionsChecked;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            chkWithMethod.Enabled = chkController.Checked;
            chkWithImpl.Enabled = chkController.Checked;
        }

        private void PermEnum_CheckedChanged(object sender, EventArgs e)
        {
            RaisePermissionChecked();
        }

        private void Controller_CheckedChanged(object sender, EventArgs e)
        {
            chkWithMethod.Enabled = chkController.Checked;
            chkWithImpl.Enabled = chkController.Checked;
        }

        private void SetupBindings()
        {
            chkController.DataBindings.Add("Checked", Options, "HasController");
            chkModel.DataBindings.Add("Checked", Options, "HasModel");
            chkViewModel.DataBindings.Add("Checked", Options, "HasViewModel");
            chkDbMapping.DataBindings.Add("Checked", Options, "HasDbMapping");
            chkDbScript.DataBindings.Add("Checked", Options, "HasDbScript");
            chkRepoInterface.DataBindings.Add("Checked", Options, "HasRepoInterface");
            chkRepoImpl.DataBindings.Add("Checked", Options, "HasRepoImplementation");
            chkApiRoutes.DataBindings.Add("Checked", Options, "HasApiRouting");
            chkPermEnum.DataBindings.Add("Checked", Options, "HasPermissionEnum");
            chkTsViewModel.DataBindings.Add("Checked", Options, "HasTsViewModel");
            chkTsApiRouting.DataBindings.Add("Checked", Options, "HasTsApiRouting");
            chkWithMethod.DataBindings.Add("Checked", Options.Controller, "HasCrudMethods");
            chkWithImpl.DataBindings.Add("Checked", Options.Controller, "HasCrudImpl");
        }

        private bool ValidateModel()
        {
            return true;
        }

        private void RaisePermissionChecked()
        {
            PermissionsChecked?.Invoke(
                this, new PermissionCheckedEventArgs(chkPermEnum.Checked));
        }
    }
}

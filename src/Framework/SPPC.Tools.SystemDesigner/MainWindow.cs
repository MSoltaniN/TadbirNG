using System;
using System.Windows.Forms;
using SPPC.Tools.SystemDesigner.Commands;
using SPPC.Tools.SystemDesigner.Forms;
using SPPC.Tools.Model;
using SPPC.Tools.SystemDesigner.Wizards.NewCrudEntityWizard;
using SPPC.Tools.SystemDesigner.Wizards.ViewWizard;
using SPPC.Tools.SystemDesigner.Designers;
using SPPC.Tools.MetaDesigner;

namespace SPPC.Tools.SystemDesigner
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WizardsViewWizard_Click(object sender, EventArgs e)
        {
            var wizard = new ViewWizardForm();
            wizard.ShowDialog();
        }

        private void GenerateApiController_Click(object sender, EventArgs e)
        {
            var form = new GenerateControllerForm() { Controller = new ControllerModel() };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var command = new GenerateControllerCommand(form.Controller);
                command.Execute();
                MessageBox.Show(this, "Generation completed without errors.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void WizardsCrudManager_Click(object sender, EventArgs e)
        {
            var form = new NewCrudEntityWizardForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var command = new CrudEntityWizardCommand(form.WizardModel);
                command.Execute();
                MessageBox.Show(this, "Wizard completed!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ManageLogCatalog_Click(object sender, EventArgs e)
        {
            var form = new LogCatalogueForm();
            form.ShowDialog(this);
        }

        private void ManageSecurityTicket_Click(object sender, EventArgs e)
        {
            var form = new TicketManager();
            form.ShowDialog(this);
        }

        private void AddPermissionGroup_Click(object sender, EventArgs e)
        {
            var designer = new PermissionDesignerForm();
            designer.ShowDialog();
        }

        private void EditPermissionGroup_Click(object sender, EventArgs e)
        {
            //var Editor = new PermissionEditorForm();
            //Editor.ShowDialog();
        }

        private void DesignersReport_Click(object sender, EventArgs e)
        {
            var designer = new ManageReportsForm();
            designer.ShowDialog(this);
        }

        private void DesignersMetadata_Click(object sender, EventArgs e)
        {
            var designer = new MetadataDesigner();
            designer.ShowDialog(this);
        }
    }
}

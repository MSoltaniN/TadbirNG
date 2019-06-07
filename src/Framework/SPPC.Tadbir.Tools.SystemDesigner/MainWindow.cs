using System;
using System.Windows.Forms;
using SPPC.Tadbir.Tools.SystemDesigner.Commands;
using SPPC.Tadbir.Tools.SystemDesigner.Forms;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Tadbir.Tools.SystemDesigner.Wizards.NewCrudEntityWizard;
using SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard;

namespace SPPC.Tadbir.Tools.SystemDesigner
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void viewWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wizard = new ViewWizardForm();
            var wizardPage = new EditViewForm() { Dock = DockStyle.Fill };
            //wizardPage.View = new ViewViewModel() { Name = "MyLousyView", FetchUrl = "my/lousy/fetch/url", IsHierarchy = true };
            wizard.Controls.Add(wizardPage);
            wizard.Show();
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
    }
}

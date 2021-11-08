using System;
using System.Windows.Forms;
using SPPC.Tools.SystemDesigner.Commands;
using SPPC.Tools.SystemDesigner.Forms;
using SPPC.Tools.Model;
using SPPC.Tools.SystemDesigner.Wizards.NewCrudEntityWizard;
using SPPC.Tools.SystemDesigner.Wizards.ViewWizard;
using SPPC.Tools.SystemDesigner.Wizards.EnvSetupWizard;
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

        #region Metadata Menu

        private void MetadataEntities_Click(object sender, EventArgs e)
        {
            var designer = new MetadataDesigner();
            designer.ShowDialog(this);
        }

        private void MetadataListViews_Click(object sender, EventArgs e)
        {
            var wizard = new ViewWizardForm();
            wizard.ShowDialog();
        }

        private void MetadataPermissions_Click(object sender, EventArgs e)
        {
            var browser = new PermissionBrowserForm();
            browser.ShowDialog(this);
        }

        private void MetadataReports_Click(object sender, EventArgs e)
        {
            var browser = new ReportBrowserForm();
            browser.ShowDialog(this);
        }

        private void MetadataLogSettings_Click(object sender, EventArgs e)
        {
            var browser = new LogSettingBrowserForm();
            browser.ShowDialog(this);
        }

        private void MetadataMenus_Click(object sender, EventArgs e)
        {
            var browser = new MenuBrowserForm();
            browser.ShowDialog(this);
        }

        private void MetadataShortcuts_Click(object sender, EventArgs e)
        {
            var browser = new ShortcutBrowserForm();
            browser.ShowDialog(this);
        }

        #endregion

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

        private void GenerateFixDbScript_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = "sql",
                Filter = "SQL Script Files (*.sql)|*.sql",
                OverwritePrompt = true,
                Title = "Select target script file",
                InitialDirectory = "..\\..\\res"
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                var command = new GenerateFixScriptCommand(ofd.FileName);
                command.Execute();
                MessageBox.Show("Script file successfully generated.");
            }
        }

        private void WizardsCrudManager_Click(object sender, EventArgs e)
        {
            var form = new NewCrudEntityWizardForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var command = new CrudEntityWizardCommand(form.WizardModel);
                command.Execute();
                MessageBox.Show(this, "Wizard completed successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ManageSecurityTicket_Click(object sender, EventArgs e)
        {
            var form = new TicketManager();
            form.ShowDialog(this);
        }

        private void AddPermissionGroup_Click(object sender, EventArgs e)
        {
            var designer = new PermissionEditorForm();
            designer.ShowDialog();
        }

        private void EditPermissionGroup_Click(object sender, EventArgs e)
        {
        }

        private void WizardsEnvSetup_Click(object sender, EventArgs e)
        {
            var wizard = new EnvSetupWizardForm();
            wizard.ShowDialog(this);
        }
    }
}

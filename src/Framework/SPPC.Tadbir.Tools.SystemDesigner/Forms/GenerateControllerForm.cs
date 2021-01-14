using System;
using System.Windows.Forms;
using SPPC.Tools.Model;

namespace SPPC.Tadbir.Tools.SystemDesigner.Forms
{
    public partial class GenerateControllerForm : Form
    {
        public GenerateControllerForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
        }

        private void SetupBindings()
        {
            txtPath.DataBindings.Add("Text", Controller, "OutputPath");
            txtEntityName.DataBindings.Add("Text", Controller, "EntityName");
            txtEntityArea.DataBindings.Add("Text", Controller, "EntityArea");
            chkWithMethod.DataBindings.Add("Checked", Controller, "HasCrudMethods");
            chkWithImpl.DataBindings.Add("Checked", Controller, "HasCrudImpl");
            chkIsFiscal.DataBindings.Add("Checked", Controller, "IsFiscalEntity");
        }

        public ControllerModel Controller { get; set; }

        private void Generate_Click(object sender, EventArgs e)
        {
            bool isComplete = !String.IsNullOrWhiteSpace(Controller.OutputPath)
                && !String.IsNullOrWhiteSpace(Controller.EntityName)
                && !String.IsNullOrWhiteSpace(Controller.EntityArea);
            if (!isComplete)
            {
                MessageBox.Show(this, "One or more required fields are missing.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}

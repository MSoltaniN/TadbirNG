using System;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner.Designers
{
    public partial class ParameterEditorForm : Form
    {
        public ParameterEditorForm()
        {
            InitializeComponent();
        }

        public void SetupConrols()
        {
            cmbControlType.SelectedIndex = 0;
            cmbDataType.SelectedIndex = 0;
            cmbOperator.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

       
    }
}

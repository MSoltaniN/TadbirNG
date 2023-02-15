using System;
using System.Windows.Forms;

namespace SPPC.Tools.SystemDesigner.Designers
{
    public partial class ParameterEditorForm : Form
    {
        public ParameterEditorForm()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

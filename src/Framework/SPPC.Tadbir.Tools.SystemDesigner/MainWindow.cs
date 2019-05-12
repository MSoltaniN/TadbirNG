using System;
using System.Windows.Forms;
using SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard;
using SPPC.Tadbir.ViewModel.Metadata;

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
    }
}

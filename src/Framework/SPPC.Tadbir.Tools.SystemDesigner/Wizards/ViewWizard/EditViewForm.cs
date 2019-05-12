using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class EditViewForm : UserControl
    {
        public EditViewForm()
        {
            InitializeComponent();
            View = new ViewViewModel();
        }

        public ViewViewModel View { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
        }

        private void SetupBindings()
        {
            txtName.DataBindings.Add("Text", View, "Name");
            txtFetchUrl.DataBindings.Add("Text", View, "FetchUrl");
            chkIsHierarchy.DataBindings.Add("Checked", View, "IsHierarchy");
            chkEnableCartable.DataBindings.Add("Checked", View, "IsCartableIntegrated");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = String.Format("Name : {1}{0}Fetch URL : {2}{0}Hierarchy : {3}{0}Cartable Integrated : {4}",
                Environment.NewLine, View.Name, View.FetchUrl, View.IsHierarchy, View.IsCartableIntegrated);
            MessageBox.Show(message);
        }
    }
}

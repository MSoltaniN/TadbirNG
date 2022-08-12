using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPC.Tadbir.Setup
{
    public partial class ProgressPage : UserControl
    {
        public ProgressPage()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MessageBox.Show(this, "Fucking loaded!");
        }

        public SetupWizardModel WizardModel { get; set; }
    }
}

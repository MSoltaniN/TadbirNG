using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPPC.Licensing.Model;

namespace SPPC.Tools.LicenseManager
{
    public partial class CreateRelease : Form
    {
        public CreateRelease()
        {
            InitializeComponent();
        }

        public LicenseModel License { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtCustomer.Text = License?.Customer?.CompanyName;
            txtUser.Text = String.Format(
                $"{License?.Customer?.ContactFirstName} {License?.Customer?.ContactLastName}");
            txtLicenseKey.Text = License?.LicenseKey;
        }

        private void Create_Click(object sender, EventArgs e)
        {

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

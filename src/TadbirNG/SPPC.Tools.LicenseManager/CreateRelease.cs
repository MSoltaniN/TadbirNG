using System;
using System.ComponentModel;
using System.Windows.Forms;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tools.Utility;

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
            txtPassword.Focus();
        }

        private void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (txtConsole.InvokeRequired)
            {
                txtConsole.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (!String.IsNullOrEmpty(e.Output))
                    {
                        txtConsole.AppendText(e.Output.Replace("\n", Environment.NewLine));
                        txtConsole.AppendText(Environment.NewLine);
                        txtConsole.ScrollToCaret();
                    }
                });
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(this, "لطفاً رمز مورد نیاز برای ساختن فایل خروجی را وارد کنید.", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return;
            }

            _elapsed = TimeSpan.Zero;
            txtConsole.Focus();
            _runner.OutputReceived += Runner_OutputReceived;
            btnCreate.Enabled = false;
            btnExit.Enabled = false;
            timer.Enabled = true;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(0, "دریافت آخرین نسخه سرویس های داکر...");
            ReleaseUtility.UpdateImageCache(_runner);
            worker.ReportProgress(40);

            worker.ReportProgress(0, "کپی فایل های مورد نیاز برنامه...");
            ReleaseUtility.CopyProgramFiles(License.LicenseKey, License.Edition);
            worker.ReportProgress(40);

            worker.ReportProgress(0, "ایجاد تنظیمات نسخه جدید...");
            ReleaseUtility.GenerateSettings(License);
            worker.ReportProgress(5);

            worker.ReportProgress(0, "ساختن فایل نهایی کاربر...");
            ReleaseUtility.CreateReleaseArchive(License.LicenseKey, License.Edition, txtPassword.Text);
            worker.ReportProgress(15);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                lblStatus.Text = e.UserState.ToString();
            }

            progress.Value += e.ProgressPercentage;
            lblProgress.Text = String.Format("{0}%", progress.Value);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCreate.Enabled = true;
            btnExit.Enabled = true;
            lblStatus.Text = null;
            timer.Enabled = false;
            MessageBox.Show(this, "ساخت نسخه کاربر با موفقیت انجام شد.", "تکمیل عملیات",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var seconds = _elapsed.TotalSeconds;
            _elapsed = TimeSpan.FromSeconds(++seconds);
            lblElapsed.Text = _elapsed.ToString(@"hh\:mm\:ss");
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private readonly CliRunner _runner = new();
        private TimeSpan _elapsed;
    }
}

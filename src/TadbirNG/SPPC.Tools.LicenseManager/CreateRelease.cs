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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _runner.Kill();
            _utility.RestoreSettings();
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

        private void Runner_Killed(object sender, EventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
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

            if (!CommonUtility.IsDockerEngineRunning())
            {
                MessageBox.Show("لطفاً پیش از ساخت نسخه، ابتدا برنامه داکر دسکتاپ را اجرا کنید و وارد حساب کاربری سازمان شوید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return;
            }

            _elapsed = TimeSpan.Zero;
            txtConsole.Focus();
            _runner.OutputReceived += Runner_OutputReceived;
            _runner.Killed += Runner_Killed;
            btnCreate.Enabled = false;
            btnExit.Enabled = false;
            timer.Enabled = true;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(0, "ایجاد تنظیمات نسخه جدید...");
            _utility.GenerateSettings(License);
            worker.ReportProgress(5);

            worker.ReportProgress(0, "ساختن سرویس های داکر...");
            _runner.Run(String.Format(BuildTemplate, @"..\..\..\src\TadbirNG\"));
            worker.ReportProgress(25);

            worker.ReportProgress(0, "ارسال سرویس های نسخه به داکر هاب...");
            var guid = License.LicenseKey.Substring(0, 8);
            _runner.Run(String.Format(PushLicenseTemplate, guid));
            worker.ReportProgress(15);
            _runner.Run(String.Format(PushApiTemplate, guid));
            worker.ReportProgress(15);
            _runner.Run(String.Format(PushAppTemplate, guid));
            worker.ReportProgress(15);
            _runner.Run("docker push msn1368/db-server:dev");
            worker.ReportProgress(15);

            worker.ReportProgress(0, "ساختن فایل نهایی کاربر...");
            _utility.CreateReleaseArchive(License.LicenseKey, txtPassword.Text);
            worker.ReportProgress(10);
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

        private const string PushLicenseTemplate = "docker push msn1368/license-server-{0}:dev";
        private const string PushApiTemplate = "docker push msn1368/api-server-{0}:latest";
        private const string PushAppTemplate = "docker push msn1368/web-app-{0}:dev";
        private const string BuildTemplate = "docker-compose -f {0}docker-compose.override.yml -f {0}docker-compose.yml build";
        private readonly ReleaseUtility _utility = new();
        private readonly CliRunner _runner = new();
        private TimeSpan _elapsed;
    }
}

using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tadbir.Setup
{
    public partial class ModifyWizard : Form
    {
        public ModifyWizard()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _model = GetCurrentSettings();
            if (_model == null)
            {
                return;
            }

            pnlPage.Controls.Add(new SelectActionPage() { Dock = DockStyle.Fill });
            btnPrevious.Enabled = false;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            var actionPage = GetActionPage();
            if (actionPage != null)
            {
                _isModifying = actionPage.IsModifying;
                bool confirmed = (actionPage as ISetupWizardPage).PageValidator();
                if (!confirmed)
                {
                    return;
                }

                SetupWizardDriver();
            }
        }

        private void Setup_Click(object sender, EventArgs e)
        {
            _progressPage = GetProgressPage();
            if (_progressPage == null)
            {
                return;
            }

            _progressPage.ConsoleTextBox.Focus();
            btnSetup.Enabled = false;
            btnExit.Enabled = false;
            timer.Enabled = true;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_isModifying)
            {
                RunModifyProcess();
            }
            else
            {
                RunRemoveProcess();
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnExit.Enabled = true;
            _progressPage.StatusLabel.Text = null;
            timer.Enabled = false;
            MessageBox.Show(this, "عملیات مورد نظر با موفقیت انجام شد.", "تکمیل عملیات",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var txtConsole = _progressPage.ConsoleTextBox;
            var progress = _progressPage.ProgressBar;
            if (e.UserState != null)
            {
                txtConsole.AppendText(e.UserState.ToString());
                txtConsole.AppendText(Environment.NewLine);
                txtConsole.AppendText(Environment.NewLine);
            }

            progress.Value += e.ProgressPercentage;
            _progressPage.ProgressLabel.Text = String.Format("{0}%", progress.Value);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var seconds = _elapsed.TotalSeconds;
            _elapsed = TimeSpan.FromSeconds(++seconds);
            _progressPage.ElapsedLabel.Text = _elapsed.ToString(@"hh\:mm\:ss");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static SetupWizardModel GetCurrentSettings()
        {
            var configPath = @"..\config";
            if (!File.Exists(configPath))
            {
                MessageBox.Show("لطفاً این برنامه را از مسیر نصب شده اجرا کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                return null;
            }

            var settings = JsonHelper.To<RawBuildSettings>(
                CryptoService.Default.Decrypt(
                    File.ReadAllText(configPath)));
            return new SetupWizardModel()
            {
                IsGlobal = !settings.WebApiUrl.Contains(BuildSettingValues.LocalHostUrl),
                IsLocal = settings.WebApiUrl.Contains(BuildSettingValues.LocalHostUrl),
                DbServer = settings.DbServerName,
                DbLogin = settings.DbUserName,
                DbPassword = settings.DbPassword,
                Domain = settings.WebApiUrl
                    .Replace("http://", String.Empty)
                    .Replace(":", String.Empty)
                    .Replace(BuildSettingValues.DefaultApiPort.ToString(), String.Empty)
            };
        }

        private void RunModifyProcess()
        {
        }

        private void RunRemoveProcess()
        {
            worker.ReportProgress(0, "توقف سرویس...");
            bool succeeded = SetupUtility.StopService();
            if (!succeeded)
            {
                worker.ReportProgress(0, "بروز خطا هنگام توقف سرویس");
                return;
            }

            worker.ReportProgress(20);

            worker.ReportProgress(0, "حذف سرویس...");
            succeeded = SetupUtility.UninstallService();
            if (!succeeded)
            {
                worker.ReportProgress(0, "بروز خطا هنگام حذف سرویس");
                return;
            }

            worker.ReportProgress(5);

            worker.ReportProgress(0, "حذف سرویس های برنامه...");
            SetupUtility.RemoveDockerServices();
            worker.ReportProgress(50);

            worker.ReportProgress(0, "حذف فایلهای برنامه...");
            SetupUtility.DeleteFiles();
            worker.ReportProgress(25);
        }

        private SelectActionPage GetActionPage()
        {
            return pnlPage.Controls
                .Cast<Control>()
                .Where(ctl => ctl is SelectActionPage)
                .Select(ctl => ctl as SelectActionPage)
                .FirstOrDefault();
        }

        private void SetupWizardDriver()
        {
            _driver = new WizardDriver()
            {
                PreviousButton = btnPrevious,
                NextButton = btnNext,
                PageContainer = pnlPage
            };

            int pageNo = 1;
            ISetupWizardPage page = new SelectActionPage() { Dock = DockStyle.Fill, WizardModel = _model };
            _driver.Pages.Add(page as Control);
            _driver.SetPageValidator(pageNo++, page.PageValidator);
            if (_isModifying)
            {
                page = new AppAccessPage() { Dock = DockStyle.Fill, WizardModel = _model };
                _driver.Pages.Add(page as Control);
                _driver.SetPageValidator(pageNo++, page.PageValidator);
                page = new DbAccessPage() { Dock = DockStyle.Fill, WizardModel = _model };
                _driver.Pages.Add(page as Control);
                _driver.SetPageValidator(pageNo++, page.PageValidator);
            }
            else
            {
                btnSetup.Text = "حذف برنامه";
            }

            page = new ProgressPage() { Dock = DockStyle.Fill, WizardModel = _model };
            _driver.Pages.Add(page as Control);
            _driver.InitWizard(2);
            btnNext.Click -= Next_Click;
        }

        private ISetupProgressPage GetProgressPage()
        {
            return pnlPage.Controls
                .Cast<Control>()
                .Where(ctl => ctl is ISetupProgressPage)
                .Select(ctl => ctl as ISetupProgressPage)
                .FirstOrDefault();
        }

        private SetupWizardModel _model;
        private ISetupProgressPage _progressPage;
        private WizardDriver _driver;
        private TimeSpan _elapsed;
        private bool _isModifying;
    }
}

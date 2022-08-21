using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.Utility.Model;

namespace SPPC.Tadbir.Setup
{
    public partial class SetupWizard : Form
    {
        public SetupWizard()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupWizardDriver();
        }

        private void Setup_Click(object sender, EventArgs e)
        {
            _progressPage = GetProgressPage();
            if (_progressPage == null)
            {
                return;
            }

            _progressPage.ConsoleTextBox.Focus();
            SetBuildSettings();
            btnSetup.Enabled = false;
            btnExit.Enabled = false;
            timer.Enabled = true;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(0, "آماده سازی اولیه نصب برنامه...");
            SetupUtility.CreateInstallationPath(_model.InstallPath);
            worker.ReportProgress(2);

            worker.ReportProgress(0, "کپی فایلهای مورد نیاز برنامه...");
            SetupUtility.CopyFiles(_model.InstallPath, _settings, _model.CreateShortcut);
            worker.ReportProgress(8);

            worker.ReportProgress(0, "نصب سرویس...");
            bool succeeded = SetupUtility.InstallService(_model.InstallPath);
            if (!succeeded)
            {
                worker.ReportProgress(0, "بروز خطا هنگام نصب سرویس");
                return;
            }

            worker.ReportProgress(4);

            worker.ReportProgress(0, "راه اندازی سرویس...");
            succeeded = SetupUtility.StartService();
            if (!succeeded)
            {
                worker.ReportProgress(0, "بروز خطا هنگام راه اندازی سرویس");
                return;
            }

            worker.ReportProgress(6);

            var root = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), "docker");
            worker.ReportProgress(0, "آماده سازی سرویس های برنامه...");
            SetupUtility.ConfigureDockerService(root, SysParameterUtility.LicenseServer.ImageName, _settings);
            worker.ReportProgress(20);
            SetupUtility.ConfigureDockerService(root, SysParameterUtility.ApiServer.ImageName, _settings);
            worker.ReportProgress(20);
            SetupUtility.ConfigureDockerService(root, SysParameterUtility.WebApp.ImageName, _settings);
            worker.ReportProgress(20);
            SetupUtility.ConfigureDockerService(root, SysParameterUtility.DbServer.ImageName, _settings);
            worker.ReportProgress(20);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnExit.Enabled = true;
            btnPrevious.Enabled = false;
             _progressPage.StatusLabel.Text = null;
            timer.Enabled = false;
            MessageBox.Show(this, "نصب برنامه با موفقیت انجام شد.", "تکمیل عملیات",
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

        private static SetupWizardModel GetDefaultOptions()
        {
            return new SetupWizardModel()
            {
                InstallPath = @"C:\SPPC\TadbirNG",
                CreateShortcut = true,
                IsGlobal = true,
                IsLocal = false
            };
        }

        private void SetupWizardDriver()
        {
            _model = GetDefaultOptions();
            _driver = new WizardDriver()
            {
                PreviousButton = btnPrevious,
                NextButton = btnNext,
                PageContainer = pnlPage
            };
            ISetupWizardPage page = new SettingsPage() { Dock = DockStyle.Fill, WizardModel = _model };
            _driver.Pages.Add(page as Control);
            _driver.SetPageValidator(1, page.PageValidator);
            page = new AppAccessPage() { Dock = DockStyle.Fill, WizardModel = _model };
            _driver.Pages.Add(page as Control);
            _driver.SetPageValidator(2, page.PageValidator);
            page = new DbAccessPage() { Dock = DockStyle.Fill, WizardModel = _model };
            _driver.Pages.Add(page as Control);
            _driver.SetPageValidator(3, page.PageValidator);
            page = new ProgressPage() { Dock = DockStyle.Fill, WizardModel = _model };
            _driver.Pages.Add(page as Control);
            _driver.InitWizard();
        }

        private ISetupProgressPage GetProgressPage()
        {
            return pnlPage.Controls
                .Cast<Control>()
                .Where(ctl => ctl is ISetupProgressPage)
                .Select(ctl => ctl as ISetupProgressPage)
                .FirstOrDefault();
        }

        private void SetBuildSettings()
        {
            _settings = _model.IsGlobal ? BuildSettings.DockerNetwork : BuildSettings.DockerLocal;
            if (_model.DbServer == "Docker")
            {
                _model.DbServer = SysParameterUtility.DbServer.Name;
            }
            else
            {
                _model.DbServer = _model.DbServer.Replace(Environment.MachineName, BuildSettingValues.DockerHostInternalUrl);
            }

            _settings.DbServerName = _model.DbServer;
            if (_model.DbLogin == "Default")
            {
                _model.DbLogin = AppConstants.SystemLoginName;
            }

            _settings.DbUserName = _model.DbLogin;
            _settings.DbPassword = _model.DbLogin != AppConstants.SystemLoginName
                ? _model.DbPassword
                : "Demo1234";
            if (String.IsNullOrEmpty(_model.Domain))
            {
                _model.Domain = "http://localhost";
            }

            if (!_model.Domain.StartsWith("http://"))
            {
                _model.Domain = $"http://{_model.Domain}";
            }

            _settings.WebApiUrl = $"{_model.Domain}:{BuildSettingValues.DefaultApiPort}";
            _settings.LocalServerUrl = $"{_model.Domain}:{BuildSettingValues.DefaultLicenseApiPort}";
            _settings.Tcp.Domain = BuildSettingValues.DockerHostInternalUrl;
            _settings.Key = SetupUtility.GetInstanceKey();
        }

        private SetupWizardModel _model;
        private ISetupProgressPage _progressPage;
        private WizardDriver _driver;
        private TimeSpan _elapsed;
        private IBuildSettings _settings;
    }
}

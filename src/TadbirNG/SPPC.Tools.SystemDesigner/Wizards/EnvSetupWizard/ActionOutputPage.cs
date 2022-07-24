using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;
using Constants = SPPC.Licensing.Model.Constants;

namespace SPPC.Tools.SystemDesigner.Wizards.EnvSetupWizard
{
    public partial class ActionOutputPage : UserControl
    {
        public ActionOutputPage()
        {
            InitializeComponent();
            Info = "Start setting up the development environment";
            _outputBuilder = new StringBuilder();
            _instance = new InstanceModel()
            {
                CustomerKey = Guid.NewGuid().ToString(),
                LicenseKey = Guid.NewGuid().ToString()
            };
            _apiClient = new ServiceClient() { ServiceRoot = EnvSetupParameters.LocalServerUrl };
        }

        public EnvSetupWizardModel WizardModel { get; set; }

        public string Info { get; }

        public event EventHandler Started;

        public event EventHandler Stopped;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblProgress.Text = String.Empty;
        }

        private void RaiseStartedEvent()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseStoppedEvent()
        {
            Stopped?.Invoke(this, EventArgs.Empty);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (!EnsureLocalServerIsUp())
            {
                return;
            }

            Cursor = Cursors.AppStarting;
            txtOutput.Focus();
            btnStart.Enabled = false;
            RaiseStartedEvent();
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _params = new EnvSetupParameters(WizardModel);
            string result = GenerateDevelopmentSettings();
            if (!String.IsNullOrEmpty(result))
            {
                SetStoppedState();
                return;
            }

            result = CreateLicenseDatabase();
            if (!String.IsNullOrEmpty(result))
            {
                SetStoppedState();
                return;
            }

            result = AddDeveloperLicense();
            if (!String.IsNullOrEmpty(result))
            {
                SetStoppedState();
                return;
            }

            result = GenerateDevelopmentEnvironment();
            if (!String.IsNullOrEmpty(result))
            {
                SetStoppedState();
                return;
            }

            result = CreateSystemDatabase();
            if (!String.IsNullOrEmpty(result))
            {
                SetStoppedState();
                return;
            }

            result = CreateSampleDatabase();
            if (!String.IsNullOrEmpty(result))
            {
                SetStoppedState();
                return;
            }

            result = ActivateDeveloperLicense();
            if (!String.IsNullOrEmpty(result))
            {
                SetStoppedState();
                return;
            }

            worker.ReportProgress(2);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateProgress(e.ProgressPercentage);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _outputBuilder.AppendLine("Environment setup completed successfully.");
            SetStoppedState();
        }

        private string GenerateDevelopmentSettings()
        {
            string result = null;
            _outputBuilder.Append("> Generating development settings... (1/7)    ");
            worker.ReportProgress(0);
            try
            {
                var settings = BuildSettings.WebLocal;
                settings.DbServerName = WizardModel.DbServerName;
                ITextTemplate template = new WebApiSettings(settings);
                File.WriteAllText(_params.WebApiSettings, template.TransformText());

                template = new LicenseApiSettings(settings);
                File.WriteAllText(_params.LicenseApiSettings, template.TransformText());

                template = new LocalLicenseApiSettings(settings);
                File.WriteAllText(_params.LocalLicenseApiSettings, template.TransformText());
                _outputBuilder.AppendLine("(OK)");
                worker.ReportProgress(5);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _outputBuilder.AppendLine();
                _outputBuilder.AppendFormat("  >> Error : {0}", ex.Message);
                _outputBuilder.AppendLine();
                worker.ReportProgress(0);
            }

            return result;
        }

        private string CreateLicenseDatabase()
        {
            string result = null;
            var sql = new SqlServerConsole() { ConnectionString = _params.Connection };
            _outputBuilder.Append("> Creating local licensing database... (2/7)    ");
            worker.ReportProgress(0);
            try
            {
                sql.ExecuteNonQuery(EnvSetupParameters.CreateLicenseDbScript);
                _outputBuilder.AppendLine("(OK)");
                worker.ReportProgress(10);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _outputBuilder.AppendLine();
                _outputBuilder.AppendFormat("  >> Error : {0}", ex.Message);
                _outputBuilder.AppendLine();
                worker.ReportProgress(0);
            }

            return result;
        }

        private string AddDeveloperLicense()
        {
            string result = null;
            var sql = new SqlServerConsole() { ConnectionString = _params.Connection };
            _outputBuilder.Append("> Adding developer license... (3/7)    ");
            worker.ReportProgress(0);
            try
            {
                sql.ExecuteNonQuery(String.Format(EnvSetupParameters.InsertDevLicenseScript,
                    _instance.CustomerKey, WizardModel.LicenseeFirstName, WizardModel.LicenseeLastName,
                    _instance.LicenseKey));
                CreateApiServiceLicense();
                CreateApiServiceEdition();
                _outputBuilder.AppendLine("(OK)");
                worker.ReportProgress(5);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _outputBuilder.AppendLine();
                _outputBuilder.AppendFormat("  >> Error : {0}", ex.Message);
                _outputBuilder.AppendLine();
                worker.ReportProgress(0);
            }

            return result;
        }

        private string GenerateDevelopmentEnvironment()
        {
            string result = null;
            _outputBuilder.Append("> Generating Angular development environment... (4/7)    ");
            worker.ReportProgress(0);
            try
            {
                WizardModel.InstanceKey = GetInstanceKey();
                ITextTemplate template = new NgDevEnvironment(WizardModel);
                File.WriteAllText(_params.AngularEnvPath, template.TransformText());

                _outputBuilder.AppendLine("(OK)");
                worker.ReportProgress(5);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _outputBuilder.AppendLine();
                _outputBuilder.AppendFormat("  >> Error : {0}", ex.Message);
                _outputBuilder.AppendLine();
                worker.ReportProgress(0);
            }

            return result;
        }

        private string CreateSystemDatabase()
        {
            string result = null;
            var sql = new SqlServerConsole() { ConnectionString = _params.Connection };
            _outputBuilder.Append("> Creating TadbirNG system database... (5/7)    ");
            worker.ReportProgress(0);
            try
            {
                var builder = new StringBuilder();
                builder.AppendLine(File.ReadAllText(_params.SystemDbScript));
                builder.AppendLine();
                builder.AppendFormat(File.ReadAllText(_params.SystemDataDbScript), WizardModel.DbServerName);
                sql.ExecuteNonQuery(builder.ToString());
                builder.Clear();
                builder.AppendLine(File.ReadAllText(_params.SystemDbTriggers));
                sql.ExecuteNonQuery(builder.ToString());
                builder.Clear();
                builder.AppendLine(File.ReadAllText(_params.SystemDbJobs));
                sql.ExecuteNonQuery(builder.ToString());
                _outputBuilder.AppendLine("(OK)");
                worker.ReportProgress(25);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _outputBuilder.AppendLine();
                _outputBuilder.AppendFormat("  >> Error : {0}", ex.Message);
                _outputBuilder.AppendLine();
                worker.ReportProgress(0);
            }

            return result;
        }

        private string CreateSampleDatabase()
        {
            string result = null;
            var sql = new SqlServerConsole() { ConnectionString = _params.Connection };
            _outputBuilder.Append("> Creating TadbirNG sample database... (6/7)    ");
            worker.ReportProgress(0);
            try
            {
                var builder = new StringBuilder();
                builder.AppendLine(EnvSetupParameters.CreateSampleInitScript);
                builder.AppendLine();
                builder.AppendLine(File.ReadAllText(_params.SampleDbScript));
                builder.AppendLine();
                builder.AppendLine(File.ReadAllText(_params.SampleDataDbScript));
                sql.ExecuteNonQuery(builder.ToString());
                _outputBuilder.AppendLine("(OK)");
                worker.ReportProgress(25);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _outputBuilder.AppendLine();
                _outputBuilder.AppendFormat("  >> Error : {0}", ex.Message);
                _outputBuilder.AppendLine();
                worker.ReportProgress(0);
            }

            return result;
        }

        private string ActivateDeveloperLicense()
        {
            string result = null;
            _outputBuilder.Append("> Activating developer license... (7/7)    ");
            worker.ReportProgress(0);
            try
            {
                _apiClient.AddHeader(Constants.InstanceHeaderName, WizardModel.InstanceKey);
                _apiClient.Update("Null Data", LicenseApi.ActivateLicense);
                _outputBuilder.AppendLine("(OK)");
                worker.ReportProgress(23);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                _outputBuilder.AppendLine();
                _outputBuilder.AppendFormat("  >> Error : {0}", ex.Message);
                _outputBuilder.AppendLine();
                worker.ReportProgress(0);
            }

            return result;
        }

        private void LogOutput()
        {
            txtOutput.Text = _outputBuilder.ToString();
        }

        private void UpdateProgress(int increment)
        {
            LogOutput();
            progress.Value += increment;
            lblProgress.Text = String.Format("{0}%", progress.Value);
        }

        private string GetInstanceKey()
        {
            var crypto = new CryptoService(new CertificateManager());
            return crypto.Encrypt(JsonHelper.From(_instance, false));
        }

        private bool EnsureLocalServerIsUp()
        {
            bool validated = true;
            Cursor = Cursors.WaitCursor;
            try
            {
                _apiClient.Update("Null Data", LicenseApi.ValidateLicense);
            }
            catch
            {
                MessageBox.Show(this, "Please make sure local license server (SPPC.Licensing.Local.Web) is running.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validated = false;
            }

            Cursor = Cursors.Default;
            return validated;
        }

        private void CreateApiServiceLicense()
        {
            var path = String.Format(@"{0}\license.Development.json", _params.WebApiRootPath);
            var licenseData = GetLicenseData();
            var json = JsonHelper.From(licenseData);
            File.WriteAllText(path, json);
        }

        private void CreateApiServiceEdition()
        {
            var configPath = ConfigurationManager.AppSettings["EditionConfigPath"];
            var allConfig = JsonHelper.To<EditionsConfig>(File.ReadAllText(configPath));

            var path = String.Format(@"{0}\edition.Development.json", _params.WebApiRootPath);
            string json = JsonHelper.From(allConfig.Enterprise);
            File.WriteAllText(path, json);
        }

        private LicenseViewModel GetLicenseData()
        {
            return new LicenseViewModel()
            {
                CustomerName = "تیم توسعه تدبیر وب",
                ContactName = String.Format("{0} {1}", WizardModel.LicenseeFirstName, WizardModel.LicenseeLastName),
                Edition = "Enterprise",
                UserCount = 5,
                ActiveModules = 1023,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date + TimeSpan.FromDays(365)
            };
        }

        private void SetStoppedState()
        {
            if (btnStart.InvokeRequired)
            {
                btnStart.BeginInvoke((MethodInvoker)delegate ()
                {
                    btnStart.Enabled = true;
                    Cursor = Cursors.Default;
                    RaiseStoppedEvent();
                });
            }
            else
            {
                btnStart.Enabled = true;
                Cursor = Cursors.Default;
                RaiseStoppedEvent();
            }
        }

        private readonly InstanceModel _instance;
        private readonly StringBuilder _outputBuilder;
        private readonly IApiClient _apiClient;
        private EnvSetupParameters _params;
    }
}

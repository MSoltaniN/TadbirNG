using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Licensing.Local.Persistence;
using SPPC.Tools.TadbirActivator.Properties;

namespace SPPC.Tools.TadbirActivator
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string licenseInfo = GetLicenseInfo();
            if (String.IsNullOrEmpty(licenseInfo))
            {
                MessageBox.Show(this, Resources.Error_AppNotInstalled, Resources.ErrorMessage,
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                Application.Exit();
            }

            txtLicenseInfo.Text = licenseInfo;
        }

        private void Activate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var client = new ServiceClient(_serverUrl);
            var activation = GetActivationData();
            try
            {
                var response = client.Update<ActivationModel, string>(activation, "/license/activate");
                if (String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(this, Resources.Warn_AlreadyActivated, Resources.SuccessfulOperation,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading);
                    Cursor = Cursors.Default;
                    return;
                }

                string licenseRoot = ConfigurationManager.AppSettings["LicensePath"];
                string licensePath = Path.Combine(licenseRoot, Constants.LicenseFile);
                File.WriteAllText(licensePath, response);
                ExportCertificate(licenseRoot, response);
                MessageBox.Show(this, Resources.Info_ActivationSucceeded, Resources.SuccessfulOperation,
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, Resources.Error_NoInternet, Resources.ErrorMessage,
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }

            Cursor = Cursors.Default;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string GetLicenseInfo()
        {
            string licenseInfo = String.Empty;
            string licensePath = ConfigurationManager.AppSettings["ServerLicensePath"];
            if (File.Exists(licensePath))
            {
                string json = File.ReadAllText(licensePath);
                var license = JsonHelper.To<LicenseModel>(json);

                var builder = new StringBuilder();
                builder.AppendFormat("{0} : {1}", Resources.AppEdition, license.Edition);
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.UserCount, license.UserCount);
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.ContractStart,
                    JalaliDateTime.FromDateTime(license.ContractStart).ToShortDateString());
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.ContractEnd,
                    JalaliDateTime.FromDateTime(license.ContractEnd).ToShortDateString());
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.ActiveModules, GetActiveModules(license.ActiveModules));
                builder.AppendLine();
                licenseInfo = builder.ToString();
            }

            return licenseInfo;
        }

        private string GetActiveModules(int activeModules)
        {
            var modules = new List<string>();
            var modulesFlags = (Subsystems)activeModules;
            if (HasModule(modulesFlags, Subsystems.Accounting))
            {
                modules.Add(Resources.Accounting);
            }

            if (HasModule(modulesFlags, Subsystems.Budgeting))
            {
                modules.Add(Resources.Budgeting);
            }

            if (HasModule(modulesFlags, Subsystems.CashFlow))
            {
                modules.Add(Resources.CashFlow);
            }

            if (HasModule(modulesFlags, Subsystems.Cheque))
            {
                modules.Add(Resources.Cheque);
            }

            if (HasModule(modulesFlags, Subsystems.Inventory))
            {
                modules.Add(Resources.Inventory);
            }

            if (HasModule(modulesFlags, Subsystems.Personnel))
            {
                modules.Add(Resources.Personnel);
            }

            if (HasModule(modulesFlags, Subsystems.Purchase))
            {
                modules.Add(Resources.Purchase);
            }

            if (HasModule(modulesFlags, Subsystems.Sales))
            {
                modules.Add(Resources.Sales);
            }

            if (HasModule(modulesFlags, Subsystems.WagePayment))
            {
                modules.Add(Resources.WagePayment);
            }

            if (HasModule(modulesFlags, Subsystems.Warehousing))
            {
                modules.Add(Resources.Warehousing);
            }

            return String.Join("، ", modules.ToArray());
        }

        private bool HasModule(Subsystems modules, Subsystems module)
        {
            return (modules & module) != 0;
        }

        private ActivationModel GetActivationData()
        {
            var activation = new ActivationModel()
            {
                InstanceKey = GetInstanceId(),
                HardwareKey = HardwareKey.GetSystemUniqueId(),
            };

            var manager = new CertificateManager();
            _certificate = manager.GetFromStore(_issuerName);
            if (_certificate == null)
            {
                _certificate = manager.GenerateSelfSigned(_issuerName, _subjectName);
            }

            activation.ClientKey = Convert.ToBase64String(_certificate.GetPublicKey());
            return activation;
        }

        private InstanceModel GetInstanceId()
        {
            var instance = new InstanceModel();
            using (StreamReader reader = new StreamReader(
                GetType().Assembly.GetManifestResourceStream("SPPC.Tools.TadbirActivator.instance.id")))
            {
                var json = reader.ReadToEnd();
                instance = JsonHelper.To<InstanceModel>(json);
            }

            return instance;
        }

        private void ExportCertificate(string root, string licenseData)
        {
            var utility = new LicenseUtility();
            string path = Path.Combine(root, Constants.CertificateFile);
            var license = utility.LoadLicense(licenseData);
            var certificateBytes = _certificate.Export(X509ContentType.Pkcs12, license.Secret);
            File.WriteAllBytes(path, certificateBytes);
        }

        private readonly string _issuerName = "CN=Tadbir Licensing CA";
        private readonly string _subjectName = "CN=Tadbir";
        private readonly string _serverUrl = "http://localhost:1447";
        private X509Certificate2 _certificate;
    }
}

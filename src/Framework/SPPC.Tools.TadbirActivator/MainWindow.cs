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
using SPPC.Licensing.Model;
using SPPC.Licensing.Local.Persistence;
using SPPC.Tools.TadbirActivator.Properties;
using SPPC.Tadbir.Licensing;

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

        private ILicenseUtility Service
        {
            get
            {
                if (_service == null)
                {
                    string root = ConfigurationManager.AppSettings["OnlineServerRoot"];
                    _service = LicenseUtility.CreateDefault(root);
                }

                return _service;
            }
        }

        private void Activate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                var activation = GetActivationData();
                string license = Service.GetActivatedLicense(activation);
                if (String.IsNullOrEmpty(license))
                {
                    MessageBox.Show(this, Resources.Warn_AlreadyActivated, Resources.SuccessfulOperation,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading);
                    Cursor = Cursors.Default;
                    return;
                }

                string licenseRoot = ConfigurationManager.AppSettings["LicensePath"];
                string licensePath = Path.Combine(licenseRoot, Constants.LicenseFile);
                File.WriteAllText(licensePath, license);
                ExportCertificate(licenseRoot, license);
                MessageBox.Show(this, Resources.Info_ActivationSucceeded, Resources.SuccessfulOperation,
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
            catch
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
                var license = JsonHelper.To<LicenseViewModel>(json);

                var builder = new StringBuilder();
                builder.AppendFormat("{0} : {1}", Resources.CompanyName, license.CustomerName);
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.AppEdition, license.Edition);
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.UserCount, license.UserCount);
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.ContractStart,
                    JalaliDateTime.FromDateTime(license.StartDate).ToShortDateString());
                builder.AppendLine();
                builder.AppendFormat("{0} : {1}", Resources.ContractEnd,
                    JalaliDateTime.FromDateTime(license.EndDate).ToShortDateString());
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
                HardwareKey = HardwareKey.UniqueKey,
            };

            var manager = new CertificateManager();
            _certificate = manager.GenerateSelfSigned(Constants.IssuerName, Constants.SubjectName);
            activation.ClientKey = Convert.ToBase64String(_certificate.GetPublicKey());
            return activation;
        }

        private string GetInstanceId()
        {
            string instance = null;
            using (StreamReader reader = new StreamReader(
                GetType().Assembly.GetManifestResourceStream("SPPC.Tools.TadbirActivator.instance.id")))
            {
                instance = reader.ReadToEnd();
            }

            return instance;
        }

        private void ExportCertificate(string root, string licenseData)
        {
            string path = Path.Combine(root, Constants.CertificateFile);
            var license = Service.LoadLicense(licenseData);
            var certificateBytes = _certificate.Export(X509ContentType.Pkcs12, license.Secret);
            File.WriteAllBytes(path, certificateBytes);
        }

        private ILicenseUtility _service;
        private X509Certificate2 _certificate;
    }
}

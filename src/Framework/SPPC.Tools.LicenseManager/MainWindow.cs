using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;

namespace SPPC.Tools.LicenseManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            License = new LicenseModel();
            Customer = new CustomerModel();
            _crypto = new CryptoService();
        }

        public LicenseModel License { get; set; }

        public CustomerModel Customer { get; set; }

        private void SaveCustomer_Click(object sender, EventArgs e)
        {
        }

        private void SaveLicense_Click(object sender, EventArgs e)
        {
        }

        private void SaveInstance_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings["InstanceIdPath"];
            var instance = new InstanceModel()
            {
                CustomerKey = License.CustomerKey,
                LicenseKey = License.LicenseKey
            };
            string instanceData = _crypto.Encrypt(JsonHelper.From(instance));
            File.WriteAllText(path, instanceData);
            CreateApiServiceLicense();
            MessageBox.Show(this, "شناسه برنامه با موفقیت ثبت شد.",
                "عملیات موفق", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateApiServiceLicense()
        {
            var path = ConfigurationManager.AppSettings["WebApiLicensePath"];
            var license = GetLicenseData();
            var json = JsonHelper.From(license);
            File.WriteAllText(path, json);
            return;
        }

        private LicenseViewModel GetLicenseData()
        {
            return new LicenseViewModel()
            {
                CustomerName = Customer.CompanyName,
                Edition = License.Edition,
                UserCount = License.UserCount,
                ActiveModules = License.ActiveModules,
                StartDate = License.StartDate,
                EndDate = License.EndDate
            };
        }

        private readonly ICryptoService _crypto;
    }
}

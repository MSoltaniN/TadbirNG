using System;
using System.IO;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class LicenseUtilityForm : Form
    {
        public LicenseUtilityForm()
        {
            InitializeComponent();
            _apiClient = new ServiceClient() { ServiceRoot = EnvSetupParameters.LocalServerUrl };
            _crypto = CryptoService.Default;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _license = LoadCurrentLicense();
            LoadValues(_license);
        }

        private void Renew_Click(object sender, EventArgs e)
        {
            // Make sure license servers are running ...
            if (!EnsureLocalServerIsUp())
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            try
            {
                SaveValues(_license);
                DeactivateLicense(_license);
                UpdateLicenseInfo(_license);
                ReactivateLicense(_license);
                btnRenew.Enabled = false;
                var message = "Current developer license was successfully renewed.";
                MessageBox.Show(this, message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                var message = "Error occured while renewing current developer license." +
                    $"{Environment.NewLine}Error Message :{Environment.NewLine}{ex.Message}";
                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor = Cursors.Default;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static LicenseFileModel LoadCurrentLicense()
        {
            var licensePath = Path.Combine(PathConfig.LocalServerRoot, "wwwroot", "tadbir.lic");
            return JsonHelper.To<LicenseFileModel>(
                CryptoService.Default.Decrypt(
                    File.ReadAllText(licensePath)));
        }

        private static void DeactivateLicense(LicenseFileModel license)
        {
            // Update current license into deactivated state (update database record) ...
            var dal = new SqlDataLayer(DbConnections.LicenseConnection);
            var query = String.Format(
                ToolsQuery.LicenseQueryByInstanceKey, license.LicenseKey, license.CustomerKey);
            int licenseId = Convert.ToInt32(dal.QueryScalar(query));
            query = String.Format(
                ToolsQuery.DeactivateLicense, license.UserCount, license.Edition,
                license.StartDate, license.EndDate, license.OfflineLimit, licenseId);
            dal.ExecuteNonQuery(query);
        }

        private static void UpdateLicenseInfo(LicenseFileModel license)
        {
            // Delete or rename current license files (tadbir.pfx and tadbir.lic) ...
            var root = Path.Combine(PathConfig.LocalServerRoot, "wwwroot");
            File.Move(Path.Combine(root, "tadbir.pfx"), Path.Combine(root, "_tadbir.pfx"), true);
            File.Move(Path.Combine(root, "tadbir.lic"), Path.Combine(root, "_tadbir.lic"), true);

            // Update license.Development.json with new license info ...
            var licensePath = Path.Combine(PathConfig.WebApiRoot, "wwwroot", "license.Development.json");
            File.WriteAllText(
                licensePath, JsonHelper.From(
                    LicenseFactory.FromFileModel(license)));
        }

        private void LoadValues(LicenseFileModel license)
        {
            txtCustomer.Text = license.CustomerName;
            spnUserCount.Value = license.UserCount;
            cmbEdition.SelectedIndex = cmbEdition.FindString(license.Edition);
            dtpStartDate.Value = license.StartDate;
            dtpEndDate.Value = license.EndDate;
            spnOfflineLimit.Value = license.OfflineLimit;
        }

        private void SaveValues(LicenseFileModel license, bool useDefault = true)
        {
            var jalaliNow = JalaliDateTime.Now;
            var startDate = jalaliNow.ToGregorian().Date;
            var endDate = new JalaliDateTime(jalaliNow.Year + 1, jalaliNow.Month, jalaliNow.Day)
                .ToGregorian().Date;
            license.UserCount = useDefault ? 10 : (int)spnUserCount.Value;
            license.Edition = useDefault ? "Enterprise" : (string)cmbEdition.SelectedItem;
            license.StartDate = useDefault ? startDate : dtpStartDate.Value.Date;
            license.EndDate = useDefault ? endDate : dtpEndDate.Value.Date;
            license.OfflineLimit = useDefault ? 0 : (int)spnOfflineLimit.Value;
        }

        private void ReactivateLicense(LicenseFileModel license)
        {
            // Renew license by calling /license/activate on local license server ...
            var instanceKey = _crypto.Encrypt(
                JsonHelper.From(
                    new InstanceModel()
                    {
                        CustomerKey = license.CustomerKey,
                        LicenseKey = license.LicenseKey
                    }, false));
            _apiClient.AddHeader(LicenseConstants.InstanceHeaderName, instanceKey);
            _apiClient.Update("Null Data", LicenseApi.ActivateLicense);
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

        private readonly IApiClient _apiClient;
        private readonly ICryptoService _crypto;
        private LicenseFileModel _license;
    }
}

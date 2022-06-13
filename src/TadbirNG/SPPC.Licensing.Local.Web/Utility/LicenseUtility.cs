using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Common;

namespace SPPC.Tadbir.Licensing
{
    /// <summary>
    /// امکانات مورد نیاز برای گرفتن یا کنترل درستی مجوز برنامه را پیاده سازی می کند
    /// </summary>
    public class LicenseUtility : ILicenseUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="crypto">امکان انجام عملیات رمزنگاری متقارن را فراهم می کند</param>
        /// <param name="deviceId">امکان خواندن شناسه سخت افزاری را فراهم می کند</param>
        public LicenseUtility(IApiClient apiClient, ICryptoService crypto, IDeviceIdProvider deviceId,
            ILicensePathProvider pathProvider)
        {
            _apiClient = apiClient;
            _crypto = crypto;
            _deviceId = deviceId;
            _pathProvider = pathProvider;
        }

        /// <summary>
        /// به روش آسنکرون مجوز برنامه را فعالسازی می کند
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="connection"></param>
        /// <returns>نتیجه فعالسازی مجوز به صورت کدهای سیستمی تعریف شده</returns>
        public async Task<ActivationResult> ActivateLicenseAsync(string instance, RemoteConnection connection)
        {
            if (String.IsNullOrWhiteSpace(instance))
            {
                return ActivationResult.Failed;
            }

            ActivationResult result;
            var activation = GetActivationData(instance, connection, out X509Certificate2 certificate);
            var license = await GetActivatedLicenseAsync(activation);
            if (_apiClient.LastResponse.Result == ServiceResult.ValidationFailed)
            {
                result = ActivationResult.BadInstance;
            }
            else if (_apiClient.LastResponse.Result == ServiceResult.ServerError)
            {
                result = ActivationResult.Failed;
            }
            else if (license == String.Empty)
            {
                result = ActivationResult.AlreadyActivated;
            }
            else
            {
                await File.WriteAllTextAsync(LicensePath, license);
                await ExportCertificateAsync(certificate);
                result = ActivationResult.OK;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<string> GetOnlineLicenseAsync(string instance, RemoteConnection connection)
        {
            var licenseCheck = await GetLicenseCheckAsync(instance, connection);
            _apiClient.AddHeader(
                Constants.LicenseCheckHeaderName, _crypto.Encrypt(JsonHelper.From(licenseCheck)));
            var license = await _apiClient.GetAsync<string>(LicenseApi.License);
            var licenseModel = await LoadLicenseAsync();
            ResetLoginCount(licenseModel);
            return license;
        }

        /// <summary>
        /// به روش آسنکرون اطلاعات مجوز فعال سازی شده موجود را خوانده و به صورت امضای دیجیتالی برمی گرداند
        /// </summary>
        /// <returns>امضای دیجیتالی به دست آمده از مجوز فعال سازی شده</returns>
        public async Task<string> GetLicenseAsync()
        {
            var signature = String.Empty;
            var ignored = new string[]
            {
                "CustomerKey", "LicenseKey", "HardwareKey", "ClientKey", "Secret",
                "IsActivated", "OfflineLimit", "LoginCount"
            };
            var licenseModel = await LoadLicenseAsync();
            if (licenseModel.OfflineLimit == 0 || licenseModel.LoginCount < licenseModel.OfflineLimit)
            {
                var certificate = LoadCerificate(licenseModel.Secret);
                string license = JsonHelper.From(licenseModel, true, ignored);
                var licenseBytes = Encoding.UTF8.GetBytes(license);
                signature = _crypto.SignData(licenseBytes, certificate);
                if (licenseModel.OfflineLimit > 0)
                {
                    UpdateLoginCount(licenseModel);
                }
            }

            return signature;
        }

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور کامل بررسی می کند
        /// </summary>
        /// <returns>وضعیت بررسی مجوز که نشان می دهد مجوز موجود معتبر هست یا نه</returns>
        public LicenseStatus ValidateLicense(string instance, RemoteConnection connection)
        {
            var instanceModel = GetInstance(instance);
            var status = LicenseStatus.OK;
            if (!EnsureLicenseIsActivated())
            {
                status = LicenseStatus.NotActivated;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Product license is not yet activated.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureLicenseExists())
            {
                status = LicenseStatus.NoLicense;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] License file '{1}' could not be loaded.{2}",
                    DateTime.Now.ToString(), Constants.LicenseFile, Environment.NewLine);
            }
            else if (EnsureLicenseNotCorrupt(out LicenseFileModel license))
            {
                status = LicenseStatus.Corrupt;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] License file is corrupt or tampered.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureCertificateExists(license, out X509Certificate2 certificate))
            {
                status = LicenseStatus.NoCertificate;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Certificate file '{1}' could not be loaded.{2}",
                    DateTime.Now.ToString(), Constants.CertificateFile, Environment.NewLine);
            }
            else if (!EnsureCertificateIsValid(certificate, license.ClientKey))
            {
                status = LicenseStatus.BadCertificate;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Certificate file is invalid.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureRunsOnOriginalHardware(connection, license.HardwareKey))
            {
                status = LicenseStatus.HardwareMismatch;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] This hardware system does not match original hardware.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureInstanceIsValid(instanceModel, license))
            {
                status = LicenseStatus.InstanceMismatch;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Given instance is not licensed on this server.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureLicenseNotExpired(license))
            {
                status = LicenseStatus.Expired;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] License is expired.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }

            _log.AppendLine();
            File.AppendAllText(Path.Combine("wwwroot", "license.log"), _log.ToString());
            return status;
        }

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور خلاصه بررسی می کند
        /// </summary>
        /// <returns></returns>
        public LicenseStatus QuickValidateLicense(string instance)
        {
            var status = LicenseStatus.OK;
            if (!EnsureLicenseIsActivated())
            {
                status = LicenseStatus.NotActivated;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Product license is not yet activated.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureLicenseExists())
            {
                status = LicenseStatus.NoLicense;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] License file '{1}' could not be loaded.{2}",
                    DateTime.Now.ToString(), Constants.LicenseFile, Environment.NewLine);
            }
            else if (EnsureLicenseNotCorrupt(out _))
            {
                status = LicenseStatus.Corrupt;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] License file is corrupt or tampered.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!File.Exists(CertificatePath))
            {
                status = LicenseStatus.NoCertificate;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Certificate file '{1}' does not exist.{2}",
                    DateTime.Now.ToString(), Constants.CertificateFile, Environment.NewLine);
            }

            File.AppendAllText(Path.Combine("wwwroot", "license.log"), _log.ToString());
            return status;
        }

        /// <summary>
        /// به روش آسنکرون درستی اطلاعات متنی مجوز مورد استفاده سرویس را با امضای دیجیتالی داده شده بررسی می کند
        /// </summary>
        /// <param name="apiLicense">اطلاعات متنی فایل مجوز سرویس</param>
        /// <param name="signature">امضای دیجیتالی مورد استفاده برای بررسی درستی مجوز</param>
        /// <returns>در صورت درستی مجوز مقدار بولی "درست" و در صورت
        /// عدم مطابقت اطلاعات متنی با اطلاعات فعال سازی شده مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> ValidateSignatureAsync(string apiLicense, string signature)
        {
            Verify.ArgumentNotNullOrEmptyString(apiLicense, nameof(apiLicense));
            Verify.ArgumentNotNullOrEmptyString(signature, nameof(signature));
            byte[] apiLicenseBytes = Encoding.UTF8.GetBytes(apiLicense);
            var license = await LoadLicenseAsync();
            var certificate = LoadCerificate(license.Secret);
            return _crypto.VerifyData(apiLicenseBytes, signature, certificate);
        }

        /// <summary>
        /// به روش آسنکرون اطلاعات مجوز برنامه را از فایل مرتبط خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات کامل مجوز برنامه</returns>
        public async Task <LicenseFileModel> LoadLicenseAsync()
        {
            var license = default(LicenseFileModel);
            if (File.Exists(LicensePath))
            {
                var licenseData = await File.ReadAllTextAsync(LicensePath, Encoding.UTF8);
                var json = _crypto.Decrypt(licenseData);
                license = JsonHelper.To<LicenseFileModel>(json);
            }

            return license;
        }

        /// <summary>
        /// اطلاعات فایل مجوز برنامه را مطابق با آخرین تغییرات ذخیره و به روزرسانی می کند
        /// </summary>
        /// <param name="license">اطلاعات مجوز برنامه با آخرین تغییرات</param>
        public void SaveLicense(LicenseFileModel license)
        {
            var json = JsonHelper.From(license, false, null, false);
            var encryptedLicense = _crypto.Encrypt(json);
            File.WriteAllText(LicensePath, encryptedLicense, Encoding.UTF8);
        }

        private string LicensePath
        {
            get { return _pathProvider.BinLicense; }
        }

        private string CertificatePath
        {
            get { return _pathProvider.Certificate; }
        }

        private async Task<string> GetActivatedLicenseAsync(ActivationModel activation)
        {
            Verify.ArgumentNotNull(activation, nameof(activation));
            return await _apiClient.UpdateAsync<ActivationModel, string>(activation, LicenseApi.ActivateLicense);
        }

        private ActivationModel GetActivationData(string instance, RemoteConnection connection,
            out X509Certificate2 certificate)
        {
            var activation = new ActivationModel()
            {
                InstanceKey = instance,
                HardwareKey = _deviceId.GetRemoteDeviceId(connection),
            };

            certificate = _crypto.CertificateManager.GenerateSelfSigned(
                Constants.IssuerName, Constants.SubjectName);
            activation.ClientKey = Convert.ToBase64String(certificate.GetPublicKey());
            return activation;
        }

        private async Task ExportCertificateAsync(X509Certificate2 certificate)
        {
            var license = await LoadLicenseAsync();
            var certificateBytes = certificate.Export(X509ContentType.Pkcs12, license.Secret);
            await File.WriteAllBytesAsync(CertificatePath, certificateBytes);
        }

        private async Task<LicenseCheckModel> GetLicenseCheckAsync(string instance, RemoteConnection connection)
        {
            var certificateBytes = await File.ReadAllBytesAsync(CertificatePath);
            return new LicenseCheckModel()
            {
                HardwardKey = _deviceId.GetRemoteDeviceId(connection),
                InstanceKey = instance,
                Certificate = Convert.ToBase64String(certificateBytes)
            };
        }

        private bool EnsureLicenseIsActivated()
        {
            _log.AppendFormat("[{0}] [INFO] Ensuring product is activated...",
                DateTime.Now.ToString());
            bool validated = File.Exists(LicensePath) || File.Exists(CertificatePath);
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureLicenseExists()
        {
            _log.AppendFormat("[{0}] [INFO] Ensuring license file exists...",
                DateTime.Now.ToString());
            bool validated = File.Exists(LicensePath);
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureLicenseNotCorrupt(out LicenseFileModel license)
        {
            license = null;
            bool isCorrupt = false;
            _log.AppendFormat("[{0}] [INFO] Ensuring license file is pristine ...",
                DateTime.Now.ToString());
            string licenseData = File.ReadAllText(LicensePath);
            if (!String.IsNullOrEmpty(licenseData))
            {
                try
                {
                    license = LoadLicenseAsync().Result;
                }
                catch
                {
                    isCorrupt = true;
                }
            }
            else
            {
                isCorrupt = true;
            }

            if (!isCorrupt)
            {
                _log.AppendLine(" (OK)");
            }

            return isCorrupt;
        }

        private bool EnsureCertificateExists(LicenseFileModel license, out X509Certificate2 certificate)
        {
            _log.AppendFormat("[{0}] [INFO] Loading licensing certificate...",
                DateTime.Now.ToString());
            certificate = LoadCerificate(license.Secret);
            bool validated = certificate != null;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureCertificateIsValid(X509Certificate2 certificate, string clientKey)
        {
            _log.AppendFormat("[{0}] [INFO] Validating licensing certificate...",
                DateTime.Now.ToString());
            var publicKey = Convert.ToBase64String(certificate.GetPublicKey());
            bool validated = String.Compare(clientKey, publicKey) == 0;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureRunsOnOriginalHardware(RemoteConnection connection, string hwKey)
        {
            _log.AppendFormat("[{0}] [INFO] Validating server hardware...",
                DateTime.Now.ToString());
            string hardwareKey = _deviceId.GetRemoteDeviceId(connection);
            bool validated = String.Compare(hwKey, hardwareKey) == 0;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureInstanceIsValid(InstanceModel instance, LicenseFileModel license)
        {
            _log.AppendFormat("[{0}] [INFO] Validating application instance...",
                DateTime.Now.ToString());
            bool validated = license.CustomerKey == instance.CustomerKey
                && license.LicenseKey == instance.LicenseKey;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureLicenseNotExpired(LicenseFileModel license)
        {
            _log.AppendFormat("[{0}] [INFO] Ensuring license not expired...",
                DateTime.Now.ToString());
            var now = DateTime.Now.Date;
            bool validated = now >= license.StartDate
                && now <= license.EndDate;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private InstanceModel GetInstance(string instance)
        {
            string json = _crypto.Decrypt(instance);
            return JsonHelper.To<InstanceModel>(json);
        }

        private void UpdateLoginCount(LicenseFileModel licenseModel)
        {
            lock (_loginCountLock)
            {
                licenseModel.LoginCount++;
                SaveLicense(licenseModel);
            }

            string info = String.Format(
                $"Offline licenses issued : {licenseModel.LoginCount}{Environment.NewLine}{Environment.NewLine}");
            File.AppendAllText(Path.Combine("wwwroot", "license.log"), info);
        }

        private void ResetLoginCount(LicenseFileModel license)
        {
            if (license.OfflineLimit > 0 && license.LoginCount > 0)
            {
                lock (_loginCountLock)
                {
                    license.LoginCount = 0;
                    SaveLicense(license);
                }
            }
        }

        private X509Certificate2 LoadCerificate(string password)
        {
            return _crypto.CertificateManager.GetFromFile(CertificatePath, password);
        }

        private static readonly object _loginCountLock = new();
        private readonly IApiClient _apiClient;
        private readonly ICryptoService _crypto;
        private readonly IDeviceIdProvider _deviceId;
        private readonly ILicensePathProvider _pathProvider;
        private readonly StringBuilder _log = new();
    }
}

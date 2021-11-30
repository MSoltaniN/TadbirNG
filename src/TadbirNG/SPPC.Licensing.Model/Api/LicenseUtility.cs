using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;

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
        public LicenseUtility(IApiClient apiClient, ICryptoService crypto, IDeviceIdProvider deviceId)
        {
            _apiClient = apiClient;
            _crypto = crypto;
            _deviceId = deviceId;
        }

        /// <summary>
        /// مسیر فایل مجوز ایجاد شده پس از فعال سازی برنامه
        /// </summary>
        public string LicensePath { get; set; }

        /// <summary>
        /// مجوز برنامه را فعالسازی می کند
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="connection"></param>
        /// <returns>نتیجه فعالسازی مجوز به صورت کدهای سیستمی تعریف شده</returns>
        public ActivationResult ActivateLicense(string instance, RemoteConnection connection)
        {
            if (String.IsNullOrWhiteSpace(instance))
            {
                return ActivationResult.Failed;
            }

            ActivationResult result;
            var activation = GetActivationData(instance, connection, out X509Certificate2 certificate);
            var license = GetActivatedLicense(activation);
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
                File.WriteAllText(LicensePath, license);
                ExportCertificate(certificate);
                result = ActivationResult.OK;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseCheck"></param>
        /// <returns></returns>
        public string GetOnlineLicense(string instance, RemoteConnection connection)
        {
            var licenseCheck = GetLicenseCheck(instance, connection);
            _apiClient.AddHeader(
                Constants.LicenseCheckHeaderName, _crypto.Encrypt(JsonHelper.From(licenseCheck)));
            return _apiClient.Get<string>(LicenseApi.License);
        }

        /// <summary>
        /// اطلاعات مجوز فعال سازی شده موجود را خوانده و به صورت امضای دیجیتالی برمی گرداند
        /// </summary>
        /// <returns>امضای دیجیتالی به دست آمده از مجوز فعال سازی شده</returns>
        public string GetLicense()
        {
            var ignored = new string[]
            {
                "CustomerKey", "LicenseKey", "HardwareKey", "ClientKey", "Secret", "IsActivated"
            };
            var licenseModel = LoadLicense();
            var certificate = LoadCerificate(licenseModel.Secret);
            string license = JsonHelper.From(licenseModel, true, ignored);
            var licenseBytes = Encoding.UTF8.GetBytes(license);
            return _crypto.SignData(licenseBytes, certificate);
        }

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور کامل بررسی می کند
        /// </summary>
        /// <returns>وضعیت بررسی مجوز که نشان می دهد مجوز موجود معتبر هست یا نه</returns>
        public LicenseStatus ValidateLicense(string instance, RemoteConnection connection)
        {
            var instanceModel = GetInstance(instance);
            var status = LicenseStatus.OK;
            if (!EnsureLicenseExists())
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
            File.AppendAllText(@".\wwwroot\license.log", _log.ToString());
            return status;
        }

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور خلاصه بررسی می کند
        /// </summary>
        /// <returns></returns>
        public LicenseStatus QuickValidateLicense(string instance)
        {
            var status = LicenseStatus.OK;
            if (!EnsureLicenseExists())
            {
                status = LicenseStatus.NoLicense;
            }
            else if (EnsureLicenseNotCorrupt(out _))
            {
                status = LicenseStatus.Corrupt;
            }

            string root = Path.GetDirectoryName(LicensePath);
            string certificatePath = Path.Combine(root, Constants.CertificateFile);
            if (!File.Exists(certificatePath))
            {
                status = LicenseStatus.NoCertificate;
            }

            return status;
        }

        /// <summary>
        /// درستی اطلاعات متنی مجوز مورد استفاده سرویس را با امضای دیجیتالی داده شده بررسی می کند
        /// </summary>
        /// <param name="apiLicense">اطلاعات متنی فایل مجوز سرویس</param>
        /// <param name="signature">امضای دیجیتالی مورد استفاده برای بررسی درستی مجوز</param>
        /// <returns>در صورت درستی مجوز مقدار بولی "درست" و در صورت
        /// عدم مطابقت اطلاعات متنی با اطلاعات فعال سازی شده مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool ValidateSignature(string apiLicense, string signature)
        {
            Verify.ArgumentNotNullOrEmptyString(apiLicense, nameof(apiLicense));
            Verify.ArgumentNotNullOrEmptyString(signature, nameof(signature));
            byte[] apiLicenseBytes = Encoding.UTF8.GetBytes(apiLicense);
            var license = LoadLicense();
            var certificate = LoadCerificate(license.Secret);
            return _crypto.VerifyData(apiLicenseBytes, signature, certificate);
        }

        private string GetActivatedLicense(ActivationModel activation)
        {
            Verify.ArgumentNotNull(activation, nameof(activation));
            return _apiClient.Update<ActivationModel, string>(activation, LicenseApi.ActivateLicense);
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

        private void ExportCertificate(X509Certificate2 certificate)
        {
            string path = Path.Combine(Path.GetDirectoryName(LicensePath), Constants.CertificateFile);
            var license = LoadLicense();
            var certificateBytes = certificate.Export(X509ContentType.Pkcs12, license.Secret);
            File.WriteAllBytes(path, certificateBytes);
        }

        private LicenseCheckModel GetLicenseCheck(string instance, RemoteConnection connection)
        {
            var license = LoadLicense();
            var certificate = LoadCerificate(license.Secret);
            return new LicenseCheckModel()
            {
                HardwardKey = _deviceId.GetRemoteDeviceId(connection),
                InstanceKey = instance,
                Certificate = Convert.ToBase64String(certificate.RawData)
            };
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
                    license = LoadLicense();
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

        private LicenseFileModel LoadLicense()
        {
            var licenseData = File.ReadAllText(LicensePath, Encoding.UTF8);
            var json = _crypto.Decrypt(licenseData);
            return JsonHelper.To<LicenseFileModel>(json);
        }

        private X509Certificate2 LoadCerificate(string password)
        {
            string root = Path.GetDirectoryName(LicensePath);
            string certificatePath = Path.Combine(root, Constants.CertificateFile);
            return _crypto.CertificateManager.GetFromFile(certificatePath, password);
        }

        private readonly IApiClient _apiClient;
        private readonly ICryptoService _crypto;
        private readonly IDeviceIdProvider _deviceId;
        private readonly StringBuilder _log = new();
    }
}

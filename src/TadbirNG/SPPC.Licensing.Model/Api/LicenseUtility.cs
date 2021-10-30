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
        /// <param name="signer">امکان انجام عملیات امضای دیجیتالی را فراهم می کند</param>
        /// <param name="manager">امکان مدیریت گواهینامه های امنیتی را فراهم می کند </param>
        /// <param name="deviceId">امکان خواندن شناسه سخت افزاری را فراهم می کند</param>
        public LicenseUtility(IApiClient apiClient, ICryptoService crypto, IDigitalSigner signer,
            ICertificateManager manager, IDeviceIdProvider deviceId)
        {
            _apiClient = apiClient;
            _crypto = crypto;
            _signer = signer;
            _manager = manager;
            _deviceId = deviceId;
        }

        /// <summary>
        /// مسیر فایل مجوز ایجاد شده پس از فعال سازی برنامه
        /// </summary>
        public string LicensePath { get; set; }

        /// <summary>
        /// نمونه جدیدی از این کلاس را با پیاده سازی پیش فرض ساخته و برمی گرداند
        /// </summary>
        /// <param name="webRoot">آدرس اصلی سرویس آنلاین کنترل لایسنس تدبیر</param>
        /// <returns>نمونه جدید با پیاده سازی پیش فرض برای همه وابستگی های کلاس</returns>
        public static ILicenseUtility CreateDefault(string webRoot)
        {
            var crypto = new CryptoService();
            return new LicenseUtility(new ServiceClient(webRoot),
                crypto, new DigitalSigner(crypto), new CertificateManager(), new DeviceIdProvider());
        }

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور کامل بررسی می کند
        /// </summary>
        /// <returns>وضعیت بررسی مجوز که نشان می دهد مجوز موجود معتبر هست یا نه</returns>
        public LicenseStatus ValidateLicense(string instance, RemoteConnection connection)
        {
            SetInstance(instance);
            var status = LicenseStatus.OK;
            if (!EnsureLicenseExists())
            {
                status = LicenseStatus.NoLicense;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] License file 'tadbir.lic' could not be loaded.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (EnsureLicenseNotCorrupt())
            {
                status = LicenseStatus.Corrupt;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] License file is corrupt or tampered.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureCertificateExists())
            {
                status = LicenseStatus.NoCertificate;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Certificate file 'tadbir.pfx' could not be loaded.{1}",
                    DateTime.Now.ToString());
            }
            else if (!EnsureCertificateIsValid())
            {
                status = LicenseStatus.BadCertificate;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Certificate file is invalid.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureRunsOnOriginalHardware(connection))
            {
                status = LicenseStatus.HardwareMismatch;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] This hardware system does not match original hardware.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureInstanceIsValid())
            {
                status = LicenseStatus.InstanceMismatch;
                _log.AppendLine();
                _log.AppendFormat("[{0}] [ERROR] Given instance is not licensed on this server.{1}",
                    DateTime.Now.ToString(), Environment.NewLine);
            }
            else if (!EnsureLicenseNotExpired())
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
            SetInstance(instance);
            var status = LicenseStatus.OK;
            if (!EnsureLicenseExists())
            {
                status = LicenseStatus.NoLicense;
            }
            else if (EnsureLicenseNotCorrupt())
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
            string licenseData = File.ReadAllText(LicensePath, Encoding.UTF8);
            _license = LoadLicense(licenseData);
            _signer.Certificate = LoadCerificate();
            return _signer.VerifyData(apiLicenseBytes, signature);
        }

        /// <summary>
        /// اطلاعات مجوز فعال سازی شده موجود را خوانده و به صورت امضای دیجیتالی برمی گرداند
        /// </summary>
        /// <returns>امضای دیجیتالی به دست آمده از مجوز فعال سازی شده</returns>
        public string GetActiveLicense()
        {
            _signer.Certificate = _certificate;
            var ignored = new string[]
            {
                "CustomerKey", "LicenseKey", "HardwareKey", "ClientKey", "Secret", "IsActivated"
            };
            string license = JsonHelper.From(_license, true, ignored);
            var licenseBytes = Encoding.UTF8.GetBytes(license);
            return _signer.SignData(licenseBytes);
        }

        /// <summary>
        /// اطلاعات رمزنگاری شده مجوز را خوانده و به صورت مدل اطلاعاتی مجوز برمی گرداند
        /// </summary>
        /// <param name="licenseData">اطلاعات رمزنگاری مجوز</param>
        /// <returns>اطلاعات رمزگشایی شده مجوز به صورت مدل اطلاعاتی مجوز</returns>
        public LicenseFileModel LoadLicense(string licenseData)
        {
            var json = _crypto.Decrypt(licenseData);
            return JsonHelper.To<LicenseFileModel>(json);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="activation"></param>
        /// <returns></returns>
        public string GetActivatedLicense(ActivationModel activation)
        {
            Verify.ArgumentNotNull(activation, nameof(activation));
            return _apiClient.Update<ActivationModel, string>(activation, LicenseApi.ActivateLicense);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseCheck"></param>
        /// <returns></returns>
        public string GetLicense(LicenseCheckModel licenseCheck)
        {
            Verify.ArgumentNotNull(licenseCheck, nameof(licenseCheck));
            _apiClient.AddHeader(
                Constants.LicenseCheckHeaderName, _crypto.Encrypt(JsonHelper.From(licenseCheck)));
            return _apiClient.Get<string>(LicenseApi.License);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public string GetRemoteDeviceId(RemoteConnection connection)
        {
            return _deviceId.GetRemoteDeviceId(connection);
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

        private bool EnsureLicenseNotCorrupt()
        {
            bool isCorrupt = false;
            _log.AppendFormat("[{0}] [INFO] Ensuring license file is pristine ...",
                DateTime.Now.ToString());
            string licenseData = File.ReadAllText(LicensePath);
            if (!String.IsNullOrEmpty(licenseData))
            {
                try
                {
                    _license = LoadLicense(licenseData);
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

        private bool EnsureCertificateExists()
        {
            _log.AppendFormat("[{0}] [INFO] Loading licensing certificate...",
                DateTime.Now.ToString());
            _certificate = LoadCerificate();
            bool validated = _certificate != null;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureCertificateIsValid()
        {
            _log.AppendFormat("[{0}] [INFO] Validating licensing certificate...",
                DateTime.Now.ToString());
            var publicKey = Convert.ToBase64String(_certificate.GetPublicKey());
            bool validated = String.Compare(_license.ClientKey, publicKey) == 0;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureRunsOnOriginalHardware(RemoteConnection connection)
        {
            _log.AppendFormat("[{0}] [INFO] Validating server hardware...",
                DateTime.Now.ToString());
            string hardwareKey = _deviceId.GetRemoteDeviceId(connection);
            bool validated = String.Compare(_license.HardwareKey, hardwareKey) == 0;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureInstanceIsValid()
        {
            _log.AppendFormat("[{0}] [INFO] Validating application instance...",
                DateTime.Now.ToString());
            bool validated = _license.CustomerKey == _instance.CustomerKey
                && _license.LicenseKey == _instance.LicenseKey;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private bool EnsureLicenseNotExpired()
        {
            _log.AppendFormat("[{0}] [INFO] Ensuring license not expired...",
                DateTime.Now.ToString());
            var now = DateTime.Now.Date;
            bool validated = now >= _license.StartDate
                && now <= _license.EndDate;
            if (validated)
            {
                _log.AppendLine(" (OK)");
            }

            return validated;
        }

        private void SetInstance(string instance)
        {
            string json = _crypto.Decrypt(instance);
            _instance = JsonHelper.To<InstanceModel>(json);
        }

        private X509Certificate2 LoadCerificate()
        {
            string root = Path.GetDirectoryName(LicensePath);
            string certificatePath = Path.Combine(root, Constants.CertificateFile);
            return _manager.GetFromFile(certificatePath, _license.Secret);
        }

        private readonly IApiClient _apiClient;
        private readonly ICryptoService _crypto;
        private readonly IDigitalSigner _signer;
        private readonly ICertificateManager _manager;
        private readonly IDeviceIdProvider _deviceId;
        private LicenseFileModel _license;
        private X509Certificate2 _certificate;
        private InstanceModel _instance;
        private readonly StringBuilder _log = new();
    }
}

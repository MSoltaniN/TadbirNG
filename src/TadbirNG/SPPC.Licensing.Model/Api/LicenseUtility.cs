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
            }
            else if (EnsureLicenseNotCorrupt())
            {
                status = LicenseStatus.Corrupt;
            }
            else if (!EnsureCertificateExists())
            {
                status = LicenseStatus.NoCertificate;
            }
            else if (!EnsureCertificateIsValid())
            {
                status = LicenseStatus.BadCertificate;
            }
            else if (!EnsureRunsOnOriginalHardware(connection))
            {
                status = LicenseStatus.HardwareMismatch;
            }
            else if (!EnsureInstanceIsValid())
            {
                status = LicenseStatus.InstanceMismatch;
            }
            else if (!EnsureLicenseNotExpired())
            {
                status = LicenseStatus.Expired;
            }

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
            return File.Exists(LicensePath);
        }

        private bool EnsureLicenseNotCorrupt()
        {
            bool isCorrupt = false;
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

            return isCorrupt;
        }

        private bool EnsureCertificateExists()
        {
            _certificate = LoadCerificate();
            return _certificate != null;
        }

        private bool EnsureCertificateIsValid()
        {
            var publicKey = Convert.ToBase64String(_certificate.GetPublicKey());
            return String.Compare(_license.ClientKey, publicKey) == 0;
        }

        private bool EnsureRunsOnOriginalHardware(RemoteConnection connection)
        {
            string hardwareKey = _deviceId.GetRemoteDeviceId(connection);
            return String.Compare(_license.HardwareKey, hardwareKey) == 0;
        }

        private bool EnsureInstanceIsValid()
        {
            return _license.CustomerKey == _instance.CustomerKey
                && _license.LicenseKey == _instance.LicenseKey;
        }

        private bool EnsureLicenseNotExpired()
        {
            var now = DateTime.Now.Date;
            return now >= _license.StartDate
                && now <= _license.EndDate;
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
    }
}

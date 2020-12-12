using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Licensing;

namespace SPPC.Licensing.Local.Persistence
{
    /// <summary>
    /// امکانات مورد نیاز برای گرفتن یا کنترل درستی مجوز برنامه را پیاده سازی می کند
    /// </summary>
    public class LicenseUtility : ILicenseUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="crypto">امکان انجام عملیات رمزنگاری متقارن را فراهم می کند</param>
        /// <param name="signer">امکان انجام عملیات امضای دیجیتالی را فراهم می کند</param>
        /// <param name="serializer">امکان تبدیل اشیاء سی شارپ به متن کدشده و بالعکس را فراهم می کند</param>
        /// <param name="manager">امکان مدیریت گواهینامه های امنیتی را فراهم می کند </param>
        public LicenseUtility(ICryptoService crypto, IDigitalSigner signer,
            IEncodedSerializer serializer, ICertificateManager manager)
        {
            _crypto = crypto;
            _signer = signer;
            _serializer = serializer;
            _manager = manager;
        }

        /// <summary>
        /// مسیر فایل مجوز ایجاد شده پس از فعال سازی برنامه
        /// </summary>
        public string LicensePath { get; set; }

        /// <summary>
        /// اطلاعات نمونه نصب شده برنامه که شامل شناسه مشتری و شناسه مجوز است
        /// </summary>
        public InstanceModel Instance { get; set; }

        /// <summary>
        /// نمونه جدیدی از این کلاس را با پیاده سازی پیش فرض ساخته و برمی گرداند
        /// </summary>
        /// <returns>نمونه جدید با پیاده سازی پیش فرض برای همه وابستگی های کلاس</returns>
        public static ILicenseUtility CreateDefault()
        {
            var crypto = new CryptoService();
            return new LicenseUtility(
                crypto, new DigitalSigner(crypto), new JsonSerializer(), new CertificateManager());
        }

        /// <summary>
        /// درستی اطلاعات موجود در فایل مجوز را به طور کامل بررسی می کند
        /// </summary>
        /// <returns>وضعیت بررسی مجوز که نشان می دهد مجوز موجود معتبر هست یا نه</returns>
        public LicenseStatus ValidateLicense()
        {
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
            else if (!EnsureRunsOnOriginalHardware())
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
        public LicenseStatus QuickValidateLicense()
        {
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
                "Id", "CustomerId", "CustomerKey", "LicenseKey", "HardwareKey",
                "ClientKey", "Secret", "Customer", "RowGuid", "ModifiedDate", "IsActivated"
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
        public LicenseModel LoadLicense(string licenseData)
        {
            var base64 = _crypto.Decrypt(licenseData);
            return _serializer.Deserialize<LicenseModel>(base64);
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

        private bool EnsureRunsOnOriginalHardware()
        {
            var hardwareKey = HardwareKey.GetSystemUniqueId();
            return String.Compare(_license.HardwareKey, hardwareKey) == 0;
        }

        private bool EnsureInstanceIsValid()
        {
            return _license.CustomerKey == Instance.CustomerKey
                && _license.LicenseKey == Instance.LicenseKey;
        }

        private bool EnsureLicenseNotExpired()
        {
            var now = DateTime.Now.Date;
            return now >= _license.StartDate
                && now <= _license.EndDate;
        }

        private X509Certificate2 LoadCerificate()
        {
            string root = Path.GetDirectoryName(LicensePath);
            string certificatePath = Path.Combine(root, Constants.CertificateFile);
            return _manager.GetFromFile(certificatePath, _license.Secret);
        }

        private readonly ICryptoService _crypto;
        private readonly IDigitalSigner _signer;
        private readonly IEncodedSerializer _serializer;
        private readonly ICertificateManager _manager;
        private LicenseModel _license;
        private X509Certificate2 _certificate;
    }
}

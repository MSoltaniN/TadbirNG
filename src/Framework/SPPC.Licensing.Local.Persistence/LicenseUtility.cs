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
    public class LicenseUtility : ILicenseUtility
    {
        public LicenseUtility(ICryptoService crypto, IDigitalSigner signer,
            IEncodedSerializer serializer, ICertificateManager manager)
        {
            _crypto = crypto;
            _signer = signer;
            _serializer = serializer;
            _manager = manager;
        }

        public string LicensePath { get; set; }

        public InstanceModel Instance { get; set; }

        public static ILicenseUtility CreateDefault()
        {
            var crypto = new CryptoService();
            return new LicenseUtility(
                crypto, new DigitalSigner(crypto), new JsonSerializer(), new CertificateManager());
        }

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

        public string GetActiveLicense()
        {
            _signer.Certificate = _certificate;
            ResetLicense();
            string license = JsonHelper.From(_license);
            var licenseBytes = Encoding.UTF8.GetBytes(license);
            return _signer.SignData(licenseBytes);
        }

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
            return _license.InstanceKey.Equals(Instance);
        }

        private bool EnsureLicenseNotExpired()
        {
            var now = DateTime.Now.Date;
            return now >= _license.StartDate
                && now <= _license.EndDate;
        }

        private void ResetLicense()
        {
            _license.ClientKey =
                _license.HardwareKey =
                _license.Secret = null;
            _license.InstanceKey = null;
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

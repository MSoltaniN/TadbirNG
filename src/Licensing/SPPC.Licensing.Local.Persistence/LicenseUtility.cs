using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Local.Persistence
{
    public class LicenseUtility
    {
        public LicenseUtility()
        {
        }

        public LicenseUtility(string licensePath, InstanceModel instance)
        {
            _licensePath = licensePath;
            _instance = instance;
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

            string root = Path.GetDirectoryName(_licensePath);
            string certificatePath = Path.Combine(root, Constants.CertificateFile);
            if (!File.Exists(certificatePath))
            {
                status = LicenseStatus.NoCertificate;
            }

            return status;
        }

        public string GetActiveLicense()
        {
            var signer = new DigitalSigner(LoadCerificate());
            ResetLicense();
            string license = JsonHelper.From(_license);
            var licenseBytes = Encoding.UTF8.GetBytes(license);
            return signer.SignData(licenseBytes);
        }

        public LicenseModel LoadLicense(string licenseData)
        {
            var serializer = new JsonSerializer();
            var crypto = new CryptoService();
            var base64 = crypto.Decrypt(licenseData);
            return serializer.Deserialize<LicenseModel>(base64);
        }

        private bool EnsureLicenseExists()
        {
            return File.Exists(_licensePath);
        }

        private bool EnsureLicenseNotCorrupt()
        {
            bool isCorrupt = false;
            string licenseData = File.ReadAllText(_licensePath);
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
            return _license.InstanceKey.Equals(_instance);
        }

        private bool EnsureLicenseNotExpired()
        {
            var now = DateTime.Now.Date;
            return now >= _license.ContractStart
                && now <= _license.ContractEnd;
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
            string root = Path.GetDirectoryName(_licensePath);
            string certificatePath = Path.Combine(root, Constants.CertificateFile);
            var manager = new CertificateManager();
            return manager.GetFromFile(certificatePath, _license.Secret);
        }

        private readonly string _licensePath;
        private readonly InstanceModel _instance;
        private LicenseModel _license;
        private X509Certificate2 _certificate;
    }
}

using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SPPC.Framework.Helpers;
using SPPC.Framework.Cryptography;
using SPPC.Licensing.Interfaces;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public class LicenseUtility : ILicenseUtility
    {
        public LicenseUtility(ILicenseRepository repository, IDigitalSigner signer)
        {
            _repository = repository;
            _signer = signer;
        }

        public LicenseCheckModel LicenseCheck { get; set; }

        public LicenseStatus ValidateLicense()
        {
            var status = LicenseStatus.OK;
            if (!EnsureLicenseExists())
            {
                status = LicenseStatus.NoLicense;
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
            else if (!EnsureLicenseNotExpired())
            {
                status = LicenseStatus.Expired;
            }

            return status;
        }

        public string GetActiveLicense()
        {
            _signer.Certificate = _certificate;
            ResetLicense();
            var json = JsonHelper.From(_license);
            var license = Encoding.UTF8.GetBytes(json);
            return _signer.SignData(license);
        }

        private bool EnsureLicenseExists()
        {
            _license = _repository.GetLicense(
                LicenseCheck.InstanceKey.LicenseKey, LicenseCheck.InstanceKey.CustomerKey);
            return _license != null;
        }

        private bool EnsureCertificateExists()
        {
            if (!String.IsNullOrEmpty(LicenseCheck.Certificate))
            {
                var rawData = Convert.FromBase64String(LicenseCheck.Certificate);
                _certificate = new X509Certificate2(rawData, _license.Secret);
            }

            return _certificate != null;
        }

        private bool EnsureCertificateIsValid()
        {
            var publicKey = Convert.ToBase64String(_certificate.GetPublicKey());
            return String.Compare(_license.ClientKey, publicKey) == 0;
        }

        private bool EnsureRunsOnOriginalHardware()
        {
            return String.Compare(_license.HardwareKey, LicenseCheck.HardwardKey) == 0;
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

        private readonly ILicenseRepository _repository;
        private readonly IDigitalSigner _signer;
        private LicenseModel _license;
        private X509Certificate2 _certificate;
    }
}

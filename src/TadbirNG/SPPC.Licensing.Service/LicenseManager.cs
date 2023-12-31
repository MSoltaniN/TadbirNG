﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Licensing.Persistence;

namespace SPPC.Licensing.Service
{
    public class LicenseManager : ILicenseManager, IDisposable
    {
        public LicenseManager(ILicenseRepository repository, ICryptoService crypto)
        {
            _repository = repository;
            _crypto = crypto;
        }

        public async Task<string> ActivateLicenseAsync(ActivationModel activation)
        {
            var instance = InstanceFactory.FromCrypto(activation.InstanceKey);
            var license = _repository.GetLicense(instance?.LicenseKey, instance?.CustomerKey);
            if (license != null)
            {
                license.HardwareKey = activation.HardwareKey;
                license.ClientKey = activation.ClientKey;
                license.Secret = GetNewLicenseSecret();
                license.IsActivated = true;
                await _repository.SaveLicenseAsync(license);
            }

            var licenseFile = await _repository.GetLicenseFileDataAsync(
                license.LicenseKey, license.CustomerKey);
            return _crypto.Encrypt(JsonHelper.From(licenseFile, false, null, false));
        }

        public async Task<LicenseStatus> ValidateLicenseAsync(LicenseCheckModel licenseCheck)
        {
            _licenseCheck = new InternalLicenseCheckModel()
            {
                HardwardKey = licenseCheck.HardwardKey,
                Certificate = licenseCheck.Certificate,
                Instance = InstanceFactory.FromCrypto(licenseCheck.InstanceKey)
            };
            var status = LicenseStatus.OK;
            if (!await EnsureLicenseExistsAsync())
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
            var ignored = new string[]
            {
                "CustomerKey", "LicenseKey", "HardwareKey", "ClientKey", "Secret",
                "IsActivated", "OfflineLimit", "LoginCount"
            };
            var json = JsonHelper.From(_license, true, ignored);
            var license = Encoding.UTF8.GetBytes(json);
            return _crypto.SignData(license, _certificate);
        }

        private static string GetNewLicenseSecret()
        {
            var secret = RandomGenerator.Generate(16);
            return Convert.ToBase64String(secret);
        }

        private async Task<bool> EnsureLicenseExistsAsync()
        {
            var instance = _licenseCheck.Instance;
            _license = await _repository.GetLicenseFileDataAsync(
                instance?.LicenseKey, instance?.CustomerKey);
            return _license != null;
        }

        private bool EnsureCertificateExists()
        {
            if (!String.IsNullOrEmpty(_licenseCheck.Certificate))
            {
                var rawData = Convert.FromBase64String(_licenseCheck.Certificate);
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
            return String.Compare(_license.HardwareKey, _licenseCheck.HardwardKey) == 0;
        }

        private bool EnsureLicenseNotExpired()
        {
            var now = DateTime.Now.Date;
            return now >= _license.StartDate
                && now <= _license.EndDate;
        }

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Supports correct implementation of the Disposable pattern for this class.
        /// </summary>
        /// <param name="disposing">Indicates if this instance is currently being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _certificate.Dispose();
                _disposed = true;
            }
        }

        #endregion

        private readonly ILicenseRepository _repository;
        private readonly ICryptoService _crypto;
        private InternalLicenseCheckModel _licenseCheck;
        private LicenseFileModel _license;
        private X509Certificate2 _certificate = new();
        private bool _disposed = false;
    }
}

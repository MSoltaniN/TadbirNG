using System;
using System.Linq;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Persistence;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public class LicenseRepository : ILicenseRepository
    {
        public LicenseRepository(IUnitOfWork unitOfWork, IEncodedSerializer serializer, ICryptoService crypto)
        {
            UnitOfWork = unitOfWork;
            _serializer = serializer;
            _crypto = crypto;
        }

        public void InsertCustomer(CustomerModel customer)
        {
            if (customer.Id == 0)
            {
                var repository = UnitOfWork.GetRepository<CustomerModel>();
                repository.Insert(customer);
                UnitOfWork.Commit();
            }
        }

        public int GetLicenseId(string customerKey, string licenseKey)
        {
            var repository = UnitOfWork.GetRepository<LicenseModel>();
            int licenseId = repository
                .GetEntityQuery()
                .Where(lic => lic.Customer.CustomerKey == customerKey
                    && lic.LicenseKey == licenseKey)
                .Select(lic => lic.Id)
                .FirstOrDefault();
            return licenseId;
        }

        public LicenseModel GetLicense(string licenseKey, string customerKey)
        {
            var license = default(LicenseModel);
            var repository = UnitOfWork.GetRepository<LicenseModel>();
            license = repository
                .GetByCriteria(lic => lic.LicenseKey == licenseKey)
                .SingleOrDefault();

            return license;
        }

        public LicenseModel GetActivatedLicense(ActivationModel activation)
        {
            var license = default(LicenseModel);
            license = GetLicense(activation?.InstanceKey?.LicenseKey, activation?.InstanceKey?.CustomerKey);
            if (license != null)
            {
                var repository = UnitOfWork.GetRepository<LicenseModel>();
                license.HardwareKey = activation.HardwareKey;
                license.ClientKey = activation.ClientKey;
                license.Secret = GetNewLicenseSecret();
                license.IsActivated = true;
                repository.Update(license);
                UnitOfWork.Commit();
            }

            return license;
        }

        public void InsertLicense(LicenseModel license)
        {
            var customerRepository = UnitOfWork.GetRepository<CustomerModel>();
            int customerId = customerRepository
                .GetEntityQuery()
                .Where(cus => cus.CustomerKey == license.CustomerKey)
                .Select(cus => cus.Id)
                .FirstOrDefault();
            if (customerId > 0 && license.Id == 0)
            {
                license.CustomerId = customerId;
                var repository = UnitOfWork.GetRepository<LicenseModel>();
                repository.Insert(license);
                UnitOfWork.Commit();
            }
        }

        public string GetEncryptedLicense(LicenseModel license)
        {
            var base64 = _serializer.Serialize(license);
            return _crypto.Encrypt(base64);
        }

        public bool? GetActivationStatus(string licenseKey)
        {
            var repository = UnitOfWork.GetRepository<LicenseModel>();
            bool? isActivated = repository
                .GetEntityQuery()
                .Where(lic => lic.LicenseKey == licenseKey)
                .Select(lic => lic.IsActivated)
                .FirstOrDefault();
            return isActivated;
        }

        private static string GetNewLicenseSecret()
        {
            var secret = RandomGenerator.Generate(16);
            return Convert.ToBase64String(secret);
        }

        private IUnitOfWork UnitOfWork { get; }

        private readonly IEncodedSerializer _serializer;
        private readonly ICryptoService _crypto;
    }
}

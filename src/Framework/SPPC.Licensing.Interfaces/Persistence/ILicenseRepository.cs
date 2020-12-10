using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public interface ILicenseRepository
    {
        void InsertCustomer(CustomerModel customer);

        int GetLicenseId(string customerKey, string licenseKey);

        LicenseModel GetLicense(string licenseKey, string customerKey);

        LicenseModel GetActivatedLicense(ActivationModel activation);

        void InsertLicense(LicenseModel license);

        string GetEncryptedLicense(LicenseModel license);

        bool? GetActivationStatus(string licenseKey);
    }
}

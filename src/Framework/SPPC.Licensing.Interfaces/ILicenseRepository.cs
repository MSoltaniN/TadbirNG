using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Interfaces
{
    public interface ILicenseRepository
    {
        int InsertCustomer(CustomerModel customer);

        int GetLicenseId(string customerKey, string licenseKey);

        LicenseModel GetLicense(string licenseKey, string customerKey);

        LicenseModel GetActivatedLicense(ActivationModel activation);

        int InsertLicense(LicenseModel license);

        string GetEncryptedLicense(LicenseModel license);

        bool? GetActivationStatus(string licenseKey);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public interface ILicenseRepository
    {
        int GetLicenseId(string customerKey, string licenseKey);

        LicenseModel GetLicense(string licenseKey, string customerKey);

        LicenseModel GetActivatedLicense(InternalActivationModel activation);

        Task InsertLicenseAsync(LicenseModel license);

        Task UpdateLicenseAsync(LicenseModel license);

        string GetEncryptedLicense(LicenseModel license);

        bool? GetActivationStatus(string licenseKey);
    }
}

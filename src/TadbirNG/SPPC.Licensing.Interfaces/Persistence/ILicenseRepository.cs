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

        Task<LicenseModel> GetLicenseAsync(int licenseId);

        Task<LicenseViewModel> GetLicenseAsync(string licenseKey);

        Task<LicenseFileModel> GetLicenseFileDataAsync(string licenseKey, string customerKey);

        Task<IList<LicenseModel>> GetLicensesAsync(int? customerId = null);

        Task SaveLicenseAsync(LicenseModel license);

        bool? GetActivationStatus(string licenseKey);
    }
}

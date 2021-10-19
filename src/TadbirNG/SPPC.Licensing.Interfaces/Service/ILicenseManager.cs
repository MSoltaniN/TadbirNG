using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Service
{
    public interface ILicenseManager
    {
        Task<string> ActivateLicenseAsync(ActivationModel activation);

        Task<LicenseStatus> ValidateLicenseAsync(LicenseCheckModel licenseCheck);

        string GetActiveLicense();
    }
}

using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Tadbir.Licensing
{
    public interface ILicenseUtility
    {
        string LicensePath { get; set; }

        InstanceModel Instance { get; set; }

        LicenseStatus ValidateLicense();

        LicenseStatus QuickValidateLicense();

        bool ValidateSignature(string apiLicense, string signature);

        string GetActiveLicense();

        LicenseModel LoadLicense(string licenseData);
    }
}

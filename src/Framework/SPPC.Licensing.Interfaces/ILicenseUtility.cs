using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public interface ILicenseUtility
    {
        LicenseCheckModel LicenseCheck { get; set; }

        LicenseStatus ValidateLicense();

        string GetActiveLicense();
    }
}

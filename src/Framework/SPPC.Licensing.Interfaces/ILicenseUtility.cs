using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Interfaces
{
    public interface ILicenseUtility
    {
        LicenseCheckModel LicenseCheck { get; set; }

        LicenseStatus ValidateLicense();

        string GetActiveLicense();
    }
}

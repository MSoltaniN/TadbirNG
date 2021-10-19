using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Service
{
    public interface ILicenseService
    {
        IList<LicenseModel> GetLicenses(int? customerId = null);

        string InsertLicense(LicenseModel license);

        string UpdateLicense(LicenseModel license);

        string DeleteLicense(int licenseId);
    }
}

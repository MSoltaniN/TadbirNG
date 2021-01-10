using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Service
{
    public interface ILicenseService
    {
        string InsertCustomer(CustomerModel customer);

        string InsertLicense(LicenseModel license);

        string GetActivatedLicense(ActivationModel activation);

        string GetLicense(string licenseCheck);
    }
}

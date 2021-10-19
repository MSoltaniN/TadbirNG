using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Service
{
    public class LicenseService : ILicenseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseService"/> class
        /// </summary>
        /// <param name="apiClient">Object that wraps common operations for calling a Web API service</param>
        public LicenseService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IList<LicenseModel> GetLicenses(int? customerId = null)
        {
            string url = customerId.HasValue
                ? String.Format(LicenseApi.LicensesByCustomer, customerId.Value)
                : LicenseApi.Licenses;
            var licenses = _apiClient.Get<IList<LicenseModel>>(url);
            return licenses;
        }

        public string InsertLicense(LicenseModel license)
        {
            string error = String.Empty;
            Verify.ArgumentNotNull(license, nameof(license));
            var response = _apiClient.Insert(license, LicenseApi.Licenses);
            if (!response.Succeeded)
            {
                error = response.Message;
            }

            return error;
        }

        public string UpdateLicense(LicenseModel license)
        {
            string error = String.Empty;
            Verify.ArgumentNotNull(license, nameof(license));
            var response = _apiClient.Update(license, LicenseApi.License, license.Id);
            if (!response.Succeeded)
            {
                error = response.Message;
            }

            return error;
        }

        public string DeleteLicense(int licenseId)
        {
            throw new NotImplementedException();
        }

        private readonly IApiClient _apiClient;
    }
}

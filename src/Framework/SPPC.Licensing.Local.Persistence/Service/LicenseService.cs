using System;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Api;

namespace SPPC.Licensing.Service
{
    /// <summary>
    ///
    /// </summary>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="activation"></param>
        /// <returns></returns>
        public string GetActivatedLicense(ActivationModel activation)
        {
            Verify.ArgumentNotNull(activation, nameof(activation));
            return _apiClient.Update<ActivationModel, string>(activation, LicenseApi.ActivateLicense);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="licenseCheck"></param>
        /// <returns></returns>
        public string GetLicense(string licenseCheck)
        {
            Verify.ArgumentNotNullOrEmptyString(licenseCheck, nameof(licenseCheck));
            _apiClient.AddHeader(Constants.LicenseCheckHeaderName, licenseCheck);
            return _apiClient.Get<string>(LicenseApi.License);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public string InsertCustomer(CustomerModel customer)
        {
            string error = String.Empty;
            Verify.ArgumentNotNull(customer, nameof(customer));
            var response = _apiClient.Insert(customer, CustomerApi.Customers);
            if (!response.Succeeded)
            {
                error = response.Message;
            }

            return error;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
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

        private readonly IApiClient _apiClient;
    }
}

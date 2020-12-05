using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Service;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Licensing;

namespace SPPC.Licensing.Local.Web.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        public LicenseController(IHostingEnvironment host, IEncodedSerializer serializer,
            ILicenseUtility utility, IApiClient apiClient)
        {
            _host = host;
            _serializer = serializer;
            _utility = utility;
            _apiClient = apiClient;

            _utility.LicensePath = Path.Combine(_host.WebRootPath, Constants.LicenseFile);
            _apiClient.ServiceRoot = _serverRoot;
        }

        // GET: api/license
        [HttpGet]
        [Route(LicenseApi.LicenseUrl)]
        public IActionResult GetAppLicense()
        {
            _utility.Instance = GetInstance();
            var result = GetValidationResult(_utility.Instance, out bool succeeded);
            if (!succeeded)
            {
                return result;
            }

            SetLicense();
            return Ok();
        }

        // GET: api/license/online
        [HttpGet]
        [Route(LicenseApi.OnlineLicenseUrl)]
        public IActionResult GetOnlineAppLicense()
        {
            _utility.Instance = GetInstance();
            var result = GetQuickValidationResult(_utility.Instance, out bool succeeded);
            if (!succeeded)
            {
                return result;
            }

            var licenseCheck = GetLicenseCheck(_utility.Instance);
            _apiClient.AddHeader(Constants.LicenseCheckHeaderName, GetLicenseCheckData(licenseCheck));
            var signature = _apiClient.Get<string>(LicenseApi.License);
            Response.Headers.Add(Constants.LicenseHeaderName, signature);
            return Ok();
        }

        private string GetLicenseCheckData(LicenseCheckModel licenseCheck)
        {
            return _serializer.Serialize(licenseCheck);
        }

        private IActionResult GetValidationResult(InstanceModel instance, out bool succeeded)
        {
            succeeded = false;
            if (instance == null)
            {
                return BadRequest();
            }

            var status = _utility.ValidateLicense();
            if (status == LicenseStatus.NoLicense
                || status == LicenseStatus.Corrupt)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else if (status == LicenseStatus.NoCertificate
                || status == LicenseStatus.BadCertificate
                || status == LicenseStatus.HardwareMismatch)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            else if (status == LicenseStatus.InstanceMismatch
                || status == LicenseStatus.Expired)
            {
                return Unauthorized();
            }

            succeeded = true;
            return Ok();
        }

        private IActionResult GetQuickValidationResult(InstanceModel instance, out bool succeeded)
        {
            succeeded = false;
            if (instance == null)
            {
                return BadRequest();
            }

            var status = _utility.QuickValidateLicense();
            if (status == LicenseStatus.NoLicense
                || status == LicenseStatus.Corrupt)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else if (status == LicenseStatus.NoCertificate)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            succeeded = true;
            return Ok();
        }

        private void SetLicense()
        {
            string signature = _utility.GetActiveLicense();
            Response.Headers.Add(Constants.LicenseHeaderName, signature);
        }

        private LicenseCheckModel GetLicenseCheck(InstanceModel instance)
        {
            string licensePath = Path.Combine(_host.WebRootPath, Constants.LicenseFile);
            string licenseData = System.IO.File.ReadAllText(licensePath);
            var license = _utility.LoadLicense(licenseData);

            string certificatePath = Path.Combine(_host.WebRootPath, Constants.CertificateFile);
            var certificate = System.IO.File.ReadAllBytes(certificatePath);
            return new LicenseCheckModel()
            {
                HardwardKey = license.HardwareKey,
                InstanceKey = instance,
                Certificate = Convert.ToBase64String(certificate)
            };
        }

        private InstanceModel GetInstance()
        {
            var instance = default(InstanceModel);
            var header = Request.Headers[Constants.InstanceHeaderName];
            if (!String.IsNullOrEmpty(header))
            {
                instance = _serializer.Deserialize<InstanceModel>(header);
            }

            return instance;
        }

        private readonly IHostingEnvironment _host;
        private readonly IEncodedSerializer _serializer;
        private readonly ILicenseUtility _utility;
        private readonly IApiClient _apiClient;
        private readonly string _serverRoot = "http://localhost:1447";
    }
}
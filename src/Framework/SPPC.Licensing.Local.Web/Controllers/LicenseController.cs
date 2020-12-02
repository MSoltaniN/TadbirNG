using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Service;
using SPPC.Licensing.Local.Persistence;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Local.Web.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        public LicenseController(IHostingEnvironment host)
        {
            _host = host;
        }

        // GET: api/license
        [HttpGet]
        [Route("license")]
        public IActionResult GetAppLicense()
        {
            var instance = GetInstance();
            string licensePath = Path.Combine(_host.WebRootPath, Constants.LicenseFile);
            _utility = new LicenseUtility(licensePath, instance);
            var result = GetValidationResult(instance, out bool succeeded);
            if (!succeeded)
            {
                return result;
            }

            SetLicense();
            return Ok();
        }

        // GET: api/license/online
        [HttpGet]
        [Route("license/online")]
        public IActionResult GetOnlineAppLicense()
        {
            var instance = GetInstance();
            string licensePath = Path.Combine(_host.WebRootPath, Constants.LicenseFile);
            _utility = new LicenseUtility(licensePath, instance);
            var result = GetQuickValidationResult(instance, out bool succeeded);
            if (!succeeded)
            {
                return result;
            }

            var licenseCheck = GetLicenseCheck(instance);
            var serviceClient = new ServiceClient(_serverRoot);
            serviceClient.AddHeader(Constants.LicenseCheckHeaderName, GetLicenseCheckData(licenseCheck));
            var signature = serviceClient.Get<string>("license");
            Response.Headers.Add(Constants.LicenseHeaderName, signature);
            return Ok();
        }

        private static string GetLicenseCheckData(LicenseCheckModel licenseCheck)
        {
            var serializer = new JsonSerializer();
            return serializer.Serialize(licenseCheck);
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
                var serializer = new JsonSerializer();
                instance = serializer.Deserialize<InstanceModel>(header);
            }

            return instance;
        }

        private readonly IHostingEnvironment _host;
        private readonly string _serverRoot = "http://localhost:1447";
        private LicenseUtility _utility;
    }
}
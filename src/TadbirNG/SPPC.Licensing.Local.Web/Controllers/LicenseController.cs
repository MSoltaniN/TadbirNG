using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Resources;

namespace SPPC.Licensing.Local.Web.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        public LicenseController(IWebHostEnvironment host, IConfiguration configuration,
            ILicenseUtility utility)
        {
            _webRoot = host.WebRootPath;
            _config = configuration;
            _utility = utility;
            _utility.LicensePath = Path.Combine(_webRoot, Constants.LicenseFile);
        }

        // GET: api/license
        [HttpGet]
        [Route(LicenseApi.LicenseUrl)]
        public IActionResult GetAppLicense()
        {
            try
            {
                string instance = GetInstance();
                var result = GetValidationResult(instance, out bool succeeded);
                if (!succeeded)
                {
                    return result;
                }

                var license = _utility.GetLicense();
                return Ok(license);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        // GET: api/license/online
        [HttpGet]
        [Route(LicenseApi.OnlineLicenseUrl)]
        public IActionResult GetOnlineAppLicense()
        {
            try
            {
                string instance = GetInstance();
                var result = GetQuickValidationResult(instance, out bool succeeded);
                if (!succeeded)
                {
                    return result;
                }

                var license = _utility.GetOnlineLicense(instance, GetRemoteConnection());
                if (!String.IsNullOrEmpty(license))
                {
                    return Ok(license);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, AppStrings.InvalidOrExpiredLicense);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        // PUT: api/license/activate
        [HttpPut]
        [Route(LicenseApi.ActivateLicenseUrl)]
        public IActionResult PutLicenseAsActivated()
        {
            IActionResult result;
            var activationResult = _utility.ActivateLicense(GetInstance(), GetRemoteConnection());
            if (activationResult == ActivationResult.Failed)
            {
                result = StatusCode(500, "Error occured during activation.");
            }
            else if (activationResult == ActivationResult.AlreadyActivated)
            {
                result = Ok("Already activated.");
            }
            else if (activationResult == ActivationResult.BadInstance)
            {
                result = StatusCode(403, "Given license cannot be activated.");
            }
            else
            {
                result = Ok();
            }

            return result;
        }

        // PUT: api/license/validate
        [HttpPut]
        [Route(LicenseApi.ValidateLicenseUrl)]
        public IActionResult PutLicenseValidation([FromBody] string license)
        {
            string signature = GetLicense();
            if (license == null || String.IsNullOrEmpty(signature))
            {
                return BadRequest();
            }

            bool validated = _utility.ValidateSignature(license, signature);
            return Ok(validated);
        }

        private IActionResult GetValidationResult(string instance, out bool succeeded)
        {
            succeeded = false;
            if (String.IsNullOrEmpty(instance))
            {
                return BadRequest();
            }

            var status = _utility.ValidateLicense(instance, GetRemoteConnection());
            if (status != LicenseStatus.OK)
            {
                return StatusCode(StatusCodes.Status403Forbidden, AppStrings.InvalidOrExpiredLicense);
            }

            succeeded = true;
            return Ok();
        }

        private IActionResult GetQuickValidationResult(string instance, out bool succeeded)
        {
            succeeded = false;
            if (String.IsNullOrEmpty(instance))
            {
                return BadRequest();
            }

            var status = _utility.QuickValidateLicense(instance);
            if (status != LicenseStatus.OK)
            {
                return StatusCode(StatusCodes.Status403Forbidden, AppStrings.InvalidOrExpiredLicense);
            }

            succeeded = true;
            return Ok();
        }

        private string GetInstance()
        {
            return Request.Headers[Constants.InstanceHeaderName];
        }

        private string GetLicense()
        {
            return Request.Headers[AppConstants.LicenseHeaderName];
        }

        private RemoteConnection GetRemoteConnection()
        {
            return new RemoteConnection()
            {
                Domain = _config["SSH:Domain"],
                Port = Int32.Parse(_config["SSH:Port"]),
                User = _config["SSH:User"],
                Password = _config["SSH:Password"]
            };
        }

        private readonly string _webRoot;
        private readonly IConfiguration _config;
        private readonly ILicenseUtility _utility;
    }
}
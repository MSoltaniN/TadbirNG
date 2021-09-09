﻿using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Licensing.Local.Persistence;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Resources;

namespace SPPC.Licensing.Local.Web.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        public LicenseController(IHostingEnvironment host, ILicenseUtility utility)
        {
            _webRoot = host.WebRootPath;
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

                var license = _utility.GetActiveLicense();
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

                var licenseCheck = GetLicenseCheck(instance);
                var license = _utility.GetLicense(licenseCheck);

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

            var status = _utility.ValidateLicense(instance);
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

        private LicenseCheckModel GetLicenseCheck(string instance)
        {
            string certificatePath = Path.Combine(_webRoot, Constants.CertificateFile);
            var certificate = System.IO.File.ReadAllBytes(certificatePath);
            return new LicenseCheckModel()
            {
                HardwardKey = HardwareKey.UniqueKey,
                InstanceKey = instance,
                Certificate = Convert.ToBase64String(certificate)
            };
        }

        private string GetInstance()
        {
            return Request.Headers[Constants.InstanceHeaderName];
        }

        private string GetLicense()
        {
            return Request.Headers[AppConstants.LicenseHeaderName];
        }

        private readonly string _webRoot;
        private readonly ILicenseUtility _utility;
    }
}
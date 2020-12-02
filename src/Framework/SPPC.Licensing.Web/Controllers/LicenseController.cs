using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Cryptography;
using SPPC.Licensing.Interfaces;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Web.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        public LicenseController(ILicenseRepository repository, ILicenseUtility utility,
            IEncodedSerializer serializer)
        {
            _repository = repository;
            _utility = utility;
            _serializer = serializer;
        }

        // GET: api/license
        [HttpGet]
        [Route("license")]
        public IActionResult GetAppLicense()
        {
            _utility.LicenseCheck = GetLicenseCheckData();
            var result = GetValidationResult(_utility.LicenseCheck, out bool succeeded);
            if (!succeeded)
            {
                return result;
            }

            string signature = _utility.GetActiveLicense();
            return Ok(signature);
        }

        // PUT: api/license/activate
        [HttpPut]
        [Route("license/activate")]
        public IActionResult PutLicenseAsActivated([FromBody] ActivationModel activation)
        {
            if (activation == null)
            {
                return BadRequest("Activation data is missing.");
            }

            if (!EnsureValidRequest(activation))
            {
                return BadRequest("Activation failed because license information is invalid.");
            }

            var status = _repository.GetActivationStatus(activation.InstanceKey?.LicenseKey);
            if (status.Value)
            {
                return Ok(String.Empty);
            }

            var activatedLicense = _repository.GetActivatedLicense(activation);
            var encrypted = _repository.GetEncryptedLicense(activatedLicense);
            return Ok(encrypted);
        }

        private bool EnsureValidRequest(ActivationModel activation)
        {
            int licenseId = _repository.GetLicenseId(
                activation.InstanceKey?.CustomerKey, activation.InstanceKey?.LicenseKey);
            return licenseId > 0;
        }

        private LicenseCheckModel GetLicenseCheckData()
        {
            var licenseCheck = default(LicenseCheckModel);
            var header = Request.Headers[Constants.LicenseCheckHeaderName];
            if (!String.IsNullOrEmpty(header))
            {
                _serializer.Deserialize<LicenseCheckModel>(header);
            }

            return licenseCheck;
        }

        private IActionResult GetValidationResult(LicenseCheckModel licenseCheck, out bool succeeded)
        {
            succeeded = false;
            if (licenseCheck == null)
            {
                return BadRequest();
            }

            var status = _utility.ValidateLicense();
            if (status == LicenseStatus.NoLicense)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else if (status == LicenseStatus.NoCertificate
                || status == LicenseStatus.BadCertificate
                || status == LicenseStatus.HardwareMismatch)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            else if (status == LicenseStatus.Expired)
            {
                return Unauthorized();
            }

            succeeded = true;
            return Ok();
        }

        private readonly ILicenseRepository _repository;
        private readonly ILicenseUtility _utility;
        private readonly IEncodedSerializer _serializer;
    }
}
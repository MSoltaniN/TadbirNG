using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;
using SPPC.Licensing.Persistence;
using SPPC.Licensing.Service;

namespace SPPC.Licensing.Web.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        public LicenseController(ILicenseRepository repository, ICryptoService crypto,
            ILicenseManager manager)
        {
            _repository = repository;
            _crypto = crypto;
            _manager = manager;
        }

        // GET: api/license
        [HttpGet]
        [Route(LicenseApi.LicenseUrl)]
        public IActionResult GetAppLicense()
        {
            var licenseCheck = GetLicenseCheckData();
            var result = GetValidationResult(licenseCheck, out bool succeeded);
            if (!succeeded)
            {
                return result;
            }

            string signature = _manager.GetActiveLicense();
            return Ok(signature);
        }

        // GET: api/license/{instanceKey}
        [HttpGet]
        [Route(LicenseApi.LicenseByKeyUrl)]
        public IActionResult GetAppLicenseByInstance(string instanceKey)
        {
            if (String.IsNullOrEmpty(instanceKey))
            {
                return BadRequest("Application instance was not specified.");
            }

            return Ok();
        }

        // PUT: api/license/activate
        [HttpPut]
        [Route(LicenseApi.ActivateLicenseUrl)]
        public async Task<IActionResult> PutLicenseAsActivatedAsync([FromBody] ActivationModel activation)
        {
            if (activation == null)
            {
                return BadRequest("Activation data is missing.");
            }

            string json = _crypto.Decrypt(activation.InstanceKey);
            var internalActivation = new InternalActivationModel()
            {
                Instance = JsonHelper.To<InstanceModel>(json),
                HardwareKey = activation.HardwareKey,
                ClientKey = activation.ClientKey
            };
            if (!EnsureValidRequest(internalActivation))
            {
                return BadRequest("Activation failed because license information is invalid.");
            }

            var status = _repository.GetActivationStatus(internalActivation.Instance?.LicenseKey);
            if (status.Value)
            {
                return Ok(String.Empty);
            }

            var activatedLicense = await _manager.ActivateLicenseAsync(activation);
            return Ok(activatedLicense);
        }

        // POST: api/licenses
        [HttpPost]
        [Route(LicenseApi.LicensesUrl)]
        public async Task<IActionResult> PostNewLicenseAsync([FromBody] LicenseModel license)
        {
            if (license == null)
            {
                return BadRequest("Request failed because license data is missing or malformed.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.InsertLicenseAsync(license);
            return StatusCode(StatusCodes.Status201Created);
        }

        private bool EnsureValidRequest(InternalActivationModel activation)
        {
            int licenseId = _repository.GetLicenseId(
                activation.Instance?.CustomerKey, activation.Instance?.LicenseKey);
            return licenseId > 0;
        }

        private LicenseCheckModel GetLicenseCheckData()
        {
            var licenseCheck = default(LicenseCheckModel);
            var header = Request.Headers[Constants.LicenseCheckHeaderName];
            if (!String.IsNullOrEmpty(header))
            {
                string json = _crypto.Decrypt(header);
                licenseCheck = JsonHelper.To<LicenseCheckModel>(json);
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

            var status = _manager.ValidateLicense(licenseCheck);
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
        private readonly ILicenseManager _manager;
        private readonly ICryptoService _crypto;
    }
}
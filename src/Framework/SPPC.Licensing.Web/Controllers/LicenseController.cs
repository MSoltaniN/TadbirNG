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
        [Route(LicenseApi.LicenseQueryUrl)]
        public async Task<IActionResult> GetAppLicenseAsync()
        {
            var licenseCheck = GetLicenseCheckData();
            var result = await GetValidationResultAsync(licenseCheck);
            if (!(result is OkResult))
            {
                return result;
            }

            string license = _manager.GetActiveLicense();
            return Ok(license);
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

        #region License Management Methods

        // GET: api/licenses
        [HttpGet]
        [Route(LicenseApi.LicensesUrl)]
        public async Task<IActionResult> GetAllLicensesAsync()
        {
            var licenses = await _repository.GetLicensesAsync();
            return Json(licenses);
        }

        // GET: api/licenses/by-customer/{customerId:min(1)}
        [HttpGet]
        [Route(LicenseApi.LicensesByCustomerUrl)]
        public async Task<IActionResult> GetCustomerLicensesAsync(int customerId)
        {
            var licenses = await _repository.GetLicensesAsync(customerId);
            return Json(licenses);
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

            await _repository.SaveLicenseAsync(license);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/licenses/{licenseId:min(1)}
        [HttpPut]
        [Route(LicenseApi.LicenseUrl)]
        public async Task<IActionResult> PutModifiedLicenseAsync(
            int licenseId, [FromBody] LicenseModel license)
        {
            if (license == null)
            {
                return BadRequest("Request failed because license data is missing or malformed.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (license.Id != licenseId)
            {
                return BadRequest("Request failed due to conflicting request data.");
            }

            await _repository.SaveLicenseAsync(license);
            return Ok();
        }

        #endregion

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

        private async Task<IActionResult> GetValidationResultAsync(LicenseCheckModel licenseCheck)
        {
            if (licenseCheck == null)
            {
                return BadRequest();
            }

            var status = await _manager.ValidateLicenseAsync(licenseCheck);
            if (status == LicenseStatus.OK)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            return Ok();
        }

        private readonly ILicenseRepository _repository;
        private readonly ILicenseManager _manager;
        private readonly ICryptoService _crypto;
    }
}
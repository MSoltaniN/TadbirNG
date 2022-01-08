using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Licensing.Local.Web.Controllers
{
    [Produces("application/json")]
    public class LicenseController : Controller
    {
        public LicenseController(IConfiguration configuration, ILicenseUtility utility,
            ISessionRepository repository)
        {
            _config = configuration;
            _utility = utility;
            _repository = repository;
        }

        // GET: api/license/users/{userId:min(1)}
        [HttpGet]
        [Route(LicenseApi.UserLicenseUrl)]
        public async Task<IActionResult> GetAppLicenseAsync(int userId)
        {
            try
            {
                string instance = GetInstance();
                var result = GetValidationResult(instance, out bool succeeded);
                if (!succeeded)
                {
                    return result;
                }

                var license = await _utility.GetLicenseAsync();
                if (!String.IsNullOrEmpty(license))
                {
                    await RegisterUserSessionAsync(userId);
                    result = Ok(license);
                }
                else
                {
                    result = StatusCode(StatusCodes.Status403Forbidden,
                        new ErrorViewModel(ErrorType.RequiresOnlineLicense));
                }

                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        // GET: api/license/users/{userId:min(1)}/online
        [HttpGet]
        [Route(LicenseApi.OnlineUserLicenseUrl)]
        public async Task<IActionResult> GetOnlineAppLicenseAsync(int userId)
        {
            try
            {
                string instance = GetInstance();
                var result = GetQuickValidationResult(instance, out bool succeeded);
                if (!succeeded)
                {
                    return result;
                }

                var license = await _utility.GetOnlineLicenseAsync(instance, GetRemoteConnection());
                if (!String.IsNullOrEmpty(license))
                {
                    await RegisterUserSessionAsync(userId);
                    result = Ok(license);
                }
                else
                {
                    result = StatusCode(StatusCodes.Status403Forbidden,
                        new ErrorViewModel(ErrorType.BadLicense));
                }

                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        // PUT: api/license/activate
        [HttpPut]
        [Route(LicenseApi.ActivateLicenseUrl)]
        public async Task<IActionResult> PutLicenseAsActivatedAsync()
        {
            IActionResult result;
            var activationResult = await _utility.ActivateLicenseAsync(GetInstance(), GetRemoteConnection());
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
        public async Task<IActionResult> PutLicenseValidationAsync([FromBody] string license)
        {
            string signature = GetLicense();
            if (license == null || String.IsNullOrEmpty(signature))
            {
                return BadRequest();
            }

            bool validated = await _utility.ValidateSignatureAsync(license, signature);
            return Ok(validated);
        }

        // PUT: api/sessions/current/active
        [HttpPut]
        [Route(LicenseApi.SetCurrentSessionAsActiveUrl)]
        public async Task<IActionResult> PutCurrentSessionAsActiveAsync()
        {
            string userAgent = Request.Headers["User-Agent"];
            await _repository.UpdateSessionLastActiveAsync(userAgent);
            return Ok();
        }

        // DELETE: api/sessions/current
        [HttpDelete]
        [Route(LicenseApi.CurrentSessionUrl)]
        public async Task<IActionResult> DeleteCurrentSessionAsync()
        {
            string userAgent = Request.Headers["User-Agent"];
            await _repository.DeleteSessionAsync(userAgent);
            return NoContent();
        }

        private IActionResult GetValidationResult(string instance, out bool succeeded)
        {
            succeeded = false;
            if (String.IsNullOrEmpty(instance))
            {
                return BadRequest(new ErrorViewModel(ErrorType.ValidationError));
            }

            var result = GetSessionValidationResult();
            if (!(result is OkResult))
            {
                return result;
            }

            var status = _utility.ValidateLicense(instance, GetRemoteConnection());
            if (status != LicenseStatus.OK)
            {
                var errorType = status == LicenseStatus.NotActivated
                    ? ErrorType.NotActivated
                    : ErrorType.BadLicense;
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorViewModel(errorType));
            }

            succeeded = true;
            return Ok();
        }

        private IActionResult GetQuickValidationResult(string instance, out bool succeeded)
        {
            succeeded = false;
            if (String.IsNullOrEmpty(instance))
            {
                return BadRequest(new ErrorViewModel(ErrorType.ValidationError));
            }

            var result = GetSessionValidationResult();
            if (!(result is OkResult))
            {
                return result;
            }

            var status = _utility.QuickValidateLicense(instance);
            if (status != LicenseStatus.OK)
            {
                var errorType = status == LicenseStatus.NotActivated
                    ? ErrorType.NotActivated
                    : ErrorType.BadLicense;
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorViewModel(errorType));
            }

            succeeded = true;
            return Ok();
        }

        private IActionResult GetSessionValidationResult()
        {
            IActionResult result = Ok();
            var license = _utility.LoadLicenseAsync().Result;
            int sessionCount = _repository.GetSessionCountAsync().Result;
            if (sessionCount >= license.UserCount)
            {
                var errorType = ErrorType.TooManySessions;
                result = StatusCode(StatusCodes.Status403Forbidden, new ErrorViewModel(errorType));
            }

            return result;
        }

        private async Task RegisterUserSessionAsync(int userId)
        {
            string userAgent = Request.Headers["User-Agent"];
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            await _repository.SaveSessionAsync(userAgent, ipAddress, userId);
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

        private readonly IConfiguration _config;
        private readonly ILicenseUtility _utility;
        private readonly ISessionRepository _repository;
    }
}
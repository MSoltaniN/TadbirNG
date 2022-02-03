using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Renci.SshNet.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Auth;
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
        public async Task<IActionResult> GetAppLicenseAsync(int userId, [FromBody] LoginViewModel login)
        {
            IActionResult result;
            try
            {
                await UpdateServerAccountAsync(login);
                string instance = GetInstance();
                result = GetValidationResult(instance, out bool succeeded);
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
            catch (SshAuthenticationException)
            {
                result = StatusCode(StatusCodes.Status403Forbidden,
                    new ErrorViewModel(ErrorType.InvalidUserPass));
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
        public async Task<IActionResult> GetOnlineAppLicenseAsync(int userId, [FromBody] LoginViewModel login)
        {
            IActionResult result;
            try
            {
                await UpdateServerAccountAsync(login);
                string instance = GetInstance();
                result = GetQuickValidationResult(instance, out bool succeeded);
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
            catch (SshAuthenticationException)
            {
                result = StatusCode(StatusCodes.Status403Forbidden,
                    new ErrorViewModel(ErrorType.InvalidUserPass));
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
        public async Task<IActionResult> PutLicenseAsActivatedAsync([FromBody] LoginViewModel login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult result;
            try
            {
                var connection = GetRemoteConnection();
                connection.User = login.UserName;
                connection.Password = login.Password;
                var activationResult = await _utility.ActivateLicenseAsync(GetInstance(), connection);
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
            }
            catch (SshAuthenticationException)
            {
                result = StatusCode(StatusCodes.Status403Forbidden,
                    new ErrorViewModel(ErrorType.InvalidUserPass));
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
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

        // GET: api/sessions
        [HttpGet]
        [Route(LicenseApi.OpenSessionsUrl)]
        public async Task<IActionResult> GetOpenSessionsAsync()
        {
            var sessions = await _repository.GetSessionsAsync();
            return Json(sessions);
        }

        // GET: api/sessions/users/{userId:min(1)}
        [HttpGet]
        [Route(LicenseApi.OpenSessionsByUserUrl)]
        public async Task<IActionResult> GetOpenSessionsByUserAsync(int userId)
        {
            var sessions = await _repository.GetUserSessionsAsync(userId);
            return Json(sessions);
        }

        // GET: api/sessions
        [HttpPut]
        [Route(LicenseApi.OpenSessionsUrl)]
        public async Task<IActionResult> PutExistingSessionsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null || actionDetail.Items == null)
            {
                return BadRequest();
            }

            await _repository.DeleteSessionsAsync(actionDetail.Items);
            return Ok();
        }

        // PUT: api/sessions/current/active
        [HttpPut]
        [Route(LicenseApi.SetCurrentSessionAsActiveUrl)]
        public async Task<IActionResult> PutCurrentSessionAsActiveAsync()
        {
            string userAgent = Request.Headers["User-Agent"];
            await _repository.UpdateSessionLastActiveAsync(userAgent);
            await _repository.CleanupSessionsAsync();
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

            var status = _utility.ValidateLicense(instance, GetRemoteConnection());
            if (status != LicenseStatus.OK)
            {
                var errorType = status == LicenseStatus.NotActivated
                    ? ErrorType.NotActivated
                    : ErrorType.BadLicense;
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorViewModel(errorType));
            }

            var result = GetSessionValidationResult();
            if (!(result is OkResult))
            {
                return result;
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

        private async Task UpdateServerAccountAsync(LoginViewModel login)
        {
            if (login != null)
            {
                var license = await _utility.LoadLicenseAsync();
                license.ServerUser = login.UserName;
                license.ServerPassword = login.Password;
                _utility.SaveLicense(license);
            }
        }

        private RemoteConnection GetRemoteConnection()
        {
            var license = _utility.LoadLicenseAsync().Result;
            var connection = new RemoteConnection()
            {
                Domain = _config["SSH:Domain"],
                Port = Int32.Parse(_config["SSH:Port"])
            };
            if (license != null)
            {
                connection.User = license.ServerUser;
                connection.Password = license.ServerPassword;
            }

            return connection;
        }

        private readonly IConfiguration _config;
        private readonly ILicenseUtility _utility;
        private readonly ISessionRepository _repository;
    }
}
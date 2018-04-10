using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Framework.Service.Security;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class UsersController : Controller
    {
        public UsersController(
            ISecurityRepository repository, ICryptoService crypto, ITextEncoder<SecurityContext> encoder)
        {
            _repository = repository;
            _crypto = crypto;
            _contextEncoder = encoder;
        }

        #region Asynchronous Methods

        // GET: api/users
        [Route(UserApi.UsersUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var gridOptions = GetGridOptions();
            int itemCount = await _repository.GetUserCountAsync(gridOptions);
            SetItemCount(itemCount);
            var users = await _repository.GetUsersAsync();
            return Json(users);
        }

        // GET: api/users/name/{userName}
        [Route(UserApi.UserByNameUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUserByNameAsync(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return NotFound();
            }

            var user = await _repository.GetUserAsync(userName);
            return JsonReadResult(user);
        }

        // GET: api/users/{userId:min(1)}
        [Route(UserApi.UserUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var user = await _repository.GetUserAsync(userId);
            return JsonReadResult(user);
        }

        // GET: api/users/metadata
        [Route(UserApi.UserMetadataUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUserMetadataAsync()
        {
            var metadata = await _repository.GetUserMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/users
        [HttpPost]
        [Route(UserApi.UsersUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Create)]
        public async Task<IActionResult> PostNewUserAsync([FromBody] UserViewModel user)
        {
            var result = await ValidationResultAsync(user);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputUser = await _repository.SaveUserAsync(user);
            return StatusCode(StatusCodes.Status201Created, outputUser);
        }

        // PUT: api/users/{userId:min(1)}
        [HttpPut]
        [Route(UserApi.UserUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Edit)]
        public async Task<IActionResult> PutModifiedUserAsync(int userId, [FromBody] UserViewModel user)
        {
            if (userId == AppConstants.AdminUserId)
            {
                return BadRequest("Could not put modified user because the user is read-only.");
            }

            var result = await ValidationResultAsync(user, userId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (user.Password == AppConstants.DummyPassword)
            {
                user.Password = String.Empty;
            }

            var outputUser = await _repository.SaveUserAsync(user);
            result = (outputUser != null)
                ? Ok(outputUser)
                : NotFound() as IActionResult;
            return result;
        }

        // PUT: api/users/{userId:int}/login
        [HttpPut]
        [Route(UserApi.UserLastLoginUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Edit)]
        public async Task<IActionResult> PutUserLastLoginAsync(int userId)
        {
            await _repository.UpdateUserLastLoginAsync(userId);
            return Ok();
        }

        // PUT: api/users/{userName}/password
        [HttpPut]
        [Route(UserApi.UserPasswordUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Edit)]
        public async Task<IActionResult> PutUserPasswordAsync(string userName, [FromBody] UserProfileViewModel profile)
        {
            if (profile == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.User);
                return BadRequest(message);
            }

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(profile.UserName))
            {
                return BadRequest(Strings.UserNotFound);
            }

            if (String.IsNullOrWhiteSpace(profile.NewPassword) || String.IsNullOrWhiteSpace(profile.RepeatPassword))
            {
                return BadRequest(Strings.MissingNewAndRepeatPasswords);
            }

            if (profile.NewPassword != profile.RepeatPassword)
            {
                return BadRequest(Strings.NewAndRepeatPasswordsDontMatch);
            }

            if (userName != profile.UserName)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, FieldNames.UserName);
                return BadRequest(message);
            }

            //// NOTE: DO NOT check ModelState here, because plain-text passwords are replaced by hash values.

            var user = _repository.GetUser(userName);
            if (user == null)
            {
                return BadRequest(Strings.UserNotFound);
            }

            if (String.Compare(user.Password, profile.OldPassword, true) != 0)
            {
                return BadRequest(Strings.IncorrectOldPassword);
            }

            await _repository.UpdateUserPasswordAsync(profile);
            return Ok();
        }

        // GET: api/users/{userId:min(1)}/context
        [Route(UserApi.UserContextUrl)]
        public async Task<IActionResult> GetUserContextAsync(int userId)
        {
            var userContext = await _repository.GetUserContextAsync(userId);
            return JsonReadResult(userContext);
        }

        // PUT: api/users/login
        [HttpPut]
        [Route(UserApi.UsersLoginStatusUrl)]
        public async Task<IActionResult> PutUsersLoginStatusAsync([FromBody] LoginViewModel login)
        {
            if (login == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.UserAccount);
                return BadRequest(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _repository.GetUserAsync(login.UserName);
            if (user == null)
            {
                return BadRequest(Strings.InvalidUserName);
            }

            if (!user.IsEnabled)
            {
                return BadRequest(Strings.UserIsDisabled);
            }

            if (!CheckPassword(user.Password, login.Password))
            {
                return BadRequest(Strings.InvalidPassword);
            }

            await _repository.UpdateUserLastLoginAsync(user.Id);
            string userTicket = await GetUserTicketAsync(user.Id);
            Response.Headers.Add(AppConstants.ContextHeaderName, userTicket);
            return Ok();
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        // GET: api/users
        [Route(UserApi.UsersSyncUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public IActionResult GetUsers()
        {
            var users = _repository.GetUsers();
            return Json(users);
        }

        // GET: api/users/name/{userName}
        [Route(UserApi.UserByNameSyncUrl)]
        public IActionResult GetUserByName(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return NotFound();
            }

            var user = _repository.GetUser(userName);
            return JsonReadResult(user);
        }

        // GET: api/users/{userId:min(1)}
        [Route(UserApi.UserSyncUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public IActionResult GetUser(int userId)
        {
            var user = _repository.GetUser(userId);
            return JsonReadResult(user);
        }

        // POST: api/users
        [HttpPost]
        [Route(UserApi.UsersSyncUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Create)]
        public IActionResult PostNewUser([FromBody] UserViewModel user)
        {
            var result = ValidationResult(user);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SaveUser(user);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/users/{userId:min(1)}
        [HttpPut]
        [Route(UserApi.UserSyncUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Edit)]
        public IActionResult PutModifiedUser(int userId, [FromBody] UserViewModel user)
        {
            if (userId == AppConstants.AdminUserId)
            {
                return BadRequest("Could not put modified user because the user is read-only.");
            }

            var result = ValidationResult(user, userId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (user.Password == AppConstants.DummyPassword)
            {
                user.Password = String.Empty;
            }

            _repository.SaveUser(user);
            return Ok();
        }

        // PUT: api/users/{userId:int}/login
        [HttpPut]
        [Route(UserApi.UserLastLoginSyncUrl)]
        public IActionResult PutUserLastLogin(int userId)
        {
            _repository.UpdateUserLastLogin(userId);
            return Ok();
        }

        // PUT: api/users/{userName}/password
        [HttpPut]
        [Route(UserApi.UserPasswordSyncUrl)]
        public IActionResult PutUserPassword(string userName, [FromBody] UserProfileViewModel profile)
        {
            if (profile == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.User);
                return BadRequest(message);
            }

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(profile.UserName))
            {
                return BadRequest(Strings.UserNotFound);
            }

            if (String.IsNullOrWhiteSpace(profile.NewPassword) || String.IsNullOrWhiteSpace(profile.RepeatPassword))
            {
                return BadRequest(Strings.MissingNewAndRepeatPasswords);
            }

            if (profile.NewPassword != profile.RepeatPassword)
            {
                return BadRequest(Strings.NewAndRepeatPasswordsDontMatch);
            }

            if (userName != profile.UserName)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, FieldNames.UserName);
                return BadRequest(message);
            }

            //// NOTE: DO NOT check ModelState here, because plain-text passwords are replaced by hash values.

            var user = _repository.GetUser(userName);
            if (user == null)
            {
                return BadRequest(Strings.UserNotFound);
            }

            if (String.Compare(user.Password, profile.OldPassword, true) != 0)
            {
                return BadRequest(Strings.IncorrectOldPassword);
            }

            _repository.UpdateUserPassword(profile);
            return Ok();
        }

        // GET: api/users/{userId:min(1)}/context
        [Route(UserApi.UserContextSyncUrl)]
        public IActionResult GetUserContext(int userId)
        {
            var userContext = _repository.GetUserContext(userId);
            return JsonReadResult(userContext);
        }

        #endregion

        // GET: api/users/{userId:min(1)}/ticket
        [Route("users/{userId:min(1)}/ticket")]
        public IActionResult GetUserTicket(int userId)
        {
#if DEBUG
            string ticket = null;
            var userContext = _repository.GetUserContext(userId);
            if (userContext != null)
            {
                var contextEncoder = new Base64Encoder<SecurityContext>();
                var securityContext = new SecurityContext(userContext);
                ticket = contextEncoder.Encode(securityContext);
            }

            return JsonReadResult(ticket);
#else
            return NotFound();
#endif
        }

        private GridOptions GetGridOptions()
        {
            var options = Request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return null;
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return Framework.Helpers.Json.To<GridOptions>(json);
        }

        private void SetItemCount(int count)
        {
            Response.Headers.Add(AppConstants.TotalCountHeaderName, count.ToString());
        }

        private IActionResult JsonReadResult<TData>(TData data)
        {
            var result = (data != null)
                ? Json(data)
                : NotFound() as IActionResult;

            return result;
        }

        private IActionResult BasicValidationResult(UserViewModel user, int userId)
        {
            if (user == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.User);
                return BadRequest(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userId != user.Id)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, Entities.User);
                return BadRequest(message);
            }

            return Ok();
        }

        private IActionResult ValidationResult(UserViewModel user, int userId = 0)
        {
            var result = BasicValidationResult(user, userId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (_repository.IsDuplicateUser(user))
            {
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.UserName);
                return BadRequest(message);
            }

            return Ok();
        }

        private async Task<IActionResult> ValidationResultAsync(UserViewModel user, int userId = 0)
        {
            var result = BasicValidationResult(user, userId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateUserAsync(user))
            {
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.UserName);
                return BadRequest(message);
            }

            return Ok();
        }

        private bool CheckPassword(string passwordHash, string password)
        {
            byte[] passwordHashBytes = Transform.FromHexString(passwordHash);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            return _crypto.ValidateHash(passwordBytes, passwordHashBytes);
        }

        private async Task<string> GetUserTicketAsync(int userId)
        {
            string ticket = null;
            var userContext = await _repository.GetUserContextAsync(userId);
            if (userContext != null)
            {
                var securityContext = new SecurityContext(userContext);
                ticket = _contextEncoder.Encode(securityContext);
            }

            return ticket;
        }

        private ISecurityRepository _repository;
        private ICryptoService _crypto;
        private ITextEncoder<SecurityContext> _contextEncoder;
    }
}

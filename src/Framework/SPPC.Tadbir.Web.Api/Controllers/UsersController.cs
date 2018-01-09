using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Filters;
using Fwk = SPPC.Framework.Values;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class UsersController : Controller
    {
        public UsersController(ISecurityRepository repository)
        {
            _repository = repository;
        }

        // GET: api/users
        [Route(SecurityApi.UsersUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public IActionResult GetUsers()
        {
            var users = _repository.GetUsers();
            return Json(users);
        }

        // GET: api/users/{userName}
        [Route(SecurityApi.UserByNameUrl)]
        public IActionResult GetUserByName(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return NotFound();
            }

            var user = _repository.GetUser(userName);
            var result = (user != null)
                ? Json(user)
                : NotFound() as IActionResult;
            return result;
        }

        // GET: api/users/{userId:int}
        [Route(SecurityApi.UserUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public IActionResult GetUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            var user = _repository.GetUser(userId);
            var result = (user != null)
                ? Json(user)
                : NotFound() as IActionResult;

            return result;
        }

        // POST: api/users
        [HttpPost]
        [Route(SecurityApi.UsersUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Create)]
        public IActionResult PostNewUser([FromBody] UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest("Could not post new user because a 'null' value was provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repository.IsDuplicateUser(user))
            {
                var message = String.Format(Fwk.ValidationMessages.DuplicateFieldValue, FieldNames.UserName);
                return BadRequest(message);
            }

            _repository.SaveUser(user);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/users/{userId:int}
        [HttpPut]
        [Route(SecurityApi.UserUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Edit)]
        public IActionResult PutModifiedUser(int userId, [FromBody] UserViewModel user)
        {
            if (userId == Constants.AdminUserId)
            {
                return BadRequest("Could not put modified user because the user is read-only.");
            }

            if (user == null)
            {
                return BadRequest("Could not put modified user because a 'null' value was provided.");
            }

            if (userId <= 0 || user.Id <= 0)
            {
                return BadRequest("Could not put modified user because original user does not exist.");
            }

            if (userId != user.Id)
            {
                return BadRequest("Could not put modified user because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repository.IsDuplicateUser(user))
            {
                var message = String.Format(Fwk.ValidationMessages.DuplicateFieldValue, FieldNames.UserName);
                return BadRequest(message);
            }

            if (user.Password == Constants.DummyPassword)
            {
                user.Password = String.Empty;
            }

            _repository.SaveUser(user);
            return Ok();
        }

        // PUT: api/users/{userId:int}/login
        [HttpPut]
        [Route(SecurityApi.UserLastLoginUrl)]
        public IActionResult PutUserLastLogin(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Cannot put user last login because specified user does not exist.");
            }

            _repository.UpdateUserLastLogin(userId);
            return Ok();
        }

        // PUT: api/users/{userName}/password
        [HttpPut]
        [Route(SecurityApi.UserPasswordUrl)]
        public IActionResult PutUserPassword(string userName, [FromBody] UserProfileViewModel profile)
        {
            if (profile == null)
            {
                return BadRequest("Could not put user password because a 'null' value was provided.");
            }

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(profile.UserName))
            {
                return BadRequest("Could not put user password because user does not exist.");
            }

            if (userName != profile.UserName)
            {
                return BadRequest("Could not put user password because of user name conflict in the request.");
            }

            //// NOTE: DO NOT check ModelState here, because plain-text passwords are replaced by hash values.

            var user = _repository.GetUser(userName);
            if (user == null)
            {
                return BadRequest("Could not put user password because user does not exist.");
            }

            if (String.Compare(user.Password, profile.OldPassword, true) != 0)
            {
                return BadRequest(Strings.IncorrectOldPassword);
            }

            _repository.UpdateUserPassword(profile);
            return Ok();
        }

        // GET: api/users/{userId:int}/context
        [Route(SecurityApi.UserContextUrl)]
        public IActionResult GetUserContext(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            var userContext = _repository.GetUserContext(userId);
            var result = (userContext != null)
                ? Json(userContext)
                : NotFound() as IActionResult;
            return result;
        }

        // GET: api/users/{userId:int}/ticket
        [Route("users/{userId:int}/ticket")]
        public IActionResult GetUserTicket(int userId)
        {
#if DEBUG
            if (userId <= 0)
            {
                return NotFound();
            }

            string ticket = String.Empty;
            var userContext = _repository.GetUserContext(userId);
            if (userContext != null)
            {
                var contextEncoder = new Base64Encoder<SecurityContext>();
                var securityContext = new SecurityContext(userContext);
                ticket = contextEncoder.Encode(securityContext);
            }

            var result = (!String.IsNullOrEmpty(ticket))
                ? Json(ticket)
                : NotFound() as IActionResult;
            return result;
#else
            return NotFound();
#endif
        }

        private ISecurityRepository _repository;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class UsersController : ApiController
    {
        public UsersController(ISecurityRepository repository)
        {
            _repository = repository;
        }

        // GET: api/users
        [Route(SecurityApi.UsersUrl)]
        public IHttpActionResult GetUsers()
        {
            var users = _repository.GetUsers();
            return Json(users);
        }

        // GET: api/users/{userName}
        [Route(SecurityApi.UserByNameUrl)]
        public IHttpActionResult GetUserByName(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return NotFound();
            }

            var user = _repository.GetUser(userName);
            var result = (user != null)
                ? Json(user)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // GET: api/users/{userId:int}
        [Route(SecurityApi.UserUrl)]
        public IHttpActionResult GetUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            var user = _repository.GetUser(userId);
            var result = (user != null)
                ? Json(user)
                : NotFound() as IHttpActionResult;

            return result;
        }

        // POST: api/users
        [Route(SecurityApi.UsersUrl)]
        public IHttpActionResult PostNewUser([FromBody] UserViewModel user)
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
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.UserName);
                return BadRequest(message);
            }

            _repository.SaveUser(user);
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/users/{userId:int}
        [Route(SecurityApi.UserUrl)]
        public IHttpActionResult PutModifiedUser(int userId, [FromBody] UserViewModel user)
        {
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
                var message = String.Format(ValidationMessages.DuplicateFieldValue, FieldNames.UserName);
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
        [Route(SecurityApi.UserLastLoginUrl)]
        public IHttpActionResult PutUserLastLogin(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Cannot put user last login because specified user does not exist.");
            }

            _repository.UpdateUserLastLogin(userId);
            return Ok();
        }

        private ISecurityRepository _repository;
    }
}

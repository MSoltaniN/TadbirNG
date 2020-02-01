using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class UsersController : ValidatingController<UserViewModel>
    {
        public UsersController(
            IUserRepository repository,
            ICryptoService crypto,
            ITextEncoder<SecurityContext> encoder,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _crypto = crypto;
            _contextEncoder = encoder;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.User; }
        }

        // GET: api/users
        [Route(UserApi.UsersUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUsersAsync()
        {
            int itemCount = await _repository.GetUserCountAsync(GridOptions);
            SetItemCount(itemCount);
            var users = await _repository.GetUsersAsync(GridOptions);
            SetRowNumbers(users);
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
            user.Password = AppConstants.DummyPassword;
            return JsonReadResult(user);
        }

        // GET: api/users/current/commands
        [Route(UserApi.CurrentUserCommandsUrl)]
        public async Task<IActionResult> GetCurrentUserCommandsAsync()
        {
            var commands = await _repository.GetUserCommandsAsync(SecurityContext.User.Id);
            Array.ForEach(commands.ToArray(), cmd =>
            {
                cmd.Title = _strings[cmd.Title];
                Array.ForEach(cmd.Children.ToArray(), child =>
                {
                    child.Title = _strings[child.Title];
                    Array.ForEach(child.Children.ToArray(), grandChild =>
                    {
                        grandChild.Title = _strings[grandChild.Title];
                        Array.ForEach(grandChild.Children.ToArray(),
                            grGrandChild => grGrandChild.Title = _strings[grGrandChild.Title]);
                    });
                });
            });
            return JsonReadResult(commands);
        }

        // GET: api/users/default/commands
        [Route(UserApi.UserDefaultCommandsUrl)]
        public async Task<IActionResult> GetUserDefaultCommandsAsync()
        {
            var commands = await _repository.GetUserCommandsAsync();
            Array.ForEach(commands.ToArray(), cmd => cmd.Title = _strings[cmd.Title]);
            return JsonReadResult(commands);
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
                return BadRequest(_strings.Format(AppStrings.AdminUserIsReadOnly));
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.User));
            }

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(profile.UserName))
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.User));
            }

            if (String.IsNullOrWhiteSpace(profile.NewPassword) || String.IsNullOrWhiteSpace(profile.RepeatPassword))
            {
                return BadRequest(_strings.Format(AppStrings.MissingNewAndRepeatPasswords));
            }

            if (profile.NewPassword != profile.RepeatPassword)
            {
                return BadRequest(_strings.Format(AppStrings.NewAndRepeatPasswordsDontMatch));
            }

            if (userName != profile.UserName)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.UserName));
            }

            //// NOTE: DO NOT check ModelState here, because plain-text passwords are replaced by hash values.

            var user = await _repository.GetUserAsync(userName);
            if (user == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.User));
            }

            if (!CheckPassword(user.Password, profile.OldPassword))
            {
                return BadRequest(_strings.Format(AppStrings.IncorrectOldPassword));
            }

            profile.NewPassword = _crypto.CreateHash(profile.NewPassword);
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
        [Route(UserApi.UserLoginStatusUrl)]
        public async Task<IActionResult> PutUserLoginStatusAsync([FromBody] LoginViewModel login)
        {
            if (login == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserAccount));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _repository.GetUserAsync(login.UserName);
            if (user == null)
            {
                return BadRequest(_strings.Format(AppStrings.InvalidUserName));
            }

            if (!user.IsEnabled)
            {
                return BadRequest(_strings.Format(AppStrings.UserIsDisabled));
            }

            if (!CheckPassword(user.Password, login.Password))
            {
                return BadRequest(_strings.Format(AppStrings.InvalidPassword));
            }

            await _repository.UpdateUserLastLoginAsync(user.Id);
            string userTicket = await GetUserTicketAsync(user.Id);
            Response.Headers.Add(AppConstants.ContextHeaderName, userTicket);
            return Ok();
        }

        // PUT: api/users/login/company
        [HttpPut]
        [Route(UserApi.UserCompanyLoginStatusUrl)]
        public async Task<IActionResult> PutUserCompanyLoginStatusAsync([FromBody] CompanyLoginViewModel companyLogin)
        {
            if (companyLogin == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.CompanyLogin));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userContext = SecurityContext.User;
            await _repository.UpdateUserCompanyLoginAsync(companyLogin, userContext);
            userContext.Connection = _crypto.Encrypt(userContext.Connection);
            Response.Headers[AppConstants.ContextHeaderName] = GetEncodedTicket(userContext);
            return Ok(userContext);
        }

        // PUT: api/users/current/env
        [HttpPut]
        [Route(UserApi.CurrentUserEnvironmentUrl)]
        public async Task<IActionResult> PutCurrentUserEnvironmentAsync([FromBody] CompanyLoginViewModel env)
        {
            if (env == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.AppEnvironment));
            }

            var userContext = SecurityContext.User;
            await _repository.UpdateUserEnvironmentAsync(env, userContext);
            Response.Headers[AppConstants.ContextHeaderName] = GetEncodedTicket(userContext);
            return Ok(userContext);
        }

        // GET: api/users/{userId:min(1)}/roles
        [Route(UserApi.UserRolesUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUserRolesAsync(int userId)
        {
            var roles = await _repository.GetUserRolesAsync(userId);
            Localize(roles);
            return JsonReadResult(roles);
        }

        // PUT: api/users/{userId:min(1)}/roles
        [HttpPut]
        [Route(UserApi.UserRolesUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.AssignRoles)]
        public async Task<IActionResult> PutModifiedUserRolesAsync(
            int userId, [FromBody] RelatedItemsViewModel userRoles)
        {
            var result = BasicValidationResult(userRoles, userId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveUserRolesAsync(userRoles);
            return Ok();
        }

        // GET: api/users/{userId:min(1)}/ticket
        [Route("users/{userId:min(1)}/ticket")]
        public IActionResult GetUserTicket(int userId)
        {
#if DEBUG
            string ticket = null;
            var userContext = _repository.GetUserContextAsync(userId).Result;
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

        private async Task<IActionResult> ValidationResultAsync(UserViewModel user, int userId = 0)
        {
            var result = BasicValidationResult(user, userId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateUserAsync(user))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.UserName));
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
                ticket = GetEncodedTicket(userContext);
            }

            return ticket;
        }

        private string GetEncodedTicket(UserContextViewModel userContext)
        {
            var securityContext = new SecurityContext(userContext);
            return _contextEncoder.Encode(securityContext);
        }

        private void Localize(RelatedItemsViewModel roles)
        {
            Array.ForEach(roles.RelatedItems.ToArray(), item => item.Name = _strings[item.Name]);
        }

        private IUserRepository _repository;
        private ICryptoService _crypto;
        private ITextEncoder<SecurityContext> _contextEncoder;
    }
}

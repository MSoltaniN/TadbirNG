﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class UsersController : ValidatingController<UserViewModel>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="system"></param>
        public UsersController(IUserRepository repository, ISystemTools system)
            : base(system.Strings, system.TokenManager)
        {
            _repository = repository;
            _crypto = system.Crypto;
            _commandFilter = system.CommandFilter;
            _tokenManager = system.TokenManager;
            _systemTools = system;
        }

        /// <summary>
        ///
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.User; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/users
        [HttpGet]
        [Route(UserApi.UsersUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync(GridOptions);
            return JsonListResult(users);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        // GET: api/users/name/{userName}
        [HttpGet]
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/users/{userId:min(1)}
        [HttpGet]
        [Route(UserApi.UserUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var user = await _repository.GetUserAsync(userId);
            user.Password = AppConstants.DummyPassword;
            return JsonReadResult(user);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/users/current/commands
        [HttpGet]
        [Route(UserApi.CurrentUserCommandsUrl)]
        public async Task<IActionResult> GetCurrentUserCommandsAsync()
        {
            var commands = await _repository.GetUserCommandsAsync(SecurityContext.User.Id);
            _commandFilter.FilterCommands(commands, EditionLimit.RowPermissionAccess);
            Array.ForEach(commands.ToArray(), parent =>
            {
                parent.Title = _strings[parent.Title];
                Array.ForEach(parent.Children.ToArray(), child =>
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

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/users/current/hotkeys
        [HttpGet]
        [Route(UserApi.CurrentUserHotkeysUrl)]
        public async Task<IActionResult> GetCurrentUserHotKeysAsync()
        {
            var shortcuts = await _repository.GetUserHotKeysAsync(SecurityContext.User.Id);
            return Json(shortcuts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/users/default/commands
        [HttpGet]
        [Route(UserApi.UserDefaultCommandsUrl)]
        public async Task<IActionResult> GetUserDefaultCommandsAsync()
        {
            var commands = await _repository.GetUserCommandsAsync();
            Array.ForEach(commands.ToArray(), cmd => cmd.Title = _strings[cmd.Title]);
            return JsonReadResult(commands);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // PUT: api/users/{userId:min(1)}
        [HttpPut]
        [Route(UserApi.UserUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Edit)]
        public async Task<IActionResult> PutModifiedUserAsync(int userId, [FromBody] UserViewModel user)
        {
            if (userId == AppConstants.AdminUserId)
            {
                return BadRequestResult(_strings.Format(AppStrings.AdminUserIsReadOnly));
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // PUT: api/users/{userId:int}/login
        [HttpPut]
        [Route(UserApi.UserLastLoginUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.Edit)]
        public async Task<IActionResult> PutUserLastLoginAsync(int userId)
        {
            await _repository.UpdateUserLastLoginAsync(userId);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        // PUT: api/users/{userName}/password
        [HttpPut]
        [Route(UserApi.UserPasswordUrl)]
        public async Task<IActionResult> PutUserPasswordAsync(string userName, [FromBody] UserProfileViewModel profile)
        {
            if (profile == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.User));
            }

            if (userName != profile.UserName)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.UserName));
            }

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(profile.UserName))
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemNotFound, AppStrings.User));
            }

            if (String.IsNullOrWhiteSpace(profile.NewPassword) || String.IsNullOrWhiteSpace(profile.RepeatPassword))
            {
                return BadRequestResult(_strings.Format(AppStrings.MissingNewAndRepeatPasswords));
            }

            if (profile.NewPassword != profile.RepeatPassword)
            {
                return BadRequestResult(_strings.Format(AppStrings.NewAndRepeatPasswordsDontMatch));
            }

            //// NOTE: DO NOT check ModelState here, because plain-text passwords are replaced by hash values.

            var user = await _repository.GetUserAsync(userName);
            if (user == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemNotFound, AppStrings.User));
            }

            if (!_crypto.ValidateHash(profile.OldPassword, user.Password))
            {
                return BadRequestResult(_strings.Format(AppStrings.IncorrectOldPassword));
            }

            profile.NewPassword = _crypto.CreateHash(profile.NewPassword);
            await _repository.UpdateUserPasswordAsync(profile);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/users/{userId:min(1)}/context
        [HttpGet]
        [Route(UserApi.UserContextUrl)]
        public async Task<IActionResult> GetUserContextAsync(int userId)
        {
            var userContext = await _repository.GetUserContextAsync(userId);
            return JsonReadResult(userContext);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        // PUT: api/users/login
        [HttpPut]
        [Route(UserApi.UserLoginStatusUrl)]
        public async Task<IActionResult> PutUserLoginStatusAsync([FromBody] LoginViewModel login)
        {
            if (login == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserAccount));
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            var user = await _repository.GetUserAsync(login);
            if (user == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidUserNameMessage));
            }

            if (!user.IsEnabled)
            {
                return BadRequestResult(_strings.Format(AppStrings.DisabledUserMessage));
            }

            if (!_crypto.ValidateHash(login.Password, user.Password))
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidPasswordMessage));
            }

            string userTicket = await GetUserTicketAsync(user.Id);
            Response.Headers.Add(AppConstants.ContextHeaderName, userTicket);
            await _repository.UpdateUserLastLoginAsync(user.Id);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="companyLogin"></param>
        /// <returns></returns>
        // PUT: api/users/login/company
        [HttpPut]
        [Route(UserApi.UserCompanyLoginStatusUrl)]
        public async Task<IActionResult> PutUserCompanyLoginStatusAsync(
            [FromBody] CompanyLoginViewModel companyLogin)
        {
            if (companyLogin == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.CompanyLogin));
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            string ticket = Request.Headers[AppConstants.ContextHeaderName];
            if (!_tokenManager.Validate(ticket))
            {
                return Unauthorized();
            }

            var userContext = SecurityContext.User;
            await _repository.UpdateUserCompanyLoginAsync(companyLogin, userContext);
            UpgradeDatabases(userContext.Connection);
            userContext.Connection = _crypto.Encrypt(userContext.Connection);
            userContext.Language = GetPrimaryRequestLanguage();
            Response.Headers[AppConstants.ContextHeaderName] = GetEncodedTicket(userContext);
            return Ok(userContext);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/users/{userId:min(1)}/roles
        [HttpGet]
        [Route(UserApi.UserRolesUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.View)]
        public async Task<IActionResult> GetUserRolesAsync(int userId)
        {
            var roles = await _repository.GetUserRolesAsync(userId);
            Localize(roles);
            return JsonReadResult(roles);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        // PUT: api/users/{userId:min(1)}/roles
        [HttpPut]
        [Route(UserApi.UserRolesUrl)]
        [AuthorizeRequest(SecureEntity.User, (int)UserPermissions.AssignRoles)]
        public async Task<IActionResult> PutModifiedUserRolesAsync(
            int userId, [FromBody] RelatedItemsViewModel userRoles)
        {
            if (userRoles != null)
            {
                var roleItem = userRoles.RelatedItems
                    .Where(item => item.Id == AppConstants.AdminRoleId)
                    .FirstOrDefault();
                if (userId == AppConstants.AdminUserId && roleItem != null && !roleItem.IsSelected)
                {
                    return BadRequestResult(_strings.Format(AppStrings.AdminRoleIsRequired));
                }
            }

            var result = BasicValidationResult(userRoles, userId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveUserRolesAsync(userRoles);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="specialPassword"></param>
        /// <returns></returns>
        // PUT: api/users/admin/special-pass
        [HttpPut]
        [Route(UserApi.SpecialPasswordUrl)]
        [AuthorizeRequest]
        public IActionResult PutSpecialPassword([FromBody] string specialPassword)
        {
            if (!_crypto.ValidateHash(specialPassword, SysParameterUtility.AllParameters.Db.SuperPasswordHash))
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidPasswordMessage));
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
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.UserName));
            }

            return Ok();
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
            return _tokenManager.Generate(securityContext);
        }

        private void Localize(RelatedItemsViewModel roles)
        {
            Array.ForEach(roles.RelatedItems.ToArray(), item => item.Name = _strings[item.Name]);
        }

        private void UpgradeDatabases(string companyConnection)
        {
            var dbUpgrade = _systemTools.DbUpgrade;
            var scriptFolder = _systemTools.PathProvider.ScriptRoot;
            var sysConnection = _systemTools.Configuration.GetSection("ConnectionStrings")["TadbirSysApi"];
            if (dbUpgrade.NeedsUpgrade(sysConnection, scriptFolder))
            {
                dbUpgrade.UpgradeDatabase(sysConnection, scriptFolder);
            }

            if (dbUpgrade.NeedsUpgrade(companyConnection, scriptFolder))
            {
                dbUpgrade.UpgradeDatabase(companyConnection, scriptFolder);
            }
        }

        private readonly IUserRepository _repository;
        private readonly ICryptoService _crypto;
        private readonly ICommandFilter _commandFilter;
        private readonly ITokenManager _tokenManager;
        private readonly ISystemTools _systemTools;
    }
}

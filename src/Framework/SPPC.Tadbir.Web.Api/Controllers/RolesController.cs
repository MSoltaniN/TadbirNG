using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Framework.Common;
using SPPC.Framework.Values;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class RolesController : Controller
    {
        public RolesController(ISecurityRepository repository)
        {
            _repository = repository;
        }

        #region Asynchronous Methods

        // GET: api/roles
        [Route(RoleApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRolesAsync()
        {
            var roles = await _repository.GetRolesAsync();
            return Json(roles);
        }

        // GET: api/roles/new
        [Route(RoleApi.NewRoleUrl)]
        public async Task<IActionResult> GetNewRoleAsync()
        {
            var newRole = await _repository.GetNewRoleAsync();
            return Json(newRole);
        }

        // GET: api/roles/{roleId:min(1)}
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleAsync(int roleId)
        {
            var role = await _repository.GetRoleAsync(roleId);
            return JsonReadResult(role);
        }

        // GET: api/roles/{roleId:min(1)}/details
        [Route(RoleApi.RoleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleDetailsAsync(int roleId)
        {
            var role = await _repository.GetRoleDetailsAsync(roleId);
            return JsonReadResult(role);
        }

        // POST: api/roles
        [HttpPost]
        [Route(RoleApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Create)]
        public async Task<IActionResult> PostNewRoleAsync([FromBody] RoleFullViewModel role)
        {
            var result = BasicValidationResult(role);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleAsync(role);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/roles/{roleId:min(1)}
        [HttpPut]
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Edit)]
        public async Task<IActionResult> PutModifiedRoleAsync(int roleId, [FromBody] RoleFullViewModel role)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest(Strings.AdminRoleIsReadonly);
            }

            var result = BasicValidationResult(role, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleAsync(role);
            return Ok();
        }

        // DELETE: api/roles/{roleId:min(1)}
        [HttpDelete]
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingRoleAsync(int roleId)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest(Strings.AdminRoleIsReadonly);
            }

            var role = await _repository.GetRoleBriefAsync(roleId);
            if (role == null)
            {
                string message = String.Format(LocalStrings.ItemNotFound, Entities.Role);
                return BadRequest(message);
            }

            if (await _repository.IsAssignedRoleAsync(roleId))
            {
                var message = String.Format(Strings.CannotDeleteAssignedRole, role.Name);
                return BadRequest(message);
            }

            await _repository.DeleteRoleAsync(roleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/roles/{roleId:min(1)}/branches
        [Route(RoleApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleBranchesAsync(int roleId)
        {
            var branches = await _repository.GetRoleBranchesAsync(roleId);
            return JsonReadResult(branches);
        }

        // PUT: api/roles/{roleId:min(1)}/branches
        [HttpPut]
        [Route(RoleApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public async Task<IActionResult> PutModifiedRoleBranchesAsync(int roleId, [FromBody] RoleBranchesViewModel roleBranches)
        {
            var result = BasicValidationResult(roleBranches, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleBranchesAsync(roleBranches);
            return Ok();
        }

        // GET: api/roles/{roleId:min(1)}/users
        [Route(RoleApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleUsersAsync(int roleId)
        {
            var users = await _repository.GetRoleUsersAsync(roleId);
            return JsonReadResult(users);
        }

        // PUT: api/roles/{roleId:min(1)}/users
        [HttpPut]
        [Route(RoleApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public async Task<IActionResult> PutModifiedRoleUsersAsync(int roleId, [FromBody] RoleUsersViewModel roleUsers)
        {
            var result = BasicValidationResult(roleUsers, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleUsersAsync(roleUsers);
            return Ok();
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        // GET: api/roles/sync
        [Route(RoleApi.RolesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoles()
        {
            var roles = _repository.GetRoles();
            return Json(roles);
        }

        // GET: api/roles/new/sync
        [Route(RoleApi.NewRoleSyncUrl)]
        public IActionResult GetNewRole()
        {
            var newRole = _repository.GetNewRole();
            return Json(newRole);
        }

        // GET: api/roles/{roleId:min(1)}/sync
        [Route(RoleApi.RoleSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRole(int roleId)
        {
            var role = _repository.GetRole(roleId);
            return JsonReadResult(role);
        }

        // GET: api/roles/{roleId:min(1)}/details/sync
        [Route(RoleApi.RoleDetailsSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleDetails(int roleId)
        {
            var role = _repository.GetRoleDetails(roleId);
            return JsonReadResult(role);
        }

        // POST: api/roles/sync
        [HttpPost]
        [Route(RoleApi.RolesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Create)]
        public IActionResult PostNewRole([FromBody] RoleFullViewModel role)
        {
            var result = BasicValidationResult(role);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SaveRole(role);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/roles/{roleId:min(1)}/sync
        [HttpPut]
        [Route(RoleApi.RoleSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Edit)]
        public IActionResult PutModifiedRole(int roleId, [FromBody] RoleFullViewModel role)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest(Strings.AdminRoleIsReadonly);
            }

            var result = BasicValidationResult(role, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SaveRole(role);
            return Ok();
        }

        // DELETE: api/roles/{roleId:min(1)}/sync
        [HttpDelete]
        [Route(RoleApi.RoleSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public IActionResult DeleteExistingRole(int roleId)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest(Strings.AdminRoleIsReadonly);
            }

            var role = _repository.GetRoleBrief(roleId);
            if (role == null)
            {
                string message = String.Format(LocalStrings.ItemNotFound, Entities.Role);
                return BadRequest(message);
            }

            if (_repository.IsAssignedRole(roleId))
            {
                var message = String.Format(Strings.CannotDeleteAssignedRole, role.Name);
                return BadRequest(message);
            }

            _repository.DeleteRole(roleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/roles/{roleId:min(1)}/branches/sync
        [Route(RoleApi.RoleBranchesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleBranches(int roleId)
        {
            var branches = _repository.GetRoleBranches(roleId);
            return JsonReadResult(branches);
        }

        // PUT: api/roles/{roleId:min(1)}/branches/sync
        [HttpPut]
        [Route(RoleApi.RoleBranchesSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public IActionResult PutModifiedRoleBranches(int roleId, [FromBody] RoleBranchesViewModel roleBranches)
        {
            var result = BasicValidationResult(roleBranches, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SaveRoleBranches(roleBranches);
            return Ok();
        }

        // GET: api/roles/{roleId:min(1)}/users/sync
        [Route(RoleApi.RoleUsersSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleUsers(int roleId)
        {
            var users = _repository.GetRoleUsers(roleId);
            return JsonReadResult(users);
        }

        // PUT: api/roles/{roleId:min(1)}/users/sync
        [HttpPut]
        [Route(RoleApi.RoleUsersSyncUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public IActionResult PutModifiedRoleUsers(int roleId, [FromBody] RoleUsersViewModel roleUsers)
        {
            var result = BasicValidationResult(roleUsers, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SaveRoleUsers(roleUsers);
            return Ok();
        }

        #endregion

        private IActionResult JsonReadResult<TData>(TData data)
        {
            var result = (data != null)
                ? Json(data)
                : NotFound() as IActionResult;

            return result;
        }

        private IActionResult BasicValidationResult(RoleFullViewModel role, int roleId = 0)
        {
            if (role == null || role.Role == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.Role);
                return BadRequest(message);
            }

            if (roleId != role.Role.Id)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, Entities.Role);
                return BadRequest(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        private IActionResult BasicValidationResult<TModel>(TModel model, int roleId)
        {
            if (model == null)
            {
                var message = String.Format(ValidationMessages.RequestFailedNoData, Entities.Role);
                return BadRequest(message);
            }

            int id = (int)Reflector.GetProperty(model, "id");
            if (roleId != id)
            {
                var message = String.Format(ValidationMessages.RequestFailedConflict, Entities.Role);
                return BadRequest(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        private ISecurityRepository _repository;
    }
}

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // GET: api/roles
        [Route(SecurityApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoles()
        {
            var roles = _repository.GetRoles();
            return Json(roles);
        }

        // GET: api/roles/new
        [Route(SecurityApi.NewRoleUrl)]
        public IActionResult GetNewRole()
        {
            var newRole = _repository.GetNewRole();
            return Json(newRole);
        }

        // GET: api/roles/{roleId:min(1)}
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRole(int roleId)
        {
            var role = _repository.GetRole(roleId);
            var result = (role != null)
                ? Json(role)
                : NotFound() as IActionResult;
            return result;
        }

        // GET: api/roles/{roleId:min(1)}/details
        [Route(SecurityApi.RoleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleDetails(int roleId)
        {
            var role = _repository.GetRoleDetails(roleId);
            var result = (role != null)
                ? Json(role)
                : NotFound() as IActionResult;
            return result;
        }

        // POST: api/roles
        [HttpPost]
        [Route(SecurityApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Create)]
        public IActionResult PostNewRole([FromBody] RoleFullViewModel role)
        {
            if (role == null || role.Role == null)
            {
                return BadRequest("Could not post new role because a 'null' value was provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveRole(role);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/roles/{roleId:min(1)}
        [HttpPut]
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Edit)]
        public IActionResult PutModifiedRole(int roleId, [FromBody] RoleFullViewModel role)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest("Could not put modified role because the role is read-only.");
            }

            if (role == null || role.Role == null)
            {
                return BadRequest("Could not put modified role because a 'null' value was provided.");
            }

            if (roleId <= 0 || role.Role.Id <= 0)
            {
                return BadRequest("Could not put modified role because original role does not exist.");
            }

            if (roleId != role.Role.Id)
            {
                return BadRequest("Could not put modified role because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveRole(role);
            return Ok();
        }

        // DELETE: api/roles/{roleId:min(1)}
        [HttpDelete]
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public IActionResult DeleteExistingRole(int roleId)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest("Could not delete role because the role is read-only.");
            }

            var role = _repository.GetRoleBrief(roleId);
            if (role == null)
            {
                return BadRequest("Could not delete role because it does not exist.");
            }

            if (_repository.IsAssignedRole(roleId))
            {
                var message = String.Format(Strings.CannotDeleteAssignedRole, role.Name);
                return BadRequest(message);
            }

            _repository.DeleteRole(roleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/roles/{roleId:min(1)}/branches
        [Route(SecurityApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleBranches(int roleId)
        {
            var branches = _repository.GetRoleBranches(roleId);
            var result = (branches != null)
                ? Json(branches)
                : NotFound() as IActionResult;
            return result;
        }

        // PUT: api/roles/{roleId:min(1)}/branches
        [HttpPut]
        [Route(SecurityApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public IActionResult PutModifiedRoleBranches(int roleId, [FromBody] RoleBranchesViewModel roleBranches)
        {
            if (roleBranches == null)
            {
                return BadRequest("Could not put modified role branches because a 'null' value was provided.");
            }

            if (roleId <= 0 || roleBranches.Id <= 0)
            {
                return BadRequest("Could not put modified role branches because original role does not exist.");
            }

            if (roleId != roleBranches.Id)
            {
                return BadRequest("Could not put modified role branches because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveRoleBranches(roleBranches);
            return Ok();
        }

        // GET: api/roles/{roleId:min(1)}/users
        [Route(SecurityApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleUsers(int roleId)
        {
            var users = _repository.GetRoleUsers(roleId);
            var result = (users != null)
                ? Json(users)
                : NotFound() as IActionResult;
            return result;
        }

        // PUT: api/roles/{roleId:min(1)}/users
        [HttpPut]
        [Route(SecurityApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public IActionResult PutModifiedRoleUsers(int roleId, [FromBody] RoleUsersViewModel roleUsers)
        {
            if (roleUsers == null)
            {
                return BadRequest("Could not put modified role users because a 'null' value was provided.");
            }

            if (roleId <= 0 || roleUsers.Id <= 0)
            {
                return BadRequest("Could not put modified role users because original role does not exist.");
            }

            if (roleId != roleUsers.Id)
            {
                return BadRequest("Could not put modified role users because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveRoleUsers(roleUsers);
            return Ok();
        }

        private ISecurityRepository _repository;
    }
}

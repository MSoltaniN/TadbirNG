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

        // GET: api/roles/{roleId:int}
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRole(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

            var role = _repository.GetRole(roleId);
            var result = (role != null)
                ? Json(role)
                : NotFound() as IActionResult;
            return result;
        }

        // GET: api/roles/{roleId:int}/details
        [Route(SecurityApi.RoleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleDetails(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

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

        // PUT: api/roles/{roleId:int}
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

        // DELETE: api/roles/{roleId:int}
        [HttpDelete]
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public IActionResult DeleteExistingRole(int roleId)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest("Could not delete role because the role is read-only.");
            }

            if (roleId <= 0)
            {
                return BadRequest("Could not delete role because it does not exist.");
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

        // GET: api/roles/{roleId:int}/branches
        [Route(SecurityApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleBranches(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

            var branches = _repository.GetRoleBranches(roleId);
            var result = (branches != null)
                ? Json(branches)
                : NotFound() as IActionResult;
            return result;
        }

        // PUT: api/roles/{roleId:int}/branches
        [HttpPut]
        [Route(SecurityApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public IActionResult PutModifiedRoleBranches(int roleId, RoleBranchesViewModel branches)
        {
            if (branches == null)
            {
                return BadRequest("Could not put modified role branches because a 'null' value was provided.");
            }

            if (roleId <= 0 || branches.Id <= 0)
            {
                return BadRequest("Could not put modified role branches because original role does not exist.");
            }

            if (roleId != branches.Id)
            {
                return BadRequest("Could not put modified role branches because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveRoleBranches(branches);
            return Ok();
        }

        // GET: api/roles/{roleId:int}/users
        [Route(SecurityApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IActionResult GetRoleUsers(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

            var users = _repository.GetRoleUsers(roleId);
            var result = (users != null)
                ? Json(users)
                : NotFound() as IActionResult;
            return result;
        }

        // PUT: api/roles/{roleId:int}/users
        [HttpPut]
        [Route(SecurityApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public IActionResult PutModifiedRoleUsers(int roleId, RoleUsersViewModel users)
        {
            if (users == null)
            {
                return BadRequest("Could not put modified role users because a 'null' value was provided.");
            }

            if (roleId <= 0 || users.Id <= 0)
            {
                return BadRequest("Could not put modified role users because original role does not exist.");
            }

            if (roleId != users.Id)
            {
                return BadRequest("Could not put modified role users because of an identity conflict in the request.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.SaveRoleUsers(users);
            return Ok();
        }

        private ISecurityRepository _repository;
    }
}

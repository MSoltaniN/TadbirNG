using System;
using System.Net;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class RolesController : ApiController
    {
        public RolesController(ISecurityRepository repository)
        {
            _repository = repository;
        }

        // GET: api/roles
        [Route(SecurityApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IHttpActionResult GetRoles()
        {
            var roles = _repository.GetRoles();
            return Json(roles);
        }

        // GET: api/roles/new
        [Route(SecurityApi.NewRoleUrl)]
        public IHttpActionResult GetNewRole()
        {
            var newRole = _repository.GetNewRole();
            return Json(newRole);
        }

        // GET: api/roles/{roleId:int}
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IHttpActionResult GetRole(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

            var role = _repository.GetRole(roleId);
            var result = (role != null)
                ? Json(role)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // GET: api/roles/{roleId:int}/details
        [Route(SecurityApi.RoleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IHttpActionResult GetRoleDetails(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

            var role = _repository.GetRoleDetails(roleId);
            var result = (role != null)
                ? Json(role)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // POST: api/roles
        [Route(SecurityApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Create)]
        public IHttpActionResult PostNewRole([FromBody] RoleFullViewModel role)
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
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/roles/{roleId:int}
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Edit)]
        public IHttpActionResult PutModifiedRole(int roleId, [FromBody] RoleFullViewModel role)
        {
            if (roleId == Constants.AdminRoleId)
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
        [Route(SecurityApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public IHttpActionResult DeleteExistingRole(int roleId)
        {
            if (roleId == Constants.AdminRoleId)
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
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/roles/{roleId:int}/branches
        [Route(SecurityApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public IHttpActionResult GetRoleBranches(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

            var branches = _repository.GetRoleBranches(roleId);
            var result = (branches != null)
                ? Json(branches)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // PUT: api/roles/{roleId:int}/branches
        [Route(SecurityApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public IHttpActionResult PutModifiedRoleBranches(int roleId, RoleBranchesViewModel branches)
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
        public IHttpActionResult GetRoleUsers(int roleId)
        {
            if (roleId <= 0)
            {
                return NotFound();
            }

            var users = _repository.GetRoleUsers(roleId);
            var result = (users != null)
                ? Json(users)
                : NotFound() as IHttpActionResult;
            return result;
        }

        // PUT: api/roles/{roleId:int}/users
        [Route(SecurityApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public IHttpActionResult PutModifiedRoleUsers(int roleId, RoleUsersViewModel users)
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

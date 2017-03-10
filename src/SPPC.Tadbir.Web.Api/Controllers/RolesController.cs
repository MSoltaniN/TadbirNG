using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Auth;

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

        // POST: api/roles
        [Route(SecurityApi.RolesUrl)]
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

        private ISecurityRepository _repository;
    }
}

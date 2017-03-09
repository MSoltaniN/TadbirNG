using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;

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

        private ISecurityRepository _repository;
    }
}

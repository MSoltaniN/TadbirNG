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

        private ISecurityRepository _repository;
    }
}

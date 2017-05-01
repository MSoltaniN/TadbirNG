using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class CartableController : ApiController
    {
        public CartableController(IWorkItemRepository repository)
        {
            _repository = repository;
        }

        [Route(CartableApi.UserInboxItemsUrl)]
        public IHttpActionResult GetUserInbox(int userId)
        {
            throw new NotImplementedException();
        }

        [Route(CartableApi.UserOutboxItemsUrl)]
        public IHttpActionResult GetUserOutbox(int userId)
        {
            throw new NotImplementedException();
        }

        private IWorkItemRepository _repository;
    }
}

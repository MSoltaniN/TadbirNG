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
            if (userId <= 0)
            {
                return NotFound();
            }

            var workItems = _repository.GetUserInbox(userId);
            return Json(workItems);
        }

        [Route(CartableApi.UserOutboxItemsUrl)]
        public IHttpActionResult GetUserOutbox(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            var workItems = _repository.GetUserOutbox(userId);
            return Json(workItems);
        }

        private IWorkItemRepository _repository;
    }
}

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Web.Controllers
{
    public class CartableController : Controller
    {
        public CartableController(ICartableService service, ISecurityContextManager contextManager)
        {
            Verify.ArgumentNotNull(contextManager, "contextManager");
            _userContext = contextManager.CurrentContext;
            _service = service;
        }

        // GET: cartable
        public ViewResult Index(int? page = null)
        {
            var workItems = _service.GetUserInbox(_userContext.User.Id);
            int pageNumber = (page ?? 1);
            return View(workItems.ToPagedList(pageNumber, Values.Constants.DefaultPageSize));
        }

        // GET: cartable/outbox
        public ViewResult Outbox(int? page = null)
        {
            var workItems = _service.GetUserOutbox(_userContext.User.Id);
            int pageNumber = (page ?? 1);
            return View(workItems.ToPagedList(pageNumber, Values.Constants.DefaultPageSize));
        }

        private ICartableService _service;
        private ISecurityContext _userContext;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel;
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

        // POST: cartable
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IList<SelectedItemViewModel> allItems)
        {
            Verify.ArgumentNotNull(allItems, "selectedItems");
            for (int i = 0; i < allItems.Count; i++)
            {
                var item = allItems[i];
                item.IsSelected = Request.Form.AllKeys.Contains(String.Format("[{0}].IsSelected", i));
            }

            var routeValues = GetGroupOperationRouteValues(allItems);
            routeValues.Add("paraph", Request.Form["paraph"]);
            routeValues.Add("returnUrl", "/cartable");
            routeValues.Add("area", "accounting");
            return GetNextResult(routeValues);
        }

        // GET: cartable/outbox
        public ViewResult Outbox(int? page = null)
        {
            var workItems = _service.GetUserOutbox(_userContext.User.Id);
            int pageNumber = (page ?? 1);
            return View(workItems.ToPagedList(pageNumber, Values.Constants.DefaultPageSize));
        }

        private static RouteValueDictionary GetGroupOperationRouteValues(IList<SelectedItemViewModel> allItems)
        {
            var routeValues = new RouteValueDictionary();
            int index = 0;
            foreach (var item in allItems.Where(i => i.IsSelected))
            {
                routeValues.Add(String.Format("id{0}", index), item.Id);
                index++;
            }

            return routeValues;
        }

        private ActionResult GetNextResult(RouteValueDictionary routeValues)
        {
            ActionResult result = null;
            if (Request.Form.AllKeys.Contains("submit-prepare"))
            {
                result = RedirectToAction("GroupPrepare", "transactions",  routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-review"))
            {
                result = RedirectToAction("GroupReview", "transactions", routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-reject"))
            {
                result = RedirectToAction("GroupReject", "transactions", routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-confirm"))
            {
                result = RedirectToAction("GroupConfirm", "transactions", routeValues);
            }
            else if (Request.Form.AllKeys.Contains("submit-approve"))
            {
                result = RedirectToAction("GroupApprove", "transactions", routeValues);
            }

            return result;
        }

        private ICartableService _service;
        private ISecurityContext _userContext;
    }
}

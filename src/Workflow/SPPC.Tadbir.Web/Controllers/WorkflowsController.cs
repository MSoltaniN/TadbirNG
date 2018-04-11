using System;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Web.Controllers
{
    public class WorkflowsController : Controller
    {
        public WorkflowsController(IWorkflowService service)
        {
            _service = service;
        }

        // GET: workflows[?page={no}]
        public ViewResult Index(int? page = null)
        {
            var running = _service.GetRunningWorkflows();
            int pageNumber = (page ?? 1);
            return View(running.ToPagedList(pageNumber, AppConstants.DefaultPageSize));
        }

        private IWorkflowService _service;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;

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
            return View(running.ToPagedList(pageNumber, Values.Constants.DefaultPageSize));
        }

        private IWorkflowService _service;
    }
}
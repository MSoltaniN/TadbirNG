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
    public class WorkflowsController : ApiController
    {
        public WorkflowsController(IWorkflowRepository repository)
        {
            _repository = repository;
        }

        // GET: api/workflows/running
        [Route(WorkflowApi.RunningWorkflowsUrl)]
        public IHttpActionResult GetRunningWorkflows()
        {
            var runningWorkflows = _repository.GetRunningWorkflows();
            return Json(runningWorkflows);
        }

        private IWorkflowRepository _repository;
    }
}

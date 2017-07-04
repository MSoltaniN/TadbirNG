using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class SettingsController : ApiController
    {
        public SettingsController(ISettingsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/settings/workflows
        [Route(SettingsApi.WorkflowSettingsUrl)]
        public IHttpActionResult GetWorkflowSettings()
        {
            var settings = _repository.GetWorkflowSettings();
            return Json(settings);
        }

        // PUT: api/settings/workflows
        [Route(SettingsApi.WorkflowSettingsUrl)]
        public IHttpActionResult PutModifiedWorkflowSettings([FromBody] WorkflowSettingsViewModel settings)
        {
            if (settings == null)
            {
                return BadRequest("Could not put modified workflow settings because a 'null' value was provided.");
            }

            _repository.SaveWorkflowSettings(settings);
            return Ok();
        }

        private ISettingsRepository _repository;
    }
}

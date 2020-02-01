using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class ProjectsController : ValidatingController<ProjectViewModel>
    {
        public ProjectsController(
            IProjectRepository repository, IConfigRepository config, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewName.Project).Result;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Project; }
        }

        // GET: api/projects
        [Route(ProjectApi.EnvironmentProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetEnvironmentProjectsAsync()
        {
            int itemCount = await _repository.GetCountAsync<ProjectViewModel>(GridOptions);
            SetItemCount(itemCount);
            var projects = await _repository.GetProjectsAsync(GridOptions);
            SetRowNumbers(projects);
            return Json(projects);
        }

        // GET: api/projects/lookup
        [Route(ProjectApi.EnvironmentProjectsLookupUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetEnvironmentProjectsLookupAsync()
        {
            var lookup = await _repository.GetProjectsLookupAsync(GridOptions);
            return Json(lookup);
        }

        // GET: api/projects/{projectId:min(1)}
        [Route(ProjectApi.ProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectAsync(int projectId)
        {
            var project = await _repository.GetProjectAsync(projectId);
            return JsonReadResult(project);
        }

        // GET: api/projects/{projectId:int}/children/new
        [Route(ProjectApi.EnvironmentNewChildProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Create)]
        public async Task<IActionResult> GetEnvironmentNewProjectAsync(int projectId)
        {
            var newProject = await _repository.GetNewChildProjectAsync(
                projectId > 0 ? projectId : (int?)null);
            if (newProject == null)
            {
                return BadRequest(_strings.Format(AppStrings.ParentItemNotFound, AppStrings.Project));
            }

            if (newProject.Level == -1)
            {
                return BadRequest(_strings.Format(AppStrings.ChildItemsNotAllowed, AppStrings.Project));
            }

            return Json(newProject);
        }

        // GET: api/projects/ledger
        [Route(ProjectApi.EnvironmentProjectsLedgerUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetEnvironmentAccountsLedgerAsync()
        {
            var projects = await _repository.GetProjectsLedgerAsync();
            return JsonReadResult(projects);
        }

        // GET: api/projects/{projectId:min(1)}/children
        [Route(ProjectApi.ProjectChildrenUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectChildrenAsync(int projectId)
        {
            var children = await _repository.GetProjectChildrenAsync(projectId);
            return JsonReadResult(children);
        }

        // GET: api/projects/fullcode/{parentId}
        [HttpGet]
        [Route(ProjectApi.ProjectFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Create | (int)ProjectPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int parentId)
        {
            if (parentId <= 0)
            {
                return Ok(string.Empty);
            }

            string fullCode = await _repository.GetProjectFullCodeAsync(parentId);

            return Ok(fullCode);
        }

        // POST: api/projects
        [HttpPost]
        [Route(ProjectApi.EnvironmentProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Create)]
        public async Task<IActionResult> PostNewProjectAsync([FromBody] ProjectViewModel project)
        {
            var result = await ValidationResultAsync(project);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveProjectAsync(project);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/projects/{projectId:min(1)}
        [HttpPut]
        [Route(ProjectApi.ProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Edit)]
        public async Task<IActionResult> PutModifiedProjectAsync(
            int projectId, [FromBody] ProjectViewModel project)
        {
            var result = await ValidationResultAsync(project, projectId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveProjectAsync(project);
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/projects/{projectId:min(1)}
        [HttpDelete]
        [Route(ProjectApi.ProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingProjectAsync(int projectId)
        {
            string result = await ValidateDeleteAsync(projectId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteProjectAsync(projectId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/projects
        [HttpPut]
        [Route(ProjectApi.EnvironmentProjects)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Delete)]
        public async Task<IActionResult> PutExistingProjectsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await ValidateGroupDeleteAsync(actionDetail.Items);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            await _repository.DeleteProjectsAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var project = await _repository.GetProjectAsync(item);
            if (project == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Project), item);
            }

            var result = BranchValidationResult(project);
            if (result is BadRequestObjectResult errorResult)
            {
                return errorResult.Value.ToString();
            }

            var projectInfo = String.Format("'{0} ({1})'", project.Name, project.Code);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteNonLeafItem], _strings[AppStrings.Project], projectInfo);
            }
            else if (await _repository.IsUsedProjectAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteUsedItem], _strings[AppStrings.Project], projectInfo);
            }
            else if (await _repository.IsRelatedProjectAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteRelatedItem], _strings[AppStrings.Project], projectInfo);
            }

            return message;
        }

        private async Task<IActionResult> ValidationResultAsync(ProjectViewModel project, int projectId = 0)
        {
            var result = BasicValidationResult(project, projectId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateProjectAsync(project))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateCodeValue, AppStrings.Project, project.FullCode));
            }

            result = BranchValidationResult(project);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = ConfigValidationResult(project, _treeConfig.Current);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            return Ok();
        }

        private readonly IProjectRepository _repository;
        private readonly IConfigRepository _config;
        private readonly ViewTreeFullConfig _treeConfig;
    }
}
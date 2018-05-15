﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class ProjectsController : ApiControllerBase<ProjectViewModel>
    {
        public ProjectsController(
            IProjectRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Project; }
        }

        // GET: api/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(ProjectApi.FiscalPeriodBranchProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectsAsync(int fpId, int branchId)
        {
            int itemCount = await _repository.GetCountAsync(fpId, branchId, GridOptions);
            SetItemCount(itemCount);
            var projects = await _repository.GetProjectsAsync(fpId, branchId, GridOptions);
            return Json(projects);
        }

        // GET: api/projects/{projectId:min(1)}
        [Route(ProjectApi.ProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectAsync(int projectId)
        {
            var project = await _repository.GetProjectAsync(projectId);
            return JsonReadResult(project);
        }

        // GET: api/projects/metadata
        [Route(ProjectApi.ProjectMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectMetadataAsync()
        {
            var metadata = await _repository.GetProjectMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/projects
        [HttpPost]
        [Route(ProjectApi.ProjectsUrl)]
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

        private async Task<IActionResult> ValidationResultAsync(ProjectViewModel project, int projectId = 0)
        {
            var result = BasicValidationResult(project, projectId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateProjectAsync(project))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateCodeValue, AppStrings.Project));
            }

            return Ok();
        }

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var project = await _repository.GetProjectAsync(item);
            if (project == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Project), item);
            }

            var projectInfo = String.Format("'{0} ({1})'", project.Name, project.Code);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteNonLeafItem], _strings[AppStrings.Project], projectInfo);
            }

            if (await _repository.IsUsedProjectAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteUsedItem], _strings[AppStrings.Project], projectInfo);
            }

            return message;
        }

        private IProjectRepository _repository;
    }
}
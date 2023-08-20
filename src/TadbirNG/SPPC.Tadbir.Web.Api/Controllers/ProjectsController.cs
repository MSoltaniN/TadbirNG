using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با پروژه ها در برنامه را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class ProjectsController : ValidatingController<ProjectViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات پروژه در دیتابیس را فراهم می کند</param>
        /// <param name="config">امکان خواندن اطلاعات پیکربندی برنامه را فراهم می کند</param>
        /// <param name="checkEdition"></param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        public ProjectsController(
            IProjectRepository repository, IConfigRepository config, ICheckEdition checkEdition,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewId.Project).Result;
            _checkEdition = checkEdition;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام پروژه
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Project; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده کلیه پروژه‌های فعال را خوانده و برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده پروژه های فعال</returns>
        // GET: api/projects
        [HttpGet]
        [Route(ProjectApi.ProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectsAsync()
        {
            var projects = await _repository.GetProjectsAsync(GridOptions);
            return JsonListResult(projects);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده کلیه پروژه‌های غیر فعال را خوانده و برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده پروژه های غیر فعال</returns>
        // GET: api/projects/inactive
        [HttpGet]
        [Route(ProjectApi.InactiveProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetInactiveProjectsAsync()
        {
            var projects = await _repository.GetProjectsAsync(GridOptions, (int)ActiveState.Inactive);
            return JsonListResult(projects);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده کلیه پروژه‌ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده کلیه پروژه ها</returns>
        // GET: api/projects/all
        [HttpGet]
        [Route(ProjectApi.AllProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetAllProjectsAsync()
        {
            var projects = await _repository.GetProjectsAsync(GridOptions, (int)ActiveState.All);
            return JsonListResult(projects);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی پروژه مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <returns>اطلاعات نمایشی پروژه</returns>
        // GET: api/projects/{projectId:min(1)}
        [HttpGet]
        [Route(ProjectApi.ProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectAsync(int projectId)
        {
            var project = await _repository.GetProjectAsync(projectId);
            return JsonReadResult(project);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه جدیدی زیرمجموعه پروژه والد داده شده برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه والد</param>
        /// <returns>اطلاعات پیشنهادی برای پروژه جدید</returns>
        // GET: api/projects/{projectId:int}/children/new
        [HttpGet]
        [Route(ProjectApi.NewChildProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Create)]
        public async Task<IActionResult> GetNewProjectAsync(int projectId)
        {
            var newProject = await _repository.GetNewChildProjectAsync(
                projectId > 0 ? projectId : null);
            if (newProject == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ParentItemNotFound, AppStrings.Project));
            }

            if (newProject.Level == -1)
            {
                return BadRequestResult(_strings.Format(AppStrings.ChildItemsNotAllowed, AppStrings.Project));
            }

            if (projectId > 0 && await _repository.IsUsedProjectAsync(projectId))
            {
                var parent = await _repository.GetProjectAsync(projectId);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.Project, parentInfo));
            }

            return Json(newProject);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه ها در بالاترین سطح را برمی گرداند
        /// </summary>
        /// <returns>لیست اطلاعات خلاصه پروژه ها در بالاترین سطح</returns>
        // GET: api/projects/root
        [HttpGet]
        [Route(ProjectApi.RootProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetRootProjectsAsync()
        {
            var projects = await _repository.GetRootProjectsAsync();
            return Json(projects);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه های زیرمجموعه پروژه داده شده را برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه والد</param>
        /// <returns>لیست اطلاعات خلاصه پروژه های زیرمجموعه</returns>
        // GET: api/projects/{projectId:min(1)}/children
        [HttpGet]
        [Route(ProjectApi.ProjectChildrenUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectChildrenAsync(int projectId)
        {
            var children = await _repository.GetProjectChildrenAsync(projectId);
            return Json(children);
        }

        /// <summary>
        /// به روش آسنکرون، کد کامل پروژه مشخص شده با شناسه را برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <returns>کد کامل پروژه</returns>
        // GET: api/projects/{projectId:int}/fullcode
        [HttpGet]
        [HttpGet]
        [Route(ProjectApi.ProjectFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Create | (int)ProjectPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int projectId)
        {
            if (projectId <= 0)
            {
                return Ok(String.Empty);
            }

            string fullCode = await _repository.GetProjectFullCodeAsync(projectId);
            return Ok(fullCode);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه داده شده را ایجاد می کند
        /// </summary>
        /// <param name="project">اطلاعات کامل پروژه جدید</param>
        /// <returns>اطلاعات پروژه بعد از ایجاد در دیتابیس</returns>
        // POST: api/projects
        [HttpPost]
        [Route(ProjectApi.ProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Create)]
        public async Task<IActionResult> PostNewProjectAsync([FromBody] ProjectViewModel project)
        {
            var message = _checkEdition.ValidateNewModel(project, EditionLimit.ProjectDepth);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            var result = await ValidationResultAsync(project);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveProjectAsync(project);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر برای اصلاح</param>
        /// <param name="project">اطلاعات اصلاح شده پروژه</param>
        /// <returns>اطلاعات پروژه بعد از اصلاح در دیتابیس</returns>
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
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه دیتابیسی را غیرفعال می کند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر برای غیرفعال کردن</param>
        // PUT: api/projects/{projectId:min(1)}/deactivate
        [HttpPut]
        [Route(ProjectApi.DeactivateProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Deactivate)]
        public async Task<IActionResult> PutProjectAsDeactivated(int projectId)
        {
            return await UpdateActiveStateAsync(projectId, false);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه دیتابیسی را فعال می کند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر برای فعال کردن</param>
        // PUT: api/projects/{projectId:min(1)}/reactivate
        [HttpPut]
        [Route(ProjectApi.ReactivateProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Reactivate)]
        public async Task<IActionResult> PutProjectAsReactivated(int projectId)
        {
            return await UpdateActiveStateAsync(projectId, true);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/projects/{projectId:min(1)}
        [HttpDelete]
        [Route(ProjectApi.ProjectUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingProjectAsync(int projectId)
        {
            var result = await ValidateDeleteResultAsync(projectId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteProjectAsync(projectId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/projects
        [HttpPut]
        [Route(ProjectApi.ProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Delete)]
        public async Task<IActionResult> PutExistingProjectsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteProjectsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای پروژه مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>پیغام خطای به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var project = await _repository.GetProjectAsync(item);
            if (project == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Project, item.ToString());
                return GetGroupActionResult(message, project);
            }

            var result = BranchValidationResult(project);
            if (result is BadRequestObjectResult errorResult)
            {
                return GetGroupActionResult(errorResult.Value.ToString(), project);
            }

            var projectInfo = String.Format("'{0} ({1})'", project.Name, project.FullCode);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = _strings.Format(AppStrings.CantDeleteNonLeafItem, AppStrings.Project, projectInfo);
            }
            else if (await _repository.IsUsedProjectAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteUsedItem, AppStrings.Project, projectInfo);
            }
            else if (await _repository.IsRelatedProjectAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteRelatedItem, AppStrings.Project, projectInfo);
            }

            return GetGroupActionResult(message, project);
        }

        private async Task<IActionResult> ValidationResultAsync(ProjectViewModel project, int projectId = 0)
        {
            var result = BasicValidationResult(project, projectId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateFullCodeAsync(project))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.DuplicateCodeValue, AppStrings.Project, project.FullCode));
            }

            if (await _repository.IsDuplicateNameAsync(project))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateNameValue, AppStrings.Project, project.Name));
            }

            if (project.ParentId.HasValue && await _repository.IsUsedProjectAsync(project.ParentId.Value))
            {
                var parent = await _repository.GetProjectAsync(project.ParentId.Value);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.Project, parentInfo));
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

            result = ActiveStateValidationResult(project);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = _repository as IActiveStateRepository<ProjectViewModel>;
            if (project.ParentId != null && await repository.IsDeactivatedAsync(project.ParentId.Value))
            {
                var message = _strings.Format(AppStrings.ActiveStateParentError, EntityNameKey);
                return BadRequestResult(message);
            }

            return Ok();
        }

        private async Task<IActionResult> UpdateActiveStateAsync(int projectId, bool isActive)
        {
            var project = await _repository.GetProjectAsync(projectId);
            if (project == null)
            {
                string message = _strings.Format(
                    AppStrings.ItemByIdNotFound, EntityNameKey, projectId.ToString());
                return BadRequestResult(message);
            }

            var result = ActiveStateValidationResult(project);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = _repository as IActiveStateRepository<ProjectViewModel>;
            await repository.SetActiveStatusAsync(project, isActive);
            return Ok();
        }

        private readonly IProjectRepository _repository;
        private readonly IConfigRepository _config;
        private readonly ViewTreeFullConfig _treeConfig;
        private readonly ICheckEdition _checkEdition;
    }
}
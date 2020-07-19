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
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
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
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        public ProjectsController(
            IProjectRepository repository, IConfigRepository config, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewName.Project).Result;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام پروژه
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Project; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه های قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده پروژه ها</returns>
        // GET: api/projects
        [HttpGet]
        [Route(ProjectApi.EnvironmentProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetEnvironmentProjectsAsync()
        {
            var projects = await _repository.GetProjectsAsync(GridOptions);
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
        public async Task<IActionResult> NewProjectAsync(int projectId)
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

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه ها در بالاترین سطح را برمی گرداند
        /// </summary>
        /// <returns>لیست اطلاعات خلاصه پروژه ها در بالاترین سطح</returns>
        // GET: api/projects/root
        [HttpGet]
        [Route(ProjectApi.RootProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetRootAccountsAsync()
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
        /// <param name="parentId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <returns>کد کامل پروژه</returns>
        // GET: api/projects/{parentId:int}/fullcode
        [HttpGet]
        [HttpGet]
        [Route(ProjectApi.ProjectFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.Create | (int)ProjectPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int parentId)
        {
            if (parentId <= 0)
            {
                return Ok(String.Empty);
            }

            string fullCode = await _repository.GetProjectFullCodeAsync(parentId);
            return Ok(fullCode);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه داده شده را ایجاد می کند
        /// </summary>
        /// <param name="project">اطلاعات کامل پروژه جدید</param>
        /// <returns>اطلاعات پروژه بعد از ایجاد در دیتابیس</returns>
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
            string result = await ValidateDeleteAsync(projectId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
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

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای پروژه مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>پیغام خطای به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var project = await _repository.GetProjectAsync(item);
            if (project == null)
            {
                return _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Project, item.ToString());
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
                return BadRequest(_strings.Format(
                    AppStrings.DuplicateCodeValue, AppStrings.Project, project.FullCode));
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
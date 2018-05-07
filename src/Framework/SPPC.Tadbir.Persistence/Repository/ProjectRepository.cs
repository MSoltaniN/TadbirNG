using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات پروژه ها را پیاده سازی می کند.
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="decorator">امکان ضمیمه کردن متادیتا به اطلاعات خوانده شده را فراهم می کند</param>
        public ProjectRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataDecorator decorator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _decorator = decorator;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<ProjectViewModel>> GetProjectsAsync(
            int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(
                    prj => prj.FiscalPeriod.Id == fpId
                        && prj.Branch.Id == branchId,
                    gridOptions,
                    prj => prj.FiscalPeriod, prj => prj.Branch);
            return projects
                .Select(item => _mapper.Map<ProjectViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد پروژه های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    prj => prj.FiscalPeriod.Id == fpId && prj.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، پروژه با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه عددی یکی از پروژه های موجود</param>
        /// <returns>پروژه مشخص شده با شناسه عددی</returns>
        public async Task<ProjectViewModel> GetProjectAsync(int projectId)
        {
            ProjectViewModel item = null;
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(
                projectId, prj => prj.FiscalPeriod, prj => prj.Branch);
            if (project != null)
            {
                item = _mapper.Map<ProjectViewModel>(project);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای پروژه را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای پروژه</returns>
        public async Task<EntityItemViewModel<ProjectViewModel>> GetProjectMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<Project, ProjectViewModel>(null);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک پروژه را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="project">پروژه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی پروژه ایجاد یا اصلاح شده</returns>
        public async Task<ProjectViewModel> SaveProjectAsync(ProjectViewModel project)
        {
            Verify.ArgumentNotNull(project, "project");
            Project projectModel = default(Project);
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            if (project.Id == 0)
            {
                projectModel = _mapper.Map<Project>(project);
                repository.Insert(projectModel);
            }
            else
            {
                projectModel = await repository.GetByIDAsync(
                    project.Id, prj => prj.FiscalPeriod, prj => prj.Branch);
                if (projectModel != null)
                {
                    UpdateExistingProject(project, projectModel);
                    repository.Update(projectModel);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProjectViewModel>(projectModel);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="projectId">شناسه عددی پروژه مورد نظر برای حذف</param>
        public async Task DeleteProjectAsync(int projectId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId);
            if (project != null)
            {
                project.FiscalPeriod = null;
                project.Branch = null;
                project.Parent = null;
                repository.Delete(project);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد پروژه مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="project">مدل نمایشی پروژه مورد نظر</param>
        /// <returns>اگر کد پروژه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateProjectAsync(ProjectViewModel project)
        {
            Verify.ArgumentNotNull(project, "project");
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(
                    prj => prj.Id != project.Id
                        && prj.FiscalPeriod.Id == project.FiscalPeriodId
                        && prj.Code == project.Code);
            return (projects.Count > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا پروژه انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <returns>در حالتی که پروژه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsUsedProjectAsync(int projectId)
        {
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.Project.Id == projectId);
            return (articles.Count != 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا پروژه انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <returns>در حالتی که پروژه مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int projectId)
        {
            bool? hasChildren = null;
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, prj => prj.Children);
            if (project != null)
            {
                hasChildren = project.Children.Count > 0;
            }

            return hasChildren;
        }

        private static void UpdateExistingProject(ProjectViewModel projectViewModel, Project project)
        {
            project.Code = projectViewModel.Code;
            project.FullCode = projectViewModel.FullCode;
            project.Name = projectViewModel.Name;
            project.Level = projectViewModel.Level;
            project.Description = projectViewModel.Description;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataDecorator _decorator;
    }
}

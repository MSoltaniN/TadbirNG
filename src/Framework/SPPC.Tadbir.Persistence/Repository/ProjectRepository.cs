﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات پروژه ها را پیاده سازی می کند.
    /// </summary>
    public class ProjectRepository : LoggingRepository<Project, ProjectViewModel>, IProjectRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="repository">امکان فیلتر اطلاعات روی سطرها و شعبه ها را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="config">امکان مدیریت تنظیمات برنامه را در دیتابیس فراهم می کند</param>
        public ProjectRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository log,
            ISecureRepository repository, IConfigRepository config)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
            _configRepository = config;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه هایی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<ProjectViewModel>> GetProjectsAsync(GridOptions gridOptions = null)
        {
            var projects = await _repository.GetAllAsync<Project>(ViewName.Project, prj => prj.Children);
            return projects
                .Select(item => Mapper.Map<ProjectViewModel>(item))
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه هایی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<KeyValue>> GetProjectsLookupAsync(GridOptions gridOptions = null)
        {
            return await _repository.GetAllLookupAsync<Project>(ViewName.Project, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد پروژه های تعریف شده در دوره مالی و شعبه جاری را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد پروژه های تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            return await _repository.GetCountAsync<Project>(ViewName.Project, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه عددی یکی از پروژه های موجود</param>
        /// <returns>پروژه مشخص شده با شناسه عددی</returns>
        public async Task<ProjectViewModel> GetProjectAsync(int projectId)
        {
            ProjectViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, prj => prj.Children);
            if (project != null)
            {
                item = Mapper.Map<ProjectViewModel>(project);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های زیرمجموعه را برای پروژه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکی از پروژه های موجود</param>
        /// <returns>مدل نمایشی پروژه های زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetProjectChildrenAsync(int projectId)
        {
            var children = await _repository
                .GetAllQuery<Project>(ViewName.Project, prj => prj.Children)
                .Where(prj => prj.ParentId == projectId)
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .ToListAsync();
            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای پروژه را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای پروژه</returns>
        public async Task<ViewViewModel> GetProjectMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<Project>();
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
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            if (project.Id == 0)
            {
                projectModel = Mapper.Map<Project>(project);
                await InsertAsync(repository, projectModel);
                await UpdateLevelUsageAsync(projectModel.Level);
            }
            else
            {
                projectModel = await repository.GetByIDAsync(project.Id);
                if (projectModel != null)
                {
                    await UpdateAsync(repository, projectModel, project);
                }
            }

            return Mapper.Map<ProjectViewModel>(projectModel);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="projectId">شناسه عددی پروژه مورد نظر برای حذف</param>
        public async Task DeleteProjectAsync(int projectId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId);
            if (project != null)
            {
                await DeleteAsync(repository, project);
                await UpdateLevelUsageAsync(project.Level);
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
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(
                    prj => prj.Id != project.Id
                        && prj.FiscalPeriod.Id <= project.FiscalPeriodId
                        && prj.FullCode == project.FullCode);
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
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.Project.Id == projectId);
            return (articles.Count != 0);
        }

        /// <inheritdoc/>
        public async Task<bool> IsRelatedProjectAsync(int projectId)
        {
            var accProjectRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            int relatedAccounts = await accProjectRepository.GetCountByCriteriaAsync(
                ap => ap.ProjectId == projectId, null);
            return (relatedAccounts > 0);
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
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, prj => prj.Children);
            if (project != null)
            {
                hasChildren = project.Children.Count > 0;
            }

            return hasChildren;
        }

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر پروژه را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر پروژه</param>
        /// <returns>اگر پروژه والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        public async Task<string> GetProjectFullCodeAsync(int parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(parentId);
            if (project == null)
            {
                return string.Empty;
            }

            return project.FullCode;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// <para>توجه : فراخوانی این متد با اطلاعات محیطی معتبر برای موفقیت سایر عملیات این کلاس الزامی است</para>
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public override void SetCurrentContext(UserContextViewModel userContext)
        {
            base.SetCurrentContext(userContext);
            _repository.SetCurrentContext(userContext);
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="projectViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="project">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(ProjectViewModel projectViewModel, Project project)
        {
            project.Code = projectViewModel.Code;
            project.FullCode = projectViewModel.FullCode;
            project.Name = projectViewModel.Name;
            project.Level = projectViewModel.Level;
            project.Description = projectViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Project entity)
        {
            return (entity != null)
               ? String.Format(
                   "Name : {1}{0}Code : {2}{0}FullCode : {3}{0}Description : {4}",
                   Environment.NewLine, entity.Name, entity.Code, entity.FullCode, entity.Description)
               : null;
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت استفاده از یکی از سطوح درختی پروژه را در دیتابیس بروزرسانی می کند
        /// </summary>
        /// <param name="level">شماره سطح مورد نظر</param>
        /// <remarks>قابل توجه است که در این متد هیچگونه فیلتری روی دوره مالی، شعبه یا سطرهای قابل دسترسی صورت نمی گیرد.
        /// این به این معنی است که اطلاعات سطح مورد نظر در هر شعبه یا دوره مالی ممکن است ایجاد شده باشد. </remarks>
        private async Task UpdateLevelUsageAsync(int level)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            int count = await repository.GetCountByCriteriaAsync(prj => prj.Level == level);
            await _configRepository.SaveTreeLevelUsageAsync(ViewName.Project, level, count);
        }

        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}

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
using SPPC.Tadbir.Configuration.Models;
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
        /// <param name="relations">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        public ProjectRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository log,
            ISecureRepository repository, IConfigRepository config, IRelationRepository relations)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
            _configRepository = config;
            _relationRepository = relations;
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
            var filteredProjects = projects
                .Select(item => Mapper.Map<ProjectViewModel>(item))
                .ToList();
            await FilterGrandchildrenAsync(filteredProjects);
            return filteredProjects
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
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات از آن استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد پروژه های تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new()
        {
            return await _repository.GetCountAsync<Project, TViewModel>(ViewName.Project, gridOptions);
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
            var project = await repository.GetByIDAsync(projectId);
            if (project != null)
            {
                item = Mapper.Map<ProjectViewModel>(project);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، برای پروژه والد مشخص شده پروژه زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی پروژه والد - اگر مقدار نداشته باشد پروژه جدید
        /// در سطح کل پیشنهاد می شود</param>
        /// <returns>مدل نمایشی پروژه پیشنهادی</returns>
        public async Task<ProjectViewModel> GetNewChildProjectAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var parent = await repository.GetByIDAsync(parentId ?? 0);
            if (parentId > 0 && parent == null)
            {
                return null;
            }

            var fullConfig = await _configRepository.GetViewTreeConfigByViewAsync(ViewName.Project);
            var treeConfig = fullConfig.Current;
            if (parent != null && parent.Level + 1 == treeConfig.MaxDepth)
            {
                return new ProjectViewModel() { Level = -1 };
            }

            var childrenCodes = await GetChildrenCodesAsync(parentId);
            string newCode = GetNewProjectCode(parent, childrenCodes, treeConfig);
            return GetNewChildProject(parent, newCode, treeConfig);
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های سطح اول را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه پروژه های سطح اول</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetProjectsLedgerAsync()
        {
            var projects = await _repository
                .GetAllQuery<Project>(ViewName.Project, prj => prj.Children)
                .Where(prj => prj.ParentId == null)
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .ToListAsync();
            await FilterGrandchildrenAsync(projects);
            return projects;
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
            await FilterGrandchildrenAsync(children);
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
                await _relationRepository.OnProjectInsertedAsync(projectModel.Id);
            }
            else
            {
                projectModel = await repository.GetByIDAsync(project.Id);
                if (projectModel != null)
                {
                    bool needsCascade = (projectModel.Code != project.Code);
                    await UpdateAsync(repository, projectModel, project);
                    if (needsCascade)
                    {
                        await CascadeUpdateFullCodeAsync(projectModel.Id);
                    }
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
        /// به روش آسنکرون، پروژه های مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="projectIds">مجموعه ای از شناسه های عددی پروژه های مورد نظر برای حذف</param>
        public async Task DeleteProjectsAsync(IList<int> projectIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            int level = 0;
            foreach (int projectId in projectIds)
            {
                var project = await repository.GetByIDAsync(projectId);
                if (project != null)
                {
                    level = Math.Max(level, project.Level);
                    await DeleteAsync(repository, project);
                }
            }

            await UpdateLevelUsageAsync(level);
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
                ap => ap.ProjectId == projectId);
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

        private static string GetNewProjectCode(
            Project parent, IEnumerable<string> existingCodes, ViewTreeConfig treeConfig)
        {
            int childLevel = (parent != null) ? parent.Level + 1 : 0;
            int codeLength = treeConfig.Levels[childLevel].CodeLength;
            string format = String.Format("D{0}", codeLength);
            var maxCode = (long)Math.Pow(10, codeLength) - 1;
            var lastCode = (existingCodes.Count() > 0) ? Int64.Parse(existingCodes.Max()) : 0;
            var newCode = (lastCode < maxCode) ? lastCode + 1 : 0;
            return newCode.ToString(format);
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

        /// <summary>
        /// به روش آسنکرون، تعداد زیرشاخه ها را در مجموعه ای از اطلاعات درختی
        /// با توجه به تنظیمات جاری دسترسی به شعب و سطرها اصلاح می کند
        /// </summary>
        /// <typeparam name="TTreeEntity">نوع مدل نمایشی با ساختار درختی</typeparam>
        /// <param name="children">مجموعه ای از اطلاعات درختی که زیرشاخه های آنها باید فیلتر شود</param>
        private async Task FilterGrandchildrenAsync<TTreeEntity>(IList<TTreeEntity> children)
            where TTreeEntity : ITreeEntityView
        {
            var childIds = children.Select(item => item.Id);
            var grandchildren = await _repository
                .GetAllQuery<Project>(ViewName.Project)
                .Where(prj => prj.ParentId != null && childIds.Contains(prj.ParentId.Value))
                .GroupBy(prj => prj.ParentId.Value)
                .ToArrayAsync();
            foreach (var child in children)
            {
                var grandchild = grandchildren
                    .Where(item => item.Key == child.Id)
                    .SingleOrDefault();
                child.ChildCount = (grandchild != null)
                    ? grandchild.Count()
                    : 0;
            }
        }

        private async Task CascadeUpdateFullCodeAsync(int projectId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, prj => prj.Children);
            if (project != null)
            {
                foreach (var child in project.Children)
                {
                    child.FullCode = project.FullCode + child.Code;
                    repository.Update(child);
                    await UnitOfWork.CommitAsync();
                    await CascadeUpdateFullCodeAsync(child.Id);
                }
            }
        }

        private async Task<IList<string>> GetChildrenCodesAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            return await repository
                .GetEntityQuery()
                .Where(prj => prj.ParentId == parentId)
                .Select(prj => prj.Code)
                .ToListAsync();
        }

        private ProjectViewModel GetNewChildProject(
            Project parent, string newCode, ViewTreeConfig treeConfig)
        {
            var childProject = new ProjectViewModel()
            {
                Code = newCode,
                ParentId = parent?.Id,
                FiscalPeriodId = _currentContext.FiscalPeriodId,
                BranchId = _currentContext.BranchId
            };
            childProject.FullCode = (parent != null)
                ? parent.FullCode + childProject.Code
                : childProject.Code;
            if (parent != null)
            {
                childProject.Level = (short)((parent.Level + 1 < treeConfig.MaxDepth)
                    ? parent.Level + 1
                    : -1);
            }

            return childProject;
        }

        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
        private readonly IRelationRepository _relationRepository;
    }
}

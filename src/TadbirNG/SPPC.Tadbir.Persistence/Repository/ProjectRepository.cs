using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات پروژه ها را پیاده سازی می کند.
    /// </summary>
    public class ProjectRepository
        : ActiveStateRepository<Project, ProjectViewModel>, IProjectRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="relations">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        public ProjectRepository(IRepositoryContext context, ISystemRepository system,
            IRelationRepository relations)
            : base(context, system?.Logger)
        {
            _system = system;
            _relationRepository = relations;
            var fullConfig = _system.Config.GetViewTreeConfigByViewAsync(ViewId.Project).Result;
            _treeUtility = new TreeEntityUtility<Project, ProjectViewModel>(context, fullConfig.Current);
        }

        /// <inheritdoc/>
        public async Task<PagedList<ProjectViewModel>> GetProjectsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var projects = new List<ProjectViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                projects = await Repository
                    .GetAllQuery<Project>(ViewId.Project, prj => prj.Children)
                    .Select(item => Mapper.Map<ProjectViewModel>(item))
                    .ToListAsync();
                await UpdateInactiveItemsAsync(projects);
                Array.ForEach(projects.ToArray(), prj => prj.State = Context.Localize(prj.State));
            }

            await ReadAsync(gridOptions);
            return new PagedList<ProjectViewModel>(projects, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<ProjectViewModel> GetProjectAsync(int projectId)
        {
            ProjectViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, prj => prj.Children);
            if (project != null)
            {
                item = Mapper.Map<ProjectViewModel>(project);
                var isDeactivated = await IsDeactivatedAsync(item.Id);
                item.State = isDeactivated
                    ? Context.Localize(AppStrings.Inactive)
                    : Context.Localize(AppStrings.Active);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetRootProjectsAsync()
        {
            var projects = await Repository
                .GetAllQuery<Project>(ViewId.Project, prj => prj.Children)
                .Where(prj => prj.ParentId == null)
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .ToListAsync();
            return projects;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetProjectChildrenAsync(int projectId)
        {
            var children = await Repository
                .GetAllQuery<Project>(ViewId.Project, prj => prj.Children)
                .Where(prj => prj.ParentId == projectId)
                .Select(prj => Mapper.Map<AccountItemBriefViewModel>(prj))
                .ToListAsync();
            return children;
        }

        /// <inheritdoc/>
        public async Task<ProjectViewModel> SaveProjectAsync(ProjectViewModel project)
        {
            Verify.ArgumentNotNull(project, "project");
            Project projectModel;
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            if (project.Id == 0)
            {
                projectModel = Mapper.Map<Project>(project);
                SetBaseEntityInfo(projectModel);
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
                    SetBaseEntityInfo(projectModel);
                    await UpdateAsync(repository, projectModel, project);
                    if (needsCascade)
                    {
                        await CascadeUpdateFullCodeAsync(projectModel.Id);
                    }
                }
            }

            return Mapper.Map<ProjectViewModel>(projectModel);
        }

        /// <inheritdoc/>
        public async Task DeleteProjectAsync(int projectId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId);
            if (project != null)
            {
                await OnDeleteItemAsync(project.Id);
                await DeleteAsync(repository, project);
                await UpdateLevelUsageAsync(project.Level);
            }
        }

        /// <inheritdoc/>
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
                    await OnDeleteItemAsync(project.Id);
                    await DeleteNoLogAsync(repository, project);
                }
            }

            await UpdateLevelUsageAsync(level);
            await OnEntityGroupDeleted(projectIds);
        }

        /// <inheritdoc/>
        public async Task<bool> IsUsedProjectAsync(int projectId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.Project.Id == projectId);
            return articles.Count != 0;
        }

        /// <inheritdoc/>
        public async Task<bool> IsRelatedProjectAsync(int projectId)
        {
            var accProjectRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            int relatedAccounts = await accProjectRepository.GetCountByCriteriaAsync(
                ap => ap.ProjectId == projectId);
            return relatedAccounts > 0;
        }

        #region Common TreeEntity Operations

        /// <inheritdoc/>
        public async Task<ProjectViewModel> GetNewChildProjectAsync(int? parentId)
        {
            return await _treeUtility.GetNewChildItemAsync(parentId);
        }

        /// <inheritdoc/>
        public async Task<string> GetProjectFullCodeAsync(int projectId)
        {
            return await _treeUtility.GetItemFullCodeAsync(projectId);
        }

        /// <inheritdoc/>
        public async Task<bool?> HasChildrenAsync(int projectId)
        {
            return await _treeUtility.HasChildrenAsync(projectId);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateFullCodeAsync(ProjectViewModel project)
        {
            return await _treeUtility.IsDuplicateFullCodeAsync(project);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateNameAsync(ProjectViewModel project)
        {
            return await _treeUtility.IsDuplicateNameAsync(project);
        }

        #endregion

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Project; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(ProjectViewModel projectViewModel, Project project)
        {
            project.Code = projectViewModel.Code;
            project.FullCode = projectViewModel.FullCode;
            project.Name = projectViewModel.Name;
            project.Level = projectViewModel.Level;
            project.Description = projectViewModel.Description;
        }

        /// <inheritdoc/>
        protected override string GetState(Project entity)
        {
            return entity == null
                ? String.Empty
                : $"{AppStrings.Name} : {entity.Name} , " +
                  $"{AppStrings.Code} : {entity.Code} , " +
                  $"{AppStrings.FullCode} : {entity.FullCode} , " +
                  $"{AppStrings.Description} : {entity.Description}";
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
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
            await Config.SaveTreeLevelUsageAsync(ViewId.Project, level, count);
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

        private readonly ISystemRepository _system;
        private readonly IRelationRepository _relationRepository;
        private readonly TreeEntityUtility<Project, ProjectViewModel> _treeUtility;
    }
}

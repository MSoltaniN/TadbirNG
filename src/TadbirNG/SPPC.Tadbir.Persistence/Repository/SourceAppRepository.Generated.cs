using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت منابع و مصارف را پیاده سازی می کند
    /// </summary>
    public class SourceAppRepository : ActiveStateRepository<SourceApp, SourceAppViewModel>, ISourceAppRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public SourceAppRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <inheritdoc/>
        public async Task<PagedList<SourceAppViewModel>> GetSourceAppsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var sourceApps = new List<SourceAppViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<SourceApp>(ViewId.SourceApp);
                sourceApps = await query
                    .Select(item => Mapper.Map<SourceAppViewModel>(item))
                    .ToListAsync();
                await UpdateInactiveItemsAsync(sourceApps);
                Array.ForEach(sourceApps.ToArray(), sa => Localize(sa));
            }

            await ReadAsync(gridOptions);
            return new PagedList<SourceAppViewModel>(sourceApps, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<SourceAppViewModel> GetSourceAppAsync(int sourceAppId)
        {
            SourceAppViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            var sourceApp = await repository.GetByIDAsync(sourceAppId);
            if (sourceApp != null)
            {
                item = Mapper.Map<SourceAppViewModel>(sourceApp);
                var isDeactivated = await IsDeactivatedAsync(item.Id);
                item.State = isDeactivated
                    ? AppStrings.Inactive
                    : AppStrings.Active;
            }

            return Localize(item);
        }

        /// <inheritdoc/>
        public async Task<SourceAppViewModel> GetNewSourceAppAsync()
        {
            int lastNo = await GetLastSourceAppNoAsync();
            var newSourceApp = new SourceAppViewModel()
            {
                Code = (lastNo + 1).ToString(),
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                Type = (short)SourceAppType.Source
            };

            return Localize(newSourceApp);
        }

        /// <inheritdoc/>
        public async Task<SourceAppViewModel> SaveSourceAppAsync(SourceAppViewModel sourceApp)
        {
            Verify.ArgumentNotNull(sourceApp, nameof(sourceApp));
            SourceApp sourceAppModel;
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            if (sourceApp.Id == 0)
            {
                sourceAppModel = Mapper.Map<SourceApp>(sourceApp);
                SetBaseEntityInfo(sourceAppModel);
                await InsertAsync(repository, sourceAppModel);
            }
            else
            {
                sourceAppModel = await repository.GetByIDAsync(sourceApp.Id);
                if (sourceAppModel != null)
                {
                    SetBaseEntityInfo(sourceAppModel);
                    await UpdateAsync(repository, sourceAppModel, sourceApp);
                }
            }

            return Localize(Mapper.Map<SourceAppViewModel>(sourceAppModel));
        }

        /// <inheritdoc/>
        public async Task DeleteSourceAppAsync(int sourceAppId)
        {
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            var sourceApp = await repository.GetByIDAsync(sourceAppId);
            if (sourceApp != null)
            {
                await OnDeleteItemAsync(sourceApp.Id);
                await DeleteAsync(repository, sourceApp);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteSourceAppsAsync(IList<int> sourceAppIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            foreach (int sourceAppId in sourceAppIds)
            {
                var sourceApp = await repository.GetByIDAsync(sourceAppId);
                if (sourceApp != null)
                {
                    await OnDeleteItemAsync(sourceApp.Id);
                    await DeleteNoLogAsync(repository, sourceApp);
                }
            }

            await OnEntityGroupDeleted(sourceAppIds);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateNameAsync(SourceAppViewModel sourceApp)
        {
            Verify.ArgumentNotNull(sourceApp, nameof(sourceApp));
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            int count = await repository.GetCountByCriteriaAsync(
                sa => sa.Id != sourceApp.Id
                    && sa.Name == sourceApp.Name);
            return count > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateCodeAsync(SourceAppViewModel sourceApp)
        {
            Verify.ArgumentNotNull(sourceApp, nameof(sourceApp));
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            int count = await repository.GetCountByCriteriaAsync(
                c => c.Id != sourceApp.Id
                    && c.Code == sourceApp.Code);
            return count > 0;
        }
        
        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.SourceApp; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(SourceAppViewModel sourceAppViewModel, SourceApp sourceApp)
        {
            sourceApp.BranchScope = sourceAppViewModel.BranchScope;
            sourceApp.Code = sourceAppViewModel.Code;
            sourceApp.Name = sourceAppViewModel.Name;
            sourceApp.Description = sourceAppViewModel.Description;
            sourceApp.Type = sourceAppViewModel.Type;
        }

        /// <inheritdoc/>
        protected override string GetState(SourceApp entity)
        {
            var type = entity.Type == (short)SourceAppType.Source
                ? AppStrings.Source
                : AppStrings.Application;
            return entity == null
                ? String.Empty
                : $"{AppStrings.Type} : {type} , " +
                  $"{AppStrings.Name} : {entity.Name} , " +
                  $"{AppStrings.Code} : {entity.Code} , " +
                  $"{AppStrings.Description} : {entity.Description}";
        }

        /// <summary>
        /// به روش آسنکرون، شماره آخرین منبع یا مصرف موجود را برمی گرداند
        /// </summary>
        protected async Task<int> GetLastSourceAppNoAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<SourceApp>();
            var lastByNo = await repository
                .GetEntityQuery()
                .OrderByDescending(sourceApp => sourceApp.Id)
                .FirstOrDefaultAsync();
            return (lastByNo != null) ? Int32.Parse(lastByNo.Code) : 0;
        }
        
        private SourceAppViewModel Localize(SourceAppViewModel sourceApp)
        {
            if (sourceApp != null)
            {
                sourceApp.TypeName = Context.Localize(sourceApp.TypeName);
                sourceApp.State = Context.Localize(sourceApp.State);
            }

            return sourceApp;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}

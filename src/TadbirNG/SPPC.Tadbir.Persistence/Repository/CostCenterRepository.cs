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
    /// عملیات مورد نیاز برای مدیریت اطلاعات مراکز هزینه را پیاده سازی می کند.
    /// </summary>
    public class CostCenterRepository
        : ActiveStateRepository<CostCenter, CostCenterViewModel>, ICostCenterRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="relations">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        public CostCenterRepository(IRepositoryContext context, ISystemRepository system,
            IRelationRepository relations)
            : base(context, system?.Logger)
        {
            _system = system;
            _relationRepository = relations;
            var fullConfig = _system.Config.GetViewTreeConfigByViewAsync(ViewId.CostCenter).Result;
            _treeUtility = new TreeEntityUtility<CostCenter, CostCenterViewModel>(context, fullConfig.Current);
        }

        /// <inheritdoc/>
        public async Task<PagedList<CostCenterViewModel>> GetCostCentersAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var costCenters = new List<CostCenterViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                costCenters = await Repository
                    .GetAllQuery<CostCenter>(ViewId.CostCenter, cc => cc.Children)
                    .Select(item => Mapper.Map<CostCenterViewModel>(item))
                    .ToListAsync();
                await UpdateInactiveItemsAsync(costCenters);
                Array.ForEach(costCenters.ToArray(), cc => cc.State = Context.Localize(cc.State));
            }

            await ReadAsync(gridOptions);
            return new PagedList<CostCenterViewModel>(costCenters, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetRootCostCentersAsync()
        {
            var costCenters = await Repository
                .GetAllQuery<CostCenter>(ViewId.CostCenter, cc => cc.Children)
                .Where(cc => cc.ParentId == null)
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .ToListAsync();
            return costCenters;
        }

        /// <inheritdoc/>
        public async Task<CostCenterViewModel> GetCostCenterAsync(int costCenterId)
        {
            CostCenterViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Children);
            if (costCenter != null)
            {
                item = Mapper.Map<CostCenterViewModel>(costCenter);
                var isDeactivated = await IsDeactivatedAsync(item.Id);
                item.State = isDeactivated
                    ? Context.Localize(AppStrings.Inactive)
                    : Context.Localize(AppStrings.Active);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetCostCenterChildrenAsync(int costCenterId)
        {
            var children = await Repository
                .GetAllQuery<CostCenter>(ViewId.CostCenter, cc => cc.Children)
                .Where(cc => cc.ParentId == costCenterId)
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .ToListAsync();
            return children;
        }

        /// <inheritdoc/>
        public async Task<CostCenterViewModel> SaveCostCenterAsync(CostCenterViewModel costCenter)
        {
            Verify.ArgumentNotNull(costCenter, "costCenter");
            CostCenter costCenterModel;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            if (costCenter.Id == 0)
            {
                costCenterModel = Mapper.Map<CostCenter>(costCenter);
                SetBaseEntityInfo(costCenterModel);
                await InsertAsync(repository, costCenterModel);
                await UpdateLevelUsageAsync(costCenterModel.Level);
                await _relationRepository.OnCostCenterInsertedAsync(costCenterModel.Id);
            }
            else
            {
                costCenterModel = await repository.GetByIDAsync(costCenter.Id);
                if (costCenterModel != null)
                {
                    bool needsCascade = (costCenterModel.Code != costCenter.Code);
                    SetBaseEntityInfo(costCenterModel);
                    await UpdateAsync(repository, costCenterModel, costCenter);
                    if (needsCascade)
                    {
                        await CascadeUpdateFullCodeAsync(costCenterModel.Id);
                    }
                }
            }

            return Mapper.Map<CostCenterViewModel>(costCenterModel);
        }

        /// <inheritdoc/>
        public async Task DeleteCostCenterAsync(int costCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId);
            if (costCenter != null)
            {
                await OnDeleteItemAsync(costCenter.Id);
                await DeleteAsync(repository, costCenter);
                await UpdateLevelUsageAsync(costCenter.Level);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteCostCentersAsync(IList<int> centerIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            int level = 0;
            foreach (int centerId in centerIds)
            {
                var costCenter = await repository.GetByIDAsync(centerId);
                if (costCenter != null)
                {
                    level = Math.Max(level, costCenter.Level);
                    await OnDeleteItemAsync(costCenter.Id);
                    await DeleteNoLogAsync(repository, costCenter);
                }
            }

            await UpdateLevelUsageAsync(level);
            await OnEntityGroupDeleted(centerIds);
        }

        /// <inheritdoc/>
        public async Task<bool> IsUsedCostCenterAsync(int costCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.CostCenter.Id == costCenterId);
            return (articles.Count != 0);
        }

        /// <inheritdoc/>
        public async Task<bool> IsRelatedCostCenterAsync(int costCenterId)
        {
            var accCenterRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            int relatedAccounts = await accCenterRepository.GetCountByCriteriaAsync(
                ac => ac.CostCenterId == costCenterId);
            return relatedAccounts > 0;
        }

        #region Common TreeEntity Operations

        /// <inheritdoc/>
        public async Task<CostCenterViewModel> GetNewChildCostCenterAsync(int? parentId)
        {
            return await _treeUtility.GetNewChildItemAsync(parentId);
        }

        /// <inheritdoc/>
        public async Task<string> GetCostCenterFullCodeAsync(int costCenterId)
        {
            return await _treeUtility.GetItemFullCodeAsync(costCenterId);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateFullCodeAsync(CostCenterViewModel costCenter)
        {
            return await _treeUtility.IsDuplicateFullCodeAsync(costCenter);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateNameAsync(CostCenterViewModel costCenter)
        {
            return await _treeUtility.IsDuplicateNameAsync(costCenter);
        }

        /// <inheritdoc/>
        public async Task<bool?> HasChildrenAsync(int costCenterId)
        {
            return await _treeUtility.HasChildrenAsync(costCenterId);
        }

        #endregion

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.CostCenter; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(CostCenterViewModel costCenterViewModel, CostCenter costCenter)
        {
            costCenter.Code = costCenterViewModel.Code;
            costCenter.FullCode = costCenterViewModel.FullCode;
            costCenter.Name = costCenterViewModel.Name;
            costCenter.Level = costCenterViewModel.Level;
            costCenter.Description = costCenterViewModel.Description;
        }

        /// <inheritdoc/>
        protected override string GetState(CostCenter entity)
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
        /// به روش آسنکرون، وضعیت استفاده از یکی از سطوح درختی مرکز هزینه را در دیتابیس بروزرسانی می کند
        /// </summary>
        /// <param name="level">شماره سطح مورد نظر</param>
        /// <remarks>قابل توجه است که در این متد هیچگونه فیلتری روی دوره مالی، شعبه یا سطرهای قابل دسترسی صورت نمی گیرد.
        /// این به این معنی است که اطلاعات سطح مورد نظر در هر شعبه یا دوره مالی ممکن است ایجاد شده باشد. </remarks>
        private async Task UpdateLevelUsageAsync(int level)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            int count = await repository.GetCountByCriteriaAsync(cc => cc.Level == level);
            await Config.SaveTreeLevelUsageAsync(ViewId.CostCenter, level, count);
        }

        private async Task CascadeUpdateFullCodeAsync(int costCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Children);
            if (costCenter != null)
            {
                foreach (var child in costCenter.Children)
                {
                    child.FullCode = costCenter.FullCode + child.Code;
                    repository.Update(child);
                    await UnitOfWork.CommitAsync();
                    await CascadeUpdateFullCodeAsync(child.Id);
                }
            }
        }

        private readonly ISystemRepository _system;
        private readonly IRelationRepository _relationRepository;
        private readonly TreeEntityUtility<CostCenter, CostCenterViewModel> _treeUtility;
    }
}

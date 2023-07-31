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
    /// عملیات مورد نیاز برای مدیریت اطلاعات تفصیلی های شناور را پیاده سازی می کند.
    /// </summary>
    public class DetailAccountRepository
        : ActiveStateRepository<DetailAccount, DetailAccountViewModel>, IDetailAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="relations">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        public DetailAccountRepository(IRepositoryContext context, ISystemRepository system,
            IRelationRepository relations)
            : base(context, system?.Logger)
        {
            _system = system;
            _relationRepository = relations;
            var fullConfig = _system.Config.GetViewTreeConfigByViewAsync(ViewId.DetailAccount).Result;
            _treeUtility = new TreeEntityUtility<DetailAccount, DetailAccountViewModel>(context, fullConfig.Current);
        }

        /// <inheritdoc/>
        public async Task<PagedList<DetailAccountViewModel>> GetDetailAccountsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var detailAccounts = new List<DetailAccountViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                detailAccounts = await Repository
                    .GetAllQuery<DetailAccount>(ViewId.DetailAccount, facc => facc.Children)
                    .Select(item => Mapper.Map<DetailAccountViewModel>(item))
                    .ToListAsync();
                await UpdateInactiveItemsAsync(detailAccounts);
                Array.ForEach(detailAccounts.ToArray(), facc => facc.State = Context.Localize(facc.State));
            }

            await ReadAsync(gridOptions);
            return new PagedList<DetailAccountViewModel>(detailAccounts, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<DetailAccountViewModel> GetDetailAccountAsync(int faccountId)
        {
            DetailAccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId);
            if (detailAccount != null)
            {
                item = Mapper.Map<DetailAccountViewModel>(detailAccount);
                var isDeactivated = await IsDeactivatedAsync(item.Id);
                item.State = isDeactivated
                    ? Context.Localize(AppStrings.Inactive)
                    : Context.Localize(AppStrings.Active);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync()
        {
            var detailAccounts = await Repository
                .GetAllQuery<DetailAccount>(ViewId.DetailAccount, facc => facc.Children)
                .Where(facc => facc.ParentId == null)
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .ToListAsync();
            return detailAccounts;
        }

        /// <inheritdoc/>
        public async Task<IList<AccountItemBriefViewModel>> GetDetailAccountChildrenAsync(int detailId)
        {
            var children = await Repository
                .GetAllQuery<DetailAccount>(ViewId.DetailAccount, facc => facc.Children)
                .Where(facc => facc.ParentId == detailId)
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .ToListAsync();
            return children;
        }

        /// <inheritdoc/>
        public async Task<DetailAccountViewModel> SaveDetailAccountAsync(DetailAccountViewModel detailAccount)
        {
            Verify.ArgumentNotNull(detailAccount, "detailAccount");
            DetailAccount detailModel;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            if (detailAccount.Id == 0)
            {
                detailModel = Mapper.Map<DetailAccount>(detailAccount);
                await InsertAsync(repository, detailModel);
                await UpdateLevelUsageAsync(detailModel.Level);
                await _relationRepository.OnDetailAccountInsertedAsync(detailModel.Id);
            }
            else
            {
                detailModel = await repository.GetByIDAsync(detailAccount.Id);
                if (detailModel != null)
                {
                    bool needsCascade = (detailModel.Code != detailAccount.Code);
                    await UpdateAsync(repository, detailModel, detailAccount);
                    if (needsCascade)
                    {
                        await CascadeUpdateFullCodeAsync(detailModel.Id);
                    }
                }
            }

            return Mapper.Map<DetailAccountViewModel>(detailModel);
        }

        /// <inheritdoc/>
        public async Task DeleteDetailAccountAsync(int faccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId);
            if (detailAccount != null)
            {
                await OnDeleteItemAsync(detailAccount.Id);
                await DeleteAsync(repository, detailAccount);
                await UpdateLevelUsageAsync(detailAccount.Level);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteDetailAccountsAsync(IList<int> detailIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            int level = 0;
            foreach (int detailId in detailIds)
            {
                var detailAccount = await repository.GetByIDAsync(detailId);
                if (detailAccount != null)
                {
                    level = Math.Max(level, detailAccount.Level);
                    await OnDeleteItemAsync(detailAccount.Id);
                    await DeleteNoLogAsync(repository, detailAccount);
                }
            }

            await UpdateLevelUsageAsync(level);
            await OnEntityGroupDeleted(detailIds);
        }

        /// <inheritdoc/>
        public async Task<bool> IsUsedDetailAccountAsync(int faccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.DetailAccount.Id == faccountId);
            return (articles.Count != 0);
        }

        /// <inheritdoc/>
        public async Task<bool> IsRelatedDetailAccountAsync(int faccountId)
        {
            var accDetailrepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            int relatedAccounts = await accDetailrepository.GetCountByCriteriaAsync(
                ada => ada.DetailAccountId == faccountId);
            return relatedAccounts > 0;
        }

        #region Common TreeEntity Operations

        /// <inheritdoc/>
        public async Task<DetailAccountViewModel> GetNewChildDetailAccountAsync(int? parentId)
        {
            return await _treeUtility.GetNewChildItemAsync(parentId);
        }

        /// <inheritdoc/>
        public async Task<string> GetDetailAccountFullCodeAsync(int faccountId)
        {
            return await _treeUtility.GetItemFullCodeAsync(faccountId);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateFullCodeAsync(DetailAccountViewModel detailAccount)
        {
            return await _treeUtility.IsDuplicateFullCodeAsync(detailAccount);
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateNameAsync(DetailAccountViewModel detailAccount)
        {
            return await _treeUtility.IsDuplicateNameAsync(detailAccount);
        }

        /// <inheritdoc/>
        public async Task<bool?> HasChildrenAsync(int faccountId)
        {
            return await _treeUtility.HasChildrenAsync(faccountId);
        }

        #endregion

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.DetailAccount; }
        }

        /// <inheritdoc/>
        protected override string GetState(DetailAccount entity)
        {
            return entity == null
                ? String.Empty
                : $"{AppStrings.Name} : {entity.Name} , " +
                  $"{AppStrings.Code} : {entity.Code} , " +
                  $"{AppStrings.FullCode} : {entity.FullCode} , " +
                  $"{AppStrings.Description} : {entity.Description}";
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(DetailAccountViewModel detailAccountViewModel, DetailAccount detailAccount)
        {
            detailAccount.Code = detailAccountViewModel.Code;
            detailAccount.FullCode = detailAccountViewModel.FullCode;
            detailAccount.Name = detailAccountViewModel.Name;
            detailAccount.Level = detailAccountViewModel.Level;
            detailAccount.Description = detailAccountViewModel.Description;
            detailAccount.CurrencyId = detailAccountViewModel.CurrencyId;
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
        /// به روش آسنکرون، وضعیت استفاده از یکی از سطوح درختی تفصیلی شناور را در دیتابیس بروزرسانی می کند
        /// </summary>
        /// <param name="level">شماره سطح مورد نظر</param>
        /// <remarks>قابل توجه است که در این متد هیچگونه فیلتری روی دوره مالی، شعبه یا سطرهای قابل دسترسی صورت نمی گیرد.
        /// این به این معنی است که اطلاعات سطح مورد نظر در هر شعبه یا دوره مالی ممکن است ایجاد شده باشد. </remarks>
        private async Task UpdateLevelUsageAsync(int level)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            int count = await repository.GetCountByCriteriaAsync(facc => facc.Level == level);
            await Config.SaveTreeLevelUsageAsync(ViewId.DetailAccount, level, count);
        }

        private async Task CascadeUpdateFullCodeAsync(int faccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId, facc => facc.Children);
            if (detailAccount != null)
            {
                foreach (var child in detailAccount.Children)
                {
                    child.FullCode = detailAccount.FullCode + child.Code;
                    repository.Update(child);
                    await UnitOfWork.CommitAsync();
                    await CascadeUpdateFullCodeAsync(child.Id);
                }
            }
        }

        private readonly ISystemRepository _system;
        private readonly IRelationRepository _relationRepository;
        private readonly TreeEntityUtility<DetailAccount, DetailAccountViewModel> _treeUtility;
    }
}

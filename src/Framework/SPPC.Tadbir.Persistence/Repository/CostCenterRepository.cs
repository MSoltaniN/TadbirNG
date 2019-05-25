using System;
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
    /// عملیات مورد نیاز برای مدیریت اطلاعات مراکز هزینه را پیاده سازی می کند.
    /// </summary>
    public class CostCenterRepository : LoggingRepository<CostCenter, CostCenterViewModel>, ICostCenterRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        /// <param name="repository">امکان فیلتر اطلاعات روی سطرها و شعبه ها را فراهم می کند</param>
        /// <param name="config">امکان مدیریت تنظیمات برنامه را در دیتابیس فراهم می کند</param>
        /// <param name="relations">امکان مدیریت ارتباطات بردار حساب را فراهم می کند</param>
        public CostCenterRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, IOperationLogRepository log,
            ISecureRepository repository, IConfigRepository config, IRelationRepository relations)
            : base(unitOfWork, mapper, metadata, log)
        {
            _repository = repository;
            _configRepository = config;
            _relationRepository = relations;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه ای را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<CostCenterViewModel>> GetCostCentersAsync(GridOptions gridOptions = null)
        {
            var costCenters = await _repository.GetAllAsync<CostCenter>(ViewName.CostCenter, cc => cc.Children);
            var filteredCenters = costCenters
                .Select(item => Mapper.Map<CostCenterViewModel>(item))
                .ToList();
            await FilterGrandchildrenAsync(filteredCenters);
            return filteredCenters
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه ای را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<KeyValue>> GetCostCentersLookupAsync(GridOptions gridOptions = null)
        {
            return await _repository.GetAllLookupAsync<CostCenter>(ViewName.CostCenter, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه در سطح اول را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه مراکز هزینه در سطح اول</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetCostCentersLedgerAsync()
        {
            var costCenters = await _repository
                .GetAllQuery<CostCenter>(ViewName.CostCenter, cc => cc.Children)
                .Where(cc => cc.ParentId == null)
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .ToListAsync();
            await FilterGrandchildrenAsync(costCenters);
            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه جاری را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات از آن استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new()
        {
            return await _repository.GetCountAsync<CostCenter, TViewModel>(ViewName.CostCenter, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی یکی از مراکز هزینه موجود</param>
        /// <returns>مرکز هزینه مشخص شده با شناسه عددی</returns>
        public async Task<CostCenterViewModel> GetCostCenterAsync(int costCenterId)
        {
            CostCenterViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId);
            if (costCenter != null)
            {
                item = Mapper.Map<CostCenterViewModel>(costCenter);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، برای مرکز هزینه والد مشخص شده مرکز زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی مرکز هزینه والد - اگر مقدار نداشته باشد مرکز جدید
        /// در سطح اول پیشنهاد می شود</param>
        /// <returns>مدل نمایشی مرکز هزینه پیشنهادی</returns>
        public async Task<CostCenterViewModel> GetNewChildCostCenterAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var parent = await repository.GetByIDAsync(parentId ?? 0);
            if (parentId > 0 && parent == null)
            {
                return null;
            }

            var fullConfig = await _configRepository.GetViewTreeConfigByViewAsync(ViewName.CostCenter);
            var treeConfig = fullConfig.Current;
            if (parent != null && parent.Level + 1 == treeConfig.MaxDepth)
            {
                return new CostCenterViewModel() { Level = -1 };
            }

            var childrenCodes = await GetChildrenCodesAsync(parentId);
            string newCode = GetNewCostCenterCode(parent, childrenCodes, treeConfig);
            return GetNewChildCostCenter(parent, newCode, treeConfig);
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه زیرمجموعه را برای مرکز هزینه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مراکز هزینه زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetCostCenterChildrenAsync(int costCenterId)
        {
            var children = await _repository
                .GetAllQuery<CostCenter>(ViewName.CostCenter, cc => cc.Children)
                .Where(cc => cc.ParentId == costCenterId)
                .Select(cc => Mapper.Map<AccountItemBriefViewModel>(cc))
                .ToListAsync();
            await FilterGrandchildrenAsync(children);
            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک مرکز هزینه را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="costCenter">مرکز هزینه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی مرکز هزینه ایجاد یا اصلاح شده</returns>
        public async Task<CostCenterViewModel> SaveCostCenterAsync(CostCenterViewModel costCenter)
        {
            Verify.ArgumentNotNull(costCenter, "costCenter");
            CostCenter costCenterModel = default(CostCenter);
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            if (costCenter.Id == 0)
            {
                costCenterModel = Mapper.Map<CostCenter>(costCenter);
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
                    await UpdateAsync(repository, costCenterModel, costCenter);
                    if (needsCascade)
                    {
                        await CascadeUpdateFullCodeAsync(costCenterModel.Id);
                    }
                }
            }

            return Mapper.Map<CostCenterViewModel>(costCenterModel);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی مرکز هزینه مورد نظر برای حذف</param>
        public async Task DeleteCostCenterAsync(int costCenterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId);
            if (costCenter != null)
            {
                await DeleteAsync(repository, costCenter);
                await UpdateLevelUsageAsync(costCenter.Level);
            }
        }

        /// <summary>
        /// به روش آسنکرون، مراکز مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="centerIds">مجموعه ای از شناسه های عددی مراکز مورد نظر برای حذف</param>
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
                    await DeleteAsync(repository, costCenter);
                }
            }

            await UpdateLevelUsageAsync(level);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد مرکز هزینه مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="costCenter">مدل نمایشی مرکز هزینه مورد نظر</param>
        /// <returns>اگر کد مرکز هزینه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateCostCenterAsync(CostCenterViewModel costCenter)
        {
            Verify.ArgumentNotNull(costCenter, "costCenter");
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(
                    cc => cc.Id != costCenter.Id
                        && cc.FiscalPeriod.Id <= costCenter.FiscalPeriodId
                        && cc.FullCode == costCenter.FullCode);
            return (costCenters.Count > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
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
            return (relatedAccounts > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int costCenterId)
        {
            bool? hasChildren = null;
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Children);
            if (costCenter != null)
            {
                hasChildren = costCenter.Children.Count > 0;
            }

            return hasChildren;
        }

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر مرکز هزینه را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر مرکز هزینه</param>
        /// <returns>اگر مرکز هزینه والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        public async Task<string> GetCostCenterFullCodeAsync(int parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(parentId);
            if (costCenter == null)
            {
                return string.Empty;
            }

            return costCenter.FullCode;
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
        /// <param name="costCenterViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="costCenter">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CostCenterViewModel costCenterViewModel, CostCenter costCenter)
        {
            costCenter.Code = costCenterViewModel.Code;
            costCenter.FullCode = costCenterViewModel.FullCode;
            costCenter.Name = costCenterViewModel.Name;
            costCenter.Level = costCenterViewModel.Level;
            costCenter.Description = costCenterViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CostCenter entity)
        {
            return (entity != null)
               ? String.Format(
                   "Name : {1}{0}Code : {2}{0}FullCode : {3}{0}Description : {4}",
                   Environment.NewLine, entity.Name, entity.Code, entity.FullCode, entity.Description)
               : null;
        }

        private static string GetNewCostCenterCode(
            CostCenter parent, IEnumerable<string> existingCodes, ViewTreeConfig treeConfig)
        {
            int childLevel = (parent != null) ? parent.Level + 1 : 0;
            int codeLength = treeConfig.Levels[childLevel].CodeLength;
            string format = String.Format("D{0}", codeLength);
            var maxCode = (long)Math.Pow(10, codeLength) - 1;
            var lastCode = (existingCodes.Count() > 0) ? Int64.Parse(existingCodes.Max()) : 0;
            var newCode = Math.Min(lastCode + 1, maxCode);
            return newCode.ToString(format);
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
            await _configRepository.SaveTreeLevelUsageAsync(ViewName.CostCenter, level, count);
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
                .GetAllQuery<CostCenter>(ViewName.CostCenter)
                .Where(cc => cc.ParentId != null && childIds.Contains(cc.ParentId.Value))
                .GroupBy(cc => cc.ParentId.Value)
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

        private async Task<IList<string>> GetChildrenCodesAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CostCenter>();
            return await repository
                .GetEntityQuery()
                .Where(cc => cc.ParentId == parentId
                    && cc.FiscalPeriodId <= _currentContext.FiscalPeriodId)
                .Select(cc => cc.Code)
                .ToListAsync();
        }

        private CostCenterViewModel GetNewChildCostCenter(
            CostCenter parent, string newCode, ViewTreeConfig treeConfig)
        {
            var childCenter = new CostCenterViewModel()
            {
                Code = newCode,
                ParentId = parent?.Id,
                FiscalPeriodId = _currentContext.FiscalPeriodId,
                BranchId = _currentContext.BranchId
            };
            childCenter.FullCode = (parent != null)
                ? parent.FullCode + childCenter.Code
                : childCenter.Code;
            if (parent != null)
            {
                childCenter.Level = (short)((parent.Level + 1 < treeConfig.MaxDepth)
                    ? parent.Level + 1
                    : -1);
            }

            return childCenter;
        }

        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
        private readonly IRelationRepository _relationRepository;
    }
}

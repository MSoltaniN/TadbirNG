using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات تفصیلی های شناور را پیاده سازی می کند.
    /// </summary>
    public class DetailAccountRepository
        : EntityLoggingRepository<DetailAccount, DetailAccountViewModel>, IDetailAccountRepository
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
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<PagedList<DetailAccountViewModel>> GetDetailAccountsAsync(GridOptions gridOptions = null)
        {
            var detailAccounts = new List<DetailAccountViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                detailAccounts = await Repository
                    .GetAllQuery<DetailAccount>(ViewId.DetailAccount, facc => facc.Children)
                    .Select(item => Mapper.Map<DetailAccountViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(gridOptions);
            return new PagedList<DetailAccountViewModel>(detailAccounts, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<KeyValue>> GetDetailAccountsLookupAsync(GridOptions gridOptions = null)
        {
            return await Repository.GetAllLookupAsync<DetailAccount>(ViewId.DetailAccount, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور با شناسه عددی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه عددی یکی از تفصیلی های شناور موجود</param>
        /// <returns>تفصیلی شناور مشخص شده با شناسه عددی</returns>
        public async Task<DetailAccountViewModel> GetDetailAccountAsync(int faccountId)
        {
            DetailAccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId);
            if (detailAccount != null)
            {
                item = Mapper.Map<DetailAccountViewModel>(detailAccount);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، برای تفصیلی شناور والد مشخص شده شناور زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی تفصیلی شناور والد - اگر مقدار نداشته باشد شناور جدید
        /// در سطح اول پیشنهاد می شود</param>
        /// <returns>مدل نمایشی تفصیلی شناور پیشنهادی</returns>
        public async Task<DetailAccountViewModel> GetNewChildDetailAccountAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var parent = await repository.GetByIDAsync(parentId ?? 0);
            if (parentId > 0 && parent == null)
            {
                return null;
            }

            var fullConfig = await Config.GetViewTreeConfigByViewAsync(ViewId.DetailAccount);
            var treeConfig = fullConfig.Current;
            if (parent != null && parent.Level + 1 == treeConfig.MaxDepth)
            {
                return new DetailAccountViewModel() { Level = -1 };
            }

            var childrenCodes = await GetChildrenCodesAsync(parentId);
            string newCode = GetNewDetailAccountCode(parent, childrenCodes, treeConfig);
            return GetNewChildDetailAccount(parent, newCode, treeConfig);
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور در سطح اول را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه تفصیلی های شناور در سطح اول</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync()
        {
            var detailAccounts = await Repository
                .GetAllQuery<DetailAccount>(ViewId.DetailAccount, facc => facc.Children)
                .Where(facc => facc.ParentId == null)
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .ToListAsync();
            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، شناورهای زیرمجموعه را برای تفصیلی شناور مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی های شناور زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetDetailAccountChildrenAsync(int detailId)
        {
            var children = await Repository
                .GetAllQuery<DetailAccount>(ViewId.DetailAccount, facc => facc.Children)
                .Where(facc => facc.ParentId == detailId)
                .Select(facc => Mapper.Map<AccountItemBriefViewModel>(facc))
                .ToListAsync();
            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک تفصیلی شناور را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="detailAccount">تفصیلی شناور مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی تفصیلی شناور ایجاد یا اصلاح شده</returns>
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

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور مشخص شده با شناسه عددی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="faccountId">شناسه عددی تفصیلی شناور مورد نظر برای حذف</param>
        public async Task DeleteDetailAccountAsync(int faccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId);
            if (detailAccount != null)
            {
                await DeleteAsync(repository, detailAccount);
                await UpdateLevelUsageAsync(detailAccount.Level);
            }
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="detailIds">مجموعه ای از شناسه های عددی تفصیلی های مورد نظر برای حذف</param>
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
                    await DeleteNoLogAsync(repository, detailAccount);
                }
            }

            await UpdateLevelUsageAsync(level);
            await OnEntityGroupDeleted(detailIds);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد تفصیلی شناور مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="detailAccount">مدل نمایشی تفصیلی شناور مورد نظر</param>
        /// <returns>اگر کد تفصیلی شناور تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateFullCodeAsync(DetailAccountViewModel detailAccount)
        {
            Verify.ArgumentNotNull(detailAccount, "detailAccount");
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            int count = await repository
                .GetCountByCriteriaAsync(facc => facc.Id != detailAccount.Id
                    && facc.FullCode == detailAccount.FullCode);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که نام تفصیلی شناور مورد نظر بین تفصیلی های همسطح
        /// با والد یکسان تکراری است یا نه
        /// </summary>
        /// <param name="detailAccount">مدل نمایشی تفصیلی شناور مورد نظر</param>
        /// <returns>اگر نام تفصیلی شناور تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        public async Task<bool> IsDuplicateNameAsync(DetailAccountViewModel detailAccount)
        {
            Verify.ArgumentNotNull(detailAccount, "detailAccount");
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            int count = await repository
                .GetCountByCriteriaAsync(facc => facc.Id != detailAccount.Id
                    && facc.ParentId == detailAccount.ParentId
                    && facc.Name == detailAccount.Name);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
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
                ada => ada.DetailId == faccountId);
            return (relatedAccounts > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده دارای شناور زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از شناور های موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده دارای شناور زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int faccountId)
        {
            bool? hasChildren = null;
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId, facc => facc.Children);
            if (detailAccount != null)
            {
                hasChildren = detailAccount.Children.Count > 0;
            }

            return hasChildren;
        }

        /// <summary>
        /// به روش آسنکرون، کد کامل تفصیلی شناور با شناسه داده شده را برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <returns>اگر تفصیلی شناور با شناسه داده شده وجود نداشته باشد مقدار خالی
        /// و در غیر این صورت کد کامل را برمی گرداند</returns>
        public async Task<string> GetDetailAccountFullCodeAsync(int faccountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDAsync(faccountId);
            if (detailAccount == null)
            {
                return String.Empty;
            }

            return detailAccount.FullCode;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.DetailAccount; }
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(DetailAccount entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7}",
                    AppStrings.Name, entity.Name, AppStrings.Code, entity.Code,
                    AppStrings.FullCode, entity.FullCode, AppStrings.Description, entity.Description)
                : null;
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="detailAccountViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="detailAccount">سطر اطلاعاتی موجود</param>
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

        private static string GetNewDetailAccountCode(
            DetailAccount parent, IEnumerable<string> existingCodes, ViewTreeConfig treeConfig)
        {
            int childLevel = (parent != null) ? parent.Level + 1 : 0;
            int codeLength = treeConfig.Levels[childLevel].CodeLength;
            string format = String.Format("D{0}", codeLength);
            var maxCode = (long)Math.Pow(10, codeLength) - 1;
            var lastCode = (existingCodes.Any()) ? Int64.Parse(existingCodes.Max()) : 0;
            var newCode = Math.Min(lastCode + 1, maxCode);
            return newCode.ToString(format);
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

        private async Task<IList<string>> GetChildrenCodesAsync(int? parentId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            return await repository
                .GetEntityQuery()
                .Where(facc => facc.ParentId == parentId)
                .Select(facc => facc.Code)
                .ToListAsync();
        }

        private DetailAccountViewModel GetNewChildDetailAccount(
            DetailAccount parent, string newCode, ViewTreeConfig treeConfig)
        {
            var childDetail = new DetailAccountViewModel()
            {
                Code = newCode,
                ParentId = parent?.Id,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                BranchId = UserContext.BranchId
            };
            childDetail.FullCode = (parent != null)
                ? parent.FullCode + childDetail.Code
                : childDetail.Code;
            if (parent != null)
            {
                childDetail.Level = (short)((parent.Level + 1 < treeConfig.MaxDepth)
                    ? parent.Level + 1
                    : -1);
            }

            return childDetail;
        }

        private readonly ISystemRepository _system;
        private readonly IRelationRepository _relationRepository;
    }
}

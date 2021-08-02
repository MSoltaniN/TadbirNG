using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///  عملیات مورد نیاز برای مدیریت اطلاعات دوره مالی را پیاده سازی می کند.
    /// </summary>
    public class FiscalPeriodRepository
        : EntityLoggingRepository<FiscalPeriod, FiscalPeriodViewModel>, IFiscalPeriodRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public FiscalPeriodRepository(IRepositoryContext context, IOperationLogRepository log)
            : base(context, log)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه دوره های مالی را که در شرکت جاری تعریف شده اند
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دوره های مالی تعریف شده در شرکت جاری</returns>
        public async Task<PagedList<FiscalPeriodViewModel>> GetFiscalPeriodsAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriods = await repository.GetByCriteriaAsync(await GetSecurityFilterAsync());
            await ReadAsync(gridOptions);
            return new PagedList<FiscalPeriodViewModel>(
                fiscalPeriods.Select(fp => Mapper.Map<FiscalPeriodViewModel>(fp)), gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون،دوره مالی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی یکی از دوره های مالی</param>
        /// <returns>دوره مالی مشخص شده با شناسه عددی</returns>
        public async Task<FiscalPeriodViewModel> GetFiscalPeriodAsync(int fperiodId)
        {
            FiscalPeriodViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(fperiodId);
            if (fiscalPeriod != null)
            {
                item = Mapper.Map<FiscalPeriodViewModel>(fiscalPeriod);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک دوره مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetFiscalPeriodRolesAsync(int fpId)
        {
            RelatedItemsViewModel periodRoles = null;
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var existing = await repository.GetByIDAsync(fpId, br => br.RoleFiscalPeriods);
            if (existing != null)
            {
                UnitOfWork.UseSystemContext();
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                var enabledRoleIds = existing.RoleFiscalPeriods.Select(rfp => rfp.RoleId);
                var enabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                var disabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => !enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);
                UnitOfWork.UseCompanyContext();

                periodRoles = new RelatedItemsViewModel() { Id = fpId };
                Array.ForEach(enabledRoles
                    .Concat(disabledRoles)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => periodRoles.RelatedItems.Add(item));
            }

            return periodRoles;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های دارای دسترسی به یک دوره مالی را ذخیره می کند
        /// </summary>
        /// <param name="periodRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        public async Task SaveFiscalPeriodRolesAsync(RelatedItemsViewModel periodRoles)
        {
            Verify.ArgumentNotNull(periodRoles, nameof(periodRoles));
            var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
            var existing = await repository.GetByCriteriaAsync(rfp => rfp.FiscalPeriodId == periodRoles.Id);
            if (AreRolesModified(existing, periodRoles))
            {
                if (existing.Count > 0)
                {
                    RemoveUnassignedRoles(repository, existing, periodRoles);
                }

                AddNewRoles(repository, existing, periodRoles);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.RoleAccess);
                Log.Description = await GetFiscalPeriodRoleDescriptionAsync(periodRoles.Id);
                await TrySaveLogAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دوره مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="fiscalPeriodView">دوره مالی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دوره مالی ایجاد یا اصلاح شده</returns>
        public async Task<FiscalPeriodViewModel> SaveFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriodView)
        {
            Verify.ArgumentNotNull(fiscalPeriodView, "fiscalPeriodView");
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            FiscalPeriod fiscalPeriod;
            if (fiscalPeriodView.Id == 0)
            {
                fiscalPeriod = Mapper.Map<FiscalPeriod>(fiscalPeriodView);
                await InsertAsync(repository, fiscalPeriod);
                await CopyInactiveAccountsAsync(fiscalPeriod.Id);
            }
            else
            {
                fiscalPeriod = await repository.GetByIDAsync(fiscalPeriodView.Id);
                if (fiscalPeriod != null)
                {
                    await UpdateAsync(repository, fiscalPeriod, fiscalPeriodView);
                }
            }

            return Mapper.Map<FiscalPeriodViewModel>(fiscalPeriod);
        }

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی دوره مالی مورد نظر برای حذف</param>
        public async Task DeleteFiscalPeriodAsync(int fperiodId)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(fperiodId);
            if (fiscalPeriod != null)
            {
                await DeleteInactiveAccounts(fperiodId);
                await DeleteAsync(repository, fiscalPeriod);
            }
        }

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه عددی را به همراه کلیه اطلاعات وابسته به آن حذف می کند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی دوره مالی مورد نظر برای حذف</param>
        public async Task DeleteFiscalPeriodWithDataAsync(int fperiodId)
        {
            await DeleteWithCascadeAsync(fperiodId);
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        public async Task DeleteFiscalPeriodsAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            foreach (int item in items)
            {
                var fiscalPeriod = await repository.GetByIDAsync(item);
                if (fiscalPeriod != null)
                {
                    await DeleteInactiveAccounts(item);
                    await DeleteNoLogAsync(repository, fiscalPeriod);
                }
            }

            await OnEntityGroupDeleted(items);
        }

        /// <summary>
        /// مشخص میکند که آیا تاریخ شروع دوره مالی بعد از تاریخ پایان دوره مالی است؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>اگر تاریخ شروع دوره مالی بعد از تاریخ پایان دوره مالی باشد مقدار "درست" در غیر این صورت مقدار "نادرست" برمیگرداند</returns>
        public bool IsStartDateAfterEndDate(FiscalPeriodViewModel fiscalPeriod)
        {
            return (fiscalPeriod.EndDate.Subtract(fiscalPeriod.StartDate).Days < 1);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا این دوره مالی با سایر دوره های مالی شرکت مربوطه هم پوشانی دارد یا خیر؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی هم پوشان با مدل نمایشی مورد نظر وجود داشته باشد مقدار "درست" در غیر این صورت مقدار "نادرست" برمیگرداند</returns>
        public async Task<bool> IsOverlapFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod)
        {
            Verify.ArgumentNotNull(fiscalPeriod, "fiscalPeriod");
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriods = await repository
                .GetByCriteriaAsync(
                fp => fp.CompanyId == fiscalPeriod.CompanyId && fp.Id != fiscalPeriod.Id
                && ((fiscalPeriod.StartDate.CompareWith(fp.StartDate) >= 0
                    && fiscalPeriod.StartDate.CompareWith(fp.EndDate) <= 0)
                || (fiscalPeriod.EndDate.CompareWith(fp.StartDate) >= 0
                    && fiscalPeriod.EndDate.CompareWith(fp.EndDate) <= 0)));

            return (fiscalPeriods.Count > 0);
        }

        /// <summary>
        /// مشخص می کند که آیا دوره مالی داده شده از نظر تاریخ شروع رو به جلو است یا نه؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>در صورتی که تاریخ شروع دوره بعد از تاریخ پایان دوره قبل باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsProgressiveFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod)
        {
            bool isProgressive = true;
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            if (fiscalPeriod.Id == 0)
            {
                var last = await repository
                    .GetEntityQuery()
                    .LastOrDefaultAsync();
                if (last != null)
                {
                    isProgressive = fiscalPeriod.StartDate.Date > last.EndDate.Date;
                }
            }
            else
            {
                var before = await repository
                    .GetEntityQuery()
                    .Where(fp => fp.Id < fiscalPeriod.Id)
                    .OrderByDescending(fp => fp.Id)
                    .FirstOrDefaultAsync();
                if (before != null)
                {
                    isProgressive = isProgressive && (fiscalPeriod.StartDate > before.EndDate);
                }

                var after = await repository
                    .GetEntityQuery()
                    .Where(fp => fp.Id > fiscalPeriod.Id)
                    .OrderBy(fp => fp.Id)
                    .FirstOrDefaultAsync();
                if (after != null)
                {
                    isProgressive = isProgressive && (fiscalPeriod.EndDate < after.StartDate);
                }
            }

            return isProgressive;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا دوره مالی مشخص شده قابل حذف است یا نه؟
        /// </summary>
        /// <param name="fiscalPeriodId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی مورد نظر در برنامه به طور مستقیم استفاده شده باشد
        /// مقدار "نادرست" و در غیر این صورت مقدار "درست" را برمی گرداند</returns>
        public async Task<bool> CanDeleteFiscalPeriodAsync(int fiscalPeriodId)
        {
            bool canDelete = true;
            var fiscalTypes = ModelCatalogue.GetAllOfType<FiscalEntity>();
            foreach (var type in fiscalTypes)
            {
                if (HasFiscalPeriodReference(type, fiscalPeriodId))
                {
                    canDelete = false;
                    break;
                }
            }

            if (canDelete)
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                int roleCount = await repository.GetCountByCriteriaAsync(
                    rfp => rfp.FiscalPeriodId == fiscalPeriodId);
                canDelete = (roleCount == 0);
            }

            return canDelete;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که دوره مالی با شناسه دیتابیسی داده شده
        /// سندی با وضعیت ثبت، ثبت قطعی، تأییدشده یا تصویب شده دارد یا نه
        /// </summary>
        /// <param name="fiscalPeriodId"></param>
        /// <returns></returns>
        public async Task<bool> HasCommittedVouchersAsync(int fiscalPeriodId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            int committedCount = await repository.GetCountByCriteriaAsync(
                voucher => voucher.FiscalPeriodId == fiscalPeriodId &&
                (voucher.StatusId != (int)DocumentStatusId.NotChecked ||
                voucher.ConfirmedById.HasValue || voucher.ApprovedById.HasValue));
            return committedCount > 0;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.FiscalPeriod; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="fiscalPeriodView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="fiscalPeriod">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(FiscalPeriodViewModel fiscalPeriodView, FiscalPeriod fiscalPeriod)
        {
            fiscalPeriod.Name = fiscalPeriodView.Name;
            fiscalPeriod.StartDate = fiscalPeriodView.StartDate;
            fiscalPeriod.EndDate = fiscalPeriodView.EndDate;
            fiscalPeriod.Description = fiscalPeriodView.Description;
            fiscalPeriod.CompanyId = fiscalPeriodView.CompanyId;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(FiscalPeriod entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5}",
                    AppStrings.StartDate, entity.StartDate, AppStrings.EndDate, entity.EndDate,
                    AppStrings.Description, entity.Description)
                : null;
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static bool AreRolesModified(
            IList<RoleFiscalPeriod> existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing
                .Select(rfp => rfp.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return !AreEqual(existingItems, enabledItems);
        }

        private static void RemoveUnassignedRoles(
            IRepository<RoleFiscalPeriod> repository, IList<RoleFiscalPeriod> existing,
            RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing
                .Select(rfp => rfp.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                var removed = existing
                    .Where(rfp => rfp.RoleId == id)
                    .Single();
                repository.Delete(removed);
            }
        }

        private void AddNewRoles(
            IRepository<RoleFiscalPeriod> repository, IList<RoleFiscalPeriod> existing,
            RelatedItemsViewModel roleItems)
        {
            var currentItems = existing.Select(rfp => rfp.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var roleFiscalPeriod = new RoleFiscalPeriod()
                {
                    FiscalPeriodId = roleItems.Id,
                    RoleId = item.Id
                };
                repository.Insert(roleFiscalPeriod);
            }
        }

        private async Task<string> GetFiscalPeriodRoleDescriptionAsync(int fiscalPeriodId)
        {
            string description = String.Empty;
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(fiscalPeriodId);
            if (fiscalPeriod != null)
            {
                string template = Context.Localize(AppStrings.RolesWithAccessToResource);
                string entity = Context.Localize(AppStrings.FiscalPeriod).ToLower();
                description = String.Format(template, entity, fiscalPeriod.Name);
            }

            return description;
        }

        private async Task<Expression<Func<FiscalPeriod, bool>>> GetSecurityFilterAsync()
        {
            if (!UserContext.Roles.Contains(AppConstants.AdminRoleId))
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                var periodIds = await repository
                    .GetEntityQuery()
                    .Where(rfp => UserContext.Roles.Contains(rfp.RoleId))
                    .Select(rc => rc.FiscalPeriodId)
                    .Distinct()
                    .ToListAsync();
                return fp => periodIds.Contains(fp.Id);
            }
            else
            {
                return fp => true;
            }
        }

        private async Task CopyInactiveAccountsAsync(int fpId)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var previous = await repository
                .GetEntityQuery()
                .Where(fp => fp.Id < fpId)
                .OrderByDescending(fp => fp.Id)
                .FirstOrDefaultAsync();
            if (previous != null)
            {
                var accountRepository = UnitOfWork.GetAsyncRepository<InactiveAccount>();
                var inactiveItems = await accountRepository.GetByCriteriaAsync(
                    acc => acc.FiscalPeriodId == previous.Id);
                foreach (var item in inactiveItems)
                {
                    var newItem = new InactiveAccount()
                    {
                        AccountId = item.AccountId,
                        FiscalPeriodId = fpId
                    };
                    accountRepository.Insert(newItem);
                }

                await UnitOfWork.CommitAsync();
            }
        }

        private async Task DeleteInactiveAccounts(int fpId)
        {
            var repository = UnitOfWork.GetAsyncRepository<InactiveAccount>();
            var inactiveItems = await repository.GetByCriteriaAsync(acc => acc.FiscalPeriodId == fpId);
            foreach (var inactiveItem in inactiveItems)
            {
                repository.Delete(inactiveItem);
            }

            await UnitOfWork.CommitAsync();
        }
    }
}

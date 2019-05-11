using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///  عملیات مورد نیاز برای مدیریت اطلاعات دوره مالی را پیاده سازی می کند.
    /// </summary>
    public class FiscalPeriodRepository : LoggingRepository<FiscalPeriod, FiscalPeriodViewModel>, IFiscalPeriodRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public FiscalPeriodRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata,
            IOperationLogRepository log)
            : base(unitOfWork, mapper, metadata, log)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه دوره های مالی را که در شرکت مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دوره های مالی تعریف شده در شرکت مشخص شده</returns>
        public async Task<IList<FiscalPeriodViewModel>> GetFiscalPeriodsAsync(int companyId, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriods = await repository
                .GetByCriteriaAsync(fp => fp.CompanyId == companyId);
            return fiscalPeriods
                .Select(item => Mapper.Map<FiscalPeriodViewModel>(item))
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد دوره های مالی تعریف شده در شرکت مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد دوره های مالی تعریف شده در شرکت مشخص شده</returns>
        public async Task<int> GetCountAsync(int companyId, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var items = await repository.GetByCriteriaAsync(fp => fp.CompanyId == companyId);
            return items
                .Select(fp => Mapper.Map<FiscalPeriodViewModel>(fp))
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی ایجاد شده در دوره مالی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>تعداد اسناد مالی ایجاد شده در دوره مالی</returns>
        public async Task<int> GetVoucherCountAsync(int fpId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            int voucherCount = await repository.GetCountByCriteriaAsync(v => v.FiscalPeriodId == fpId);
            return voucherCount;
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
        /// به روش آسنکرون، مشخص می کند که آیا در محدوده تاریخی داده شده دوره مالی تعریف شده یا نه
        /// </summary>
        /// <param name="start">شروع دوره مالی مورد نظر</param>
        /// <param name="end">پایان دوره مالی مورد نظر</param>
        /// <returns>اگر در محدوده تاریخی داده شده دوره مالی تعریف شده باشد، مقدار "درست"
        /// و در غیر این صورت مقدار "نادرست" را برمی گرداند.</returns>
        public async Task<bool> ExistsFiscalPeriodInRange(DateTime start, DateTime end)
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetFirstByCriteriaAsync(
                fp => fp.StartDate == start && fp.EndDate == end && fp.CompanyId == _currentContext.CompanyId);
            return (fiscalPeriod != null);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای دوره مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای دوره مالی</returns>
        public async Task<ViewViewModel> GetFiscalPeriodMetadataAsync()
        {
            return await Metadata.GetViewMetadataAsync<FiscalPeriod>();
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
            Verify.ArgumentNotNull(periodRoles, "periodRoles");
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var existing = await repository.GetByIDWithTrackingAsync(periodRoles.Id, r => r.RoleFiscalPeriods);
            if (existing != null && AreRolesModified(existing, periodRoles))
            {
                if (existing.RoleFiscalPeriods.Count > 0)
                {
                    RemoveInaccessibleRoles(existing, periodRoles);
                }

                AddNewRoles(existing, periodRoles);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
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
            FiscalPeriod fiscalPeriod = default(FiscalPeriod);
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            if (fiscalPeriodView.Id == 0)
            {
                fiscalPeriod = Mapper.Map<FiscalPeriod>(fiscalPeriodView);
                await InsertAsync(repository, fiscalPeriod);
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
                await DeleteAsync(repository, fiscalPeriod);
            }
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
        /// به روش آسنکرون، مشخص می کند که آیا دوره مالی مشخص شده قابل حذف است یا نه؟
        /// </summary>
        /// <param name="fperiodId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی مورد نظر در برنامه به طور مستقیم استفاده شده باشد
        /// مقدار "نادرست" و در غیر این صورت مقدار "درست" را برمی گرداند</returns>
        public async Task<bool> CanDeleteFiscalPeriodAsync(int fperiodId)
        {
            bool isUsed = await IsFiscalPeriodUsedBy<Account>(fperiodId)
                || await IsFiscalPeriodUsedBy<DetailAccount>(fperiodId)
                || await IsFiscalPeriodUsedBy<CostCenter>(fperiodId)
                || await IsFiscalPeriodUsedBy<Project>(fperiodId)
                || await IsFiscalPeriodUsedBy<Voucher>(fperiodId);
            if (!isUsed)
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                int count = await repository.GetCountByCriteriaAsync(
                    rfp => rfp.FiscalPeriodId == fperiodId);
                isUsed = count > 0;
            }

            return !isUsed;
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
                    "Name : {1}{0}Start Date : {2}{0}End Date : {3}{0}Description : {4}",
                    Environment.NewLine, entity.Name, entity.StartDate, entity.EndDate, entity.Description)
                : null;
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static bool AreRolesModified(FiscalPeriod existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing.RoleFiscalPeriods
                .Select(rfp => rfp.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static void RemoveInaccessibleRoles(FiscalPeriod existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing.RoleFiscalPeriods
                .Select(rfp => rfp.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.RoleFiscalPeriods.Remove(existing.RoleFiscalPeriods
                    .Where(rfp => rfp.RoleId == id)
                    .Single());
            }
        }

        private void AddNewRoles(FiscalPeriod existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = existing.RoleFiscalPeriods.Select(rfp => rfp.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var roleFiscalPeriod = new RoleFiscalPeriod()
                {
                    FiscalPeriod = existing,
                    FiscalPeriodId = existing.Id,
                    RoleId = item.Id
                };
                existing.RoleFiscalPeriods.Add(roleFiscalPeriod);
            }
        }

        private async Task<bool> IsFiscalPeriodUsedBy<TFiscalEntity>(int fperiodId)
            where TFiscalEntity : class, IFiscalEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TFiscalEntity>();
            int count = await repository.GetCountByCriteriaAsync(
                entity => entity.FiscalPeriodId == fperiodId);
            return (count > 0);
        }
    }
}

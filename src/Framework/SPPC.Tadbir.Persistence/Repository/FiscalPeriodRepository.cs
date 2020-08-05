﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;

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
                OnEntityAction(OperationId.RoleAccess);
                Log.Description = Context.Localize(await GetFiscalPeriodRoleDescriptionAsync(existing));
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
        /// <param name="fperiodId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی مورد نظر در برنامه به طور مستقیم استفاده شده باشد
        /// مقدار "نادرست" و در غیر این صورت مقدار "درست" را برمی گرداند</returns>
        public async Task<bool> CanDeleteFiscalPeriodAsync(int fperiodId)
        {
            bool canDelete = true;
            var fiscalTypes = ModelCatalogue.GetAllOfType<FiscalEntity>();
            foreach (var type in fiscalTypes)
            {
                if (HasFiscalPeriodReference(type, fperiodId))
                {
                    canDelete = false;
                    break;
                }
            }

            if (canDelete)
            {
                var repository = UnitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                int roleCount = await repository.GetCountByCriteriaAsync(
                    rfp => rfp.FiscalPeriodId == fperiodId);
                canDelete = (roleCount == 0);
            }

            return canDelete;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین دوره مالی را در محل ذخیره ایجاد می کند
        /// </summary>
        /// <param name="fiscalPeriod">دوره مالی مورد نظر برای ایجاد</param>
        /// <returns>اطلاعات نمایشی دوره مالی ایجاد شده</returns>
        public async Task<FiscalPeriodViewModel> SaveInitialFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod)
        {
            Verify.ArgumentNotNull(fiscalPeriod, "fiscalPeriod");
            FiscalPeriod newFiscalPeriod = default(FiscalPeriod);

            UnitOfWork.UseSystemContext();
            CompanyConnection = await BuildConnectionStringAsync(fiscalPeriod.CompanyId);
            UnitOfWork.UseCompanyContext();

            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            newFiscalPeriod = Mapper.Map<FiscalPeriod>(fiscalPeriod);
            await InsertAsync(repository, newFiscalPeriod);
            return Mapper.Map<FiscalPeriodViewModel>(newFiscalPeriod);
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
                    FiscalPeriodId = existing.Id,
                    RoleId = item.Id
                };
                existing.RoleFiscalPeriods.Add(roleFiscalPeriod);
            }
        }

        private async Task<string> GetFiscalPeriodRoleDescriptionAsync(FiscalPeriod fiscalPeriod)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0} : {1} , {2} : ",
                AppStrings.FiscalPeriod, fiscalPeriod.Name, AppStrings.RolesWithAccess);
            if (fiscalPeriod.RoleFiscalPeriods.Count > 0)
            {
                UnitOfWork.UseSystemContext();
                var repository = UnitOfWork.GetAsyncRepository<Role>();
                var roles = await repository.GetByCriteriaAsync(r => fiscalPeriod.RoleFiscalPeriods
                    .Select(rfp => rfp.RoleId)
                    .Contains(r.Id));
                builder.Append(String.Join(" , ", roles.Select(r => r.Name)));
                UnitOfWork.UseCompanyContext();
            }
            else
            {
                builder.Append(AppStrings.None);
            }

            return builder.ToString();
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
    }
}

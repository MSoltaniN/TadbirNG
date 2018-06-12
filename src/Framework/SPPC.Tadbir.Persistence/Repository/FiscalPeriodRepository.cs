using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
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
    public class FiscalPeriodRepository : IFiscalPeriodRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadataRepository">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public FiscalPeriodRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadataRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _metadataRepository = metadataRepository;
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
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriods = await repository
                .GetByCriteriaAsync(
                    fp => fp.Company.Id == companyId,
                    gridOptions, fp => fp.Company);
            return fiscalPeriods
                .Select(item => _mapper.Map<FiscalPeriodViewModel>(item))
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
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    fp => fp.Company.Id == companyId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون،دوره مالی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی یکی از دوره های مالی</param>
        /// <returns>دوره مالی مشخص شده با شناسه عددی</returns>
        public async Task<FiscalPeriodViewModel> GetFiscalPeriodAsync(int fperiodId)
        {
            FiscalPeriodViewModel item = null;
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(
                fperiodId,
                fp => fp.Company);
            if (fiscalPeriod != null)
            {
                item = _mapper.Map<FiscalPeriodViewModel>(fiscalPeriod);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای دوره مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای دوره مالی</returns>
        public async Task<EntityViewModel> GetFiscalPeriodMetadataAsync()
        {
            return await _metadataRepository.GetEntityMetadataAsync<FiscalPeriod>();
        }

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک دوره مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        public async Task<RelatedItemsViewModel> GetFiscalPeriodRolesAsync(int fpId)
        {
            RelatedItemsViewModel periodRoles = null;
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var existing = await repository
                .GetEntityQuery()
                .Include(fp => fp.RoleFiscalPeriods)
                    .ThenInclude(rfp => rfp.Role)
                .Where(fp => fp.Id == fpId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledRoles = existing.RoleFiscalPeriods
                    .Select(rfp => rfp.Role)
                    .Select(r => _mapper.Map<RelatedItemViewModel>(r))
                    .ToArray();
                var roleRepository = _unitOfWork.GetAsyncRepository<Role>();
                var allRoles = await roleRepository
                    .GetAllAsync();
                var disabledRoles = allRoles
                    .Select(r => _mapper.Map<RelatedItemViewModel>(r))
                    .Except(enabledRoles, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);

                periodRoles = _mapper.Map<RelatedItemsViewModel>(existing);
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
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var existing = await repository.GetByIDWithTrackingAsync(periodRoles.Id, r => r.RoleFiscalPeriods);
            if (existing != null && AreRolesModified(existing, periodRoles))
            {
                if (existing.RoleFiscalPeriods.Count > 0)
                {
                    RemoveInaccessibleRoles(existing, periodRoles);
                }

                AddNewRoles(existing, periodRoles);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دوره مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="fiscalPeriod">دوره مالی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دوره مالی ایجاد یا اصلاح شده</returns>
        public async Task<FiscalPeriodViewModel> SaveFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod)
        {
            Verify.ArgumentNotNull(fiscalPeriod, "fiscalPeriod");
            FiscalPeriod fiscalPeriodModel = default(FiscalPeriod);
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            if (fiscalPeriod.Id == 0)
            {
                fiscalPeriodModel = _mapper.Map<FiscalPeriod>(fiscalPeriod);
                repository.Insert(fiscalPeriodModel);
            }
            else
            {
                fiscalPeriodModel = await repository.GetByIDAsync(
                    fiscalPeriod.Id, fp => fp.Company);
                if (fiscalPeriodModel != null)
                {
                    UpdateExistingFiscalPeriod(fiscalPeriod, fiscalPeriodModel);
                    repository.Update(fiscalPeriodModel);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<FiscalPeriodViewModel>(fiscalPeriodModel);
        }

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی دوره مالی مورد نظر برای حذف</param>
        public async Task DeleteFiscalPeriodAsync(int fperiodId)
        {
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(fperiodId);
            if (fiscalPeriod != null)
            {
                fiscalPeriod.Company = null;
                repository.Delete(fiscalPeriod);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// مشخص میکند که آیا تاریخ شروع دوره مالی بعد از تاریخ پایان دوره مالی است؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>اگر تاریخ شروع دوره مالی بعد از تاریخ پایان دوره مالی باشد مقدار "درست" در غیر این صورت مقدار "نادرست" برمیگرداند</returns>
        public bool IsStartDateAfterEndDate(FiscalPeriodViewModel fiscalPeriod)
        {
            if (fiscalPeriod.EndDate.Subtract(fiscalPeriod.StartDate).Days < 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا این دوره مالی با سایر دوره های مالی شرکت مربوطه هم پوشانی دارد یا خیر؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی هم پوشان با مدل نمایشی مورد نظر وجود داشته باشد مقدار "درست" در غیر این صورت مقدار "نادرست" برمیگرداند</returns>
        public async Task<bool> IsOverlapFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod)
        {
            Verify.ArgumentNotNull(fiscalPeriod, "fiscalPeriod");
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriods = await repository
                .GetByCriteriaAsync(
                fp => fp.Company.Id == fiscalPeriod.CompanyId && fp.Id != fiscalPeriod.Id
                && ((fp.StartDate > fiscalPeriod.StartDate && fp.StartDate < fiscalPeriod.EndDate)
                || (fp.StartDate < fiscalPeriod.StartDate && fp.EndDate > fiscalPeriod.EndDate)
                || (fp.EndDate > fiscalPeriod.StartDate && fp.EndDate < fiscalPeriod.EndDate)));

            return (fiscalPeriods.Count > 0);
        }

        private static void UpdateExistingFiscalPeriod(FiscalPeriodViewModel fiscalPeriodModel, FiscalPeriod fiscalPeriod)
        {
            fiscalPeriod.Name = fiscalPeriodModel.Name;
            fiscalPeriod.StartDate = fiscalPeriodModel.StartDate;
            fiscalPeriod.EndDate = fiscalPeriodModel.EndDate;
            fiscalPeriod.Description = fiscalPeriodModel.Description;
            fiscalPeriod.Company.Id = fiscalPeriodModel.CompanyId;
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
            var repository = _unitOfWork.GetRepository<Role>();
            var currentItems = existing.RoleFiscalPeriods.Select(rfp => rfp.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var role = repository.GetByIDWithTracking(item.Id);
                var roleFiscalPeriod = new RoleFiscalPeriod()
                {
                    FiscalPeriod = existing,
                    FiscalPeriodId = existing.Id,
                    Role = role,
                    RoleId = role.Id
                };
                existing.RoleFiscalPeriods.Add(roleFiscalPeriod);
            }
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataRepository _metadataRepository;
    }
}

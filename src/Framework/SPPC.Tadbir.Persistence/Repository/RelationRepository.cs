using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارتباطات بین مولفه های مختلف بردار حساب را پیاده سازی می کند
    /// </summary>
    public class RelationRepository : IRelationRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="itemRepository">پیاده سازی اینترفیس مربوط به عملیات بردار حساب</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        public RelationRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IAccountItemRepository itemRepository,
            ISecureRepository repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _itemRepository = itemRepository;
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsAsync(GridOptions gridOptions = null)
        {
            var accounts = await _itemRepository.GetLeafAccountsAsync(gridOptions);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsAsync(
            bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var detailAccounts = useLeafItems
                ? await _itemRepository.GetLeafDetailAccountsAsync(gridOptions)
                : await _itemRepository.GetRootDetailAccountsAsync(gridOptions);
            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersAsync(
            bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var costCenters = useLeafItems
                ? await _itemRepository.GetLeafCostCentersAsync(gridOptions)
                : await _itemRepository.GetRootCostCentersAsync(gridOptions);
            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>پروژه های قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsAsync(
            bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var projects = useLeafItems
                ? await _itemRepository.GetLeafProjectsAsync(gridOptions)
                : await _itemRepository.GetRootProjectsAsync(gridOptions);
            return projects;
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سرفصل های حسابداری قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableAccountsLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafAccountsAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableDetailAccountsLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafDetailAccountsAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableCostCentersLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafCostCentersAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>پروژه های قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetUsableProjectsLookupAsync(
            GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafProjectsAsync(gridOptions);
            return items;
        }

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی بردار حساب داده شده را در ارتباطات موجود جستجو کرده
        /// و در صورت پیدا نکردن بردار حساب، وضعیت اشکال موجود را به صورت شناسه متن چندزبانه خطا برمی گرداند
        /// </summary>
        /// <param name="fullAccount">مدل نمایشی بردار حساب مورد جستجو</param>
        /// <returns>در صورت پیدا نکردن بردار حساب، شناسه متن چندزبانه خطا و در صورت پیدا کردن
        /// رشته خالی را برمی گرداند</returns>
        public async Task<string> LookupFullAccountAsync(FullAccountViewModel fullAccount)
        {
            Verify.ArgumentNotNull(fullAccount, nameof(fullAccount));
            var account = await _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children,
                    acc => acc.AccountDetailAccounts, acc => acc.AccountCostCenters, acc => acc.AccountProjects)
                .Where(acc => acc.Id == fullAccount.Account.Id)
                .SingleOrDefaultAsync();
            string errorKey = EnsureValidAccountInFullAccount(account);
            if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            var criteria = GetFullAccountCriteria(account);
            errorKey = EnsureNoMissingItemInFullAccount(criteria, fullAccount);
            if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            errorKey = await EnsureValidItemsInFullAccountAsync(criteria, fullAccount);
            if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            errorKey = EnsureValidRelationsInFullAccount(account, fullAccount, criteria);
            if (!String.IsNullOrEmpty(errorKey))
            {
                return errorKey;
            }

            return String.Empty;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountDetailAccountsAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var existingItemsQuery = relationRepository
                .GetEntityQuery()
                .Where(ada => ada.AccountId == accountId);
            if (leafOnly)
            {
                existingItemsQuery = existingItemsQuery
                    .Where(ada => ada.DetailAccount.Children.Count == 0);
            }

            var relatedDetailIds = await existingItemsQuery
                .Select(ada => ada.DetailId)
                .ToListAsync();
            var detailAccounts = await _repository
                .GetAllQuery<DetailAccount>(ViewName.DetailAccount)
                .Where(facc => relatedDetailIds.Contains(facc.Id))
                .Select(facc => _mapper.Map<AccountItemBriefViewModel>(facc))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(detailAccounts.ToArray(), facc => facc.IsSelected = true);
            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountCostCentersAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
            var existingItemsQuery = relationRepository
                .GetEntityQuery()
                .Where(ac => ac.AccountId == accountId);
            if (leafOnly)
            {
                existingItemsQuery = existingItemsQuery
                    .Where(ac => ac.CostCenter.Children.Count == 0);
            }

            var relatedCenterIds = await existingItemsQuery
                .Select(ac => ac.CostCenterId)
                .ToListAsync();
            var costCenters = await _repository
                .GetAllQuery<CostCenter>(ViewName.CostCenter)
                .Where(cc => relatedCenterIds.Contains(cc.Id))
                .Select(cc => _mapper.Map<AccountItemBriefViewModel>(cc))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(costCenters.ToArray(), cc => cc.IsSelected = true);
            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های مرتبط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از پروژه های مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountProjectsAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
            var existingItemsQuery = relationRepository
                .GetEntityQuery()
                .Where(ap => ap.AccountId == accountId);
            if (leafOnly)
            {
                existingItemsQuery = existingItemsQuery
                    .Where(ap => ap.Project.Children.Count == 0);
            }

            var relatedProjectIds = await existingItemsQuery
                .Select(ap => ap.ProjectId)
                .ToListAsync();
            var projects = await _repository
                .GetAllQuery<Project>(ViewName.Project)
                .Where(prj => relatedProjectIds.Contains(prj.Id))
                .Select(prj => _mapper.Map<AccountItemBriefViewModel>(prj))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(projects.ToArray(), prj => prj.IsSelected = true);
            return projects;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetDetailAccountAccountsAsync(
            int detailId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.DetailId == detailId
                    && ada.Account.Children.Count == 0)
                .Select(ada => ada.AccountId)
                .ToListAsync();
            var accounts = await _repository
                .GetAllQuery<Account>(ViewName.Account)
                .Where(acc => relatedAccountIds.Contains(acc.Id))
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(accounts.ToArray(), acc => acc.IsSelected = true);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه دیتابیسی یکی از مراکز هزینه موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetCostCenterAccountsAsync(
            int costCenterId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.CostCenterId == costCenterId
                    && ac.Account.Children.Count == 0)
                .Select(ac => ac.AccountId)
                .ToListAsync();
            var accounts = await _repository
                .GetAllQuery<Account>(ViewName.Account)
                .Where(acc => relatedAccountIds.Contains(acc.Id))
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(accounts.ToArray(), acc => acc.IsSelected = true);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با پروژه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی یکی از پروژه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با پروژه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetProjectAccountsAsync(
            int projectId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.ProjectId == projectId
                    && ap.Account.Children.Count == 0)
                .Select(ap => ap.AccountId)
                .ToListAsync();
            var accounts = await _repository
                .GetAllQuery<Account>(ViewName.Account)
                .Where(acc => relatedAccountIds.Contains(acc.Id))
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(accounts.ToArray(), acc => acc.IsSelected = true);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت تفصیلی های شناور مرتبط با یک حساب را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات تفصیلی های شناور مرتبط با یک حساب</param>
        public async Task SaveAccountDetailAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountDetailAccounts);
            if (existing != null)
            {
                if (existing.AccountDetailAccounts.Count > 0)
                {
                    RemoveDisconnectedDetailAccounts(existing, relations);
                }

                await AddNewAccountDetailAccounts(existing, relations);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت مراکز هزینه مرتبط با یک حساب را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات مراکز هزینه مرتبط با یک حساب</param>
        public async Task SaveAccountCostCentersAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountCostCenters);
            if (existing != null)
            {
                if (existing.AccountCostCenters.Count > 0)
                {
                    RemoveDisconnectedCostCenters(existing, relations);
                }

                await AddNewAccountCostCenters(existing, relations);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت پروژه های مرتبط با یک حساب را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات پروژه های مرتبط با یک حساب</param>
        public async Task SaveAccountProjectsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var existing = await repository.GetByIDWithTrackingAsync(
                relations.Id, acc => acc.AccountProjects);
            if (existing != null)
            {
                if (existing.AccountProjects.Count > 0)
                {
                    RemoveDisconnectedProjects(existing, relations);
                }

                await AddNewAccountProjects(existing, relations);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت حساب های مرتبط با یک تفصیلی شناور را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک تفصیلی شناور</param>
        public async Task SaveDetailAccountAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var existing = await repository.GetByIDWithTrackingAsync(relations.Id, facc => facc.AccountDetailAccounts);
            if (existing != null && AreRelationsModified(
                    existing.AccountDetailAccounts
                        .Select(ada => ada.AccountId)
                        .ToArray(),
                    relations))
            {
                if (existing.AccountDetailAccounts.Count > 0)
                {
                    RemoveDisconnectedAccounts(existing, relations);
                }

                AddConnectedAccounts(existing, relations);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت حساب های مرتبط با یک مرکز هزینه را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک مرکز هزینه</param>
        public async Task SaveCostCenterAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var existing = await repository.GetByIDWithTrackingAsync(relations.Id, cc => cc.AccountCostCenters);
            if (existing != null && AreRelationsModified(
                    existing.AccountCostCenters
                        .Select(ac => ac.AccountId)
                        .ToArray(),
                    relations))
            {
                if (existing.AccountCostCenters.Count > 0)
                {
                    RemoveDisconnectedAccounts(existing, relations);
                }

                AddConnectedAccounts(existing, relations);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت حساب های مرتبط با یک پروژه را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک پروژه</param>
        public async Task SaveProjectAccountsAsync(AccountItemRelationsViewModel relations)
        {
            Verify.ArgumentNotNull(relations, "relations");
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var existing = await repository.GetByIDWithTrackingAsync(relations.Id, prj => prj.AccountProjects);
            if (existing != null && AreRelationsModified(
                    existing.AccountProjects
                        .Select(ap => ap.AccountId)
                        .ToArray(),
                    relations))
            {
                if (existing.AccountProjects.Count > 0)
                {
                    RemoveDisconnectedAccounts(existing, relations);
                }

                AddConnectedAccounts(existing, relations);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور قابل ارتباط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور قابل ارتباط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedDetailIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.AccountId == accountId)
                .Select(ada => ada.DetailId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<DetailAccount>(ViewName.DetailAccount, facc => facc.Children)
                .Where(facc => !relatedDetailIds.Contains(facc.Id));
            if (useLeafItems)
            {
                query = query.Where(facc => facc.Children.Count == 0);
            }

            return await query
                .Select(facc => _mapper.Map<AccountItemBriefViewModel>(facc))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه قابل ارتباط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه قابل ارتباط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedCenterIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.AccountId == accountId)
                .Select(ac => ac.CostCenterId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<CostCenter>(ViewName.CostCenter, cc => cc.Children)
                .Where(cc => !relatedCenterIds.Contains(cc.Id));
            if (useLeafItems)
            {
                query = query.Where(cc => cc.Children.Count == 0);
            }

            return await query
                .Select(cc => _mapper.Map<AccountItemBriefViewModel>(cc))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های قابل ارتباط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های قابل ارتباط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
            var relatedProjectIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.AccountId == accountId)
                .Select(ap => ap.ProjectId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<Project>(ViewName.Project, prj => prj.Children)
                .Where(prj => !relatedProjectIds.Contains(prj.Id));
            if (useLeafItems)
            {
                query = query.Where(prj => prj.Children.Count == 0);
            }

            return await query
                .Select(prj => _mapper.Map<AccountItemBriefViewModel>(prj))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با تفصیلی شناور مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با تفصیلی شناور مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForDetailAccountAsync(
            int detailId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.DetailId == detailId)
                .Select(ada => ada.AccountId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id)
                    && acc.Children.Count == 0);

            return await query
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با مرکز هزینه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با مرکز هزینه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForCostCenterAsync(
            int costCenterId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.CostCenterId == costCenterId)
                .Select(ac => ac.AccountId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id)
                    && acc.Children.Count == 0);

            return await query
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با پروژه مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با پروژه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForProjectAsync(
            int projectId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.ProjectId == projectId)
                .Select(ap => ap.AccountId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id)
                    && acc.Children.Count == 0);

            return await query
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک تفصیلی شناور جدید انجام می دهد
        /// </summary>
        /// <param name="insertedDetailId">شناسه دیتابیسی تفصیلی شناور ایجاد شده</param>
        public async Task OnDetailAccountInsertedAsync(int insertedDetailId)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetByIDWithTrackingAsync(
                insertedDetailId, facc => facc.AccountDetailAccounts);
            if (detailAccount != null && detailAccount.ParentId.HasValue)
            {
                int count = detailAccount.AccountDetailAccounts.Count;
                var parent = await repository
                    .GetEntityWithTrackingQuery()
                        .Include(facc => facc.AccountDetailAccounts)
                            .ThenInclude(ada => ada.Account)
                    .Where(facc => facc.Id == detailAccount.ParentId.Value)
                    .SingleAsync();
                foreach (var relation in parent.AccountDetailAccounts)
                {
                    var accountDetailAccount = new AccountDetailAccount()
                    {
                        Account = relation.Account,
                        AccountId = relation.AccountId,
                        DetailAccount = detailAccount,
                        DetailId = detailAccount.Id
                    };
                    detailAccount.AccountDetailAccounts.Add(accountDetailAccount);
                }

                if (detailAccount.AccountDetailAccounts.Count != count)
                {
                    repository.Update(detailAccount);
                    await _unitOfWork.CommitAsync();
                }
            }
        }

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک مرکز هزینه جدید انجام می دهد
        /// </summary>
        /// <param name="insertedCenterId">شناسه دیتابیسی مرکز هزینه ایجاد شده</param>
        public async Task OnCostCenterInsertedAsync(int insertedCenterId)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDWithTrackingAsync(
                insertedCenterId, cc => cc.AccountCostCenters);
            if (costCenter != null && costCenter.ParentId.HasValue)
            {
                int count = costCenter.AccountCostCenters.Count;
                var parent = await repository
                    .GetEntityWithTrackingQuery()
                        .Include(cc => cc.AccountCostCenters)
                            .ThenInclude(ac => ac.Account)
                    .Where(cc => cc.Id == costCenter.ParentId.Value)
                    .SingleAsync();
                foreach (var relation in parent.AccountCostCenters)
                {
                    var accountCostCenter = new AccountCostCenter()
                    {
                        Account = relation.Account,
                        AccountId = relation.AccountId,
                        CostCenter = costCenter,
                        CostCenterId = costCenter.Id
                    };
                    costCenter.AccountCostCenters.Add(accountCostCenter);
                }

                if (costCenter.AccountCostCenters.Count != count)
                {
                    repository.Update(costCenter);
                    await _unitOfWork.CommitAsync();
                }
            }
        }

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک پروژه جدید انجام می دهد
        /// </summary>
        /// <param name="insertedProjectId">شناسه دیتابیسی پروژه ایجاد شده</param>
        public async Task OnProjectInsertedAsync(int insertedProjectId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDWithTrackingAsync(
                insertedProjectId, prj => prj.AccountProjects);
            if (project != null && project.ParentId.HasValue)
            {
                int count = project.AccountProjects.Count;
                var parent = await repository
                    .GetEntityWithTrackingQuery()
                        .Include(prj => prj.AccountProjects)
                            .ThenInclude(ap => ap.Account)
                    .Where(prj => prj.Id == project.ParentId.Value)
                    .SingleAsync();
                foreach (var relation in parent.AccountProjects)
                {
                    var accountProject = new AccountProject()
                    {
                        Account = relation.Account,
                        AccountId = relation.AccountId,
                        Project = project,
                        ProjectId = project.Id
                    };
                    project.AccountProjects.Add(accountProject);
                }

                if (project.AccountProjects.Count != count)
                {
                    repository.Update(project);
                    await _unitOfWork.CommitAsync();
                }
            }
        }

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای برای خواندن اطلاعات وابسته به شعبه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _repository.SetCurrentContext(userContext);
            _itemRepository.SetCurrentContext(userContext);
        }

        private static bool AreRelationsModified(int[] existingIds, AccountItemRelationsViewModel relations)
        {
            var connectedItems = relations
                .RelatedItemIds
                .ToArray();
            return (!AreEqual(existingIds, connectedItems));
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static void RemoveDisconnectedDetailAccounts(Account existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = relations.RelatedItemIds;
            var disconnectedItems = existing.AccountDetailAccounts
                .Select(ada => ada.DetailId)
                .Where(id => !currentItems.Contains(id))
                .ToList();
            foreach (int id in disconnectedItems)
            {
                existing.AccountDetailAccounts.Remove(existing.AccountDetailAccounts
                    .Where(ada => ada.DetailId == id)
                    .Single());
            }
        }

        private static void RemoveDisconnectedCostCenters(Account existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = relations.RelatedItemIds;
            var disconnectedItems = existing.AccountCostCenters
                .Select(ac => ac.CostCenterId)
                .Where(id => !currentItems.Contains(id))
                .ToList();
            foreach (int id in disconnectedItems)
            {
                existing.AccountCostCenters.Remove(existing.AccountCostCenters
                    .Where(ac => ac.CostCenterId == id)
                    .Single());
            }
        }

        private static void RemoveDisconnectedProjects(Account existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = relations.RelatedItemIds;
            var disconnectedItems = existing.AccountProjects
                .Select(ap => ap.ProjectId)
                .Where(id => !currentItems.Contains(id))
                .ToList();
            foreach (int id in disconnectedItems)
            {
                existing.AccountProjects.Remove(existing.AccountProjects
                    .Where(ap => ap.ProjectId == id)
                    .Single());
            }
        }

        private static void RemoveDisconnectedAccounts(DetailAccount existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = relations.RelatedItemIds;
            var disconnectedItems = existing.AccountDetailAccounts
                .Select(ada => ada.AccountId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in disconnectedItems)
            {
                existing.AccountDetailAccounts.Remove(existing.AccountDetailAccounts
                    .Where(ada => ada.AccountId == id)
                    .Single());
            }
        }

        private static void RemoveDisconnectedAccounts(CostCenter existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = relations.RelatedItemIds;
            var disconnectedItems = existing.AccountCostCenters
                .Select(ac => ac.AccountId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in disconnectedItems)
            {
                existing.AccountCostCenters.Remove(existing.AccountCostCenters
                    .Where(ac => ac.AccountId == id)
                    .Single());
            }
        }

        private static void RemoveDisconnectedAccounts(Project existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = relations.RelatedItemIds;
            var disconnectedItems = existing.AccountProjects
                .Select(ap => ap.AccountId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in disconnectedItems)
            {
                existing.AccountProjects.Remove(existing.AccountProjects
                    .Where(ap => ap.AccountId == id)
                    .Single());
            }
        }

        private static string EnsureValidAccountInFullAccount(Account account)
        {
            string errorKey = String.Empty;
            if (account == null)
            {
                errorKey = FullAccountError.InvalidAccountInFullAccount;
            }
            else if (account.Children.Count > 0)
            {
                errorKey = FullAccountError.NonLeafAccountInFullAccount;
            }

            return errorKey;
        }

        private static string EnsureNoMissingItemInFullAccount(
            FullAccountCriteriaViewModel criteria, FullAccountViewModel fullAccount)
        {
            string errorKey = String.Empty;
            if (criteria.RequiresDetailAccount && fullAccount.DetailAccount.Id == 0)
            {
                errorKey = FullAccountError.MissingDetailAccountInFullAccount;
            }
            else if (criteria.RequiresCostCenter && fullAccount.CostCenter.Id == 0)
            {
                errorKey = FullAccountError.MissingCostCenterInFullAccount;
            }
            else if (criteria.RequiresProject && fullAccount.Project.Id == 0)
            {
                errorKey = FullAccountError.MissingProjectInFullAccount;
            }

            return errorKey;
        }

        private static string EnsureValidRelationsInFullAccount(
            Account account, FullAccountViewModel fullAccount, FullAccountCriteriaViewModel criteria)
        {
            if (criteria.RequiresDetailAccount || fullAccount.DetailAccount.Id > 0)
            {
                bool isValidRelation = account.AccountDetailAccounts
                    .Select(ada => ada.DetailId)
                    .Contains(fullAccount.DetailAccount.Id);
                if (!isValidRelation)
                {
                    return FullAccountError.UnrelatedDetailAccountInFullAccount;
                }
            }

            if (criteria.RequiresCostCenter || fullAccount.CostCenter.Id > 0)
            {
                bool isValidRelation = account.AccountCostCenters
                    .Select(ac => ac.CostCenterId)
                    .Contains(fullAccount.CostCenter.Id);
                if (!isValidRelation)
                {
                    return FullAccountError.UnrelatedCostCenterInFullAccount;
                }
            }

            if (criteria.RequiresProject || fullAccount.Project.Id > 0)
            {
                bool isValidRelation = account.AccountProjects
                    .Select(ap => ap.ProjectId)
                    .Contains(fullAccount.Project.Id);
                if (!isValidRelation)
                {
                    return FullAccountError.UnrelatedProjectInFullAccount;
                }
            }

            return String.Empty;
        }

        private static FullAccountCriteriaViewModel GetFullAccountCriteria(Account account)
        {
            return new FullAccountCriteriaViewModel()
            {
                AccountId = account.Id,
                RequiresDetailAccount = account.AccountDetailAccounts.Count > 0,
                RequiresCostCenter = account.AccountCostCenters.Count > 0,
                RequiresProject = account.AccountProjects.Count > 0
            };
        }

        private async Task AddNewAccountDetailAccounts(Account existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = existing.AccountDetailAccounts.Select(ada => ada.DetailId);
            var newItems = relations.RelatedItemIds.Where(id => !currentItems.Contains(id));
            foreach (int detailId in newItems)
            {
                await AddNewAccountDetailAccount(existing, detailId);
            }
        }

        private async Task AddNewAccountDetailAccount(Account existing, int detailId)
        {
            var existingRelation = existing.AccountDetailAccounts
                .Where(ada => ada.AccountId == existing.Id && ada.DetailId == detailId)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = _unitOfWork.GetRepository<DetailAccount>();
            var detailAccount = repository.GetByIDWithTracking(detailId, facc => facc.Children);
            var accountDetailAccount = new AccountDetailAccount()
            {
                Account = existing,
                AccountId = existing.Id,
                DetailAccount = detailAccount,
                DetailId = detailAccount.Id
            };
            existing.AccountDetailAccounts.Add(accountDetailAccount);
            foreach (var child in detailAccount.Children)
            {
                await AddNewAccountDetailAccount(existing, child.Id);
            }
        }

        private async Task AddNewAccountCostCenters(Account existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = existing.AccountCostCenters.Select(ac => ac.CostCenterId);
            var newItems = relations.RelatedItemIds.Where(id => !currentItems.Contains(id));
            foreach (int centerId in newItems)
            {
                await AddNewAccountCostCenter(existing, centerId);
            }
        }

        private async Task AddNewAccountCostCenter(Account existing, int centerId)
        {
            var existingRelation = existing.AccountCostCenters
                .Where(ac => ac.AccountId == existing.Id && ac.CostCenterId == centerId)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = _unitOfWork.GetRepository<CostCenter>();
            var costCenter = repository.GetByIDWithTracking(centerId, cc => cc.Children);
            var accountCostCenter = new AccountCostCenter()
            {
                Account = existing,
                AccountId = existing.Id,
                CostCenter = costCenter,
                CostCenterId = costCenter.Id
            };
            existing.AccountCostCenters.Add(accountCostCenter);
            foreach (var child in costCenter.Children)
            {
                await AddNewAccountCostCenter(existing, child.Id);
            }
        }

        private async Task AddNewAccountProjects(Account existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = existing.AccountProjects.Select(ap => ap.ProjectId);
            var newItems = relations.RelatedItemIds.Where(id => !currentItems.Contains(id));
            foreach (int projectId in newItems)
            {
                await AddNewAccountProject(existing, projectId);
            }
        }

        private async Task AddNewAccountProject(Account existing, int projectId)
        {
            var existingRelation = existing.AccountProjects
                .Where(ap => ap.AccountId == existing.Id && ap.ProjectId == projectId)
                .FirstOrDefault();
            if (existingRelation != null)
            {
                return;
            }

            var repository = _unitOfWork.GetRepository<Project>();
            var project = repository.GetByIDWithTracking(projectId, prj => prj.Children);
            var accountProject = new AccountProject()
            {
                Account = existing,
                AccountId = existing.Id,
                Project = project,
                ProjectId = project.Id
            };
            existing.AccountProjects.Add(accountProject);
            foreach (var child in project.Children)
            {
                await AddNewAccountProject(existing, child.Id);
            }
        }

        private void AddConnectedAccounts(DetailAccount existing, AccountItemRelationsViewModel relations)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var currentItems = existing
                .AccountDetailAccounts
                .Select(ada => ada.AccountId);
            var newItems = relations.RelatedItemIds
                .Where(id => !currentItems.Contains(id));
            foreach (var item in newItems)
            {
                var account = repository.GetByIDWithTracking(item);
                var accountDetailAccount = new AccountDetailAccount()
                {
                    DetailAccount = existing,
                    DetailId = existing.Id,
                    Account = account,
                    AccountId = account.Id
                };
                existing.AccountDetailAccounts.Add(accountDetailAccount);
            }
        }

        private void AddConnectedAccounts(CostCenter existing, AccountItemRelationsViewModel relations)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var currentItems = existing
                .AccountCostCenters
                .Select(ac => ac.AccountId);
            var newItems = relations.RelatedItemIds
                .Where(id => !currentItems.Contains(id));
            foreach (var item in newItems)
            {
                var account = repository.GetByIDWithTracking(item);
                var accountCostCenter = new AccountCostCenter()
                {
                    Account = account,
                    AccountId = account.Id,
                    CostCenter = existing,
                    CostCenterId = existing.Id
                };
                existing.AccountCostCenters.Add(accountCostCenter);
            }
        }

        private void AddConnectedAccounts(Project existing, AccountItemRelationsViewModel relations)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var currentItems = existing
                .AccountProjects
                .Select(ap => ap.AccountId);
            var newItems = relations.RelatedItemIds
                .Where(id => !currentItems.Contains(id));
            foreach (var item in newItems)
            {
                var account = repository.GetByIDWithTracking(item);
                var accountProject = new AccountProject()
                {
                    Account = account,
                    AccountId = account.Id,
                    Project = existing,
                    ProjectId = existing.Id
                };
                existing.AccountProjects.Add(accountProject);
            }
        }

        private async Task<string> EnsureValidItemsInFullAccountAsync(
            FullAccountCriteriaViewModel criteria, FullAccountViewModel fullAccount)
        {
            string errorKey = String.Empty;
            if (criteria.RequiresDetailAccount || fullAccount.DetailAccount.Id > 0)
            {
                errorKey = await EnsureValidDetailAccountInFullAccountAsync(fullAccount.DetailAccount.Id);
                if (!String.IsNullOrEmpty(errorKey))
                {
                    return errorKey;
                }
            }

            if (criteria.RequiresCostCenter || fullAccount.CostCenter.Id > 0)
            {
                errorKey = await EnsureValidCostCenterInFullAccountAsync(fullAccount.CostCenter.Id);
                if (!String.IsNullOrEmpty(errorKey))
                {
                    return errorKey;
                }
            }

            if (criteria.RequiresProject || fullAccount.Project.Id > 0)
            {
                errorKey = await EnsureValidProjectInFullAccountAsync(fullAccount.Project.Id);
                if (!String.IsNullOrEmpty(errorKey))
                {
                    return errorKey;
                }
            }

            return errorKey;
        }

        private async Task<string> EnsureValidDetailAccountInFullAccountAsync(int detailId)
        {
            string errorKey = String.Empty;
            var detailAccount = await _repository
                .GetAllQuery<DetailAccount>(
                    ViewName.DetailAccount, facc => facc.Children, facc => facc.AccountDetailAccounts)
                .Where(facc => facc.Id == detailId)
                .SingleOrDefaultAsync();
            if (detailAccount == null)
            {
                errorKey = FullAccountError.InvalidDetailAccountInFullAccount;
            }
            else if (detailAccount.Children.Count > 0)
            {
                errorKey = FullAccountError.NonLeafDetailAccountInFullAccount;
            }

            return errorKey;
        }

        private async Task<string> EnsureValidCostCenterInFullAccountAsync(int costCenterId)
        {
            string errorKey = String.Empty;
            var costCenter = await _repository
                .GetAllQuery<CostCenter>(ViewName.CostCenter, cc => cc.Children, cc => cc.AccountCostCenters)
                .Where(cc => cc.Id == costCenterId)
                .SingleOrDefaultAsync();
            if (costCenter == null)
            {
                errorKey = FullAccountError.InvalidCostCenterInFullAccount;
            }
            else if (costCenter.Children.Count > 0)
            {
                errorKey = FullAccountError.NonLeafCostCenterInFullAccount;
            }

            return errorKey;
        }

        private async Task<string> EnsureValidProjectInFullAccountAsync(int projectId)
        {
            string errorKey = String.Empty;
            var project = await _repository
                .GetAllQuery<Project>(ViewName.Project, prj => prj.Children, prj => prj.AccountProjects)
                .Where(prj => prj.Id == projectId)
                .SingleOrDefaultAsync();
            if (project == null)
            {
                errorKey = FullAccountError.InvalidProjectInFullAccount;
            }
            else if (project.Children.Count > 0)
            {
                errorKey = FullAccountError.NonLeafProjectInFullAccount;
            }

            return errorKey;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly IAccountItemRepository _itemRepository;
        private readonly ISecureRepository _repository;
    }
}

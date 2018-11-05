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
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsAsync(
            bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var accounts = useLeafItems
                ? await _itemRepository.GetLeafAccountsAsync(gridOptions)
                : await _itemRepository.GetRootAccountsAsync(gridOptions);
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
        public async Task<IList<KeyValue>> GetUsableAccountsLookupAsync(GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafAccountsAsync(gridOptions);
            return items
                .Select(item => _mapper.Map<KeyValue>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<KeyValue>> GetUsableDetailAccountsLookupAsync(GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafDetailAccountsAsync(gridOptions);
            return items
                .Select(item => _mapper.Map<KeyValue>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<KeyValue>> GetUsableCostCentersLookupAsync(GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafCostCentersAsync(gridOptions);
            return items
                .Select(item => _mapper.Map<KeyValue>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>پروژه های قابل استفاده در دوره مالی و شعبه جاری</returns>
        public async Task<IList<KeyValue>> GetUsableProjectsLookupAsync(GridOptions gridOptions = null)
        {
            var items = await _itemRepository.GetLeafProjectsAsync(gridOptions);
            return items
                .Select(item => _mapper.Map<KeyValue>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountDetailAccountsAsync(
            int accountId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedDetailIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.AccountId == accountId)
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
        /// <returns>مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountCostCentersAsync(
            int accountId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedCenterIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.AccountId == accountId)
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
        /// <returns>مجموعه ای از پروژه های مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountProjectsAsync(
            int accountId, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
            var relatedProjectIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.AccountId == accountId)
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
                .Where(ada => ada.DetailId == detailId)
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
                .Where(ac => ac.CostCenterId == costCenterId)
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
                .Where(ap => ap.ProjectId == projectId)
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
            var existing = await repository.GetByIDWithTrackingAsync(relations.Id, acc => acc.AccountDetailAccounts);
            if (existing != null && AreRelationsModified(
                    existing.AccountDetailAccounts
                        .Select(ada => ada.DetailId)
                        .ToArray(),
                    relations))
            {
                if (existing.AccountDetailAccounts.Count > 0)
                {
                    RemoveDisconnectedDetails(existing, relations);
                }

                AddConnectedDetails(existing, relations);
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
            var existing = await repository.GetByIDWithTrackingAsync(relations.Id, acc => acc.AccountCostCenters);
            if (existing != null && AreRelationsModified(
                    existing.AccountCostCenters
                        .Select(ac => ac.CostCenterId)
                        .ToArray(),
                    relations))
            {
                if (existing.AccountCostCenters.Count > 0)
                {
                    RemoveDisconnectedCostCenters(existing, relations);
                }

                AddConnectedCostCenters(existing, relations);
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
            var existing = await repository.GetByIDWithTrackingAsync(relations.Id, acc => acc.AccountProjects);
            if (existing != null && AreRelationsModified(
                    existing.AccountProjects
                        .Select(ap => ap.ProjectId)
                        .ToArray(),
                    relations))
            {
                if (existing.AccountProjects.Count > 0)
                {
                    RemoveDisconnectedProjects(existing, relations);
                }

                AddConnectedProjects(existing, relations);
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
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با تفصیلی شناور مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForDetailAccountAsync(
            int detailId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ada => ada.DetailId == detailId)
                .Select(ada => ada.AccountId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id));
            if (useLeafItems)
            {
                query = query.Where(acc => acc.Children.Count == 0);
            }

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
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با مرکز هزینه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForCostCenterAsync(
            int costCenterId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ac => ac.CostCenterId == costCenterId)
                .Select(ac => ac.AccountId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id));
            if (useLeafItems)
            {
                query = query.Where(acc => acc.Children.Count == 0);
            }

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
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با پروژه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForProjectAsync(
            int projectId, bool useLeafItems = true, GridOptions gridOptions = null)
        {
            var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
            var relatedAccountIds = await relationRepository
                .GetEntityQuery()
                .Where(ap => ap.ProjectId == projectId)
                .Select(ap => ap.AccountId)
                .ToListAsync();
            var query = _repository
                .GetAllQuery<Account>(ViewName.Account, acc => acc.Children)
                .Where(acc => !relatedAccountIds.Contains(acc.Id));
            if (useLeafItems)
            {
                query = query.Where(acc => acc.Children.Count == 0);
            }

            return await query
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .Apply(gridOptions)
                .ToListAsync();
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

        private static void RemoveDisconnectedDetails(Account existing, AccountItemRelationsViewModel relations)
        {
            var currentItems = relations.RelatedItemIds;
            var disconnectedItems = existing.AccountDetailAccounts
                .Select(ada => ada.DetailId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
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
                .ToArray();
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
                .ToArray();
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

        private static IQueryable<Account> GetAccountChildrenQuery(IRepository<Account> repository, int accountId)
        {
            return repository
                .GetEntityQuery()
                .Include(acc => acc.Children)
                    .ThenInclude(acc => acc.Children)
                .Where(acc => acc.Id == accountId);
        }

        private static IQueryable<DetailAccount> GetDetailAccountChildrenQuery(IRepository<DetailAccount> repository, int faccountId)
        {
            return repository
                .GetEntityQuery()
                .Include(facc => facc.Children)
                    .ThenInclude(facc => facc.Children)
                .Where(facc => facc.Id == faccountId);
        }

        private static IQueryable<CostCenter> GetCostCenterChildrenQuery(IRepository<CostCenter> repository, int costCenterId)
        {
            return repository
                .GetEntityQuery()
                .Include(cc => cc.Children)
                    .ThenInclude(cc => cc.Children)
                .Where(cc => cc.Id == costCenterId);
        }

        private static IQueryable<Project> GetProjectChildrenQuery(IRepository<Project> repository, int projectId)
        {
            return repository
                .GetEntityQuery()
                .Include(prj => prj.Children)
                    .ThenInclude(prj => prj.Children)
                .Where(prj => prj.Id == projectId);
        }

        private void AddConnectedDetails(Account existing, AccountItemRelationsViewModel relations)
        {
            var repository = _unitOfWork.GetRepository<DetailAccount>();
            var currentItems = existing
                .AccountDetailAccounts
                .Select(ada => ada.DetailId);
            var newItems = relations.RelatedItemIds
                .Where(id => !currentItems.Contains(id));
            foreach (var item in newItems)
            {
                var detailAccount = repository.GetByIDWithTracking(item);
                var accountDetailAccount = new AccountDetailAccount()
                {
                    Account = existing,
                    AccountId = existing.Id,
                    DetailAccount = detailAccount,
                    DetailId = detailAccount.Id
                };
                existing.AccountDetailAccounts.Add(accountDetailAccount);
            }
        }

        private void AddConnectedCostCenters(Account existing, AccountItemRelationsViewModel relations)
        {
            var repository = _unitOfWork.GetRepository<CostCenter>();
            var currentItems = existing
                .AccountCostCenters
                .Select(ac => ac.CostCenterId);
            var newItems = relations.RelatedItemIds
                .Where(id => !currentItems.Contains(id));
            foreach (var item in newItems)
            {
                var costCenter = repository.GetByIDWithTracking(item);
                var accountCostCenter = new AccountCostCenter()
                {
                    Account = existing,
                    AccountId = existing.Id,
                    CostCenter = costCenter,
                    CostCenterId = costCenter.Id
                };
                existing.AccountCostCenters.Add(accountCostCenter);
            }
        }

        private void AddConnectedProjects(Account existing, AccountItemRelationsViewModel relations)
        {
            var repository = _unitOfWork.GetRepository<Project>();
            var currentItems = existing
                .AccountProjects
                .Select(ap => ap.ProjectId);
            var newItems = relations.RelatedItemIds
                .Where(id => !currentItems.Contains(id));
            foreach (var item in newItems)
            {
                var project = repository.GetByIDWithTracking(item);
                var accountProject = new AccountProject()
                {
                    Account = existing,
                    AccountId = existing.Id,
                    Project = project,
                    ProjectId = project.Id
                };
                existing.AccountProjects.Add(accountProject);
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

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly IAccountItemRepository _itemRepository;
        private readonly ISecureRepository _repository;
    }
}

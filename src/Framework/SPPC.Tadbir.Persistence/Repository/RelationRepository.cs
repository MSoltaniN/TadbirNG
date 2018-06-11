using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Finance;
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
        public RelationRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IAccountItemRepository itemRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsAsync(
            int fpId, int branchId, bool useLeafItems = true)
        {
            var accounts = useLeafItems
                ? await _itemRepository.GetLeafAccountsAsync(fpId, branchId)
                : await _itemRepository.GetRootAccountsAsync(fpId, branchId);
            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsAsync(
            int fpId, int branchId, bool useLeafItems = true)
        {
            var detailAccounts = useLeafItems
                ? await _itemRepository.GetLeafDetailAccountsAsync(fpId, branchId)
                : await _itemRepository.GetRootDetailAccountsAsync(fpId, branchId);
            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersAsync(
            int fpId, int branchId, bool useLeafItems = true)
        {
            var costCenters = useLeafItems
                ? await _itemRepository.GetLeafCostCentersAsync(fpId, branchId)
                : await _itemRepository.GetRootCostCentersAsync(fpId, branchId);
            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>پروژه های قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsAsync(
            int fpId, int branchId, bool useLeafItems = true)
        {
            var projects = useLeafItems
                ? await _itemRepository.GetLeafProjectsAsync(fpId, branchId)
                : await _itemRepository.GetRootProjectsAsync(fpId, branchId);
            return projects;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountDetailAccountsAsync(
            int accountId, bool useLeafItems = true)
        {
            IList<AccountItemBriefViewModel> detailAccounts = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.FiscalPeriod)
                .Include(acc => acc.Branch)
                .Include(acc => acc.AccountDetailAccounts)
                    .ThenInclude(ada => ada.DetailAccount);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                var relatedItemIds = account.AccountDetailAccounts.Select(ada => ada.DetailId);
                if (useLeafItems)
                {
                    detailAccounts = await _itemRepository.GetLeafDetailAccountsAsync(
                        account.FiscalPeriod.Id, account.Branch.Id);
                }
                else
                {
                    detailAccounts = await _itemRepository.GetRootDetailAccountsAsync(
                        account.FiscalPeriod.Id, account.Branch.Id);
                }

                Array.ForEach(
                    detailAccounts
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountCostCentersAsync(
            int accountId, bool useLeafItems = true)
        {
            IList<AccountItemBriefViewModel> costCenters = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.FiscalPeriod)
                .Include(acc => acc.Branch)
                .Include(acc => acc.AccountCostCenters)
                    .ThenInclude(acc => acc.CostCenter);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                var relatedItemIds = account.AccountCostCenters.Select(ac => ac.CostCenterId);
                if (useLeafItems)
                {
                    costCenters = await _itemRepository.GetLeafCostCentersAsync(account.FiscalPeriod.Id, account.Branch.Id);
                }
                else
                {
                    costCenters = await _itemRepository.GetRootCostCentersAsync(account.FiscalPeriod.Id, account.Branch.Id);
                }

                Array.ForEach(
                    costCenters
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از پروژه های مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountProjectsAsync(
            int accountId, bool useLeafItems = true)
        {
            IList<AccountItemBriefViewModel> projects = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.FiscalPeriod)
                .Include(acc => acc.Branch)
                .Include(acc => acc.AccountProjects)
                    .ThenInclude(ap => ap.Project);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                var relatedItemIds = account.AccountProjects.Select(ap => ap.ProjectId);
                if (useLeafItems)
                {
                    projects = await _itemRepository.GetLeafProjectsAsync(account.FiscalPeriod.Id, account.Branch.Id);
                }
                else
                {
                    projects = await _itemRepository.GetRootProjectsAsync(account.FiscalPeriod.Id, account.Branch.Id);
                }

                Array.ForEach(
                    projects
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return projects;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetDetailAccountAccountsAsync(
            int detailId, bool useLeafItems = true)
        {
            IList<AccountItemBriefViewModel> accounts = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var query = repository.GetEntityQuery()
                .Where(facc => facc.Id == detailId)
                .Include(facc => facc.FiscalPeriod)
                .Include(facc => facc.Branch)
                .Include(facc => facc.AccountDetailAccounts)
                    .ThenInclude(ada => ada.DetailAccount);
            var detail = await query.SingleOrDefaultAsync();
            if (detail != null)
            {
                var relatedItemIds = detail.AccountDetailAccounts.Select(ada => ada.AccountId);
                if (useLeafItems)
                {
                    accounts = await _itemRepository.GetLeafAccountsAsync(
                        detail.FiscalPeriod.Id, detail.Branch.Id);
                }
                else
                {
                    accounts = await _itemRepository.GetRootAccountsAsync(
                        detail.FiscalPeriod.Id, detail.Branch.Id);
                }

                Array.ForEach(
                    accounts
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه دیتابیسی یکی از مراکز هزینه موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetCostCenterAccountsAsync(
            int costCenterId, bool useLeafItems = true)
        {
            IList<AccountItemBriefViewModel> accounts = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var query = repository.GetEntityQuery()
                .Where(cc => cc.Id == costCenterId)
                .Include(cc => cc.FiscalPeriod)
                .Include(cc => cc.Branch)
                .Include(cc => cc.AccountCostCenters)
                    .ThenInclude(ac => ac.Account);
            var costCenter = await query.SingleOrDefaultAsync();
            if (costCenter != null)
            {
                var relatedItemIds = costCenter.AccountCostCenters.Select(ac => ac.AccountId);
                if (useLeafItems)
                {
                    accounts = await _itemRepository.GetLeafAccountsAsync(costCenter.FiscalPeriod.Id, costCenter.Branch.Id);
                }
                else
                {
                    accounts = await _itemRepository.GetRootAccountsAsync(costCenter.FiscalPeriod.Id, costCenter.Branch.Id);
                }

                Array.ForEach(
                    accounts
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return accounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با پروژه مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی یکی از پروژه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از حساب های مرتبط با پروژه مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetProjectAccountsAsync(
            int projectId, bool useLeafItems = true)
        {
            IList<AccountItemBriefViewModel> accounts = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var query = repository.GetEntityQuery()
                .Where(prj => prj.Id == projectId)
                .Include(cc => cc.FiscalPeriod)
                .Include(cc => cc.Branch)
                .Include(prj => prj.AccountProjects)
                    .ThenInclude(ap => ap.Account);
            var project = await query.SingleOrDefaultAsync();
            if (project != null)
            {
                var relatedItemIds = project.AccountProjects.Select(ap => ap.AccountId);
                if (useLeafItems)
                {
                    accounts = await _itemRepository.GetLeafAccountsAsync(project.FiscalPeriod.Id, project.Branch.Id);
                }
                else
                {
                    accounts = await _itemRepository.GetRootAccountsAsync(project.FiscalPeriod.Id, project.Branch.Id);
                }

                Array.ForEach(
                    accounts
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

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
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک حساب با یک تفصیلی شناور را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه حساب پدر</param>
        /// <param name="faccountId">شناسه تفصیلی شناور مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های حساب پدر با تفصیلی شناور</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildAccountsRelatedToDetailAccount(
            int accountId, int faccountId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await GetAccountChildrenQuery(repository, accountId)
                .SingleOrDefaultAsync();
            if (account != null)
            {
                children.AddRange(account.Children.Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc)));
                var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
                var relatedItems = await relationRepository
                    .GetByCriteriaAsync(item => item.DetailId == faccountId && item.Account.ParentId == accountId);
                var relatedItemIds = relatedItems
                    .Select(item => item.AccountId)
                    .ToArray();
                Array.ForEach(
                    children
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک حساب با یک مرکز هزینه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه حساب پدر</param>
        /// <param name="costCenterId">شناسه مرکز هزینه مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های حساب پدر با مرکز هزینه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildAccountsRelatedToCostCenter(
            int accountId, int costCenterId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await GetAccountChildrenQuery(repository, accountId)
                .SingleOrDefaultAsync();
            if (account != null)
            {
                children.AddRange(account.Children.Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc)));
                var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
                var relatedItems = await relationRepository
                    .GetByCriteriaAsync(item => item.CostCenterId == costCenterId && item.Account.ParentId == accountId);
                var relatedItemIds = relatedItems
                    .Select(item => item.AccountId)
                    .ToArray();
                Array.ForEach(
                    children
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک حساب با یک پروژه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه حساب پدر</param>
        /// <param name="projectId">شناسه پروژه مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های حساب پدر با پروژه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildAccountsRelatedToProject(
            int accountId, int projectId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await GetAccountChildrenQuery(repository, accountId)
                .SingleOrDefaultAsync();
            if (account != null)
            {
                children.AddRange(account.Children.Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc)));
                var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
                var relatedItems = await relationRepository
                    .GetByCriteriaAsync(item => item.ProjectId == projectId && item.Account.ParentId == accountId);
                var relatedItemIds = relatedItems
                    .Select(item => item.AccountId)
                    .ToArray();
                Array.ForEach(
                    children
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک تفصیلی شناور با یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه تفصیلی شناور پدر</param>
        /// <param name="accountId">شناسه حساب مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های تفصیلی شناور پدر با حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildDetailAccountsRelatedToAccount(
            int faccountId, int accountId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await GetDetailAccountChildrenQuery(repository, faccountId)
                .SingleOrDefaultAsync();
            if (detailAccount != null)
            {
                children.AddRange(detailAccount.Children.Select(facc => _mapper.Map<AccountItemBriefViewModel>(facc)));
                var relationRepository = _unitOfWork.GetAsyncRepository<AccountDetailAccount>();
                var relatedItems = await relationRepository
                    .GetByCriteriaAsync(item => item.AccountId == accountId && item.DetailAccount.ParentId == faccountId);
                var relatedItemIds = relatedItems
                    .Select(item => item.DetailId)
                    .ToArray();
                Array.ForEach(
                    children
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک مرکز هزینه با یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه مرکز هزینه پدر</param>
        /// <param name="accountId">شناسه حساب مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های مرکز هزینه پدر با حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildCostCentersRelatedToAccount(
            int costCenterId, int accountId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await GetCostCenterChildrenQuery(repository, costCenterId)
                .SingleOrDefaultAsync();
            if (costCenter != null)
            {
                children.AddRange(costCenter.Children.Select(cc => _mapper.Map<AccountItemBriefViewModel>(cc)));
                var relationRepository = _unitOfWork.GetAsyncRepository<AccountCostCenter>();
                var relatedItems = await relationRepository
                    .GetByCriteriaAsync(item => item.AccountId == accountId && item.CostCenter.ParentId == costCenterId);
                var relatedItemIds = relatedItems
                    .Select(item => item.CostCenterId)
                    .ToArray();
                Array.ForEach(
                    children
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک پروژه با یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه پروژه پدر</param>
        /// <param name="accountId">شناسه حساب مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های پروژه پدر با حساب</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildProjectsRelatedToAccount(
            int projectId, int accountId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var project = await GetProjectChildrenQuery(repository, projectId)
                .SingleOrDefaultAsync();
            if (project != null)
            {
                children.AddRange(project.Children.Select(prj => _mapper.Map<AccountItemBriefViewModel>(prj)));
                var relationRepository = _unitOfWork.GetAsyncRepository<AccountProject>();
                var relatedItems = await relationRepository
                    .GetByCriteriaAsync(item => item.AccountId == accountId && item.Project.ParentId == projectId);
                var relatedItemIds = relatedItems
                    .Select(item => item.ProjectId)
                    .ToArray();
                Array.ForEach(
                    children
                        .Where(item => relatedItemIds.Contains(item.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return children;
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
                var detailAccount = repository.GetByIDWithTracking(
                    item, facc => facc.Branch, facc => facc.FiscalPeriod);
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
                var costCenter = repository.GetByIDWithTracking(
                    item, cc => cc.Branch, cc => cc.FiscalPeriod);
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
                var project = repository.GetByIDWithTracking(
                    item, prj => prj.Branch, prj => prj.FiscalPeriod);
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
                var account = repository.GetByIDWithTracking(
                    item, acc => acc.Branch, acc => acc.FiscalPeriod);
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
                var account = repository.GetByIDWithTracking(
                    item, acc => acc.Branch, acc => acc.FiscalPeriod);
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
                var account = repository.GetByIDWithTracking(
                    item, acc => acc.Branch, acc => acc.FiscalPeriod);
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

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IAccountItemRepository _itemRepository;
    }
}

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
                    detailAccounts = await _itemRepository.GetLeafDetailAccounts(
                        account.FiscalPeriod.Id, account.Branch.Id);
                }
                else
                {
                    detailAccounts = await _itemRepository.GetRootDetailAccounts(
                        account.FiscalPeriod.Id, account.Branch.Id);
                }

                Array.ForEach(
                    detailAccounts
                        .Where(det => relatedItemIds.Contains(det.Id))
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
                    costCenters = await _itemRepository.GetLeafCostCenters(account.FiscalPeriod.Id, account.Branch.Id);
                }
                else
                {
                    costCenters = await _itemRepository.GetRootCostCenters(account.FiscalPeriod.Id, account.Branch.Id);
                }

                Array.ForEach(
                    costCenters
                        .Where(det => relatedItemIds.Contains(det.Id))
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
                .Include(acc => acc.AccountProjects)
                    .ThenInclude(ap => ap.Project);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                var relatedItemIds = account.AccountProjects.Select(ap => ap.ProjectId);
                if (useLeafItems)
                {
                    projects = await _itemRepository.GetLeafProjects(account.FiscalPeriod.Id, account.Branch.Id);
                }
                else
                {
                    projects = await _itemRepository.GetRootProjects(account.FiscalPeriod.Id, account.Branch.Id);
                }

                Array.ForEach(
                    projects
                        .Where(det => relatedItemIds.Contains(det.Id))
                        .ToArray(),
                    item => item.IsSelected = true);
            }

            return projects;
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
            if (existing != null &&
                AreRelationsModified(
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
            if (existing != null &&
                AreRelationsModified(
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
            if (existing != null &&
                AreRelationsModified(
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
                    item, cc => cc.Branch, facc => facc.FiscalPeriod);
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
                    item, prj => prj.Branch, facc => facc.FiscalPeriod);
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

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IAccountItemRepository _itemRepository;
    }
}

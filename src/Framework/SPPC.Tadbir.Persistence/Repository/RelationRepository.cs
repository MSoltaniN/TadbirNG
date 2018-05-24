﻿using System;
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

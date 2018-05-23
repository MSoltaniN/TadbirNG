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
        public RelationRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <returns>مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRelatedDetailAccountsAsync(int accountId)
        {
            var detailAccounts = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.AccountDetailAccounts)
                    .ThenInclude(ada => ada.DetailAccount);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                detailAccounts.AddRange(account.AccountDetailAccounts
                    .Select(ada => ada.DetailAccount)
                    .Select(facc => _mapper.Map<AccountItemBriefViewModel>(facc)));
            }

            return detailAccounts;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <returns>مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRelatedCostCentersAsync(int accountId)
        {
            var costCenters = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.AccountCostCenters)
                    .ThenInclude(acc => acc.CostCenter);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                costCenters.AddRange(account.AccountCostCenters
                    .Select(acc => acc.CostCenter)
                    .Select(cc => _mapper.Map<AccountItemBriefViewModel>(cc)));
            }

            return costCenters;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <returns>مجموعه ای از پروژه های مرتبط با حساب مشخص شده</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRelatedProjectsAsync(int accountId)
        {
            var projects = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.AccountProjects)
                    .ThenInclude(ap => ap.Project);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                projects.AddRange(account.AccountProjects
                    .Select(ap => ap.Project)
                    .Select(prj => _mapper.Map<AccountItemBriefViewModel>(prj)));
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
            if (existing != null && AreRelationsModified(existing, relations))
            {
                if (existing.AccountDetailAccounts.Count > 0)
                {
                    RemoveDisconnectedItems(existing, relations);
                }

                AddNewConnectedItems(existing, relations);
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }
        }

        private static bool AreRelationsModified(Account existing, AccountItemRelationsViewModel relations)
        {
            var existingItems = existing.AccountDetailAccounts
                .Select(ada => ada.DetailId)
                .ToArray();
            var connectedItems = relations.RelatedItemIds
                .ToArray();
            return (!AreEqual(existingItems, connectedItems));
        }

        private static void RemoveDisconnectedItems(Account existing, AccountItemRelationsViewModel relations)
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

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private void AddNewConnectedItems(Account existing, AccountItemRelationsViewModel relations)
        {
            var repository = _unitOfWork.GetRepository<DetailAccount>();
            var currentItems = existing.AccountDetailAccounts.Select(ada => ada.DetailId);
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

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// عملیات کمکی مورد نیاز برای استفاده از مجموعه حساب ها را پیاده سازی می کند
    /// </summary>
    public class AccountCollectionUtility : RepositoryBase, IAccountCollectionUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public AccountCollectionUtility(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// به روش آسنکرون، حساب های قابل استفاده تعریف شده در یک مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب</param>
        /// <param name="withRelations">مشخص می کند که ارتباطات حساب هم باید خوانده شوند یا نه</param>
        /// <param name="branchId">شعبه مورد نظر برای خواندن حساب های مجموعه حساب
        /// که در صورت تعیین نشدن، شعبه جاری فرض می شود</param>
        /// <returns>مجموعه ای از حساب های قابل استفاده در یک مجموعه حساب</returns>
        /// <remarks>منظور از حساب های قابل استفاده، حساب هایی است که می توان در آرتیکل های سند از آنها استفاده کرد.
        /// لازم به یادآوری است که حساب های یک مجموعه حساب ممکن است در سطوح ماقبل آخر به یک مجموعه حساب تخصیص داده شوند
        /// و مستقیماً قابل استفاده در آرتیکل های سند نباشند.</remarks>
        public async Task<IList<Account>> GetUsableAccountsAsync(
            AccountCollectionId collectionId, bool withRelations = false, int? branchId = null)
        {
            int inBranchId = branchId ?? UserContext.BranchId;
            var accounts = await GetInheritedAccountsAsync(collectionId, inBranchId);
            var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
            var query = accountRepository.GetEntityQuery();
            if (withRelations)
            {
                query = query
                    .Include(acc => acc.AccountDetailAccounts)
                    .Include(acc => acc.AccountCostCenters)
                    .Include(acc => acc.AccountProjects);
            }

            var leafAccounts = await query
                .Where(acc => acc.Children.Count == 0 &&
                    accounts.Any(item => acc.FullCode.StartsWith(item.FullCode)))
                .ToListAsync();
            return leafAccounts;
        }

        /// <summary>
        /// حساب های مجموعه حساب داده شده را در شعبه داده شده و تمام شعبه های بالادستی آن خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب مورد نظر</param>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر</param>
        /// <returns>حساب های ارث بری شده برای مجموعه حساب</returns>
        public async Task<IEnumerable<Account>> GetInheritedAccountsAsync(AccountCollectionId collectionId, int branchId)
        {
            var accounts = new List<Account>();
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
            var branch = await branchRepository.GetByIDWithTrackingAsync(branchId);
            var currentBranch = branch;
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            while (currentBranch != null)
            {
                var collectionAccounts = await repository
                    .GetEntityQuery()
                    .Include(aca => aca.Account)
                    .Where(aca => aca.FiscalPeriodId <= UserContext.FiscalPeriodId &&
                        aca.BranchId == currentBranch.Id &&
                        aca.CollectionId == (int)collectionId)
                    .Select(aca => aca.Account)
                    .ToListAsync();
                if (collectionAccounts.Count > 0)
                {
                    accounts.AddRange(collectionAccounts);
                }

                branchRepository.LoadReference(currentBranch, br => br.Parent);
                currentBranch = currentBranch.Parent;
            }

            return accounts;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class AccountBalanceUtility : BalanceUtilityBase, ITestBalanceUtility
    {
        public AccountBalanceUtility(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IConfigRepository config)
            : base(unitOfWork, mapper, config)
        {
        }

        public IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query)
        {
            return query.Include(line => line.Account);
        }

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.AccountLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.AccountLevel >= level;
        }

        public async Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            return new TestBalanceItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                AccountId = account.Id,
                AccountName = account.Name,
                AccountFullCode = account.FullCode,
                AccountLevel = account.Level
            };
        }

        public async Task<decimal> GetInitialBalanceAsync(
            int itemId, TestBalanceParameters parameters, IReportRepository report)
        {
            decimal balance = parameters.FromDate.HasValue
                ? await report.GetAccountBalanceAsync(itemId, parameters.FromDate.Value)
                : await report.GetAccountBalanceAsync(itemId, parameters.FromNo.Value);
            if ((parameters.Options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                balance += await report.GetSpecialVoucherBalanceAsync(
                    VoucherType.OpeningVoucher, itemId);
            }

            return balance;
        }
    }
}

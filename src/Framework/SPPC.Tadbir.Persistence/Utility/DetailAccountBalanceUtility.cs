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
    internal class DetailAccountBalanceUtility : BalanceUtilityBase, ITestBalanceUtility
    {
        public DetailAccountBalanceUtility(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IConfigRepository config)
            : base(unitOfWork, mapper, config)
        {
        }

        public IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query)
        {
            return query.Include(line => line.Account);
        }

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.DetailAccountLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.DetailAccountLevel >= level;
        }

        public async Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await repository.GetSingleByCriteriaAsync(facc => facc.FullCode == fullCode);
            return new TestBalanceItemViewModel()
            {
                BranchId = line.BranchId,
                BranchName = line.BranchName,
                DetailAccountId = detailAccount.Id,
                DetailAccountName = detailAccount.Name,
                DetailAccountFullCode = detailAccount.FullCode,
                DetailAccountLevel = detailAccount.Level
            };
        }

        public async Task<decimal> GetInitialBalanceAsync(
            int itemId, TestBalanceParameters parameters, IReportRepository report)
        {
            decimal balance = parameters.FromDate.HasValue
                ? await report.GetDetailAccountBalanceAsync(itemId, parameters.FromDate.Value)
                : await report.GetDetailAccountBalanceAsync(itemId, parameters.FromNo.Value);
            if ((parameters.Options & TestBalanceOptions.OpeningVoucherAsInitBalance) > 0)
            {
                balance += await report.GetSpecialVoucherBalanceAsync(
                    VoucherType.OpeningVoucher, itemId);
            }

            return balance;
        }

        protected override Func<TModel, string> GetGroupSelector<TModel>(int groupLevel)
        {
            int codeLength = GetLevelCodeLength(ViewName.DetailAccount, groupLevel);
            return item => item.DetailAccountFullCode.Substring(0, codeLength);
        }
    }
}

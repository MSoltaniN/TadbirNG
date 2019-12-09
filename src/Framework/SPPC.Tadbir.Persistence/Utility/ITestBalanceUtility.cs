using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface ITestBalanceUtility : IReportUtility
    {
        IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query);
        Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level);
        Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level);
        Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode);
        Task<decimal> GetInitialBalanceAsync(int itemId, TestBalanceParameters parameters, IReportRepository report);
    }
}

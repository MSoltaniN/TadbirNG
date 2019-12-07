using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface ITestBalanceUtility : IReportUtility
    {
        IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query);
        Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level);
        Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level);
    }
}

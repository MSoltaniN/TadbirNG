using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal interface ITestBalanceUtility
    {
        //IEnumerable<IGrouping<string, TestBalanceItemViewModel>> GetTurnoverGroups(
        //    IEnumerable<TestBalanceItemViewModel> lines, int groupLevel,
        //    Func<TestBalanceItemViewModel, bool> lineFilter);
        Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level);
        Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level);
    }
}

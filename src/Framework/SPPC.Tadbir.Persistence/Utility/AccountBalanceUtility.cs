using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class AccountBalanceUtility : ITestBalanceUtility
    {
        //public IEnumerable<IGrouping<string, TestBalanceItemViewModel>> GetTurnoverGroups(
        //    IEnumerable<TestBalanceItemViewModel> lines, int groupLevel,
        //    Func<TestBalanceItemViewModel, bool> lineFilter)
        //{
        //    int codeLength = GetLevelCodeLength(ViewName.Account, groupLevel);
        //    return GetGroupByThenByItems(lines.Where(lineFilter),
        //            item => item.AccountFullCode.Substring(0, codeLength));
        //}

        public Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level)
        {
            return line => line.AccountLevel == level;
        }

        public Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level)
        {
            return line => line.AccountLevel >= level;
        }
    }
}

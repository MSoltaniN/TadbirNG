using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class TestBalanceUtilityFactory
    {
        internal ITestBalanceUtility Create(int viewId)
        {
            ITestBalanceUtility utility = null;
            switch (viewId)
            {
                case ViewName.DetailAccount:
                    utility = new DetailAccountBalanceUtility();
                    break;
                case ViewName.CostCenter:
                    utility = new CostCenterBalanceUtility();
                    break;
                case ViewName.Project:
                    utility = new ProjectBalanceUtility();
                    break;
                case ViewName.Account:
                default:
                    utility = new AccountBalanceUtility();
                    break;
            }

            return utility;
        }
    }
}

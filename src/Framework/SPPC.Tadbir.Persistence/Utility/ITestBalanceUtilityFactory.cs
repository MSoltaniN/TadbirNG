using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface ITestBalanceUtilityFactory
    {
        ITestBalanceUtility Create(int viewId);
    }
}

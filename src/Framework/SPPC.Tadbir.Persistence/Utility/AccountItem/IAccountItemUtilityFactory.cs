using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface IAccountItemUtilityFactory
    {
        IAccountItemUtility Create(int viewId);
    }
}

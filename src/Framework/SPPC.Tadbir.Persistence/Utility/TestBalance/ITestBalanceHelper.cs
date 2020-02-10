using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبه مانده مولفه های حساب را تعریف می کند
    /// </summary>
    public interface ITestBalanceHelper
    {
        int GetSourceList(TestBalanceFormat format, string itemTypeName);

        Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId);

        Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId);
    }
}

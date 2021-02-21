using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface IReportDirectUtility
    {
        IEnumerable<int> GetChildTree(int branchId);

        int GetLevelCodeLength(int viewId, int level);

        T ValueOrDefault<T>(DataRow row, string field);

        string ValueOrDefault(DataRow row, string field);

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح قابل استفاده برای گزارشگیری را
        /// برای مولفه حساب داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح قابل استفاده</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، فهرست سطوح زیرمجموعه قابل انتخاب برای گزارشگیری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه مولفه حساب</param>
        /// <returns>فهرست سطوح زیرمجموعه قابل انتخاب</returns>
        Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId);
    }
}

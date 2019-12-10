using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface ITestBalanceUtility : IBalanceUtility
    {
        Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync();
        Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync();
        IQueryable<VoucherLine> IncludeVoucherLineReference(IQueryable<VoucherLine> query);
        Func<TestBalanceItemViewModel, bool> GetUpperlevelFilter(int level);
        Func<TestBalanceItemViewModel, bool> GetCurrentlevelFilter(int level);
        Task<TestBalanceItemViewModel> GetItemFromVoucherLineAsync(TestBalanceItemViewModel line, string fullCode);
        Task<decimal> GetInitialBalanceAsync(int itemId, TestBalanceParameters parameters);
        Task<IEnumerable<TestBalanceItemViewModel>> GetZeroBalanceItemsAsync(
            IEnumerable<TestBalanceItemViewModel> items, TestBalanceMode mode);
        IEnumerable<TestBalanceItemViewModel> FilterBalanceLines(
            IEnumerable<TestBalanceItemViewModel> lines, TreeEntity accountItem);
        int GetItemId(TestBalanceItemViewModel item);
        IEnumerable<TestBalanceItemViewModel> GetSortedItems(IEnumerable<TestBalanceItemViewModel> items);
    }
}

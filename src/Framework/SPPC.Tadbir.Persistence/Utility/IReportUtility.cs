using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface IReportUtility
    {
        Task<List<TModel>> GetRawReportByDateLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions);
        Task<List<TModel>> GetRawReportByDateByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions);
        Task<List<TModel>> GetRawReportByNumberLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions);
        Task<List<TModel>> GetRawReportByNumberByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions);
        IEnumerable<IGrouping<string, TModel>> GetTurnoverGroups<TModel>(
            IEnumerable<TModel> lines, int groupLevel, Func<TModel, bool> lineFilter)
            where TModel : class, IAccountView;
        int GetLevelCodeLength(int viewId, int level);
        IEnumerable<IGrouping<TKey1, TModel>> GetGroupByItems<TModel, TKey1>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1);
        IEnumerable<IGrouping<TKey2, TModel>> GetGroupByItems<TModel, TKey1, TKey2>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2);
        IEnumerable<IGrouping<TKey3, TModel>> GetGroupByItems<TModel, TKey1, TKey2, TKey3>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2,
            Func<TModel, TKey3> selector3);
    }
}

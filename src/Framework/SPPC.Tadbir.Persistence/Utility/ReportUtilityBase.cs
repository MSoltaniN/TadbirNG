using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class ReportUtilityBase : IReportUtility
    {
        public ReportUtilityBase(IConfigRepository config, IDomainMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public async Task<List<TModel>> GetRawReportByDateLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        public async Task<List<TModel>> GetRawReportByDateByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                        .ThenBy(art => art.BranchId)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        public async Task<List<TModel>> GetRawReportByNumberLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .OrderBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        public async Task<List<TModel>> GetRawReportByNumberByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions)
        {
            var lines = await query
                .Where(art => art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .OrderBy(art => art.Voucher.No)
                    .ThenBy(art => art.BranchId)
                .Select(art => _mapper.Map<TModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(gridOptions)
                .ToList();
        }

        public IEnumerable<IGrouping<string, TModel>> GetTurnoverGroups<TModel>(
            IEnumerable<TModel> lines, int groupLevel, Func<TModel, bool> lineFilter)
            where TModel : class, IAccountView
        {
            var selector = GetGroupSelector<TModel>(groupLevel);
            return GetGroupByItems(lines.Where(lineFilter), selector);
        }

        public int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = _config
                .GetViewTreeConfigByViewAsync(viewId)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
        }

        public IEnumerable<IGrouping<TKey1, TModel>> GetGroupByItems<TModel, TKey1>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1)
        {
            foreach (var byFirst in items
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                yield return byFirst;
            }
        }

        public IEnumerable<IGrouping<TKey2, TModel>> GetGroupByItems<TModel, TKey1, TKey2>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2)
        {
            foreach (var byFirst in items
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                foreach (var bySecond in byFirst
                    .OrderBy(selector2)
                    .GroupBy(selector2))
                {
                    yield return bySecond;
                }
            }
        }

        public IEnumerable<IGrouping<TKey3, TModel>> GetGroupByItems<TModel, TKey1, TKey2, TKey3>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2,
            Func<TModel, TKey3> selector3)
        {
            foreach (var byFirst in items
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                foreach (var bySecond in byFirst
                    .OrderBy(selector2)
                    .GroupBy(selector2))
                {
                    foreach (var byThird in bySecond
                        .OrderBy(selector3)
                        .GroupBy(selector3))
                    {
                        yield return byThird;
                    }
                }
            }
        }

        protected virtual Func<TModel, string> GetGroupSelector<TModel>(int groupLevel)
            where TModel : class, IAccountView
        {
            int codeLength = GetLevelCodeLength(ViewName.Account, groupLevel);
            return item => item.AccountFullCode.Substring(0, codeLength);
        }

        private readonly IConfigRepository _config;
        private readonly IDomainMapper _mapper;
    }
}

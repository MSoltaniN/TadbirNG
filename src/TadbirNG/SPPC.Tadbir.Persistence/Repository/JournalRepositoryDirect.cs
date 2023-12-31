﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class JournalRepositoryDirect : LoggingRepositoryBase, IJournalRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="utility"></param>
        public JournalRepositoryDirect(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility utility)
            : base(context, system.Logger)
        {
            Config = system.Config;
            _utility = utility;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ</returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(JournalParameters parameters)
        {
            var journal = new JournalViewModel();
            var sourceList = GetSourceList(parameters.Mode);
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
                switch (parameters.Mode)
                {
                    case JournalMode.ByRows:
                        journal = GetJournalByRow(parameters);
                        break;
                    case JournalMode.ByRowsWithDetail:
                        journal = GetJournalByRow(parameters, false, false, true);
                        break;
                    case JournalMode.ByLedger:
                        journal = await GetJournalByLedgerAsync(parameters);
                        break;
                    case JournalMode.BySubsidiary:
                        journal = await GetJournalBySubsidiaryAsync(parameters);
                        break;
                    case JournalMode.LedgerSummary:
                        journal = await GetJournalLedgerSummaryAsync(parameters);
                        break;
                    case JournalMode.LedgerSummaryByDate:
                        journal = await GetJournalLedgerSummaryByDateAsync(parameters);
                        break;
                    case JournalMode.MonthlyLedgerSummary:
                        journal = await GetJournalMonthlyLedgerSummaryAsync(parameters);
                        break;
                    default:
                        break;
                }
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByDateByBranchAsync(JournalParameters parameters)
        {
            var journal = new JournalViewModel();
            var sourceList = GetSourceList(parameters.Mode);
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
                switch (parameters.Mode)
                {
                    case JournalMode.ByRows:
                        journal = GetJournalByRow(parameters, false, true, false);
                        break;
                    case JournalMode.ByRowsWithDetail:
                        journal = GetJournalByRow(parameters, false, true, true);
                        break;
                    case JournalMode.ByLedger:
                        journal = await GetJournalByLedgerAsync(parameters, false, true);
                        break;
                    case JournalMode.BySubsidiary:
                        journal = await GetJournalBySubsidiaryAsync(parameters, false, true);
                        break;
                    case JournalMode.LedgerSummary:
                        journal = await GetJournalLedgerSummaryAsync(parameters, false, true);
                        break;
                    case JournalMode.LedgerSummaryByDate:
                        journal = await GetJournalLedgerSummaryByDateAsync(parameters, true);
                        break;
                    case JournalMode.MonthlyLedgerSummary:
                        journal = await GetJournalMonthlyLedgerSummaryAsync(parameters, true);
                        break;
                    default:
                        break;
                }
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند</returns>
        public async Task<JournalViewModel> GetJournalByNoAsync(JournalParameters parameters)
        {
            var journal = new JournalViewModel();
            var sourceList = GetSourceList(parameters.Mode, true);
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
                switch (parameters.Mode)
                {
                    case JournalMode.ByRows:
                        journal = GetJournalByRow(parameters, true);
                        break;
                    case JournalMode.ByRowsWithDetail:
                        journal = GetJournalByRow(parameters, true, false, true);
                        break;
                    case JournalMode.ByLedger:
                        journal = await GetJournalByLedgerAsync(parameters, true);
                        break;
                    case JournalMode.BySubsidiary:
                        journal = await GetJournalBySubsidiaryAsync(parameters, true);
                        break;
                    case JournalMode.LedgerSummary:
                        journal = await GetJournalLedgerSummaryAsync(parameters, true);
                        break;
                    default:
                        break;
                }
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByNoByBranchAsync(JournalParameters parameters)
        {
            var journal = new JournalViewModel();
            var sourceList = GetSourceList(parameters.Mode, true);
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
                switch (parameters.Mode)
                {
                    case JournalMode.ByRows:
                        journal = GetJournalByRow(parameters, true, true, false);
                        break;
                    case JournalMode.ByRowsWithDetail:
                        journal = GetJournalByRow(parameters, true, true, true);
                        break;
                    case JournalMode.ByLedger:
                        journal = await GetJournalByLedgerAsync(parameters, true, true);
                        break;
                    case JournalMode.BySubsidiary:
                        journal = await GetJournalBySubsidiaryAsync(parameters, true, true);
                        break;
                    case JournalMode.LedgerSummary:
                        journal = await GetJournalLedgerSummaryAsync(parameters, true, true);
                        break;
                    default:
                        break;
                }
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.Journal; }
        }

        private IConfigRepository Config { get; }

        private static SourceListId GetSourceList(JournalMode mode, bool isByNo = false)
        {
            var sourceList = SourceListId.None;
            switch (mode)
            {
                case JournalMode.ByRows:
                    sourceList = isByNo
                        ? SourceListId.JournalByNoByRow
                        : SourceListId.JournalByDateByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    sourceList = isByNo
                        ? SourceListId.JournalByNoByRowDetail
                        : SourceListId.JournalByDateByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    sourceList = isByNo
                        ? SourceListId.JournalByNoByLedger
                        : SourceListId.JournalByDateByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    sourceList = isByNo
                        ? SourceListId.JournalByNoBySubsidiary
                        : SourceListId.JournalByDateBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    sourceList = isByNo
                        ? SourceListId.JournalByNoSummary
                        : SourceListId.JournalByDateSummary;
                    break;
                case JournalMode.LedgerSummaryByDate:
                    sourceList = isByNo
                        ? SourceListId.None
                        : SourceListId.JournalByDateSummaryByDate;
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    sourceList = isByNo
                        ? SourceListId.None
                        : SourceListId.JournalByDateSummaryByMonth;
                    break;
                default:
                    break;
            }

            return sourceList;
        }

        private static ReportQuery GetJournalByLevelQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            ReportQuery query;
            string command = !byNo
                ? String.Format(JournalQuery.ByDateByLevel, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.ByNoByLevel, length, parameters.FromNo, parameters.ToNo);
            if (isDebit)
            {
                query = new ReportQuery(command);
            }
            else
            {
                query = new ReportQuery(command
                    .Replace("Debit", "Credit")
                    .Replace("Credit1", "Debit"));
            }

            return query;
        }

        private static ReportQuery GetJournalByLevelByBranchQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            string command = !byNo
                ? String.Format(JournalQuery.ByDateByLevelByBranch, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.ByNoByLevelByBranch, length, parameters.FromNo, parameters.ToNo);
            ReportQuery query;
            if (isDebit)
            {
                query = new ReportQuery(command);
            }
            else
            {
                query = new ReportQuery(command
                    .Replace("Debit", "Credit")
                    .Replace("Credit1", "Debit"));
            }

            return query;
        }

        private static ReportQuery GetJournalLedgerSummaryQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            string command = !byNo
                ? String.Format(JournalQuery.ByDateLedgerSummary, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.ByNoLedgerSummary, length, parameters.FromNo, parameters.ToNo);
            ReportQuery query;
            if (isDebit)
            {
                query = new ReportQuery(command);
            }
            else
            {
                query = new ReportQuery(command
                    .Replace("Debit", "Credit")
                    .Replace("Credit1", "Debit"));
            }

            return query;
        }

        private static ReportQuery GetJournalLedgerSummaryByBranchQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            string command = !byNo
                ? String.Format(JournalQuery.ByDateLedgerSummaryByBranch, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.ByNoLedgerSummaryByBranch, length, parameters.FromNo, parameters.ToNo);
            ReportQuery query;
            if (isDebit)
            {
                query = new ReportQuery(command);
            }
            else
            {
                query = new ReportQuery(command
                    .Replace("Debit", "Credit")
                    .Replace("Credit1", "Debit"));
            }

            return query;
        }

        private static IEnumerable<JournalItemViewModel> MergeByNumber(
            IEnumerable<JournalItemViewModel> first, IEnumerable<JournalItemViewModel> second,
            GridOptions gridOptions)
        {
            var items = new List<JournalItemViewModel>();
            var grossItems = first
                .Concat(second)
                .OrderBy(item => item.VoucherNo)
                .ApplyPaging(gridOptions);
            int totalCount = grossItems.Count();
            if (totalCount == 0)
            {
                return items;
            }

            int minNo = grossItems.Min(item => item.VoucherNo);
            int maxNo = grossItems.Max(item => item.VoucherNo);
            int previousCount = first
                .Concat(second)
                .Where(item => item.VoucherNo < minNo)
                .Count();

            if (gridOptions.Filter != null)
            {
                items.AddRange(GetFilteredMergeByNo(first, second, minNo, maxNo));
            }
            else
            {
                foreach (var byNum in first
                    .Where(item => item.VoucherNo >= minNo && item.VoucherNo <= maxNo)
                    .OrderBy(item => item.VoucherNo)
                    .GroupBy(item => item.VoucherNo))
                {
                    items.AddRange(byNum.OrderBy(item => item.AccountFullCode));
                    items.AddRange(second
                        .Where(item => item.VoucherNo == byNum.Key)
                        .OrderBy(item => item.AccountFullCode));
                }
            }

            var paging = gridOptions.Paging;
            int fromIndex = paging.PageSize * (paging.PageIndex - 1);
            return items.Skip(fromIndex - previousCount).Take(paging.PageSize);
        }

        private static IEnumerable<JournalItemViewModel> GetFilteredMergeByNo(
            IEnumerable<JournalItemViewModel> debit, IEnumerable<JournalItemViewModel> credit,
            int minNo, int maxNo)
        {
            var merged = debit
                .Concat(credit)
                .Where(item => item.VoucherNo >= minNo
                    && item.VoucherNo <= maxNo)
                .OrderBy(item => item.VoucherNo)
                    .ThenBy(item => item.Credit)
                    .ThenBy(item => item.AccountFullCode)
                .ToList();
            var sortedCredit = new List<JournalItemViewModel>();
            int start = 0;
            int count = 0;
            for (int i = 0; i < merged.Count; i++)
            {
                var current = merged[i];
                int currentNo = current.VoucherNo;
                while (current.Credit > 0.0M && current.VoucherNo == currentNo)
                {
                    start = i;
                    sortedCredit.Add(current);
                    count++;
                    if ((start + count) < merged.Count)
                    {
                        current = merged[start + count];
                    }
                    else
                    {
                        break;
                    }
                }

                if (sortedCredit.Count > 1)
                {
                    sortedCredit = sortedCredit.OrderBy(item => item.AccountFullCode).ToList();
                    int index = 0;
                    for (int j = start; j < start + count; j++)
                    {
                        merged[j] = sortedCredit[index];
                        index++;
                    }
                }

                count = 0;
                sortedCredit.Clear();
            }

            return merged;
        }

        private static IEnumerable<JournalItemViewModel> MergeByNumber(
            IEnumerable<JournalItemViewModel> first, IEnumerable<JournalItemViewModel> second,
            IEnumerable<JournalItemViewModel> third, IEnumerable<JournalItemViewModel> fourth,
            GridOptions gridOptions)
        {
            var items = new List<JournalItemViewModel>();
            var grossItems = first
                .Concat(second)
                .Concat(third)
                .Concat(fourth)
                .OrderBy(item => item.VoucherNo)
                .ApplyPaging(gridOptions);
            int totalCount = grossItems.Count();
            if (totalCount == 0)
            {
                return items;
            }

            int minNo = grossItems.Min(item => item.VoucherNo);
            int maxNo = grossItems.Max(item => item.VoucherNo);
            int previousCount = first
                .Concat(second)
                .Concat(third)
                .Concat(fourth)
                .Where(item => item.VoucherNo < minNo)
                .Count();

            if (gridOptions.Filter != null)
            {
                items.AddRange(GetFilteredMergeByNo(first, second, third, fourth, minNo, maxNo));
            }
            else
            {
                foreach (var byNum in first
                    .Where(item => item.VoucherNo >= minNo && item.VoucherNo <= maxNo)
                    .OrderBy(item => item.VoucherNo)
                    .GroupBy(item => item.VoucherNo))
                {
                    items.AddRange(byNum.OrderBy(item => item.AccountFullCode));
                    items.AddRange(second
                        .Where(item => item.VoucherNo == byNum.Key)
                        .OrderBy(item => item.AccountFullCode));
                    items.AddRange(third
                        .Where(item => item.VoucherNo == byNum.Key)
                        .OrderBy(item => item.AccountFullCode));
                    items.AddRange(fourth
                        .Where(item => item.VoucherNo == byNum.Key)
                        .OrderBy(item => item.AccountFullCode));
                }
            }

            var paging = gridOptions.Paging;
            int fromIndex = paging.PageSize * (paging.PageIndex - 1);
            return items.Skip(fromIndex - previousCount).Take(paging.PageSize);
        }

        private static IEnumerable<JournalItemViewModel> GetFilteredMergeByNo(
            IEnumerable<JournalItemViewModel> debitS, IEnumerable<JournalItemViewModel> debitL,
            IEnumerable<JournalItemViewModel> creditS, IEnumerable<JournalItemViewModel> creditL,
            int minNo, int maxNo)
        {
            var merged = debitS
                .Concat(debitL)
                .Concat(creditS)
                .Concat(creditL)
                .Where(item => item.VoucherNo >= minNo
                    && item.VoucherNo <= maxNo)
                .OrderBy(item => item.VoucherNo)
                    .ThenBy(item => item.Credit)
                    .ThenBy(item => item.AccountFullCode)
                .ToList();
            var sortedCredit = new List<JournalItemViewModel>();
            int start = 0;
            int count = 0;
            for (int i = 0; i < merged.Count; i++)
            {
                var current = merged[i];
                int currentNo = current.VoucherNo;
                while (current.Credit > 0.0M && current.VoucherNo == currentNo)
                {
                    start = i;
                    sortedCredit.Add(current);
                    count++;
                    if ((start + count) < merged.Count)
                    {
                        current = merged[start + count];
                    }
                    else
                    {
                        break;
                    }
                }

                if (sortedCredit.Count > 1)
                {
                    sortedCredit = sortedCredit.OrderBy(item => item.AccountFullCode).ToList();
                    int index = 0;
                    for (int j = start; j < start + count; j++)
                    {
                        merged[j] = sortedCredit[index];
                        index++;
                    }
                }

                count = 0;
                sortedCredit.Clear();
            }

            return merged;
        }

        private static IEnumerable<JournalItemViewModel> MergeByDate(
            IEnumerable<JournalItemViewModel> first, IEnumerable<JournalItemViewModel> second,
            GridOptions gridOptions)
        {
            var items = new List<JournalItemViewModel>();
            var grossItems = first
                .Concat(second)
                .OrderBy(item => item.VoucherDate)
                .ApplyPaging(gridOptions);
            int totalCount = grossItems.Count();
            if (totalCount == 0)
            {
                return items;
            }

            var minDate = grossItems.Min(item => item.VoucherDate);
            var maxDate = grossItems.Max(item => item.VoucherDate);
            int previousCount = first
                .Concat(second)
                .Where(item => item.VoucherDate < minDate)
                .Count();

            if (gridOptions.Filter != null)
            {
                items.AddRange(GetFilteredMergeByDate(first, second, minDate, maxDate));
            }
            else
            {
                foreach (var byNum in first
                    .Where(item => item.VoucherDate >= minDate && item.VoucherDate <= maxDate)
                    .OrderBy(item => item.VoucherDate)
                    .GroupBy(item => item.VoucherDate))
                {
                    items.AddRange(byNum.OrderBy(item => item.AccountFullCode));
                    items.AddRange(second
                        .Where(item => item.VoucherDate == byNum.Key)
                        .OrderBy(item => item.AccountFullCode));
                }
            }

            var paging = gridOptions.Paging;
            int fromIndex = paging.PageSize * (paging.PageIndex - 1);
            return items.Skip(fromIndex - previousCount).Take(paging.PageSize);
        }

        private static IEnumerable<JournalItemViewModel> MergeByDate(
            IEnumerable<JournalItemViewModel> first, IEnumerable<JournalItemViewModel> second)
        {
            var items = new List<JournalItemViewModel>();
            foreach (var byNum in first
                .OrderBy(item => item.VoucherDate.Date)
                .GroupBy(item => item.VoucherDate.Date))
            {
                items.AddRange(byNum.OrderBy(item => item.AccountFullCode));
                items.AddRange(second
                    .Where(item => item.VoucherDate.Date == byNum.Key)
                    .OrderBy(item => item.AccountFullCode));
            }

            return items;
        }

        private static IEnumerable<JournalItemViewModel> GetFilteredMergeByDate(
            IEnumerable<JournalItemViewModel> debit, IEnumerable<JournalItemViewModel> credit,
            DateTime minDate, DateTime maxDate)
        {
            var merged = debit
                .Concat(credit)
                .Where(item => item.VoucherDate >= minDate
                    && item.VoucherDate <= maxDate)
                .OrderBy(item => item.VoucherDate)
                    .ThenBy(item => item.Credit)
                    .ThenBy(item => item.AccountFullCode)
                .ToList();
            var sortedCredit = new List<JournalItemViewModel>();
            int start = 0;
            int count = 0;
            for (int i = 0; i < merged.Count; i++)
            {
                var current = merged[i];
                DateTime currentDate = current.VoucherDate;
                while (current.Credit > 0.0M && current.VoucherDate == currentDate)
                {
                    start = i;
                    sortedCredit.Add(current);
                    count++;
                    if ((start + count) < merged.Count)
                    {
                        current = merged[start + count];
                    }
                    else
                    {
                        break;
                    }
                }

                if (sortedCredit.Count > 1)
                {
                    sortedCredit = sortedCredit.OrderBy(item => item.AccountFullCode).ToList();
                    int index = 0;
                    for (int j = start; j < start + count; j++)
                    {
                        merged[j] = sortedCredit[index];
                        index++;
                    }
                }

                count = 0;
                sortedCredit.Clear();
            }

            return merged;
        }

        private static bool HasColumnFilterOrSort(GridOptions gridOptions)
        {
            return gridOptions.Filter != null
                || gridOptions.SortColumns.Count > 0;
        }

        private ReportQuery GetJournalByRowQuery(JournalParameters parameters,
            bool byNo = false, bool byBranch = false, bool hasDetail = false)
        {
            ReportQuery query;
            var paging = parameters.GridOptions.Paging;
            int fromRow = (paging.PageSize * (paging.PageIndex - 1)) + 1;
            int toRow = paging.PageSize * paging.PageIndex;
            string sorting = _utility.GetColumnSorting(parameters.GridOptions);
            if (String.IsNullOrEmpty(sorting))
            {
                sorting = byBranch
                    ? ByBranchDefaultSorting
                    : ByRowDefaultSorting;
            }

            if (byNo)
            {
                if (byBranch)
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByNoByRowDetailByBranch,
                        sorting, parameters.FromNo, parameters.ToNo, fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByNoByRowByBranch,
                        sorting, parameters.FromNo, parameters.ToNo, fromRow, toRow));
                }
                else
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByNoByRowDetail,
                        sorting, parameters.FromNo, parameters.ToNo, fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByNoByRow,
                        sorting, parameters.FromNo, parameters.ToNo, fromRow, toRow));
                }
            }
            else
            {
                if (byBranch)
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByDateByRowDetailsByBranch,
                        sorting, parameters.FromDate.ToShortDateString(false),
                        parameters.ToDate.ToShortDateString(false), fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByDateByRowByBranch,
                        sorting, parameters.FromDate.ToShortDateString(false),
                        parameters.ToDate.ToShortDateString(false), fromRow, toRow));
                }
                else
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByDateByRowDetails,
                        sorting, parameters.FromDate.ToShortDateString(false),
                        parameters.ToDate.ToShortDateString(false), fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByDateByRow,
                        sorting, parameters.FromDate.ToShortDateString(false),
                        parameters.ToDate.ToShortDateString(false), fromRow, toRow));
                }
            }

            return query;
        }

        private JournalViewModel GetJournalByRow(JournalParameters parameters,
            bool byNo = false, bool byBranch = false, bool hasDetail = false)
        {
            var journal = new JournalViewModel();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var query = GetJournalByRowQuery(parameters, byNo, byBranch, hasDetail);
            ApplyEnvironmentFilters(query, parameters.GridOptions);
            var result = DbConsole.ExecuteQuery(query.Query);
            journal.Items.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row)));
            foreach (var item in journal.Items)
            {
                item.Description = Context.Localize(item.Description);
            }

            query = !byNo
                ? new ReportQuery(String.Format(JournalQuery.MainByDateByRow,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false)))
                : new ReportQuery(String.Format(JournalQuery.MainByNoByRow,
                    parameters.FromNo, parameters.ToNo));
            ApplyEnvironmentFilters(query, parameters.GridOptions);
            result = DbConsole.ExecuteQuery(query.Query);
            journal.TotalCount = _utility.ValueOrDefault<int>(result.Rows[0], "TotalCount");
            journal.DebitSum = _utility.ValueOrDefault<decimal>(result.Rows[0], "DebitSum");
            journal.CreditSum = _utility.ValueOrDefault<decimal>(result.Rows[0], "CreditSum");
            return journal;
        }

        private async Task<JournalViewModel> GetJournalByLedgerAsync(
            JournalParameters parameters, bool byNo = false, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            int length = Config.GetLevelCodeLength(0);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var debitItems = GetByLevelItems(parameters, length, byNo, byBranch, true);
            var creditItems = GetByLevelItems(parameters, length, byNo, byBranch, false);
            if (HasColumnFilterOrSort(parameters.GridOptions))
            {
                journal = await GetFilteredJournalByLedgerAsync(
                    debitItems, creditItems, parameters.GridOptions);
                return GetJournalWithQuote(journal, parameters, byNo);
            }

            journal.TotalCount = debitItems.Count + creditItems.Count;
            journal.DebitSum = debitItems.Sum(item => item.Debit);
            journal.CreditSum = creditItems.Sum(item => item.Credit);
            journal.Items.AddRange(MergeByNumber(debitItems, creditItems, parameters.GridOptions));
            await SetNameAndDescriptionAsync(journal.Items);

            return GetJournalWithQuote(journal, parameters, byNo);
        }

        private async Task<JournalViewModel> GetFilteredJournalByLedgerAsync(
            List<JournalItemViewModel> debit, List<JournalItemViewModel> credit,
            GridOptions gridOptions, bool byNo = true)
        {
            await SetNameAndDescriptionAsync(debit);
            await SetNameAndDescriptionAsync(credit);
            var filteredDebit = debit.Apply(gridOptions, false);
            var filteredCredit = credit.Apply(gridOptions, false);
            var journal = new JournalViewModel
            {
                TotalCount = filteredDebit.Count() + filteredCredit.Count(),
                DebitSum = filteredDebit.Sum(item => item.Debit),
                CreditSum = filteredCredit.Sum(item => item.Credit)
            };

            if (byNo)
            {
                journal.Items.AddRange(MergeByNumber(filteredDebit, filteredCredit, gridOptions));
            }
            else
            {
                journal.Items.AddRange(MergeByDate(filteredDebit, filteredCredit, gridOptions));
            }

            return journal;
        }

        private async Task<JournalViewModel> GetJournalBySubsidiaryAsync(
            JournalParameters parameters, bool byNo = false, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            int ledgerLength = Config.GetLevelCodeLength(0);
            int subsidLength = Config.GetLevelCodeLength(1);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var subsidDebit = GetByLevelItems(parameters, subsidLength, byNo, byBranch, true, " AND acc.Level >= 1 ");
            var ledgerDebit = GetByLevelItems(parameters, ledgerLength, byNo, byBranch, true, " AND acc.Level = 0 ");
            var subsidCredit = GetByLevelItems(parameters, subsidLength, byNo, byBranch, false, " AND acc.Level >= 1 ");
            var ledgerCredit = GetByLevelItems(parameters, ledgerLength, byNo, byBranch, false, " AND acc.Level = 0 ");
            if (HasColumnFilterOrSort(parameters.GridOptions))
            {
                journal = await GetFilteredJournalBySubsidiaryAsync(
                    ledgerDebit, ledgerCredit, subsidDebit, subsidCredit, parameters.GridOptions);
                return GetJournalWithQuote(journal, parameters, byNo);
            }

            journal.TotalCount = subsidDebit.Count + subsidCredit.Count + ledgerDebit.Count + ledgerCredit.Count;
            journal.DebitSum = subsidDebit.Sum(item => item.Debit) + ledgerDebit.Sum(item => item.Debit);
            journal.CreditSum = subsidCredit.Sum(item => item.Credit) + ledgerCredit.Sum(item => item.Credit);
            journal.Items.AddRange(MergeByNumber(subsidDebit, ledgerDebit, subsidCredit, ledgerCredit, parameters.GridOptions));
            await SetNameAndDescriptionAsync(journal.Items);

            return GetJournalWithQuote(journal, parameters, byNo);
        }

        private async Task<JournalViewModel> GetFilteredJournalBySubsidiaryAsync(
            List<JournalItemViewModel> ledgerDebit, List<JournalItemViewModel> ledgerCredit,
            List<JournalItemViewModel> subsidDebit, List<JournalItemViewModel> subsidCredit,
            GridOptions gridOptions)
        {
            await SetNameAndDescriptionAsync(ledgerDebit);
            await SetNameAndDescriptionAsync(ledgerCredit);
            await SetNameAndDescriptionAsync(subsidDebit);
            await SetNameAndDescriptionAsync(subsidCredit);
            var filteredDebitL = ledgerDebit.Apply(gridOptions, false);
            var filteredCreditL = ledgerCredit.Apply(gridOptions, false);
            var filteredDebitS = subsidDebit.Apply(gridOptions, false);
            var filteredCreditS = subsidCredit.Apply(gridOptions, false);
            var journal = new JournalViewModel
            {
                TotalCount = filteredDebitL.Count() + filteredDebitS.Count()
                    + filteredCreditL.Count() + filteredCreditS.Count(),
                DebitSum = filteredDebitL.Sum(item => item.Debit)
                    + filteredDebitS.Sum(item => item.Debit),
                CreditSum = filteredCreditL.Sum(item => item.Credit)
                    + filteredCreditS.Sum(item => item.Credit)
            };

            journal.Items.AddRange(MergeByNumber(
                filteredDebitS, filteredDebitL, filteredCreditS, filteredCreditL, gridOptions));
            return journal;
        }

        private async Task<JournalViewModel> GetJournalLedgerSummaryAsync(
            JournalParameters parameters, bool byNo = false, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            var items = new List<JournalItemViewModel>();
            int length = Config.GetLevelCodeLength(0);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            items.AddRange(GetLedgerSummaryItems(parameters, length, byNo, byBranch, true));
            items.AddRange(GetLedgerSummaryItems(parameters, length, byNo, byBranch, false));

            if (HasColumnFilterOrSort(parameters.GridOptions))
            {
                await SetNameAndDescriptionAsync(items);
                items = items
                    .Apply(parameters.GridOptions, false)
                    .ToList();
            }

            await PrepareJournalAsync(journal, items, parameters.GridOptions);
            return journal;
        }

        private async Task<JournalViewModel> GetJournalLedgerSummaryByDateAsync(
            JournalParameters parameters, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            int length = Config.GetLevelCodeLength(0);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var debitItems = GetLedgerSummaryByDateItems(parameters, length, byBranch, false, true);
            var creditItems = GetLedgerSummaryByDateItems(parameters, length, byBranch, false, false);
            if (HasColumnFilterOrSort(parameters.GridOptions))
            {
                return await GetFilteredJournalByLedgerAsync(
                    debitItems.ToList(), creditItems.ToList(), parameters.GridOptions, false);
            }

            journal.TotalCount = debitItems.Count() + creditItems.Count();
            journal.DebitSum = debitItems.Sum(item => item.Debit);
            journal.CreditSum = creditItems.Sum(item => item.Credit);
            journal.Items.AddRange(MergeByDate(debitItems, creditItems, parameters.GridOptions));
            await SetNameAndDescriptionAsync(journal.Items);

            return journal;
        }

        private async Task<JournalViewModel> GetJournalMonthlyLedgerSummaryAsync(
            JournalParameters parameters, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            var items = new List<JournalItemViewModel>();
            int length = Config.GetLevelCodeLength(0);
            var calendar = await Config.GetCurrentCalendarAsync();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var monthJournal = new List<JournalItemViewModel>();
            var monthEnum = new DateSpanEnumerator(parameters.FromDate, parameters.ToDate, calendar);
            var monthParams = parameters.GetCopy();
            foreach (var month in monthEnum.GetMonths())
            {
                monthParams.FromDate = month.Start;
                monthParams.ToDate = month.End;

                var debitItems = GetLedgerSummaryByDateItems(monthParams, length, byBranch, true, true)
                    .ToList();
                var creditItems = GetLedgerSummaryByDateItems(monthParams, length, byBranch, true, false)
                    .ToList();
                Array.ForEach(debitItems.ToArray(), item => item.VoucherDate = month.End);
                Array.ForEach(creditItems.ToArray(), item => item.VoucherDate = month.End);
                if (HasColumnFilterOrSort(parameters.GridOptions))
                {
                    await SetNameAndDescriptionAsync(debitItems);
                    await SetNameAndDescriptionAsync(creditItems);
                    var filteredDebit = debitItems.Apply(parameters.GridOptions, false);
                    var filteredCredit = creditItems.Apply(parameters.GridOptions, false);
                    monthJournal.AddRange(MergeByDate(filteredDebit, filteredCredit));
                }
                else
                {
                    monthJournal.AddRange(MergeByDate(debitItems, creditItems));
                }

                items.AddRange(monthJournal);
                monthJournal.Clear();
            }

            await PrepareJournalAsync(journal, items, parameters.GridOptions);
            return journal;
        }

        private JournalViewModel GetJournalWithQuote(
            JournalViewModel journal, JournalParameters parameters, bool byNo)
        {
            ReportQuery query = byNo
                ? new ReportQuery(String.Format(JournalQuery.StartTurnoverByNo, parameters.FromNo))
                : new ReportQuery(String.Format(JournalQuery.StartTurnoverByDate,
                    parameters.FromDate.ToShortDateString(false)));
            query.SetFilter(_utility.GetEnvironmentFilters(parameters.GridOptions));
            var result = DbConsole.ExecuteQuery(query.Query);
            if (result.Rows.Count == 0)
            {
                return journal;
            }

            var row = result.Rows[0];
            journal.StartDebit = _utility.ValueOrDefault<decimal>(row, "StartDebit");
            journal.StartCredit = _utility.ValueOrDefault<decimal>(row, "StartCredit");
            return journal;
        }

        private List<JournalItemViewModel> GetByLevelItems(
            JournalParameters parameters, int length, bool byNo, bool byBranch, bool isDebit, string filter = "")
        {
            var query = !byBranch
                ? GetJournalByLevelQuery(parameters, length, byNo, isDebit)
                : GetJournalByLevelByBranchQuery(parameters, length, byNo, isDebit);
            var filterBuilder = new StringBuilder(_utility.GetEnvironmentFilters(parameters.GridOptions));
            filterBuilder.Append(filter);
            query.SetFilter(filterBuilder.ToString());
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row))
                .ToList();
        }

        private IEnumerable<JournalItemViewModel> GetLedgerSummaryItems(
            JournalParameters parameters, int length, bool byNo, bool byBranch, bool isDebit)
        {
            var query = !byBranch
                ? GetJournalLedgerSummaryQuery(parameters, length, byNo, isDebit)
                : GetJournalLedgerSummaryByBranchQuery(parameters, length, byNo, isDebit);
            query.SetFilter(_utility.GetEnvironmentFilters(parameters.GridOptions));
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row));
        }

        private IEnumerable<JournalItemViewModel> GetLedgerSummaryByDateItems(
            JournalParameters parameters, int length, bool byBranch, bool isMonthly, bool isDebit)
        {
            string command = String.Empty;
            var query = default(ReportQuery);
            if (isMonthly)
            {
                command = !byBranch
                    ? String.Format(JournalQuery.MonthlyLedgerSummary, length,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                    : String.Format(JournalQuery.MonthlyLedgerSummaryByBranch, length,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false));
            }
            else
            {
                command = !byBranch
                    ? String.Format(JournalQuery.LedgerSummaryByDate, length,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                    : String.Format(JournalQuery.LedgerSummaryByDateByBranch, length,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false));
            }

            if (isDebit)
            {
                query = new ReportQuery(command);
            }
            else
            {
                query = new ReportQuery(command
                    .Replace("Debit", "Credit")
                    .Replace("Credit1", "Debit"));
            }

            query.SetFilter(_utility.GetEnvironmentFilters(parameters.GridOptions));
            var result = DbConsole.ExecuteQuery(query.Query);

            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row));
        }

        private async Task PrepareJournalAsync(
            JournalViewModel journal, IEnumerable<JournalItemViewModel> items, GridOptions gridOptions)
        {
            journal.TotalCount = items.Count();
            journal.DebitSum = items.Sum(item => item.Debit);
            journal.CreditSum = items.Sum(item => item.Credit);
            journal.Items.AddRange(items.ApplyPaging(gridOptions));
            if (gridOptions.Filter == null)
            {
                await SetNameAndDescriptionAsync(journal.Items);
            }
        }

        private JournalItemViewModel GetJournalItem(DataRow row)
        {
            var item = new JournalItemViewModel()
            {
                Id = _utility.ValueOrDefault<int>(row, "Id"),
                RowNo = _utility.ValueOrDefault<int>(row, "RowNum"),
                VoucherNo = _utility.ValueOrDefault<int>(row, "No"),
                AccountFullCode = _utility.ValueOrDefault(row, "AccountFullCode"),
                AccountName = _utility.ValueOrDefault(row, "AccountName"),
                DetailAccountFullCode = _utility.ValueOrDefault(row, "DetailAccountFullCode"),
                DetailAccountName = _utility.ValueOrDefault(row, "DetailAccountName"),
                CostCenterFullCode = _utility.ValueOrDefault(row, "CostCenterFullCode"),
                CostCenterName = _utility.ValueOrDefault(row, "CostCenterName"),
                ProjectFullCode = _utility.ValueOrDefault(row, "ProjectFullCode"),
                ProjectName = _utility.ValueOrDefault(row, "ProjectName"),
                Description = _utility.ValueOrDefault(row, "Description"),
                Debit = _utility.ValueOrDefault<decimal>(row, "Debit"),
                Credit = _utility.ValueOrDefault<decimal>(row, "Credit"),
                Mark = _utility.ValueOrDefault(row, "Mark"),
                BranchName = _utility.ValueOrDefault(row, "BranchName")
            };

            if (String.IsNullOrEmpty(item.AccountFullCode))
            {
                item.AccountFullCode = _utility.ValueOrDefault(row, "FullCode");
            }

            if (String.IsNullOrEmpty(item.AccountName))
            {
                item.AccountName = _utility.ValueOrDefault(row, "Name");
            }

            item.VoucherDate = row.Table.Columns.Contains("Date")
                ? DateTime.Parse(row["Date"].ToString())
                : DateTime.MinValue;
            return item;
        }

        private async Task SetNameAndDescriptionAsync(List<JournalItemViewModel> items)
        {
            var repository = UnitOfWork.GetRepository<Account>();
            var fullCodes = items
                .Select(item => item.AccountFullCode);
            var accounts = await repository
                .GetEntityQuery()
                .Where(acc => fullCodes.Contains(acc.FullCode))
                .Select(acc => new KeyValuePair<string, string>(acc.FullCode, acc.Name))
                .ToListAsync();
            var accountMap = new Dictionary<string, string>(accounts.Count);
            foreach (var keyValue in accounts)
            {
                accountMap.Add(keyValue.Key, keyValue.Value);
            }

            foreach (var item in items)
            {
                item.AccountName = accountMap[item.AccountFullCode];
                item.Description = Context.Localize(AppStrings.AsQuotedInVoucherLines);
            }
        }

        private void ApplyEnvironmentFilters(ReportQuery query, GridOptions gridOptions)
        {
            var filterBuilder = new StringBuilder(_utility.GetEnvironmentFilters(gridOptions));
            if (gridOptions.Filter != null)
            {
                var columnFilters = _utility
                    .GetColumnFilters(gridOptions)
                    .Replace("Description", "vl.Description");
                filterBuilder.AppendFormat(" AND {0}", columnFilters);
            }

            query.SetFilter(filterBuilder.ToString());
        }

        private const string ByRowDefaultSorting = "v.Date, v.No, vl.RowNo";
        private const string ByBranchDefaultSorting = "v.Date, v.No, vl.BranchId";
        private readonly IReportDirectUtility _utility;
    }
}

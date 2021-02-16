using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public class JournalRepositoryDirect : LoggingRepositoryBase, IJournalRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public JournalRepositoryDirect(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ</returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    sourceList = SourceListId.JournalByDateByRow;
                    journal = GetJournalByRow(parameters);
                    break;
                case JournalMode.ByRowsWithDetail:
                    sourceList = SourceListId.JournalByDateByRowDetail;
                    journal = GetJournalByRow(parameters, false, false, true);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByLedgerAsync(parameters);
                    sourceList = SourceListId.JournalByDateByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalBySubsidiaryAsync(parameters);
                    sourceList = SourceListId.JournalByDateBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalLedgerSummaryAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummary;
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalLedgerSummaryByDateAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummaryByDate;
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalMonthlyLedgerSummaryAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummaryByMonth;
                    break;
                default:
                    break;
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
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = GetJournalByRow(parameters, false, true, false);
                    sourceList = SourceListId.JournalByDateByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = GetJournalByRow(parameters, false, true, true);
                    sourceList = SourceListId.JournalByDateByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByLedgerAsync(parameters, false, true);
                    sourceList = SourceListId.JournalByDateByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalBySubsidiaryAsync(parameters, false, true);
                    sourceList = SourceListId.JournalByDateBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalLedgerSummaryAsync(parameters, false, true);
                    sourceList = SourceListId.JournalByDateSummary;
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalLedgerSummaryByDateAsync(parameters, true);
                    sourceList = SourceListId.JournalByDateSummaryByDate;
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalMonthlyLedgerSummaryAsync(parameters, true);
                    sourceList = SourceListId.JournalByDateSummaryByMonth;
                    break;
                default:
                    break;
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
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = GetJournalByRow(parameters, true);
                    sourceList = SourceListId.JournalByNoByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = GetJournalByRow(parameters, true, false, true);
                    sourceList = SourceListId.JournalByNoByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByLedgerAsync(parameters, true);
                    sourceList = SourceListId.JournalByNoByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalBySubsidiaryAsync(parameters, true);
                    sourceList = SourceListId.JournalByNoBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalLedgerSummaryAsync(parameters, true);
                    sourceList = SourceListId.JournalByNoSummary;
                    break;
                default:
                    break;
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
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = GetJournalByRow(parameters, true, true, false);
                    sourceList = SourceListId.JournalByNoByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = GetJournalByRow(parameters, true, true, true);
                    sourceList = SourceListId.JournalByNoByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByLedgerAsync(parameters, true, true);
                    sourceList = SourceListId.JournalByNoByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalBySubsidiaryAsync(parameters, true, true);
                    sourceList = SourceListId.JournalByNoBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalLedgerSummaryAsync(parameters, true, true);
                    sourceList = SourceListId.JournalByNoSummary;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private JournalViewModel GetJournalByRow(JournalParameters parameters,
            bool byNo = false, bool byBranch = false, bool hasDetail = false)
        {
            var journal = new JournalViewModel();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var paging = parameters.GridOptions.Paging;
            int fromRow = (paging.PageSize * (paging.PageIndex - 1)) + 1;
            int toRow = paging.PageSize * paging.PageIndex;
            var query = GetJournalByRowQuery(parameters, byNo, byBranch, hasDetail);
            ApplyEnvironmentFilters(query, parameters.GridOptions);
            var result = DbConsole.ExecuteQuery(query.Query);
            journal.Items.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row)));

            query = !byNo
                ? new ReportQuery(String.Format(JournalQuery.MainByDateByRow,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false)))
                : new ReportQuery(String.Format(JournalQuery.MainByNoByRow,
                    parameters.FromNo, parameters.ToNo));
            ApplyEnvironmentFilters(query, parameters.GridOptions);
            result = DbConsole.ExecuteQuery(query.Query);
            journal.TotalCount = Int32.Parse(result.Rows[0]["TotalCount"].ToString());
            journal.DebitSum = Decimal.Parse(result.Rows[0]["DebitSum"].ToString());
            journal.CreditSum = Decimal.Parse(result.Rows[0]["CreditSum"].ToString());
            return journal;
        }

        private async Task<JournalViewModel> GetJournalByLedgerAsync(
            JournalParameters parameters, bool byNo = false, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            int length = GetLevelCodeLength(0);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var debitItems = GetByLevelItems(parameters, length, byNo, byBranch, true);
            var creditItems = GetByLevelItems(parameters, length, byNo, byBranch, false);
            journal.TotalCount = debitItems.Count() + creditItems.Count();
            journal.DebitSum = debitItems.Sum(item => item.Debit);
            journal.CreditSum = creditItems.Sum(item => item.Credit);
            journal.Items.AddRange(MergeByNumber(debitItems, creditItems, parameters.GridOptions));
            await SetNameAndDescriptionAsync(journal.Items);

            return journal;
        }

        private async Task<JournalViewModel> GetJournalBySubsidiaryAsync(
            JournalParameters parameters, bool byNo = false, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            int ledgerLength = GetLevelCodeLength(0);
            int subsidLength = GetLevelCodeLength(1);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var subsidDebit = GetByLevelItems(parameters, subsidLength, byNo, byBranch, true, " AND acc.Level >= 1 ");
            var ledgerDebit = GetByLevelItems(parameters, ledgerLength, byNo, byBranch, true, " AND acc.Level = 0 ");
            var subsidCredit = GetByLevelItems(parameters, subsidLength, byNo, byBranch, false, " AND acc.Level >= 1 ");
            var ledgerCredit = GetByLevelItems(parameters, ledgerLength, byNo, byBranch, false, " AND acc.Level = 0 ");
            journal.TotalCount = subsidDebit.Count() + subsidCredit.Count() + ledgerDebit.Count() + ledgerCredit.Count();
            journal.DebitSum = subsidDebit.Sum(item => item.Debit) + ledgerDebit.Sum(item => item.Debit);
            journal.CreditSum = subsidCredit.Sum(item => item.Credit) + ledgerCredit.Sum(item => item.Credit);
            journal.Items.AddRange(MergeByNumber(subsidDebit, ledgerDebit, subsidCredit, ledgerCredit, parameters.GridOptions));
            await SetNameAndDescriptionAsync(journal.Items);

            return journal;
        }

        private async Task<JournalViewModel> GetJournalLedgerSummaryAsync(
            JournalParameters parameters, bool byNo = false, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            var items = new List<JournalItemViewModel>();
            int length = GetLevelCodeLength(0);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            items.AddRange(GetLedgerSummaryItems(parameters, length, byNo, byBranch, true));
            items.AddRange(GetLedgerSummaryItems(parameters, length, byNo, byBranch, false));

            await PrepareJournalAsync(journal, items, parameters.GridOptions);
            return journal;
        }

        private async Task<JournalViewModel> GetJournalLedgerSummaryByDateAsync(
            JournalParameters parameters, bool byBranch = false)
        {
            var journal = new JournalViewModel();
            int length = GetLevelCodeLength(0);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var debitItems = GetLedgerSummaryByDateItems(parameters, length, byBranch, true);
            var creditItems = GetLedgerSummaryByDateItems(parameters, length, byBranch, false);
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
            int length = GetLevelCodeLength(0);
            int calendarType = await Config.GetCurrentCalendarAsync();
            Calendar calendar = (calendarType == (int)CalendarType.Jalali)
                ? new PersianCalendar() as Calendar
                : new GregorianCalendar();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

            var monthJournal = new List<JournalItemViewModel>();
            var monthEnum = new MonthEnumerator(parameters.FromDate, parameters.ToDate, calendar);
            var monthParams = parameters.GetCopy();
            foreach (var month in monthEnum.GetMonths())
            {
                monthParams.FromDate = month.Start;
                monthParams.ToDate = month.End;

                var debitItems = GetLedgerSummaryByDateItems(monthParams, length, byBranch, true);
                var creditItems = GetLedgerSummaryByDateItems(monthParams, length, byBranch, false);
                monthJournal.AddRange(MergeByDate(debitItems, creditItems));

                if (monthJournal.Count > 0)
                {
                    Array.ForEach(monthJournal.ToArray(), item => item.VoucherDate = month.End);
                    items.AddRange(monthJournal);
                    monthJournal.Clear();
                }
            }

            await PrepareJournalAsync(journal, items, parameters.GridOptions);
            return journal;
        }

        private ReportQuery GetJournalByRowQuery(JournalParameters parameters,
            bool byNo = false, bool byBranch = false, bool hasDetail = false)
        {
            var query = default(ReportQuery);
            var paging = parameters.GridOptions.Paging;
            int fromRow = (paging.PageSize * (paging.PageIndex - 1)) + 1;
            int toRow = paging.PageSize * paging.PageIndex;
            if (byNo)
            {
                if (byBranch)
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByNoByRowDetailByBranch,
                        parameters.FromNo, parameters.ToNo, fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByNoByRowByBranch,
                        parameters.FromNo, parameters.ToNo, fromRow, toRow));
                }
                else
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByNoByRowDetail,
                        parameters.FromNo, parameters.ToNo, fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByNoByRow,
                        parameters.FromNo, parameters.ToNo, fromRow, toRow));
                }
            }
            else
            {
                if (byBranch)
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByDateByRowDetailsByBranch,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false),
                        fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByDateByRowByBranch,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false),
                        fromRow, toRow));
                }
                else
                {
                    query = hasDetail
                    ? new ReportQuery(String.Format(JournalQuery.ByDateByRowDetails,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false),
                        fromRow, toRow))
                    : new ReportQuery(String.Format(JournalQuery.ByDateByRow,
                        parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false),
                        fromRow, toRow));
                }
            }

            return query;
        }

        private IEnumerable<JournalItemViewModel> GetByLevelItems(
            JournalParameters parameters, int length, bool byNo, bool byBranch, bool isDebit, string filter = "")
        {
            var query = !byBranch
                ? GetJournalByLevelQuery(parameters, length, byNo, isDebit)
                : GetJournalByLevelByBranchQuery(parameters, length, byNo, isDebit);
            query.AddFilter(filter);
            ApplyEnvironmentFilters(query, parameters.GridOptions);
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row));
        }

        private ReportQuery GetJournalByLevelQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            var query = default(ReportQuery);
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

        private ReportQuery GetJournalByLevelByBranchQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            var query = default(ReportQuery);
            string command = !byNo
                ? String.Format(JournalQuery.ByDateByLevelByBranch, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.ByNoByLevelByBranch, length, parameters.FromNo, parameters.ToNo);
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

        private IEnumerable<JournalItemViewModel> GetLedgerSummaryItems(
            JournalParameters parameters, int length, bool byNo, bool byBranch, bool isDebit)
        {
            var query = !byBranch
                ? GetJournalLedgerSummaryQuery(parameters, length, byNo, isDebit)
                : GetJournalLedgerSummaryByBranchQuery(parameters, length, byNo, isDebit);
            ApplyEnvironmentFilters(query, parameters.GridOptions);
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row));
        }

        private ReportQuery GetJournalLedgerSummaryQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            var query = default(ReportQuery);
            string command = !byNo
                ? String.Format(JournalQuery.ByDateLedgerSummary, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.ByNoLedgerSummary, length, parameters.FromNo, parameters.ToNo);
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

        private ReportQuery GetJournalLedgerSummaryByBranchQuery(
            JournalParameters parameters, int length, bool byNo, bool isDebit)
        {
            var query = default(ReportQuery);
            string command = !byNo
                ? String.Format(JournalQuery.ByDateLedgerSummaryByBranch, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.ByNoLedgerSummaryByBranch, length, parameters.FromNo, parameters.ToNo);
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

        private IEnumerable<JournalItemViewModel> GetLedgerSummaryByDateItems(
            JournalParameters parameters, int length, bool byBranch, bool isDebit)
        {
            var query = default(ReportQuery);
            string command = !byBranch
                ? String.Format(JournalQuery.LedgerSummaryByDate, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false))
                : String.Format(JournalQuery.LedgerSummaryByDateByBranch, length,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false));
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

            ApplyEnvironmentFilters(query, parameters.GridOptions);
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row));
        }

        private IEnumerable<JournalItemViewModel> MergeByNumber(
            IEnumerable<JournalItemViewModel> first, IEnumerable<JournalItemViewModel> second,
            GridOptions gridOptions)
        {
            var items = new List<JournalItemViewModel>();

            // NOTE: This is the obvious logic for merging by number. However, it performs
            // very poorly with big-to-huge collections...
            ////foreach (var byNum in first
            ////    .OrderBy(item => item.VoucherNo)
            ////    .GroupBy(item => item.VoucherNo))
            ////{
            ////    items.AddRange(byNum.OrderBy(item => item.AccountFullCode));
            ////    items.AddRange(second
            ////        .Where(item => item.VoucherNo == byNum.Key)
            ////        .OrderBy(item => item.AccountFullCode));
            ////}

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

            var paging = gridOptions.Paging;
            int fromIndex = paging.PageSize * (paging.PageIndex - 1);
            return items.Skip(fromIndex - previousCount).Take(paging.PageSize);
        }

        private IEnumerable<JournalItemViewModel> MergeByNumber(
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

            var paging = gridOptions.Paging;
            int fromIndex = paging.PageSize * (paging.PageIndex - 1);
            return items.Skip(fromIndex - previousCount).Take(paging.PageSize);
        }

        private IEnumerable<JournalItemViewModel> MergeByDate(
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

            var paging = gridOptions.Paging;
            int fromIndex = paging.PageSize * (paging.PageIndex - 1);
            return items.Skip(fromIndex - previousCount).Take(paging.PageSize);
        }

        private IEnumerable<JournalItemViewModel> MergeByDate(
            IEnumerable<JournalItemViewModel> first, IEnumerable<JournalItemViewModel> second)
        {
            var items = new List<JournalItemViewModel>();
            foreach (var byNum in first
                .OrderBy(item => item.VoucherDate)
                .GroupBy(item => item.VoucherDate))
            {
                items.AddRange(byNum.OrderBy(item => item.AccountFullCode));
                items.AddRange(second
                    .Where(item => item.VoucherDate == byNum.Key)
                    .OrderBy(item => item.AccountFullCode));
            }

            return items;
        }

        private async Task PrepareJournalAsync(
            JournalViewModel journal, IEnumerable<JournalItemViewModel> items, GridOptions gridOptions)
        {
            journal.TotalCount = items.Count();
            journal.DebitSum = items.Sum(item => item.Debit);
            journal.CreditSum = items.Sum(item => item.Credit);
            journal.Items.AddRange(items.ApplyPaging(gridOptions));
            await SetNameAndDescriptionAsync(journal.Items);
        }

        private JournalItemViewModel GetJournalItem(DataRow row)
        {
            return new JournalItemViewModel()
            {
                RowNo = ValueOrDefault<int>(row, "RowNum"),
                VoucherDate = ValueOrDefault<DateTime>(row, "Date"),
                VoucherNo = ValueOrDefault<int>(row, "No"),
                AccountFullCode = ValueOrDefault(row, "FullCode"),
                AccountName = ValueOrDefault(row, "Name"),
                DetailAccountFullCode = ValueOrDefault(row, "DetailFullCode"),
                DetailAccountName = ValueOrDefault(row, "DetailName"),
                CostCenterFullCode = ValueOrDefault(row, "CostFullCode"),
                CostCenterName = ValueOrDefault(row, "CostName"),
                ProjectFullCode = ValueOrDefault(row, "ProjectFullCode"),
                ProjectName = ValueOrDefault(row, "ProjectName"),
                Description = ValueOrDefault(row, "Description"),
                Debit = ValueOrDefault<Decimal>(row, "Debit"),
                Credit = ValueOrDefault<Decimal>(row, "Credit"),
                Mark = ValueOrDefault(row, "Mark"),
                BranchName = ValueOrDefault(row, "BranchName")
            };
        }

        private T ValueOrDefault<T>(DataRow row, string field)
        {
            var value = default(T);
            if (row.Table.Columns.Contains(field))
            {
                value = (T)Convert.ChangeType(row[field], typeof(T));
            }

            return value;
        }

        private string ValueOrDefault(DataRow row, string field)
        {
            string value = null;
            if (row.Table.Columns.Contains(field))
            {
                value = row[field].ToString();
            }

            return value;
        }

        private int GetLevelCodeLength(int level)
        {
            var fullConfig = Config
                .GetViewTreeConfigByViewAsync(ViewId.Account)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
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
                item.Description = AppStrings.AsQuotedInVoucherLines;
            }
        }

        private void ApplyEnvironmentFilters(ReportQuery query, GridOptions gridOptions)
        {
            string environmentFilter = String.Empty;
            string fpFilter = String.Format(" FiscalPeriodId = {0}", UserContext.FiscalPeriodId);
            var quickFilter = gridOptions.QuickFilter?.ToString();
            if (quickFilter != null && quickFilter.IndexOf("BranchId") != -1)
            {
                environmentFilter = fpFilter;
            }
            else
            {
                var branchIds = GetChildTree(UserContext.BranchId);
                string branchList = String.Join(",", branchIds.Select(id => id.ToString()));
                environmentFilter = String.Format(
                    "{0} AND (BranchId = {1} OR BranchId IN({2}))", fpFilter, UserContext.BranchId, branchList);
            }

            query.ApplyDefaultFilters(environmentFilter, quickFilter);
        }

        private IEnumerable<int> GetChildTree(int branchId)
        {
            var tree = new List<int>();
            var repository = UnitOfWork.GetRepository<Branch>();
            var branch = repository.GetByID(branchId, br => br.Children);
            AddChildren(branch, tree);
            return tree;
        }

        private void AddChildren(Branch branch, IList<int> children)
        {
            var repository = UnitOfWork.GetRepository<Branch>();
            foreach (var child in branch.Children)
            {
                children.Add(child.Id);
                var item = repository.GetByID(child.Id, br => br.Children);
                AddChildren(item, children);
            }
        }

        private readonly ISystemRepository _system;
    }
}

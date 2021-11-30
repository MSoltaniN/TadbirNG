using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر عملیات ارزی را تعریف می کند
    /// </summary>
    public class CurrencyBookRepositoryDirect : LoggingRepositoryBase, ICurrencyBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="utility">امکانات مشترک برای اجرای مستقیم دستورات دیتابیسی را فراهم می کند</param>
        public CurrencyBookRepositoryDirect(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility utility)
            : base(context, system.Logger)
        {
            _system = system;
            _utility = utility;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookAsync(CurrencyBookParameters parameters)
        {
            var book = new CurrencyBookViewModel();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var sourceList = GetSourceList(parameters.Mode);
            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
                switch (parameters.Mode)
                {
                    case AccountBookMode.ByRows:
                        book = await GetSimpleBookAsync(parameters);
                        break;
                    case AccountBookMode.VoucherSum:
                        book = await GetSummaryBookAsync(parameters, true);
                        break;
                    case AccountBookMode.DailySum:
                        book = await GetSummaryBookAsync(parameters);
                        break;
                    case AccountBookMode.MonthlySum:
                        book = await GetMonthlySummaryBookAsync(parameters);
                        break;
                    default:
                        break;
                }
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return book;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی را برای کلیه ارزهای تعریف شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <returns>اطلاعات دفتر عملیات ارزی برای کلیه ارزها</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookAllCurrenciesAsync(
            CurrencyBookParameters parameters)
        {
            throw new NotImplementedException();
        }

        private static SourceListId GetSourceList(AccountBookMode mode)
        {
            var sourceList = SourceListId.None;
            switch (mode)
            {
                case AccountBookMode.ByRows:
                    sourceList = SourceListId.CurrencyBookByRow;
                    break;
                case AccountBookMode.VoucherSum:
                    sourceList = SourceListId.CurrencyBookVoucherSum;
                    break;
                case AccountBookMode.DailySum:
                    sourceList = SourceListId.CurrencyBookDailySum;
                    break;
                case AccountBookMode.MonthlySum:
                    sourceList = SourceListId.CurrencyBookMonthlySum;
                    break;
                default:
                    break;
            }

            return sourceList;
        }

        private async Task<CurrencyBookViewModel> GetSimpleBookAsync(
            CurrencyBookParameters parameters)
        {
            var book = new CurrencyBookViewModel();
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(GetSelectClause(parameters));
            queryBuilder.AppendLine(GetFromClause(parameters));
            queryBuilder.AppendLine(await GetWhereClauseAsync(parameters));
            book.Items.AddRange(GetQueryResult(queryBuilder.ToString(), parameters));
            PrepareCurrencyBook(book, parameters);
            return book;
        }

        private async Task<CurrencyBookViewModel> GetSummaryBookAsync(
           CurrencyBookParameters parameters, bool byNo = false)
        {
            var book = new CurrencyBookViewModel();
            var bookItems = await GetQueryResultAsync(parameters, byNo);
            foreach (var item in bookItems)
            {
                item.Description = Context.Localize(AppStrings.AsQuotedInJournal);
            }

            book.Items.AddRange(bookItems);
            PrepareCurrencyBook(book, parameters);
            return book;
        }

        private async Task<CurrencyBookViewModel> GetMonthlySummaryBookAsync(
            CurrencyBookParameters parameters)
        {
            var book = new CurrencyBookViewModel();
            var items = new List<CurrencyBookItemViewModel>();
            items.AddRange(await GetQueryResultAsync(parameters, false, VoucherOriginId.OpeningVoucher));

            var calendarType = await _system.Config.GetCurrentCalendarAsync();
            var calendar = (calendarType == CalendarType.Jalali)
                ? new PersianCalendar() as Calendar
                : new GregorianCalendar();

            List<CurrencyBookItemViewModel> monthlyBook;
            var monthEnum = new MonthEnumerator(parameters.FromDate, parameters.ToDate, calendar);
            var monthParams = parameters.GetCopy();
            foreach (var month in monthEnum.GetMonths())
            {
                monthParams.FromDate = month.Start;
                monthParams.ToDate = month.End;
                monthlyBook = await GetQueryResultAsync(monthParams);
                foreach (var item in monthlyBook)
                {
                    item.VoucherDate = month.End;
                }

                items.AddRange(monthlyBook);
            }

            items.AddRange(await GetQueryResultAsync(parameters, false, VoucherOriginId.ClosingTempAccounts));
            items.AddRange(await GetQueryResultAsync(parameters, false, VoucherOriginId.ClosingVoucher));

            var description = Context.Localize(AppStrings.AsQuotedInJournal);
            foreach (var item in items)
            {
                item.Description = description;
            }

            book.SetItems(items.Where(item => item.Debit > 0.0M || item.Credit > 0.0M));
            PrepareCurrencyBook(book, parameters);
            return book;
        }

        private async Task<List<CurrencyBookItemViewModel>> GetQueryResultAsync(
            CurrencyBookParameters parameters, bool byNo = false, VoucherOriginId? origin = null)
        {
            var baseBuilder = new StringBuilder();
            baseBuilder.AppendLine(GetSelectClause(parameters));
            baseBuilder.AppendLine(GetFromClause(parameters));

            var queryBuilder = new StringBuilder(baseBuilder.ToString());
            queryBuilder.AppendLine(await GetWhereClauseAsync(parameters, origin, true));
            queryBuilder.AppendLine(GetGroupByClause(parameters));
            var debitItems = GetQueryResult(queryBuilder.ToString(), parameters);

            queryBuilder = new StringBuilder(baseBuilder.ToString());
            queryBuilder.AppendLine(await GetWhereClauseAsync(parameters, origin, false));
            queryBuilder.AppendLine(GetGroupByClause(parameters));
            var creditItems = GetQueryResult(queryBuilder.ToString(), parameters);
            var result = new List<CurrencyBookItemViewModel>();
            if (byNo)
            {
                result = debitItems
                    .Concat(creditItems)
                    .OrderBy(item => item.VoucherDate)
                    .ThenBy(item => item.VoucherNo)
                    .ThenBy(item => item.BranchName)
                    .ThenBy(item => item.Credit)
                    .ToList();
            }
            else
            {
                result = debitItems
                    .Concat(creditItems)
                    .OrderBy(item => item.VoucherDate)
                    .ThenBy(item => item.BranchName)
                    .ThenBy(item => item.Credit)
                    .ToList();
            }

            return result;
        }

        private IEnumerable<CurrencyBookItemViewModel> GetQueryResult(
            string query, CurrencyBookParameters parameters)
        {
            LogExecutingQuery(query, parameters);
            var result = DbConsole.ExecuteQuery(query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetBookItem(row));
        }

        private CurrencyBookItemViewModel GetBookItem(DataRow row)
        {
            var bookItem = new CurrencyBookItemViewModel()
            {
                VoucherDate = _utility.ValueOrDefault<DateTime>(row, "Date"),
                VoucherNo = _utility.ValueOrDefault<int>(row, "No"),
                Description = _utility.ValueOrDefault(row, "Description"),
                VoucherReference = _utility.ValueOrDefault(row, "Reference"),
                BaseCurrencyDebit = _utility.ValueOrDefault<decimal>(row, "BaseCurrencyDebit"),
                BaseCurrencyCredit = _utility.ValueOrDefault<decimal>(row, "BaseCurrencyCredit"),
                Debit = _utility.ValueOrDefault<decimal>(row, "Debit"),
                Credit = _utility.ValueOrDefault<decimal>(row, "Credit"),
                BranchName = _utility.ValueOrDefault(row, "BranchName"),
                Mark = _utility.ValueOrDefault(row, "Mark"),
                Id = _utility.ValueOrDefault<int>(row, "VoucherLineID"),
                LineCount = _utility.ValueOrDefault<int>(row, "LineCount")
            };
            bookItem.Debit = bookItem.BaseCurrencyDebit > 0M
                ? bookItem.Debit
                : 0M;
            bookItem.Credit = bookItem.BaseCurrencyCredit > 0M
                ? bookItem.Credit
                : 0M;
            var rate = bookItem.BaseCurrencyDebit > 0M
                ? bookItem.BaseCurrencyDebit / Math.Max(1M, bookItem.Debit)
                : bookItem.BaseCurrencyCredit / Math.Max(1M, bookItem.Credit);
            bookItem.CurrencyRate = Math.Round(rate);
            return bookItem;
        }

        private static string GetSelectClause(CurrencyBookParameters parameters)
        {
            string selectClause;
            switch (parameters.Mode)
            {
                case AccountBookMode.ByRows:
                    selectClause = parameters.ByBranch
                        ? CurrencyBookQuery.ByRowByBranchSelect
                        : CurrencyBookQuery.ByRowSelect;
                    break;
                case AccountBookMode.VoucherSum:
                    selectClause = parameters.ByBranch
                        ? CurrencyBookQuery.VoucherSumByBranchSelect
                        : CurrencyBookQuery.VoucherSumSelect;
                    break;
                case AccountBookMode.DailySum:
                    selectClause = parameters.ByBranch
                        ? CurrencyBookQuery.DailySumByBranchSelect
                        : CurrencyBookQuery.DailySumSelect;
                    break;
                case AccountBookMode.MonthlySum:
                default:
                    selectClause = parameters.ByBranch
                        ? CurrencyBookQuery.MonthlySumByBranchSelect
                        : CurrencyBookQuery.MonthlySumSelect;
                    break;
            }

            return selectClause;
        }

        private static string GetFromClause(CurrencyBookParameters parameters)
        {
            var fromBuilder = new StringBuilder(@"FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON v.VoucherID = vl.VoucherID");
            if (parameters.AccountId.HasValue)
            {
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID");
            }
            if (parameters.DetailAccountId.HasValue)
            {
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    INNER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID");
            }
            if (parameters.CostCenterId.HasValue)
            {
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    INNER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID");
            }
            if (parameters.ProjectId.HasValue)
            {
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    INNER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID");
            }
            if (parameters.ByBranch)
            {
                fromBuilder.AppendLine();
                fromBuilder.AppendFormat(
                    "    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID");
            }

            return fromBuilder.ToString();
        }

        private async Task<string> GetWhereClauseAsync(
            CurrencyBookParameters parameters, VoucherOriginId? originId = null, bool? isDebit = null)
        {
            var whereBuilder = new StringBuilder();
            whereBuilder.AppendFormat("WHERE v.Date >= '{0}' AND v.Date <= '{1}'",
                parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false));
            if (parameters.AccountId.HasValue)
            {
                whereBuilder.AppendFormat(" AND {0}",
                    await GetAccountItemCriteriaAsync<Account>(parameters.AccountId.Value, "acc"));
            }
            if (parameters.DetailAccountId.HasValue)
            {
                whereBuilder.AppendFormat(" AND {0}",
                    await GetAccountItemCriteriaAsync<DetailAccount>(parameters.DetailAccountId.Value, "facc"));
            }
            if (parameters.CostCenterId.HasValue)
            {
                whereBuilder.AppendFormat(" AND {0}",
                    await GetAccountItemCriteriaAsync<CostCenter>(parameters.CostCenterId.Value, "cc"));
            }
            if (parameters.ProjectId.HasValue)
            {
                whereBuilder.AppendFormat(" AND {0}",
                    await GetAccountItemCriteriaAsync<Project>(parameters.ProjectId.Value, "prj"));
            }

            string envFilters = _utility.GetEnvironmentFilters(parameters.GridOptions);
            whereBuilder.AppendFormat(" AND {0}", envFilters);
            if (isDebit.HasValue)
            {
                string filter = isDebit.Value
                    ? String.Format(" AND vl.Debit > 0")
                    : String.Format(" AND vl.Credit > 0");
                whereBuilder.Append(filter);
            }

            if (originId.HasValue)
            {
                whereBuilder.AppendFormat(" AND OriginID = {0}", (int)originId);
            }

            return ReportQuery.TranslateFinanceQuery(whereBuilder.ToString());
        }

        private static string GetGroupByClause(CurrencyBookParameters parameters)
        {
            string groupByClause;
            switch (parameters.Mode)
            {
                case AccountBookMode.ByRows:
                    groupByClause = String.Empty;
                    break;
                case AccountBookMode.VoucherSum:
                    groupByClause = parameters.ByBranch
                        ? CurrencyBookQuery.VoucherSumByBranchGroupBy
                        : CurrencyBookQuery.VoucherSumGroupBy;
                    break;
                case AccountBookMode.DailySum:
                    groupByClause = parameters.ByBranch
                        ? CurrencyBookQuery.DailySumByBranchGroupBy
                        : CurrencyBookQuery.DailySumGroupBy;
                    break;
                case AccountBookMode.MonthlySum:
                default:
                    groupByClause = parameters.ByBranch
                        ? CurrencyBookQuery.MonthlySumByBranchGroupBy
                        : String.Empty;
                    break;
            }

            return groupByClause;
        }

        private async Task<string> GetAccountItemCriteriaAsync<TItem>(int itemId, string alias)
            where TItem : TreeEntity
        {
            string criteria = String.Empty;
            var repository = UnitOfWork.GetAsyncRepository<TItem>();
            var fullCode = await repository
                .GetEntityQuery()
                .Where(acc => acc.Id == itemId)
                .Select(acc => acc.FullCode)
                .SingleOrDefaultAsync();
            if (!String.IsNullOrEmpty(fullCode))
            {
                criteria = String.Format("{0}.FullCode LIKE '{1}%'", alias, fullCode);
            }

            return criteria;
        }

        private void PrepareCurrencyBook(CurrencyBookViewModel book, CurrencyBookParameters parameters)
        {
            var filteredItems = book.Items
                .Apply(parameters.GridOptions, false)
                .ToList();
            decimal balance = 0M;
            decimal currencyBalance = 0M;
            foreach (var item in filteredItems)
            {
                balance = balance + item.BaseCurrencyDebit - item.BaseCurrencyCredit;
                currencyBalance = currencyBalance + item.Debit - item.Credit;
                item.BaseCurrencyBalance = balance;
                item.Balance = currencyBalance;
                item.Description = Context.Localize(item.Description);
            }

            book.BaseCurrencyDebitSum = filteredItems.Sum(item => item.BaseCurrencyDebit);
            book.BaseCurrencyCreditSum = filteredItems.Sum(item => item.BaseCurrencyCredit);
            book.DebitSum = filteredItems.Sum(item => item.Debit);
            book.CreditSum = filteredItems.Sum(item => item.Credit);
            if (filteredItems.Any())
            {
                book.BaseCurrencyBalance = filteredItems.Last().BaseCurrencyBalance;
                book.Balance = filteredItems.Last().Balance;
            }

            book.TotalCount = filteredItems.Count;
            book.SetItems(filteredItems.ApplyPaging(parameters.GridOptions));
        }

        private static void LogExecutingQuery(string query, CurrencyBookParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Report mode : {0}, By Branch : {1}", parameters.Mode, parameters.ByBranch);
            Debug.WriteLine(query);
            Debug.WriteLine("----------------------------------------------------------------------------");
#endif
        }

        private readonly ISystemRepository _system;
        private readonly IReportDirectUtility _utility;
    }
}

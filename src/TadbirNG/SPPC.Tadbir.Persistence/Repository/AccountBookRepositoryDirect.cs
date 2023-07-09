using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class AccountBookRepositoryDirect : LoggingRepositoryBase, IAccountBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="utility"></param>
        public AccountBookRepositoryDirect(IRepositoryContext context, ISystemRepository system,
            IReportDirectUtility utility)
            : base(context, system.Logger)
        {
            _system = system;
            _utility = utility;
        }

        /// <inheritdoc/>
        public async Task<AccountBookViewModel> GetAccountBookAsync(
            AccountBookParameters parameters)
        {
            return await GetAccountBookDataAsync(parameters);
        }

        /// <inheritdoc/>
        public async Task<AccountBookViewModel> GetAccountBookByBranchAsync(
            AccountBookParameters parameters)
        {
            return await GetAccountBookDataAsync(parameters);
        }

        /// <inheritdoc/>
        public async Task<AccountItemBriefViewModel> GetPreviousAccountItemAsync(int viewId, int itemId)
        {
            var previous = default(AccountItemBriefViewModel);
            var accountItem = await _utility.GetItemAsync(viewId, itemId);
            var previousItem = await GetAccountItemQuery(viewId)
                .Where(item => item.Id < itemId && item.Level == accountItem.Level)
                .OrderByDescending(item => item.Id)
                .FirstOrDefaultAsync();
            if (previousItem != null)
            {
                previous = Mapper.Map<AccountItemBriefViewModel>(previousItem);
            }

            return previous;
        }

        /// <inheritdoc/>
        public async Task<AccountItemBriefViewModel> GetNextAccountItemAsync(int viewId, int itemId)
        {
            var next = default(AccountItemBriefViewModel);
            var accountItem = await _utility.GetItemAsync(viewId, itemId);
            var nextItem = await GetAccountItemQuery(viewId)
                .Where(item => item.Id > itemId && item.Level == accountItem.Level)
                .OrderBy(item => item.Id)
                .FirstOrDefaultAsync();
            if (nextItem != null)
            {
                next = Mapper.Map<AccountItemBriefViewModel>(nextItem);
            }

            return next;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.AccountBook; }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private static string GetSummaryQuery(bool byNo, bool byBranch)
        {
            string query;
            if (byNo)
            {
                query = byBranch ? BookQuery.VoucherSumByDateByBranch : BookQuery.VoucherSumByDate;
            }
            else
            {
                query = byBranch ? BookQuery.DailySumByDateByBranch : BookQuery.DailySumByDate;
            }

            return query;
        }

        private static SourceListId GetSourceList(AccountBookMode mode)
        {
            var sourceList = SourceListId.None;
            switch (mode)
            {
                case AccountBookMode.ByRows:
                    sourceList = SourceListId.AccountBookByRow;
                    break;
                case AccountBookMode.VoucherSum:
                    sourceList = SourceListId.AccountBookVoucherSum;
                    break;
                case AccountBookMode.DailySum:
                    sourceList = SourceListId.AccountBookDailySum;
                    break;
                case AccountBookMode.MonthlySum:
                    sourceList = SourceListId.AccountBookMonthlySum;
                    break;
                default:
                    break;
            }

            return sourceList;
        }

        private async Task<AccountBookViewModel> GetAccountBookDataAsync(
            AccountBookParameters parameters)
        {
            Verify.ArgumentNotNull(parameters, nameof(parameters));
            var book = default(AccountBookViewModel);
            var sourceList = GetSourceList(parameters.Mode);

            if (parameters.GridOptions.Operation != (int)OperationId.Print)
            {
                var accountItem = await _utility.GetItemAsync(parameters.ViewId, parameters.ItemId);
                string fullCode = accountItem?.FullCode ?? String.Empty;
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;

                switch (parameters.Mode)
                {
                    case AccountBookMode.ByRows:
                        book = await GetSimpleBookAsync(fullCode, parameters);
                        break;
                    case AccountBookMode.VoucherSum:
                        book = await GetSummaryBookAsync(fullCode, parameters, true);
                        break;
                    case AccountBookMode.DailySum:
                        book = await GetSummaryBookAsync(fullCode, parameters, false);
                        break;
                    case AccountBookMode.MonthlySum:
                        book = await GetMonthlySummaryBookAsync(fullCode, parameters);
                        break;
                    default:
                        break;
                }
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return book;
        }

        private async Task<AccountBookViewModel> GetSimpleBookAsync(
            string fullCode, AccountBookParameters parameters)
        {
            string bookQuery = parameters.IsByBranch ? BookQuery.ByDateByRowByBranch : BookQuery.ByDateByRow;
            var book = new AccountBookViewModel();
            var items = await GetInitialItemsAsync(parameters);
            var query = new ReportQuery(String.Format(bookQuery, _utility.GetItemName(parameters.ViewId),
                parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false),
                fullCode));

            query.SetFilter(_utility.GetEnvironmentFilters(parameters.GridOptions));
            var result = DbConsole.ExecuteQuery(query.Query);
            items.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => GetBookItem(row)));
            foreach (var item in items)
            {
                item.Description = Context.Localize(item.Description);
            }

            PrepareAccountBook(book, items, parameters.GridOptions);
            return book;
        }

        private async Task<AccountBookViewModel> GetSummaryBookAsync(
            string fullCode, AccountBookParameters parameters, bool byNo)
        {
            string bookQuery = GetSummaryQuery(byNo, parameters.IsByBranch);
            var book = new AccountBookViewModel();
            var items = await GetInitialItemsAsync(parameters);
            items.AddRange(GetQueryResult(fullCode, bookQuery, parameters, byNo));
            foreach (var item in items)
            {
                item.Description = Context.Localize(item.Description);
            }

            PrepareAccountBook(book, items, parameters.GridOptions);
            return book;
        }

        private async Task<AccountBookViewModel> GetMonthlySummaryBookAsync(
            string fullCode, AccountBookParameters parameters)
        {
            string bookQuery = parameters.IsByBranch ? BookQuery.MonthlySumByDateByBranch : BookQuery.MonthlySumByDate;
            var book = new AccountBookViewModel();
            var items = await GetInitialItemsAsync(parameters);
            items.AddRange(GetQueryResult(fullCode, VoucherOriginId.OpeningVoucher, parameters));

            var calendar = await Config.GetCurrentCalendarAsync();
            var monthlyBook = new List<AccountBookItemViewModel>();
            var monthEnum = new DateSpanEnumerator(parameters.FromDate, parameters.ToDate, calendar);
            var monthParams = parameters.GetCopy();
            foreach (var month in monthEnum.GetMonths())
            {
                monthParams.FromDate = month.Start;
                monthParams.ToDate = month.End;

                monthlyBook = GetQueryResult(fullCode, bookQuery, monthParams);
                Array.ForEach(monthlyBook.ToArray(), item => item.VoucherDate = month.End);
                items.AddRange(monthlyBook.Where(item => item.Debit > 0.0M || item.Credit > 0.0M));
            }

            items.AddRange(GetQueryResult(fullCode, VoucherOriginId.ClosingTempAccounts, parameters));
            items.AddRange(GetQueryResult(fullCode, VoucherOriginId.ClosingVoucher, parameters));
            foreach (var item in items)
            {
                item.Description = Context.Localize(item.Description);
            }

            PrepareAccountBook(book, items, parameters.GridOptions);
            return book;
        }

        private void PrepareAccountBook(AccountBookViewModel book,
            IList<AccountBookItemViewModel> items, GridOptions gridOptions)
        {
            string description = Context.Localize(AppStrings.InitialBalance);
            var initBalance = items
                .Where(item => item.Description == description)
                .FirstOrDefault();
            var filteredItems = items
                .Where(item => item.Description != description)
                .Apply(gridOptions, false)
                .ToList();

            if (initBalance != null)
            {
                filteredItems.Insert(0, initBalance);
            }

            decimal balance = 0.0M;
            foreach (var item in filteredItems)
            {
                balance = balance + item.Debit - item.Credit;
                item.Balance = balance;
            }

            var last = filteredItems.LastOrDefault();
            book.DebitSum = filteredItems.Sum(item => item.Debit);
            book.CreditSum = filteredItems.Sum(item => item.Credit);
            book.Balance = last != null ? last.Balance : 0.0M;
            book.TotalCount = filteredItems.Count;
            book.Items.AddRange(filteredItems.ApplyPaging(gridOptions));
        }

        private List<AccountBookItemViewModel> GetQueryResult(
            string fullCode, string bookQuery, AccountBookParameters parameters, bool byNo = false)
        {
            var builder = new StringBuilder(String.Format(bookQuery, _utility.GetItemName(parameters.ViewId),
                parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false),
                fullCode));
            var debitItems = GetQueryResult(builder.ToString(), parameters);
            builder.Replace("Debit", "Credit")
                .Replace("Credit1", "Debit");
            var creditItems = GetQueryResult(builder.ToString(), parameters);
            var result = new List<AccountBookItemViewModel>();
            if (byNo)
            {
                result.AddRange(debitItems
                    .Concat(creditItems)
                    .OrderBy(item => item.VoucherDate)
                    .ThenBy(item => item.VoucherNo)
                    .ThenBy(item => item.BranchName)
                    .ThenBy(item => item.Credit));
            }
            else
            {
                result.AddRange(debitItems
                    .Concat(creditItems)
                    .OrderBy(item => item.VoucherDate)
                    .ThenBy(item => item.BranchName)
                    .ThenBy(item => item.Credit));
            }

            Array.ForEach(result.ToArray(), item => item.Description = AppStrings.AsQuotedInJournal);
            return result;
        }

        private IEnumerable<AccountBookItemViewModel> GetQueryResult(
            string fullCode, VoucherOriginId originId, AccountBookParameters parameters)
        {
            string bookQuery = parameters.IsByBranch
                ? BookQuery.SpecialVoucherByDateByBranch
                : BookQuery.SpecialVoucherByDate;
            var builder = new StringBuilder(String.Format(bookQuery, _utility.GetItemName(parameters.ViewId),
                parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false),
                (int)originId, fullCode));
            var debitItems = GetQueryResult(builder.ToString(), parameters);
            builder.Replace("Debit", "Credit")
                .Replace("Credit1", "Debit");
            var creditItems = GetQueryResult(builder.ToString(), parameters);
            var mergedItems = debitItems
                .Concat(creditItems)
                .OrderBy(item => item.BranchName)
                .ThenBy(item => item.Credit)
                .ToList();
            foreach (var item in mergedItems)
            {
                item.Description = originId.ToString();
            }

            return mergedItems
                .Where(item => item.LineCount > 0);
        }

        private IEnumerable<AccountBookItemViewModel> GetQueryResult(
            string bookQuery, AccountBookParameters parameters)
        {
            var query = new ReportQuery(bookQuery);
            query.SetFilter(_utility.GetEnvironmentFilters(parameters.GridOptions));
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetBookItem(row));
        }

        private AccountBookItemViewModel GetBookItem(DataRow row)
        {
            return new AccountBookItemViewModel()
            {
                Id = _utility.ValueOrDefault<int>(row, "Id"),
                VoucherDate = _utility.ValueOrDefault<DateTime>(row, "Date"),
                VoucherNo = _utility.ValueOrDefault<int>(row, "No"),
                LineCount = _utility.ValueOrDefault<int>(row, "LineCount"),
                Description = _utility.ValueOrDefault(row, "Description"),
                Debit = _utility.ValueOrDefault<decimal>(row, "Debit"),
                Credit = _utility.ValueOrDefault<decimal>(row, "Credit"),
                Mark = _utility.ValueOrDefault(row, "Mark"),
                BranchName = _utility.ValueOrDefault(row, "BranchName")
            };
        }

        private async Task<List<AccountBookItemViewModel>> GetInitialItemsAsync(
            AccountBookParameters parameters)
        {
            var quickFilter = parameters.GridOptions.QuickFilter?.ToString();
            var hasMarkFilter = quickFilter?.Contains("Mark");
            return hasMarkFilter == true
                ? new List<AccountBookItemViewModel>()
                : new List<AccountBookItemViewModel>
                    {
                        await GetFirstBookItemAsync(parameters)
                    };
        }

        private async Task<AccountBookItemViewModel> GetFirstBookItemAsync(AccountBookParameters parameters)
        {
            var firstItem = default(AccountBookItemViewModel);
            var item = await _utility.GetItemAsync(parameters.ViewId, parameters.ItemId);
            var query = new ReportQuery(String.Format(AccountItemQuery.BalanceByDate,
                _utility.GetItemName(parameters.ViewId),
                parameters.FromDate.ToShortDateString(false), item.FullCode));
            query.SetFilter(_utility.GetEnvironmentFilters(
                parameters.GridOptions, UserContext.FiscalPeriodId));
            var result = DbConsole.ExecuteQuery(query.Query);
            if (result.Rows.Count > 0)
            {
                firstItem = new AccountBookItemViewModel()
                {
                    Balance = _utility.ValueOrDefault<decimal>(result.Rows[0], "Balance"),
                    BranchName = UserContext.BranchName,
                    Description = AppStrings.InitialBalance,
                    RowNo = 1,
                    VoucherDate = parameters.FromDate
                };
                firstItem.Debit = Math.Max(0, firstItem.Balance);
                firstItem.Credit = Math.Abs(Math.Min(0, firstItem.Balance));
            }

            return firstItem;
        }

        private IQueryable<TreeEntity> GetAccountItemQuery(int viewId)
        {
            IQueryable<TreeEntity> items = null;
            if (viewId == ViewId.Account)
            {
                items = Repository.GetAllQuery<Account>(viewId);
            }
            else if (viewId == ViewId.DetailAccount)
            {
                items = Repository.GetAllQuery<DetailAccount>(viewId);
            }
            else if (viewId == ViewId.CostCenter)
            {
                items = Repository.GetAllQuery<CostCenter>(viewId);
            }
            else if (viewId == ViewId.Project)
            {
                items = Repository.GetAllQuery<Project>(viewId);
            }

            return items;
        }

        private readonly ISystemRepository _system;
        private readonly IReportDirectUtility _utility;
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر حساب را پیاده سازی می کند
    /// </summary>
    public class AccountBookRepository : SimpleLoggingRepository, IAccountBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="report">امکان انجام محاسبات مشترک در گزارشات برنامه را فراهم می کند</param>
        public AccountBookRepository(IRepositoryContext context, ISystemRepository system,
            IReportRepository report, ILogConfigRepository config)
            : base(context, system.Logger, config)
        {
            _system = system;
            _report = report;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر حساب بر حسب تاریخ</returns>
        public async Task<AccountBookViewModel> GetAccountBookAsync(AccountBookParameters parameters)
        {
            return await GetAccountBookDataAsync(parameters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر حساب به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر حساب به تفکیک شعبه</returns>
        public async Task<AccountBookViewModel> GetAccountBookByBranchAsync(AccountBookParameters parameters)
        {
            return await GetAccountBookDataAsync(parameters);
        }

        /// <summary>
        /// به روش آسنکرون، مولفه حساب قبلی قابل دسترسی نسبت به مولفه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مولفه حساب</param>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب جاری</param>
        /// <returns>اطلاعات نمایشی مختصر برای مولفه حساب قبلی</returns>
        public async Task<AccountItemBriefViewModel> GetPreviousAccountItemAsync(int viewId, int itemId)
        {
            var previous = default(AccountItemBriefViewModel);
            var accountItem = GetAccountItem(viewId, itemId);
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

        /// <summary>
        /// به روش آسنکرون، مولفه حساب بعدی قابل دسترسی نسبت به مولفه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مولفه حساب</param>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب جاری</param>
        /// <returns>اطلاعات نمایشی مختصر برای مولفه حساب بعدی</returns>
        public async Task<AccountItemBriefViewModel> GetNextAccountItemAsync(int viewId, int itemId)
        {
            var next = default(AccountItemBriefViewModel);
            var accountItem = GetAccountItem(viewId, itemId);
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

        private static void PrepareAccountBook(AccountBookViewModel book, GridOptions gridOptions)
        {
            decimal balance = book.Items[0].Balance;
            Array.ForEach(book.Items.Skip(1).ToArray(), item =>
            {
                balance = balance + item.Debit - item.Credit;
                item.Balance = balance;
            });
            book.DebitSum = book.Items.Sum(item => item.Debit);
            book.CreditSum = book.Items.Sum(item => item.Credit);
            book.Balance = book.Items.Last().Balance;
            book.TotalCount = book.Items.Count;
            book.SetItems(book.Items.ApplyPaging(gridOptions).ToArray());
        }

        private static IEnumerable<AccountBookItemViewModel> GetAggregatedBookItems(
            IEnumerable<AccountBookItemViewModel> items, bool singleMode)
        {
            var aggregates = new List<AccountBookItemViewModel>();
            decimal debitSum = items.Sum(line => line.Debit);
            decimal creditSum = items.Sum(line => line.Credit);
            if (debitSum > 0.0M)
            {
                var item = GetAggregatedBookItem(items.First(), singleMode);
                item.Debit = debitSum;
                item.LineCount = items.Count(line => line.Debit > 0.0M);
                aggregates.Add(item);
            }

            if (creditSum > 0.0M)
            {
                var item = GetAggregatedBookItem(items.First(), singleMode);
                item.Credit = creditSum;
                item.LineCount = items.Count(line => line.Credit > 0.0M);
                aggregates.Add(item);
            }

            return aggregates;
        }

        private static AccountBookItemViewModel GetAggregatedBookItem(
            AccountBookItemViewModel item, bool singleMode)
        {
            return new AccountBookItemViewModel()
            {
                Description = "AsQuotedInJournal",
                VoucherDate = item.VoucherDate,
                VoucherNo = singleMode ? item.VoucherNo : 0,
                VoucherStatusId = singleMode ? item.VoucherStatusId : 0,
                VoucherConfirmedById = singleMode ? item.VoucherConfirmedById : null,
                VoucherApprovedById = singleMode ? item.VoucherApprovedById : null,
                BranchId = singleMode ? item.BranchId : 0,
                BranchName = singleMode ? item.BranchName : null
            };
        }

        private async Task<AccountBookViewModel> GetAccountBookDataAsync(AccountBookParameters parameters)
        {
            Verify.ArgumentNotNull(parameters, nameof(parameters));
            var book = default(AccountBookViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case AccountBookMode.ByRows:
                    book = await GetSimpleBookAsync(parameters);
                    sourceList = SourceListId.AccountBookByRow;
                    break;
                case AccountBookMode.VoucherSum:
                    book = await GetSummaryBookAsync(parameters, true);
                    sourceList = SourceListId.AccountBookVoucherSum;
                    break;
                case AccountBookMode.DailySum:
                    book = await GetSummaryBookAsync(parameters, false);
                    sourceList = SourceListId.AccountBookDailySum;
                    break;
                case AccountBookMode.MonthlySum:
                    book = await GetMonthlySummaryBookAsync(parameters);
                    sourceList = SourceListId.AccountBookMonthlySum;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(OperationId.View, sourceList);
            return book;
        }

        private async Task<AccountBookViewModel> GetSimpleBookAsync(AccountBookParameters parameters)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(parameters.ViewId, parameters.ItemId, parameters.FromDate));

            var itemCriteria = GetItemCriteria(parameters.ViewId, parameters.ItemId);
            var bookItems = await GetRawAccountBookLines(itemCriteria, parameters.FromDate, parameters.ToDate)
                .ToListAsync();
            book.Items.AddRange(bookItems
                .Select(line => Mapper.Map<AccountBookItemViewModel>(line))
                .ApplyQuickFilter(parameters.GridOptions)
                .Apply(parameters.GridOptions, false));
            PrepareAccountBook(book, parameters.GridOptions);
            return book;
        }

        private async Task<AccountBookViewModel> GetSummaryBookAsync(
            AccountBookParameters parameters, bool byNo)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(parameters.ViewId, parameters.ItemId, parameters.FromDate));

            var itemCriteria = GetItemCriteria(parameters.ViewId, parameters.ItemId);
            var lines = await GetRawAccountBookLines(itemCriteria, parameters.FromDate, parameters.ToDate)
                .Select(line => Mapper.Map<AccountBookItemViewModel>(line))
                .ToListAsync();
            lines = lines
                .ApplyQuickFilter(parameters.GridOptions)
                .ToList();
            AggregateAccountBook(book, lines, byNo, parameters.IsByBranch);
            book.SetItems(book.Items.Apply(parameters.GridOptions, false).ToArray());
            PrepareAccountBook(book, parameters.GridOptions);
            return book;
        }

        private async Task<AccountBookViewModel> GetMonthlySummaryBookAsync(AccountBookParameters parameters)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(parameters.ViewId, parameters.ItemId, parameters.FromDate));

            var itemCriteria = GetItemCriteria(parameters.ViewId, parameters.ItemId);
            await AddSpecialBookItemsAsync(
                book, itemCriteria, VoucherType.OpeningVoucher, parameters.FromDate, parameters.ToDate,
                parameters.GridOptions, parameters.IsByBranch);

            var monthEnum = new MonthEnumerator(parameters.FromDate, parameters.ToDate, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = GetRawAccountBookLines(itemCriteria, month.Start, month.End)
                    .Where(art => art.Voucher.Type == (short)VoucherType.NormalVoucher)
                    .Select(art => Mapper.Map<AccountBookItemViewModel>(art))
                    .ToList();
                monthLines = monthLines
                    .ApplyQuickFilter(parameters.GridOptions)
                    .ToList();
                if (monthLines.Count > 0)
                {
                    if (parameters.IsByBranch)
                    {
                        Array.ForEach(GetGroupByThenByItems(monthLines, item => item.BranchId).ToArray(), group =>
                            {
                                var aggregates = GetAggregatedBookItems(group, true);
                                Array.ForEach(aggregates.ToArray(), item => item.VoucherDate = month.End);
                                book.Items.AddRange(aggregates);
                            });
                    }
                    else
                    {
                        var aggregates = GetAggregatedBookItems(monthLines, false);
                        Array.ForEach(aggregates.ToArray(), item => item.VoucherDate = month.End);
                        book.Items.AddRange(aggregates);
                    }
                }
            }

            await AddSpecialBookItemsAsync(
                book, itemCriteria, VoucherType.ClosingTempAccounts, parameters.FromDate, parameters.ToDate,
                parameters.GridOptions, parameters.IsByBranch);
            await AddSpecialBookItemsAsync(
                book, itemCriteria, VoucherType.ClosingVoucher, parameters.FromDate, parameters.ToDate,
                parameters.GridOptions, parameters.IsByBranch);

            book.SetItems(book.Items.Apply(parameters.GridOptions, false).ToArray());
            PrepareAccountBook(book, parameters.GridOptions);
            return book;
        }

        private async Task AddSpecialBookItemsAsync(
            AccountBookViewModel book, Expression<Func<VoucherLine, bool>> itemCriteria,
            VoucherType voucherType, DateTime from, DateTime to, GridOptions gridOptions, bool byBranch = false)
        {
            if (voucherType != VoucherType.NormalVoucher)
            {
                var date = await _report.GetSpecialVoucherDateAsync(voucherType);
                if (date.HasValue && date.Value.IsBetween(from, to))
                {
                    var lines = await Repository
                        .GetAllOperationQuery<VoucherLine>(
                            ViewName.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
                        .Where(line => line.Voucher.Type == (short)voucherType)
                        .Where(itemCriteria)
                        .Select(art => Mapper.Map<AccountBookItemViewModel>(art))
                        .ToListAsync();
                    lines = lines
                        .ApplyQuickFilter(gridOptions)
                        .ToList();
                    if (byBranch)
                    {
                        Array.ForEach(GetGroupByThenByItems(lines, item => item.BranchId).ToArray(), group =>
                        {
                            var aggregates = GetAggregatedBookItems(group, true);
                            Array.ForEach(aggregates.ToArray(), item => item.Description = voucherType.ToString());
                            book.Items.AddRange(aggregates);
                        });
                    }
                    else
                    {
                        var aggregates = GetAggregatedBookItems(lines, false);
                        Array.ForEach(aggregates.ToArray(), item => item.Description = voucherType.ToString());
                        book.Items.AddRange(aggregates);
                    }
                }
            }
        }

        private Expression<Func<VoucherLine, bool>> GetItemCriteria(int viewId, int itemId)
        {
            Expression<Func<VoucherLine, bool>> itemCriteria = null;
            switch (viewId)
            {
                case ViewName.Account:
                    var account = GetAccountItem<Account>(itemId);
                    Verify.ArgumentNotNull(account, nameof(account));
                    itemCriteria = (line => line.Account.FullCode.StartsWith(account.FullCode));
                    break;
                case ViewName.DetailAccount:
                    itemCriteria = (line => line.DetailId == itemId);
                    break;
                case ViewName.CostCenter:
                    itemCriteria = (line => line.CostCenterId == itemId);
                    break;
                case ViewName.Project:
                    itemCriteria = (line => line.ProjectId == itemId);
                    break;
                default:
                    break;
            }

            return itemCriteria;
        }

        private async Task<AccountBookItemViewModel> GetFirstBookItemAsync(
            int viewId, int accountId, DateTime date)
        {
            decimal balance = 0.0M;
            switch (viewId)
            {
                case ViewName.Account:
                    balance = await _report.GetAccountBalanceAsync(accountId, date);
                    break;
                case ViewName.DetailAccount:
                    balance = await _report.GetDetailAccountBalanceAsync(accountId, date);
                    break;
                case ViewName.CostCenter:
                    balance = await _report.GetCostCenterBalanceAsync(accountId, date);
                    break;
                case ViewName.Project:
                    balance = await _report.GetProjectBalanceAsync(accountId, date);
                    break;
                default:
                    break;
            }

            return new AccountBookItemViewModel()
            {
                Balance = balance,
                BranchId = UserContext.BranchId,
                BranchName = UserContext.BranchName,
                Description = "InitialBalance",
                RowNo = 1,
                VoucherDate = date.Date
            };
        }

        private IQueryable<VoucherLine> GetRawAccountBookLines(
            Expression<Func<VoucherLine, bool>> itemCriteria, DateTime from, DateTime to)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(
                    ViewName.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
                .Where(line => line.Voucher.Date.IsBetween(from, to))
                .Where(itemCriteria)
                .OrderBy(line => line.Voucher.Date)
                .ThenBy(line => line.Voucher.No)
                .ThenBy(line => line.RowNo);
            return query;
        }

        private void AggregateAccountBook(
            AccountBookViewModel book, IEnumerable<AccountBookItemViewModel> lines,
            bool byNo, bool byBranch = false)
        {
            foreach (var bookGroup in GetAccountBookGroups(lines, byNo, byBranch))
            {
                var aggregates = GetAggregatedBookItems(bookGroup, byNo || byBranch);
                book.Items.AddRange(aggregates);
            }
        }

        private TreeEntity GetAccountItem(int viewId, int itemId)
        {
            var item = default(TreeEntity);
            if (viewId == ViewName.Account)
            {
                item = GetAccountItem<Account>(itemId);
            }
            else if (viewId == ViewName.DetailAccount)
            {
                item = GetAccountItem<DetailAccount>(itemId);
            }
            else if (viewId == ViewName.CostCenter)
            {
                item = GetAccountItem<CostCenter>(itemId);
            }
            else if (viewId == ViewName.Project)
            {
                item = GetAccountItem<Project>(itemId);
            }

            return item;
        }

        private TItem GetAccountItem<TItem>(int itemId)
            where TItem : TreeEntity
        {
            var repository = UnitOfWork.GetRepository<TItem>();
            return repository.GetByID(itemId);
        }

        private IQueryable<TreeEntity> GetAccountItemQuery(int viewId)
        {
            IQueryable<TreeEntity> items = null;
            if (viewId == ViewName.Account)
            {
                items = Repository.GetAllQuery<Account>(viewId);
            }
            else if (viewId == ViewName.DetailAccount)
            {
                items = Repository.GetAllQuery<DetailAccount>(viewId);
            }
            else if (viewId == ViewName.CostCenter)
            {
                items = Repository.GetAllQuery<CostCenter>(viewId);
            }
            else if (viewId == ViewName.Project)
            {
                items = Repository.GetAllQuery<Project>(viewId);
            }

            return items;
        }

        private IEnumerable<IEnumerable<AccountBookItemViewModel>> GetAccountBookGroups(
            IEnumerable<AccountBookItemViewModel> items, bool byNo, bool byBranch)
        {
            IEnumerable<IEnumerable<AccountBookItemViewModel>> groups = null;
            if (byNo)
            {
                groups = byBranch
                    ? GetGroupByThenByItems(
                        items, item => item.VoucherDate, item => item.VoucherNo, item => item.BranchId)
                    : GetGroupByThenByItems(items, item => item.VoucherDate, item => item.VoucherNo);
            }
            else
            {
                groups = byBranch
                    ? GetGroupByThenByItems(items, item => item.VoucherDate, item => item.BranchId)
                    : GetGroupByThenByItems(items, item => item.VoucherDate);
            }

            return groups;
        }

        private IEnumerable<IEnumerable<AccountBookItemViewModel>> GetGroupByThenByItems<TKey1>(
            IEnumerable<AccountBookItemViewModel> lines, Func<AccountBookItemViewModel, TKey1> selector1)
        {
            foreach (var byFirst in lines
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                yield return byFirst;
            }
        }

        private IEnumerable<IEnumerable<AccountBookItemViewModel>> GetGroupByThenByItems<TKey1, TKey2>(
            IEnumerable<AccountBookItemViewModel> lines, Func<AccountBookItemViewModel, TKey1> selector1,
            Func<AccountBookItemViewModel, TKey2> selector2)
        {
            foreach (var byFirst in lines
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

        private IEnumerable<IEnumerable<AccountBookItemViewModel>> GetGroupByThenByItems<TKey1, TKey2, TKey3>(
            IEnumerable<AccountBookItemViewModel> lines, Func<AccountBookItemViewModel, TKey1> selector1,
            Func<AccountBookItemViewModel, TKey2> selector2, Func<AccountBookItemViewModel, TKey3> selector3)
        {
            foreach (var byFirst in lines
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

        private readonly ISystemRepository _system;
        private readonly IReportRepository _report;
    }
}

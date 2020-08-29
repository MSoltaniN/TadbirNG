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
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر حساب را پیاده سازی می کند
    /// </summary>
    public class AccountBookRepository : LoggingRepository<Account, object>, IAccountBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="factory">امکان ساختن کلاس کمکی محاسبات مالی را فراهم می کند</param>
        public AccountBookRepository(IRepositoryContext context, ISystemRepository system,
            IAccountItemUtilityFactory factory)
            : base(context, system.Logger)
        {
            _system = system;
            _factory = factory;
            _report = _factory.Create(ViewId.Account);
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
            var utility = _factory.Create(viewId);
            var accountItem = await utility.GetItemAsync(itemId);
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
            var utility = _factory.Create(viewId);
            var accountItem = await utility.GetItemAsync(itemId);
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

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return book;
        }

        private async Task<AccountBookViewModel> GetSimpleBookAsync(AccountBookParameters parameters)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(parameters.ViewId, parameters.ItemId, parameters.FromDate));

            var itemCriteria = await GetItemCriteriaAsync(parameters.ViewId, parameters.ItemId);
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

            var itemCriteria = await GetItemCriteriaAsync(parameters.ViewId, parameters.ItemId);
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

            var itemCriteria = await GetItemCriteriaAsync(parameters.ViewId, parameters.ItemId);
            await AddSpecialBookItemsAsync(
                book, itemCriteria, VoucherOriginId.OpeningVoucher, parameters.FromDate, parameters.ToDate,
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
                        Array.ForEach(_report.GetGroupByItems(monthLines, item => item.BranchId).ToArray(), group =>
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
                book, itemCriteria, VoucherOriginId.ClosingTempAccounts, parameters.FromDate, parameters.ToDate,
                parameters.GridOptions, parameters.IsByBranch);
            await AddSpecialBookItemsAsync(
                book, itemCriteria, VoucherOriginId.ClosingVoucher, parameters.FromDate, parameters.ToDate,
                parameters.GridOptions, parameters.IsByBranch);

            book.SetItems(book.Items.Apply(parameters.GridOptions, false).ToArray());
            PrepareAccountBook(book, parameters.GridOptions);
            return book;
        }

        private async Task AddSpecialBookItemsAsync(
            AccountBookViewModel book, Expression<Func<VoucherLine, bool>> itemCriteria,
            VoucherOriginId origin, DateTime from, DateTime to, GridOptions gridOptions, bool byBranch = false)
        {
            if (origin != VoucherOriginId.NormalVoucher)
            {
                var date = await _report.GetSpecialVoucherDateAsync(origin);
                if (date.HasValue && date.Value.IsBetween(from, to))
                {
                    var lines = await Repository
                        .GetAllOperationQuery<VoucherLine>(
                            ViewId.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
                        .Where(line => line.Voucher.VoucherOriginId == (int)origin)
                        .Where(itemCriteria)
                        .Select(art => Mapper.Map<AccountBookItemViewModel>(art))
                        .ToListAsync();
                    lines = lines
                        .ApplyQuickFilter(gridOptions)
                        .ToList();
                    if (byBranch)
                    {
                        Array.ForEach(_report.GetGroupByItems(lines, item => item.BranchId).ToArray(), group =>
                        {
                            var aggregates = GetAggregatedBookItems(group, true);
                            Array.ForEach(aggregates.ToArray(), item => item.Description = origin.ToString());
                            book.Items.AddRange(aggregates);
                        });
                    }
                    else
                    {
                        var aggregates = GetAggregatedBookItems(lines, false);
                        Array.ForEach(aggregates.ToArray(), item => item.Description = origin.ToString());
                        book.Items.AddRange(aggregates);
                    }
                }
            }
        }

        private async Task<Expression<Func<VoucherLine, bool>>> GetItemCriteriaAsync(int viewId, int itemId)
        {
            var utility = _factory.Create(viewId);
            var accountItem = await utility.GetItemAsync(itemId);
            return utility.GetItemCriteria(accountItem);
        }

        private async Task<AccountBookItemViewModel> GetFirstBookItemAsync(
            int viewId, int accountId, DateTime date)
        {
            var utility = _factory.Create(viewId);
            decimal balance = await utility.GetBalanceAsync(accountId, date);
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
                    ViewId.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
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

        private IEnumerable<IEnumerable<AccountBookItemViewModel>> GetAccountBookGroups(
            IEnumerable<AccountBookItemViewModel> items, bool byNo, bool byBranch)
        {
            IEnumerable<IEnumerable<AccountBookItemViewModel>> groups = null;
            if (byNo)
            {
                groups = byBranch
                    ? _report.GetGroupByItems(
                        items, item => item.VoucherDate, item => item.VoucherNo, item => item.BranchId)
                    : _report.GetGroupByItems(items, item => item.VoucherDate, item => item.VoucherNo);
            }
            else
            {
                groups = byBranch
                    ? _report.GetGroupByItems(items, item => item.VoucherDate, item => item.BranchId)
                    : _report.GetGroupByItems(items, item => item.VoucherDate)
                          as IEnumerable<IEnumerable<AccountBookItemViewModel>>;
            }

            return groups;
        }

        private readonly ISystemRepository _system;
        private readonly IAccountItemUtilityFactory _factory;
        private readonly IReportUtility _report;
    }
}

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
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر عملیات ارزی را تعریف می کند
    /// </summary>
    public class CurrencyBookRepository : LoggingRepository<Account, object>, ICurrencyBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="factory">امکان ساختن کلاس کمکی محاسبات مالی را فراهم می کند</param>
        public CurrencyBookRepository(IRepositoryContext context, ISystemRepository system,
            IAccountItemUtilityFactory factory)
            : base(context, system.Logger)
        {
            _system = system;
            _factory = factory;
            _report = _factory.Create(ViewId.Account);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookAsync(CurrencyBookParameters parameters)
        {
            return await GetCurrencyBookDataAsync(parameters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <returns>اطلاعات دفتر عملیات ارزی به تفکیک شعبه</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookByBranchAsync(CurrencyBookParameters parameters)
        {
            return await GetCurrencyBookDataAsync(parameters);
        }

        /// <summary>
        /// به روش آسنکرون، تمامی ارزهای استفاده شده در آرتیکل های سند را به همراه مجموع بدهکار و بستانکار برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <returns>اطلاعات دفتر عملیات ارزی برای کلیه ارزها</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookAllCurrenciesAsync(
            CurrencyBookParameters bookParam)
        {
            var sourceList = SourceListId.None;
            switch (bookParam.Mode)
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

            await OnSourceActionAsync(bookParam.GridOptions, sourceList);
            return await GetSummaryBookAsync(bookParam, true, false);
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.CurrencyBook; }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private static IEnumerable<CurrencyBookItemViewModel> GetAggregatedBookItems(
            IEnumerable<CurrencyBookItemViewModel> items, bool singleMode, bool byCurrency = false)
        {
            var aggregates = new List<CurrencyBookItemViewModel>();
            decimal debitSum = items.Sum(line => line.Debit);
            decimal creditSum = items.Sum(line => line.Credit);
            decimal baseCurrencyDebitSum = items.Sum(line => line.BaseCurrencyDebit);
            decimal baseCurrencyCreditSum = items.Sum(line => line.BaseCurrencyCredit);

            if (byCurrency)
            {
                var item = GetAggregatedBookItem(items.First(), singleMode);
                item.Debit = item.CurrencyId == null ? baseCurrencyDebitSum : debitSum;
                item.Credit = item.CurrencyId == null ? baseCurrencyCreditSum : creditSum;
                item.CurrencyName = !string.IsNullOrEmpty(item.CurrencyName) ? item.CurrencyName : "Currency_Free_Rows";
                item.LineCount = items.Count();
                item.HasChild = true;
                aggregates.Add(item);
            }
            else
            {
                if (debitSum > 0.0M)
                {
                    var item = GetAggregatedBookItem(items.First(), singleMode);
                    item.Debit = debitSum;
                    item.BaseCurrencyDebit = baseCurrencyDebitSum;
                    item.LineCount = items.Count(line => line.Debit > 0.0M);
                    aggregates.Add(item);
                }

                if (creditSum > 0.0M)
                {
                    var item = GetAggregatedBookItem(items.First(), singleMode);
                    item.Credit = creditSum;
                    item.BaseCurrencyCredit = baseCurrencyCreditSum;
                    item.LineCount = items.Count(line => line.Credit > 0.0M);
                    aggregates.Add(item);
                }
            }

            return aggregates;
        }

        private static CurrencyBookItemViewModel GetAggregatedBookItem(
           CurrencyBookItemViewModel item, bool singleMode)
        {
            return new CurrencyBookItemViewModel()
            {
                Description = "AsQuotedInJournal",
                VoucherDate = item.VoucherDate,
                VoucherNo = singleMode ? item.VoucherNo : 0,
                VoucherStatusId = singleMode ? item.VoucherStatusId : 0,
                VoucherConfirmedById = singleMode ? item.VoucherConfirmedById : null,
                VoucherApprovedById = singleMode ? item.VoucherApprovedById : null,
                BranchId = singleMode ? item.BranchId : 0,
                BranchName = singleMode ? item.BranchName : null,
                CurrencyName = item.CurrencyName,
                CurrencyRate = item.CurrencyRate,
                CurrencyId = item.CurrencyId
            };
        }

        private static void PrepareCurrencyBook(CurrencyBookViewModel book, GridOptions gridOptions)
        {
            Array.ForEach(book.Items.ToArray(), item =>
            {
                item.Balance = item.Debit - item.Credit;
                item.BaseCurrencyBalance = item.BaseCurrencyDebit - item.BaseCurrencyCredit;
            });
            book.DebitSum = book.Items.Sum(item => item.Debit);
            book.CreditSum = book.Items.Sum(item => item.Credit);
            book.Balance = book.DebitSum - book.CreditSum;

            book.BaseCurrencyDebitSum = book.Items.Sum(item => item.BaseCurrencyDebit);
            book.BaseCurrencyCreditSum = book.Items.Sum(item => item.BaseCurrencyCredit);
            book.BaseCurrencyBalance = book.BaseCurrencyDebitSum - book.BaseCurrencyCreditSum;

            book.TotalCount = book.Items.Count;
            book.SetItems(book.Items.ApplyPaging(gridOptions).ToArray());
        }

        private async Task<CurrencyBookViewModel> GetCurrencyBookDataAsync(
            CurrencyBookParameters parameters)
        {
            var currencyBook = default(CurrencyBookViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case AccountBookMode.ByRows:
                    currencyBook = await GetSimpleBookAsync(parameters);
                    sourceList = SourceListId.CurrencyBookByRow;
                    break;
                case AccountBookMode.VoucherSum:
                    currencyBook = await GetSummaryBookAsync(parameters, false, true);
                    sourceList = SourceListId.CurrencyBookVoucherSum;
                    break;
                case AccountBookMode.DailySum:
                    currencyBook = await GetSummaryBookAsync(parameters, false, false);
                    sourceList = SourceListId.CurrencyBookDailySum;
                    break;
                case AccountBookMode.MonthlySum:
                    currencyBook = await GetMonthlySummaryBookAsync(parameters);
                    sourceList = SourceListId.CurrencyBookMonthlySum;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return currencyBook;
        }

        private IList<Expression<Func<VoucherLine, bool>>> GetItemCriteria(
            CurrencyBookParameters bookParam, bool byCurrency = false)
        {
            var itemCriteria = new List<Expression<Func<VoucherLine, bool>>>();

            if (bookParam.AccountId != null)
            {
                var account = GetAccountItem<Account>(bookParam.AccountId.Value);
                Verify.ArgumentNotNull(account, nameof(account));
                itemCriteria.Add(line => line.Account.FullCode.StartsWith(account.FullCode));
            }

            if (bookParam.FAccountId != null)
            {
                var detailAccount = GetAccountItem<DetailAccount>(bookParam.FAccountId.Value);
                Verify.ArgumentNotNull(detailAccount, nameof(detailAccount));
                itemCriteria.Add(line => line.Account.FullCode.StartsWith(detailAccount.FullCode));
            }

            if (bookParam.CCenterId != null)
            {
                var costCenter = GetAccountItem<CostCenter>(bookParam.CCenterId.Value);
                Verify.ArgumentNotNull(costCenter, nameof(costCenter));
                itemCriteria.Add(line => line.Account.FullCode.StartsWith(costCenter.FullCode));
            }

            if (bookParam.ProjectId != null)
            {
                var project = GetAccountItem<Project>(bookParam.ProjectId.Value);
                Verify.ArgumentNotNull(project, nameof(project));
                itemCriteria.Add(line => line.Account.FullCode.StartsWith(project.FullCode));
            }

            if (!bookParam.CurrFree && byCurrency)
            {
                itemCriteria.Add(line => line.CurrencyId != null);
            }

            return itemCriteria;
        }

        private async Task<CurrencyBookViewModel> GetSimpleBookAsync(
            CurrencyBookParameters bookParam)
        {
            var book = new CurrencyBookViewModel();

            var itemCriteria = GetItemCriteria(bookParam);
            var bookItems = await GetRawCurrencyBookLines(itemCriteria, bookParam.From, bookParam.To)
                .ToListAsync();
            book.Items.AddRange(bookItems
                .Select(line => Mapper.Map<CurrencyBookItemViewModel>(line))
                .ApplyQuickFilter(bookParam.GridOptions)
                .Apply(bookParam.GridOptions, false));
            PrepareCurrencyBook(book, bookParam.GridOptions);
            return book;
        }

        private async Task<CurrencyBookViewModel> GetSummaryBookAsync(
           CurrencyBookParameters bookParam, bool byCurrency, bool byNo)
        {
            var book = new CurrencyBookViewModel();

            var itemCriteria = GetItemCriteria(bookParam, byCurrency);
            var lines = await GetRawCurrencyBookLines(itemCriteria, bookParam.From, bookParam.To)
                .Select(line => Mapper.Map<CurrencyBookItemViewModel>(line))
                .ToListAsync();
            lines = lines
                .ApplyQuickFilter(bookParam.GridOptions)
                .ToList();
            AggregateCurrencyBook(book, lines, byCurrency, byNo, bookParam.ByBranch);
            book.SetItems(book.Items.Apply(bookParam.GridOptions, false).ToArray());
            PrepareCurrencyBook(book, bookParam.GridOptions);
            return book;
        }

        private async Task<CurrencyBookViewModel> GetMonthlySummaryBookAsync(
            CurrencyBookParameters bookParam)
        {
            var book = new CurrencyBookViewModel();

            var itemCriteria = GetItemCriteria(bookParam);
            await AddSpecialBookItemsAsync(book, itemCriteria,
                VoucherOriginId.OpeningVoucher, bookParam);

            var monthEnum = new MonthEnumerator(bookParam.From, bookParam.To, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = GetRawCurrencyBookLines(itemCriteria, month.Start, month.End)
                    .Where(art => art.Voucher.Type == (short)VoucherType.NormalVoucher)
                    .Select(art => Mapper.Map<CurrencyBookItemViewModel>(art))
                    .ToList();
                monthLines = monthLines
                    .ApplyQuickFilter(bookParam.GridOptions)
                    .ToList();
                if (monthLines.Count > 0)
                {
                    if (bookParam.ByBranch)
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

            await AddSpecialBookItemsAsync(book, itemCriteria,
                VoucherOriginId.ClosingTempAccounts, bookParam);
            await AddSpecialBookItemsAsync(book, itemCriteria,
                VoucherOriginId.ClosingVoucher, bookParam);

            book.SetItems(book.Items.Apply(bookParam.GridOptions, false).ToArray());
            PrepareCurrencyBook(book, bookParam.GridOptions);
            return book;
        }

        private async Task AddSpecialBookItemsAsync(
           CurrencyBookViewModel book, IList<Expression<Func<VoucherLine, bool>>> itemCriteria,
           VoucherOriginId origin, CurrencyBookParameters bookParam)
        {
            if (origin != VoucherOriginId.NormalVoucher)
            {
                var date = await _report.GetSpecialVoucherDateAsync(origin);
                if (date.HasValue && date.Value.IsBetween(bookParam.From, bookParam.To))
                {
                    var query = Repository
                        .GetAllOperationQuery<VoucherLine>(
                            ViewId.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
                        .Where(line => line.Voucher.VoucherOriginId == (int)origin);

                    foreach (var item in itemCriteria)
                    {
                        query = query.Where(item);
                    }

                    var lines = await query
                        .Select(art => Mapper.Map<CurrencyBookItemViewModel>(art))
                        .ToListAsync();
                    lines = lines
                        .ApplyQuickFilter(bookParam.GridOptions)
                        .ToList();
                    if (bookParam.ByBranch)
                    {
                        Array.ForEach(GetGroupByThenByItems(lines, item => item.BranchId).ToArray(), group =>
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

        private IQueryable<VoucherLine> GetRawCurrencyBookLines(
            IList<Expression<Func<VoucherLine, bool>>> itemCriteria, DateTime from, DateTime to)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(
                    ViewId.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch, line => line.Currency)
                .Where(line => line.Voucher.Date.IsBetween(from, to));

            foreach (var item in itemCriteria)
            {
                query = query.Where(item);
            }

            query = query
                .OrderBy(line => line.Voucher.Date)
                .ThenBy(line => line.Voucher.No)
                .ThenBy(line => line.RowNo);

            return query;
        }

        private void AggregateCurrencyBook(
            CurrencyBookViewModel book,
            IEnumerable<CurrencyBookItemViewModel> lines,
           bool byCurrency, bool byNo, bool byBranch = false)
        {
            foreach (var bookGroup in GetCurrencyBookGroups(lines, byCurrency, byNo, byBranch))
            {
                var aggregates = GetAggregatedBookItems(bookGroup, byNo || byBranch, byCurrency);
                book.Items.AddRange(aggregates);
            }
        }

        private IEnumerable<IEnumerable<CurrencyBookItemViewModel>> GetCurrencyBookGroups(
            IEnumerable<CurrencyBookItemViewModel> items, bool byCurrency, bool byNo, bool byBranch)
        {
            IEnumerable<IEnumerable<CurrencyBookItemViewModel>> groups = null;

            if (byCurrency)
            {
                groups = GetGroupByThenByItems(items, item => item.CurrencyId);
            }
            else
            {
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
            }

            return groups;
        }

        private IEnumerable<IEnumerable<CurrencyBookItemViewModel>> GetGroupByThenByItems<TKey1>(
            IEnumerable<CurrencyBookItemViewModel> lines, Func<CurrencyBookItemViewModel, TKey1> selector1)
        {
            foreach (var byFirst in lines
                .OrderBy(selector1)
                .GroupBy(selector1))
            {
                yield return byFirst;
            }
        }

        private IEnumerable<IEnumerable<CurrencyBookItemViewModel>> GetGroupByThenByItems<TKey1, TKey2>(
            IEnumerable<CurrencyBookItemViewModel> lines, Func<CurrencyBookItemViewModel, TKey1> selector1,
            Func<CurrencyBookItemViewModel, TKey2> selector2)
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

        private IEnumerable<IEnumerable<CurrencyBookItemViewModel>> GetGroupByThenByItems<TKey1, TKey2, TKey3>(
            IEnumerable<CurrencyBookItemViewModel> lines, Func<CurrencyBookItemViewModel, TKey1> selector1,
            Func<CurrencyBookItemViewModel, TKey2> selector2, Func<CurrencyBookItemViewModel, TKey3> selector3)
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

        private TItem GetAccountItem<TItem>(int itemId)
           where TItem : TreeEntity
        {
            var repository = UnitOfWork.GetRepository<TItem>();
            return repository.GetByID(itemId);
        }

        private readonly ISystemRepository _system;
        private readonly IAccountItemUtilityFactory _factory;
        private readonly IReportUtility _report;
    }
}

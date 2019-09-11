using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر عملیات ارزی را تعریف می کند
    /// </summary>
    public class CurrencyBookRepository : RepositoryBase, ICurrencyBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="report">پیاده سازی اینترفیس مربوط به عملیات گزارشی</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        public CurrencyBookRepository(IRepositoryContext context, IReportRepository report, ISecureRepository repository)
            : base(context)
        {
            _reportRepository = report;
            _repository = repository;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public override void SetCurrentContext(UserContextViewModel userContext)
        {
            base.SetCurrentContext(userContext);
            _repository.SetCurrentContext(userContext);
            _reportRepository.SetCurrentContext(userContext);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "ساده : مطابق ردیف های سند" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookByRowAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetSimpleBookAsync(bookParam, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ هر سند" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookVoucherSumAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(bookParam, gridOptions, false, true);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر روز" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookDailySumAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(bookParam, gridOptions, false, false);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر ماه" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookMonthlySumAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetMonthlySummaryBookAsync(bookParam, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "ساده : مطابق ردیف های سند" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookByRowByBranchAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetSimpleBookAsync(bookParam, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ هر سند" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookVoucherSumByBranchAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(bookParam, gridOptions, false, true);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر روز" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookDailySumByBranchAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(bookParam, gridOptions, false, false);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی با نمایش "مرکب : جمع مبالغ اسناد در هر ماه" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookMonthlySumByBranchAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetMonthlySummaryBookAsync(bookParam, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تمامی ارزهای استفاده شده در آرتیکل های سند را به همراه مجموع بدهکار و بستانکار برمی گرداند
        /// </summary>
        /// <param name="bookParam">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns></returns>
        public async Task<CurrencyBookViewModel> GetCurrencyBookAllCurrenciesAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(bookParam, gridOptions, true, false);
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
            int rowNo = 1;
            Array.ForEach(book.Items.ToArray(), item =>
            {
                item.Balance = item.Debit - item.Credit;
                item.BaseCurrencyBalance = item.BaseCurrencyDebit - item.BaseCurrencyCredit;
                item.RowNo = rowNo++;
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

        private async Task<CurrencyBookViewModel> GetSimpleBookAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            var book = new CurrencyBookViewModel();

            var itemCriteria = GetItemCriteria(bookParam);
            var bookItems = await GetRawCurrencyBookLines(itemCriteria, bookParam.From, bookParam.To)
                .ToListAsync();
            AddBookItems(book, bookItems, gridOptions);
            return book;
        }

        private async Task<CurrencyBookViewModel> GetSummaryBookAsync(
           CurrencyBookParamViewModel bookParam, GridOptions gridOptions,
           bool byCurrency, bool byNo)
        {
            var book = new CurrencyBookViewModel();

            var itemCriteria = GetItemCriteria(bookParam);
            var lines = await GetRawCurrencyBookLines(itemCriteria, bookParam.From, bookParam.To)
                .ToListAsync();
            AggregateCurrencyBook(book, lines, gridOptions, byCurrency, byNo, bookParam.ByBranch);

            return book;
        }

        private async Task<CurrencyBookViewModel> GetMonthlySummaryBookAsync(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            var book = new CurrencyBookViewModel();

            var itemCriteria = GetItemCriteria(bookParam);
            await AddSpecialBookItemsAsync(book, itemCriteria,
                VoucherType.OpeningVoucher, bookParam, gridOptions);

            var monthEnum = new MonthEnumerator(bookParam.From, bookParam.To, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = GetRawAccountBookLines(itemCriteria, month.Start, month.End)
                    .Where(art => art.Voucher.Type == (short)VoucherType.NormalVoucher)
                    .Select(art => Mapper.Map<CurrencyBookItemViewModel>(art))
                    .Apply(gridOptions, false)
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
                VoucherType.ClosingTempAccounts, bookParam, gridOptions);
            await AddSpecialBookItemsAsync(book, itemCriteria,
                VoucherType.ClosingVoucher, bookParam, gridOptions);

            PrepareCurrencyBook(book, gridOptions);
            return book;
        }

        private async Task AddSpecialBookItemsAsync(
           CurrencyBookViewModel book, IList<Expression<Func<VoucherLine, bool>>> itemCriteria,
           VoucherType voucherType, CurrencyBookParamViewModel bookParam, GridOptions gridOptions)
        {
            if (voucherType != VoucherType.NormalVoucher)
            {
                var date = await _reportRepository.GetSpecialVoucherDateAsync(voucherType);
                if (date.HasValue && date.Value.IsBetween(bookParam.From, bookParam.To))
                {
                    var query = _repository
                        .GetAllOperationQuery<VoucherLine>(
                            ViewName.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
                        .Where(line => line.Voucher.Type == (short)voucherType);

                    foreach (var item in itemCriteria)
                    {
                        query = query.Where(item);
                    }

                    var lines = query
                    .Select(art => Mapper.Map<CurrencyBookItemViewModel>(art))
                        .Apply(gridOptions, false)
                        .ToList();

                    if (bookParam.ByBranch)
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

        private IQueryable<VoucherLine> GetRawAccountBookLines(
            IList<Expression<Func<VoucherLine, bool>>> itemCriteria, DateTime from, DateTime to)
        {
            var query = _repository
                .GetAllOperationQuery<VoucherLine>(
                    ViewName.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
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

        private IList<Expression<Func<VoucherLine, bool>>> GetItemCriteria(CurrencyBookParamViewModel bookParam)
        {
            var itemCriteria = new List<Expression<Func<VoucherLine, bool>>>();

            if (bookParam.AccountId != null)
            {
                itemCriteria.Add(line => line.AccountId == bookParam.AccountId);
            }

            if (bookParam.FAccountId != null)
            {
                itemCriteria.Add(line => line.DetailId == bookParam.FAccountId);
            }

            if (bookParam.CCenterId != null)
            {
                itemCriteria.Add(line => line.CostCenterId == bookParam.CCenterId);
            }

            if (bookParam.ProjectId != null)
            {
                itemCriteria.Add(line => line.ProjectId == bookParam.ProjectId);
            }

            if (!bookParam.CurrFree)
            {
                itemCriteria.Add(line => line.CurrencyId != null);
            }

            return itemCriteria;
        }

        private IQueryable<VoucherLine> GetRawCurrencyBookLines(
            IList<Expression<Func<VoucherLine, bool>>> itemCriteria, DateTime from, DateTime to)
        {
            var query = _repository
                .GetAllOperationQuery<VoucherLine>(
                    ViewName.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch, line => line.Currency)
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

        private void AddBookItems(CurrencyBookViewModel book,
            IList<VoucherLine> items, GridOptions gridOptions)
        {
            book.Items.AddRange(items
                .Select(line => Mapper.Map<CurrencyBookItemViewModel>(line))
                .Apply(gridOptions, false));
            PrepareCurrencyBook(book, gridOptions);
        }

        private void AggregateCurrencyBook(
            CurrencyBookViewModel book,
            IEnumerable<VoucherLine> lines,
            GridOptions gridOptions,
           bool byCurrency, bool byNo, bool byBranch = false)
        {
            var items = lines
                .Select(line => Mapper.Map<CurrencyBookItemViewModel>(line))
                .Apply(gridOptions, false);

            foreach (var bookGroup in GetCurrencyBookGroups(items, byCurrency, byNo, byBranch))
            {
                var aggregates = GetAggregatedBookItems(bookGroup, byNo || byBranch, byCurrency);
                book.Items.AddRange(aggregates);
            }

            PrepareCurrencyBook(book, gridOptions);
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

        private readonly IReportRepository _reportRepository;
        private readonly ISecureRepository _repository;
    }
}

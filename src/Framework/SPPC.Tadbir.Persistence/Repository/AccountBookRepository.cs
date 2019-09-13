using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر حساب را پیاده سازی می کند
    /// </summary>
    public class AccountBookRepository : RepositoryBase, IAccountBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="report">پیاده سازی اینترفیس مربوط به عملیات گزارشی</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        public AccountBookRepository(IRepositoryContext context, IReportRepository report, ISecureRepository repository)
            : base(context)
        {
            _reportRepository = report;
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "ساده : مطابق ردیف های سند" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookByRowAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetSimpleBookAsync(viewId, accountId, from, to, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "مرکب : جمع مبالغ هر سند" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookVoucherSumAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(viewId, accountId, from, to, gridOptions, true, false);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "مرکب : جمع مبالغ اسناد در هر روز" را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookDailySumAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(viewId, accountId, from, to, gridOptions, false, false);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "مرکب : جمع مبالغ اسناد در هر ماه" را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookMonthlySumAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetMonthlySummaryBookAsync(viewId, accountId, from, to, gridOptions, false);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "ساده : مطابق ردیف های سند" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookByRowByBranchAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetSimpleBookAsync(viewId, accountId, from, to, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "مرکب : جمع مبالغ هر سند" را به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookVoucherSumByBranchAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(viewId, accountId, from, to, gridOptions, true, true);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "مرکب : جمع مبالغ اسناد در هر روز" را
        /// به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookDailySumByBranchAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetSummaryBookAsync(viewId, accountId, from, to, gridOptions, false, true);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر حساب با نمایش "مرکب : جمع مبالغ اسناد در هر ماه" را
        /// به تفکیک شعبه خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر - حساب، شناور، مرکز هزینه یا پروژه</param>
        /// <param name="accountId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookMonthlySumByBranchAsync(int viewId, int accountId,
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            return await GetMonthlySummaryBookAsync(viewId, accountId, from, to, gridOptions, true);
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

        private static void PrepareAccountBook(AccountBookViewModel book, GridOptions gridOptions)
        {
            int rowNo = 2;
            decimal balance = book.Items[0].Balance;
            Array.ForEach(book.Items.Skip(1).ToArray(), item =>
            {
                balance = balance + item.Debit - item.Credit;
                item.Balance = balance;
                item.RowNo = rowNo++;
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

        private async Task<AccountBookViewModel> GetSimpleBookAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(viewId, accountId, from));

            var itemCriteria = GetItemCriteria(viewId, accountId);
            var bookItems = await GetRawAccountBookLines(itemCriteria, from, to)
                .ToListAsync();
            AddBookItems(book, bookItems, gridOptions);
            return book;
        }

        private async Task<AccountBookViewModel> GetSummaryBookAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions,
            bool byNo, bool byBranch = false)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(viewId, accountId, from));

            var itemCriteria = GetItemCriteria(viewId, accountId);
            var lines = await GetRawAccountBookLines(itemCriteria, from, to).ToListAsync();
            AggregateAccountBook(book, lines, gridOptions, byNo, byBranch);
            return book;
        }

        private async Task<AccountBookViewModel> GetMonthlySummaryBookAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions,
            bool byBranch = false)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(viewId, accountId, from));

            var itemCriteria = GetItemCriteria(viewId, accountId);
            await AddSpecialBookItemsAsync(book, itemCriteria,
                VoucherType.OpeningVoucher, from, to, gridOptions, byBranch);

            var monthEnum = new MonthEnumerator(from, to, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = GetRawAccountBookLines(itemCriteria, month.Start, month.End)
                    .Where(art => art.Voucher.Type == (short)VoucherType.NormalVoucher)
                    .Select(art => Mapper.Map<AccountBookItemViewModel>(art))
                    .Apply(gridOptions, false)
                    .ToList();
                if (monthLines.Count > 0)
                {
                    if (byBranch)
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
                VoucherType.ClosingTempAccounts, from, to, gridOptions, byBranch);
            await AddSpecialBookItemsAsync(book, itemCriteria,
                VoucherType.ClosingVoucher, from, to, gridOptions, byBranch);

            PrepareAccountBook(book, gridOptions);
            return book;
        }

        private async Task AddSpecialBookItemsAsync(
            AccountBookViewModel book, Expression<Func<VoucherLine, bool>> itemCriteria,
            VoucherType voucherType, DateTime from, DateTime to, GridOptions gridOptions,
            bool byBranch = false)
        {
            if (voucherType != VoucherType.NormalVoucher)
            {
                var date = await _reportRepository.GetSpecialVoucherDateAsync(voucherType);
                if (date.HasValue && date.Value.IsBetween(from, to))
                {
                    var lines = _repository
                        .GetAllOperationQuery<VoucherLine>(
                            ViewName.VoucherLine, line => line.Voucher, line => line.Account, line => line.Branch)
                        .Where(line => line.Voucher.Type == (short)voucherType)
                        .Where(itemCriteria)
                        .Select(art => Mapper.Map<AccountBookItemViewModel>(art))
                        .Apply(gridOptions, false)
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
                    balance = await _reportRepository.GetAccountBalanceAsync(accountId, date);
                    break;
                case ViewName.DetailAccount:
                    balance = await _reportRepository.GetDetailAccountBalanceAsync(accountId, date);
                    break;
                case ViewName.CostCenter:
                    balance = await _reportRepository.GetCostCenterBalanceAsync(accountId, date);
                    break;
                case ViewName.Project:
                    balance = await _reportRepository.GetProjectBalanceAsync(accountId, date);
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

        private void AddBookItems(AccountBookViewModel book,
            IList<VoucherLine> items, GridOptions gridOptions)
        {
            book.Items.AddRange(items
                .Select(line => Mapper.Map<AccountBookItemViewModel>(line))
                .Apply(gridOptions, false));
            PrepareAccountBook(book, gridOptions);
        }

        private IQueryable<VoucherLine> GetRawAccountBookLines(
            Expression<Func<VoucherLine, bool>> itemCriteria, DateTime from, DateTime to)
        {
            var query = _repository
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
            AccountBookViewModel book, IEnumerable<VoucherLine> lines, GridOptions gridOptions,
            bool byNo, bool byBranch = false)
        {
            var items = lines
                .Select(line => Mapper.Map<AccountBookItemViewModel>(line))
                .Apply(gridOptions, false);
            foreach (var bookGroup in GetAccountBookGroups(items, byNo, byBranch))
            {
                var aggregates = GetAggregatedBookItems(bookGroup, byNo || byBranch);
                book.Items.AddRange(aggregates);
            }

            PrepareAccountBook(book, gridOptions);
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
                items = _repository.GetAllQuery<Account>(viewId);
            }
            else if (viewId == ViewName.DetailAccount)
            {
                items = _repository.GetAllQuery<DetailAccount>(viewId);
            }
            else if (viewId == ViewName.CostCenter)
            {
                items = _repository.GetAllQuery<CostCenter>(viewId);
            }
            else if (viewId == ViewName.Project)
            {
                items = _repository.GetAllQuery<Project>(viewId);
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

        private readonly IReportRepository _reportRepository;
        private readonly ISecureRepository _repository;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
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
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن اطلاعات فراداده ای را فراهم می کند</param>
        /// <param name="item">پیاده سازی اینترفیس مربوط به عملیات بردار حساب</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        public AccountBookRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper,
            IMetadataRepository metadata, IAccountItemRepository item, ISecureRepository repository)
            : base(unitOfWork, mapper, metadata)
        {
            _itemRepository = item;
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
            _itemRepository.SetCurrentContext(userContext);
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
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(viewId, accountId, from));

            var itemCriteria = GetItemCriteria(viewId, accountId);
            var bookItems = await GetRawAccountBookLines(itemCriteria, from, to)
                .Select(line => Mapper.Map<AccountBookItemViewModel>(line))
                .ToListAsync();
            AddBookItems(book, bookItems, gridOptions);
            return book;
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
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(viewId, accountId, from));

            var itemCriteria = GetItemCriteria(viewId, accountId);
            var lines = await GetRawAccountBookLines(itemCriteria, from, to).ToListAsync();
            AggregateAccountBook(book, lines, gridOptions, line => line.VoucherNo);
            return book;
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
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(viewId, accountId, from));

            var itemCriteria = GetItemCriteria(viewId, accountId);
            var lines = await GetRawAccountBookLines(itemCriteria, from, to).ToListAsync();
            AggregateAccountBook(book, lines, gridOptions, line => line.VoucherDate);
            return book;
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
            var previousItem = await GetAccountItemQuery(viewId)
                .Where(item => item.Id < itemId)
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
            var nextItem = await GetAccountItemQuery(viewId)
                .Where(item => item.Id > itemId)
                .OrderBy(item => item.Id)
                .FirstOrDefaultAsync();
            if (nextItem != null)
            {
                next = Mapper.Map<AccountItemBriefViewModel>(nextItem);
            }

            return next;
        }

        private static void PrepareAccountBook(AccountBookViewModel book)
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
        }

        private static AccountBookItemViewModel GetAggregatedBookItem(
            AccountBookItemViewModel line, bool singleMode)
        {
            return new AccountBookItemViewModel()
            {
                Description = "AsQuotedInJournal",
                VoucherDate = line.VoucherDate,
                VoucherNo = singleMode ? line.VoucherNo : 0,
                VoucherStatusId = singleMode ? line.VoucherStatusId : 0,
                VoucherConfirmedById = singleMode ? line.VoucherConfirmedById : null,
                VoucherApprovedById = singleMode ? line.VoucherApprovedById : null
            };
        }

        private Expression<Func<VoucherLine, bool>> GetItemCriteria(int viewId, int itemId)
        {
            Expression<Func<VoucherLine, bool>> itemCriteria = null;
            switch (viewId)
            {
                case ViewName.Account:
                    var account = GetAccountItem(itemId);
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
                    balance = await _itemRepository.GetAccountBalanceAsync(accountId, date);
                    break;
                case ViewName.DetailAccount:
                    balance = await _itemRepository.GetDetailAccountBalanceAsync(accountId, date);
                    break;
                case ViewName.CostCenter:
                    balance = await _itemRepository.GetCostCenterBalanceAsync(accountId, date);
                    break;
                case ViewName.Project:
                    balance = await _itemRepository.GetProjectBalanceAsync(accountId, date);
                    break;
                default:
                    break;
            }

            return new AccountBookItemViewModel()
            {
                Balance = balance,
                BranchId = _currentContext.BranchId,
                BranchName = _currentContext.BranchName,
                Description = "InitialBalance",
                RowNo = 1,
                VoucherDate = date.Date
            };
        }

        private void AddBookItems(AccountBookViewModel book,
            IList<AccountBookItemViewModel> items, GridOptions gridOptions)
        {
            items = items
                .Apply(gridOptions)
                .ToList();
            book.Items.AddRange(items);
            PrepareAccountBook(book);
        }

        private IQueryable<VoucherLine> GetRawAccountBookLines(
            Expression<Func<VoucherLine, bool>> itemCriteria, DateTime from, DateTime to)
        {
            return _repository
                .GetAllOperationQuery<VoucherLine>(
                    ViewName.VoucherLine, line => line.Voucher, line => line.Account)
                .Where(line => line.Voucher.Date.CompareWith(from) >= 0
                    && line.Voucher.Date.CompareWith(to) <= 0)
                .Where(itemCriteria)
                .OrderBy(line => line.Voucher.Date)
                    .ThenBy(line => line.Voucher.No);
        }

        private void AggregateAccountBook<TKey>(
            AccountBookViewModel book, IEnumerable<VoucherLine> lines, GridOptions gridOptions,
            Func<AccountBookItemViewModel, TKey> keySelector)
        {
            bool byNo = typeof(TKey) == typeof(int);
            var items = lines
                .Select(line => Mapper.Map<AccountBookItemViewModel>(line))
                .Apply(gridOptions);
            var bookGroups = items
                .GroupBy(keySelector);
            foreach (var bookGroup in bookGroups)
            {
                var bookItem = GetAggregatedBookItem(bookGroup.First(), byNo);
                bookItem.Debit = bookGroup.Sum(line => line.Debit);
                bookItem.Credit = bookGroup.Sum(line => line.Credit);
                bookItem.LineCount = bookGroup.Count();
                book.Items.Add(bookItem);
            }

            PrepareAccountBook(book);
        }

        private Account GetAccountItem(int accountId)
        {
            var repository = UnitOfWork.GetRepository<Account>();
            return repository.GetByID(accountId);
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

        private readonly IAccountItemRepository _itemRepository;
        private readonly ISecureRepository _repository;
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
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
        /// <returns>اطلاعات دفتر حساب با مشخصات داده شده</returns>
        public async Task<AccountBookViewModel> GetAccountBookByRowAsync(
            int viewId, int accountId, DateTime from, DateTime to, GridOptions gridOptions)
        {
            var book = new AccountBookViewModel();
            book.Items.Add(await GetFirstBookItemAsync(viewId, accountId, from));
            if (viewId == ViewName.Account)
            {
                await AddBookItemsAsync(book, line => line.AccountId == accountId, from, to);
            }
            else if (viewId == ViewName.DetailAccount)
            {
                await AddBookItemsAsync(book, line => line.DetailId == accountId, from, to);
            }
            else if (viewId == ViewName.CostCenter)
            {
                await AddBookItemsAsync(book, line => line.CostCenterId == accountId, from, to);
            }
            else if (viewId == ViewName.Project)
            {
                await AddBookItemsAsync(book, line => line.ProjectId == accountId, from, to);
            }

            return book;
        }

        private static void SetRunningBalance(AccountBookViewModel book)
        {
            decimal balance = book.Items[0].Balance;
            Array.ForEach(book.Items.Skip(1).ToArray(), item =>
            {
                balance = balance + item.Debit - item.Credit;
                item.Balance = balance;
            });
        }

        private async Task<AccountBookItemViewModel> GetFirstBookItemAsync(int viewId, int accountId, DateTime date)
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
                Balance = await _itemRepository.GetAccountBalanceAsync(accountId, date),
                BranchId = _currentContext.BranchId,
                BranchName = _currentContext.BranchName,
                Description = "InitialBalance",
                RowNo = 1,
                VoucherDate = date.Date
            };
        }

        private async Task AddBookItemsAsync(AccountBookViewModel book, Expression<Func<VoucherLine, bool>> itemCriteria,
            DateTime from, DateTime to)
        {
            var bookItems = await GetRawAccountBookLines(itemCriteria, from, to)
                .Select(line => Mapper.Map<AccountBookItemViewModel>(line))
                .ToListAsync();
            book.Items.AddRange(bookItems);
            SetRunningBalance(book);
        }

        private IQueryable<VoucherLine> GetRawAccountBookLines(Expression<Func<VoucherLine, bool>> itemCriteria,
            DateTime from, DateTime to)
        {
            return _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, line => line.Voucher)
                .Where(line => line.Voucher.Date.CompareWith(from) >= 0
                    && line.Voucher.Date.CompareWith(to) <= 0)
                .Where(itemCriteria)
                .OrderBy(line => line.Voucher.Date)
                    .ThenBy(line => line.Voucher.No);
        }

        private readonly IAccountItemRepository _itemRepository;
        private readonly ISecureRepository _repository;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات سرفصل های حسابداری را تعریف می کند.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="decorator">امکان ضمیمه کردن متادیتا به اطلاعات خوانده شده را فراهم می کند</param>
        public AccountRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataDecorator decorator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _decorator = decorator;
        }

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<EntityListViewModel<AccountViewModel>> GetAccountsAsync(
            int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(
                    acc => acc.FiscalPeriod.Id == fpId
                        && acc.Branch.Id == branchId,
                    gridOptions,
                    acc => acc.FiscalPeriod, acc => acc.Branch);
            return await _decorator.GetDecoratedListAsync<Account, AccountViewModel>(accounts
                .Select(item => _mapper.Map<AccountViewModel>(item))
                .ToList());
        }

        /// <summary>
        /// به روش آسنکرون، حساب با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی</returns>
        public async Task<EntityItemViewModel<AccountViewModel>> GetAccountAsync(int accountId)
        {
            EntityItemViewModel<AccountViewModel> item = null;
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.FiscalPeriod, acc => acc.Branch);
            if (account != null)
            {
                item = await _decorator.GetDecoratedItemAsync<Account, AccountViewModel>(
                    _mapper.Map<AccountViewModel>(account));
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، حساب با شناسه عددی مشخص شده را به همراه اطلاعات کامل آن
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی به همراه اطلاعات کامل آن</returns>
        public async Task<EntityItemViewModel<AccountFullViewModel>> GetAccountDetailAsync(int accountId)
        {
            EntityItemViewModel<AccountFullViewModel> item = null;
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = GetAccountDetailsQuery(repository, accountId);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                item = await _decorator.GetDecoratedItemAsync<Account, AccountFullViewModel>(
                    _mapper.Map<AccountFullViewModel>(account));
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای حساب را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای حساب</returns>
        public async Task<EntityItemViewModel<AccountViewModel>> GetAccountMetadataAsync()
        {
            return await _decorator.GetDecoratedItemAsync<Account, AccountViewModel>(null);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های مالی را که از حساب مشخص شده استفاده می کندد را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از آرتیکل های مالی که از حساب مشخص شده استفاده می کندد</returns>
        public async Task<EntityListViewModel<VoucherLineViewModel>> GetAccountArticlesAsync(
            int accountId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var query = GetArticleDetailsQuery(
                repository, line => line.Account.Id == accountId, gridOptions);
            var list = await query
                .Select(line => _mapper.Map<VoucherLineViewModel>(line))
                .ToListAsync();
            return await _decorator.GetDecoratedListAsync<VoucherLine, VoucherLineViewModel>(list);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    acc => acc.FiscalPeriod.Id == fpId && acc.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="account">حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی حساب ایجاد یا اصلاح شده</returns>
        public async Task<AccountViewModel> SaveAccountAsync(AccountViewModel account)
        {
            Verify.ArgumentNotNull(account, "account");
            Account accountModel = default(Account);
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            if (account.Id == 0)
            {
                accountModel = _mapper.Map<Account>(account);
                repository.Insert(accountModel);
            }
            else
            {
                accountModel = await repository.GetByIDAsync(account.Id, acc => acc.FiscalPeriod, acc => acc.Branch);
                if (accountModel != null)
                {
                    UpdateExistingAccount(account, accountModel);
                    repository.Update(accountModel);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<AccountViewModel>(accountModel);
        }

        /// <summary>
        /// به روش آسنکرون، حساب مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountId">شناسه عددی حساب مورد نظر برای حذف</param>
        public async Task DeleteAccountAsync(int accountId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId);
            if (account != null)
            {
                account.FiscalPeriod = null;
                account.Branch = null;
                account.Parent = null;
                repository.Delete(account);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد حساب مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="accountViewModel">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر کد حساب تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        /// <remarks>اگر کد حساب در حسابی با شناسه یکتای همین حساب به کار رفته باشد (مثلاً در حالتی که
        /// یک حساب در حالت ویرایش است) در این صورت مقدار "نادرست" را برمی گرداند</remarks>
        public async Task<bool> IsDuplicateAccountAsync(AccountViewModel accountViewModel)
        {
            Verify.ArgumentNotNull(accountViewModel, "accountViewModel");
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(
                    acc => acc.Id != accountViewModel.Id
                        && acc.FiscalPeriod.Id == accountViewModel.FiscalPeriodId
                        && acc.Code == accountViewModel.Code);
            return (accounts.Count > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsUsedAccountAsync(int accountId)
        {
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.Account.Id == accountId);
            return (articles.Count != 0);
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// کلیه حساب هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public IList<AccountViewModel> GetAccounts(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var accounts = repository
                .GetByCriteria(
                    acc => acc.FiscalPeriod.Id == fpId
                        && acc.Branch.Id == branchId,
                    gridOptions,
                    acc => acc.FiscalPeriod, acc => acc.Branch)
                .Select(item => _mapper.Map<AccountViewModel>(item))
                .ToList();
            return accounts;
        }

        /// <summary>
        /// حساب با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی</returns>
        public AccountViewModel GetAccount(int accountId)
        {
            AccountViewModel accountViewModel = null;
            var repository = _unitOfWork.GetRepository<Account>();
            var account = repository.GetByID(accountId, acc => acc.FiscalPeriod, acc => acc.Branch);
            if (account != null)
            {
                accountViewModel = _mapper.Map<AccountViewModel>(account);
            }

            return accountViewModel;
        }

        /// <summary>
        /// حساب با شناسه عددی مشخص شده را به همراه اطلاعات کامل آن
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی به همراه اطلاعات کامل آن</returns>
        public AccountFullViewModel GetAccountDetail(int accountId)
        {
            AccountFullViewModel accountViewModel = null;
            var repository = _unitOfWork.GetRepository<Account>();
            var query = GetAccountDetailsQuery(repository, accountId);
            var account = query.SingleOrDefault();
            if (account != null)
            {
                accountViewModel = _mapper.Map<AccountFullViewModel>(account);
            }

            return accountViewModel;
        }

        /// <summary>
        /// کلیه آرتیکل های مالی را که از حساب مشخص شده استفاده می کندد را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از آرتیکل های مالی که از حساب مشخص شده استفاده می کندد</returns>
        public IList<VoucherLineViewModel> GetAccountArticles(int accountId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<VoucherLine>();
            var query = GetArticleDetailsQuery(
                repository, line => line.Account.Id == accountId, gridOptions);
            return query
                .Select(line => _mapper.Map<VoucherLineViewModel>(line))
                .ToList();
        }

        /// <summary>
        /// تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public int GetCount(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            int count = repository
                .GetCountByCriteria(
                    acc => acc.FiscalPeriod.Id == fpId && acc.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// اطلاعات یک حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="account">حساب مورد نظر برای ایجاد یا اصلاح</param>
        public void SaveAccount(AccountViewModel account)
        {
            Verify.ArgumentNotNull(account, "account");
            var repository = _unitOfWork.GetRepository<Account>();
            if (account.Id == 0)
            {
                var newAccount = _mapper.Map<Account>(account);
                repository.Insert(newAccount);
            }
            else
            {
                var existing = repository.GetByID(account.Id, acc => acc.FiscalPeriod, acc => acc.Branch);
                if (existing != null)
                {
                    UpdateExistingAccount(account, existing);
                    repository.Update(existing);
                }
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// حساب مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountId">شناسه عددی حساب مورد نظر برای حذف</param>
        public void DeleteAccount(int accountId)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var account = repository.GetByID(accountId, acc => acc.FiscalPeriod, acc => acc.Branch);
            if (account != null)
            {
                repository.Delete(account);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// مشخص می کند که آیا کد حساب مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="accountViewModel">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر کد حساب تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        /// <remarks>اگر کد حساب در حسابی با شناسه یکتای همین حساب به کار رفته باشد (مثلاً در حالتی که
        /// یک حساب در حالت ویرایش است) در این صورت مقدار "نادرست" را برمی گرداند</remarks>
        public bool IsDuplicateAccount(AccountViewModel accountViewModel)
        {
            Verify.ArgumentNotNull(accountViewModel, "accountViewModel");
            var repository = _unitOfWork.GetRepository<Account>();
            var account = repository
                .GetByCriteria(acc => acc.Id != accountViewModel.Id
                    && acc.FiscalPeriod.Id == accountViewModel.FiscalPeriodId
                    && acc.Code == accountViewModel.Code)
                .FirstOrDefault();
            return (account != null);
        }

        /// <summary>
        /// مشخص می کند که آیا حساب انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public bool IsUsedAccount(int accountId)
        {
            var repository = _unitOfWork.GetRepository<VoucherLine>();
            var articleCount = repository
                .GetByCriteria(art => art.Account.Id == accountId)
                .Count();
            return (articleCount != 0);
        }

        #endregion

        private static void UpdateExistingAccount(AccountViewModel accountViewModel, Account account)
        {
            account.Code = accountViewModel.Code;
            account.FullCode = accountViewModel.FullCode;
            account.Name = accountViewModel.Name;
            account.Level = accountViewModel.Level;
            account.Description = accountViewModel.Description;
        }

        private IQueryable<Account> GetAccountDetailsQuery(IRepository<Account> repository, int accountId)
        {
            var query = repository
                .GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.Branch)
                    .ThenInclude(br => br.Company)
                .Include(acc => acc.FiscalPeriod);
            return query;
        }

        private IQueryable<VoucherLine> GetArticleDetailsQuery(
            IRepository<VoucherLine> repository, Expression<Func<VoucherLine, bool>> criteria,
            GridOptions gridOptions = null)
        {
            var query = repository
                .GetEntityQuery()
                .Include(art => art.Account)
                .Include(art => art.DetailAccount)
                .Include(art => art.CostCenter)
                .Include(art => art.Project)
                .Include(art => art.Voucher)
                .Include(art => art.FiscalPeriod)
                .Include(art => art.Currency)
                .Include(art => art.Branch)
                    .ThenInclude(br => br.Company)
                .Where(criteria);
            query = (gridOptions != null)
                ? query
                    .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                    .Take(gridOptions.Paging.PageSize)
                : query;
            return query;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataDecorator _decorator;
    }
}

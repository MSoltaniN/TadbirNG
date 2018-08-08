using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات سرفصل های حسابداری را تعریف می کند.
    /// </summary>
    public class AccountRepository : SecureRepository, IAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadataRepository">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public AccountRepository(IUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadataRepository)
            : base(unitOfWork, mapper)
        {
            _metadataRepository = metadataRepository;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<AccountViewModel>> GetAccountsAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            var accounts = await GetAllAsync<Account>(
                userAccess, fpId, branchId, gridOptions, acc => acc.FiscalPeriod, acc => acc.Branch,
                acc => acc.Parent, acc => acc.Children);
            return accounts
                .Select(item => Mapper.Map<AccountViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<KeyValue>> GetAccountsLookupAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            return await GetAllLookupAsync<Account>(userAccess, fpId, branchId, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، حساب با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی</returns>
        public async Task<AccountViewModel> GetAccountAsync(int accountId)
        {
            AccountViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(
                accountId,
                acc => acc.FiscalPeriod, acc => acc.Branch, acc => acc.Parent, acc => acc.Children);
            if (account != null)
            {
                item = Mapper.Map<AccountViewModel>(account);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، حساب با شناسه عددی مشخص شده را به همراه اطلاعات کامل آن
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی به همراه اطلاعات کامل آن</returns>
        public async Task<AccountFullViewModel> GetAccountDetailAsync(int accountId)
        {
            AccountFullViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var query = GetAccountDetailsQuery(repository, accountId);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                item = Mapper.Map<AccountFullViewModel>(account);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری زیرمجموعه یک سرفصل حسابداری مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکی از سرفصل های حسابداری موجود</param>
        /// <returns>مجموعه ای از سرفصل های حسابداری زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetAccountChildrenAsync(int accountId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                children.AddRange(account.Children.Select(acc => Mapper.Map<AccountItemBriefViewModel>(acc)));
            }

            return children;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای حساب را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای حساب</returns>
        public async Task<EntityViewModel> GetAccountMetadataAsync()
        {
            return await _metadataRepository.GetEntityMetadataAsync<Account>();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های مالی را که از حساب مشخص شده استفاده می کندد را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از آرتیکل های مالی که از حساب مشخص شده استفاده می کندد</returns>
        public async Task<IList<VoucherLineViewModel>> GetAccountArticlesAsync(
            int accountId, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var query = GetArticleDetailsQuery(
                repository, line => line.Account.Id == accountId, gridOptions);
            var list = await query
                .Select(line => Mapper.Map<VoucherLineViewModel>(line))
                .ToListAsync();
            return list;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userAccess">
        /// اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها
        /// </param>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(
            UserAccessViewModel userAccess, int fpId, int branchId, GridOptions gridOptions = null)
        {
            return await GetCountAsync<Account>(userAccess, fpId, branchId, gridOptions);
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
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            if (account.Id == 0)
            {
                accountModel = Mapper.Map<Account>(account);
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

            await UnitOfWork.CommitAsync();
            return Mapper.Map<AccountViewModel>(accountModel);
        }

        /// <summary>
        /// به روش آسنکرون، حساب مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountId">شناسه عددی حساب مورد نظر برای حذف</param>
        public async Task DeleteAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId);
            if (account != null)
            {
                account.FiscalPeriod = null;
                account.Branch = null;
                account.Parent = null;
                repository.Delete(account);
                await UnitOfWork.CommitAsync();
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
            var repository = UnitOfWork.GetAsyncRepository<Account>();
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
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var articleCount = await repository
                .GetCountByCriteriaAsync(art => art.Account.Id == accountId);
            return (articleCount > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsRelatedAccountAsync(int accountId)
        {
            var accDetailrepository = UnitOfWork.GetAsyncRepository<AccountDetailAccount>();
            int relatedDetails = await accDetailrepository.GetCountByCriteriaAsync(
                ada => ada.AccountId == accountId, null);
            var accCenterRepository = UnitOfWork.GetAsyncRepository<AccountCostCenter>();
            int relatedCenters = await accCenterRepository.GetCountByCriteriaAsync(
                ac => ac.AccountId == accountId, null);
            var accProjectRepository = UnitOfWork.GetAsyncRepository<AccountProject>();
            int relatedProjects = await accProjectRepository.GetCountByCriteriaAsync(
                ap => ap.AccountId == accountId, null);

            return (relatedDetails > 0 || relatedCenters > 0 || relatedProjects > 0);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده دارای حساب زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده دارای حساب زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int accountId)
        {
            bool? hasChildren = null;
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                hasChildren = account.Children.Count > 0;
            }

            return hasChildren;
        }

        /// <inheritdoc/>
        protected override int ViewId
        {
            // TODO: Remove this hard-coded value later
            get { return 1; }
        }

        private static void UpdateExistingAccount(AccountViewModel accountViewModel, Account account)
        {
            account.Code = accountViewModel.Code;
            account.FullCode = accountViewModel.FullCode;
            account.Name = accountViewModel.Name;
            account.Level = accountViewModel.Level;
            account.Description = accountViewModel.Description;
        }

        private static IQueryable<Account> GetAccountDetailsQuery(IRepository<Account> repository, int accountId)
        {
            var query = repository
                .GetEntityQuery()
                .Where(acc => acc.Id == accountId)
                .Include(acc => acc.Branch)
                    .ThenInclude(br => br.Company)
                .Include(acc => acc.FiscalPeriod);
            return query;
        }

        private static IQueryable<VoucherLine> GetArticleDetailsQuery(
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
                .Where(criteria)
                .Apply(gridOptions);
            return query;
        }

        private IMetadataRepository _metadataRepository;
    }
}

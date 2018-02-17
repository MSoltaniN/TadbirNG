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

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Provides repository operations for managing a financial account in the application database.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class using specified
        /// unit of work implementation and domain mapper.
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> implementation to use for all database operations
        /// in this repository.</param>
        /// <param name="mapper">Domain mapper to use for mapping between entitiy and view model classes</param>
        /// </summary>
        public AccountRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Asynchronous Methods

        /// <summary>
        /// Asynchronously retrieves all accounts in specified fiscal period and branch from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <param name="gridOptions">Options used for displaying data in a tabular grid view</param>
        /// <returns>A collection of <see cref="AccountViewModel"/> objects retrieved from repository</returns>
        public async Task<IList<AccountViewModel>> GetAccountsAsync(
            int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(
                    acc => acc.FiscalPeriod.Id == fpId
                        && acc.Branch.Id == branchId,
                    gridOptions,
                    acc => acc.FiscalPeriod, acc => acc.Branch);
            return accounts
                .Select(item => _mapper.Map<AccountViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// Asynchronously retrieves a single account specified by Id from repository.
        /// </summary>
        /// <param name="accountId">Identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountViewModel"/> object</returns>
        public async Task<AccountViewModel> GetAccountAsync(int accountId)
        {
            AccountViewModel accountViewModel = null;
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.FiscalPeriod, acc => acc.Branch);
            if (account != null)
            {
                accountViewModel = _mapper.Map<AccountViewModel>(account);
            }

            return accountViewModel;
        }

        /// <summary>
        /// Asynchronously retrieves a single financial account with detail information from repository
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountFullViewModel"/> object</returns>
        public async Task<AccountFullViewModel> GetAccountDetailAsync(int accountId)
        {
            AccountFullViewModel accountViewModel = null;
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var query = GetAccountDetailsQuery(repository, accountId);
            var account = await query.SingleOrDefaultAsync();
            if (account != null)
            {
                accountViewModel = _mapper.Map<AccountFullViewModel>(account);
            }

            return accountViewModel;
        }

        /// <summary>
        /// Asynchronously retrieves all transaction lines (articles) that use the financial account specified by
        /// given unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing financial account</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records</param>
        /// <returns>Collection of all transaction lines (articles) for specified account</returns>
        public async Task<IList<TransactionLineViewModel>> GetAccountArticlesAsync(
            int accountId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<TransactionLine>();
            var query = GetArticleDetailsQuery(repository, line => line.FullAccount.Account.Id == accountId);
            return await query
                .Select(line => _mapper.Map<TransactionLineViewModel>(line))
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves the count of all account items in a specified fiscal period and branch
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records</param>
        /// <returns>Count of all account items</returns>
        public async Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var items = await repository
                .GetByCriteriaAsync(
                    acc => acc.FiscalPeriod.Id == fpId && acc.Branch.Id == branchId,
                    gridOptions);
            return items.Count;
        }

        /// <summary>
        /// Asynchronously inserts or updates a single account in repository.
        /// </summary>
        /// <param name="account">Item to insert or update</param>
        public async Task SaveAccountAsync(AccountViewModel account)
        {
            Verify.ArgumentNotNull(account, "account");
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            if (account.Id == 0)
            {
                var newAccount = _mapper.Map<Account>(account);
                repository.Insert(newAccount);
            }
            else
            {
                var existing = await repository.GetByIDAsync(account.Id, acc => acc.FiscalPeriod, acc => acc.Branch);
                if (existing != null)
                {
                    UpdateExistingAccount(account, existing);
                    repository.Update(existing);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Asynchronously deletes an existing financial account from repository.
        /// </summary>
        /// <param name="accountId">Identifier of the account to delete</param>
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
        /// Asynchronously determines if the specified <see cref="AccountViewModel"/> instance uses a code
        /// that is already used in a different account item.
        /// </summary>
        /// <param name="accountViewModel">Account item to check for duplicate code</param>
        /// <returns>True if the Code of specified account item is already used in a different account;
        /// otherwise, returns false.</returns>
        /// <remarks>If the account code is used in the same account (i.e. the account is being edited
        /// without changing its code value), this method will return false.</remarks>
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
        /// Asynchronously determines if the account specified by identifier is referenced by other records.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<bool> IsUsedAccountAsync(int accountId)
        {
            var repository = _unitOfWork.GetAsyncRepository<TransactionLine>();
            var articles = await repository
                .GetByCriteriaAsync(art => art.FullAccount.Account.Id == accountId);
            return (articles.Count != 0);
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// Retrieves all accounts in specified fiscal period and branch from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <param name="options">Options used for displaying data in a tabular grid view</param>
        /// <returns>A collection of <see cref="AccountViewModel"/> objects retrieved from repository</returns>
        public IList<AccountViewModel> GetAccounts(int fpId, int branchId, GridOptions options = null)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var accounts = repository
                .GetByCriteria(
                    acc => acc.FiscalPeriod.Id == fpId
                        && acc.Branch.Id == branchId,
                    options,
                    acc => acc.FiscalPeriod, acc => acc.Branch)
                .Select(item => _mapper.Map<AccountViewModel>(item))
                .ToList();
            return accounts;
        }

        /// <summary>
        /// Retrieves a single account specified by Id from repository.
        /// </summary>
        /// <param name="accountId">Identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountViewModel"/> object</returns>
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
        /// Retrieves a single financial account with detail information from repository
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing account</param>
        /// <returns>The account retrieved from repository as a <see cref="AccountFullViewModel"/> object</returns>
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
        /// Retrieves all transaction lines (articles) that use the financial account specified by given unique identifier.
        /// </summary>
        /// <param name="accountId">Unique identifier of an existing financial account</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records</param>
        /// <returns>Collection of all transaction lines (articles) for specified account</returns>
        public IList<TransactionLineViewModel> GetAccountArticles(int accountId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var articles = repository
                .GetByCriteria(
                    line => line.FullAccount.Id == accountId,
                    gridOptions,
                    line => line.Transaction, line => line.FullAccount, line => line.Currency,
                    line => line.FiscalPeriod, line => line.Branch)
                .Select(line => _mapper.Map<TransactionLineViewModel>(line))
                .ToList();

            return articles;
        }

        /// <summary>
        /// Retrieves the count of all account items in a specified fiscal period and branch
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records</param>
        /// <returns>Count of all account items</returns>
        public int GetCount(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            int count = repository
                .GetByCriteria(
                    acc => acc.FiscalPeriod.Id == fpId && acc.Branch.Id == branchId,
                    gridOptions)
                .Count();
            return count;
        }

        /// <summary>
        /// Inserts or updates a single account in repository.
        /// </summary>
        /// <param name="account">Item to insert or update</param>
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
        /// Deletes an existing financial account from repository.
        /// </summary>
        /// <param name="accountId">Identifier of the account to delete</param>
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
        /// Determines if the specified <see cref="AccountViewModel"/> instance uses a code that is already used
        /// in a different account item.
        /// </summary>
        /// <param name="accountViewModel">Account item to check for duplicate code</param>
        /// <returns>True if the Code of specified account item is already used in a different account;
        /// otherwise, returns false.</returns>
        /// <remarks>If the account code is used in the same account (i.e. the account is being edited
        /// without changing its code value), this method will return false.</remarks>
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
        /// Determines if the account specified by identifier is referenced by other records.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool IsUsedAccount(int accountId)
        {
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var articleCount = repository
                .GetByCriteria(art => art.FullAccount.Account.Id == accountId)
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

        private IQueryable<TransactionLine> GetArticleDetailsQuery(
            IRepository<TransactionLine> repository, Expression<Func<TransactionLine, bool>> criteria)
        {
            var query = repository
                .GetEntityQuery()
                .Include(art => art.FullAccount)
                    .ThenInclude(full => full.Account)
                .Include(art => art.FullAccount)
                    .ThenInclude(full => full.Detail)
                .Include(art => art.FullAccount)
                    .ThenInclude(full => full.Project)
                .Include(art => art.FullAccount)
                    .ThenInclude(full => full.CostCenter)
                .Include(art => art.Transaction)
                .Include(art => art.FiscalPeriod)
                .Include(art => art.Currency)
                .Include(art => art.Branch)
                    .ThenInclude(br => br.Company)
                .Where(criteria);
            return query;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

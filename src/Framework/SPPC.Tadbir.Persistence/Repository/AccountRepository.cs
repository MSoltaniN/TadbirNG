using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.UI;

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
                    acc => acc.FiscalPeriod, acc => acc.Branch)
                .OrderBy(acc => acc.Code)
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
                var existing = repository.GetByID(account.Id);
                if (existing != null)
                {
                    UpdateExistingAccount(account, existing);
                    repository.Update(existing);
                }
            }

            _unitOfWork.Commit();
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
                .GetByCriteria(art => art.Account.Id == accountId)
                .Count();
            return (articleCount != 0);
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
            var account = repository.GetByID(accountId, acc => acc.FiscalPeriod, acc => acc.Branch);
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
        /// <returns>Collection of all transaction lines (articles) for specified account</returns>
        public IList<TransactionLineViewModel> GetAccountArticles(int accountId)
        {
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var articles = repository
                .GetByCriteria(
                    line => line.Account.Id == accountId,
                    line => line.Account, line => line.Branch, line => line.Currency, line => line.FiscalPeriod,
                    line => line.Transaction)
                .Select(line => _mapper.Map<TransactionLineViewModel>(line))
                .ToList();

            return articles;
        }

        /// <summary>
        /// Deletes an existing financial account from repository.
        /// </summary>
        /// <param name="accountId">Identifier of the account to delete</param>
        public void DeleteAccount(int accountId)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var account = repository.GetByID(accountId);
            if (account != null)
            {
                repository.Delete(account);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Retrieves the count of all account items in a specified fiscal period and branch
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>Count of all account items</returns>
        public int GetCount(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            int count = repository
                .GetByCriteria(acc => acc.FiscalPeriod.Id == fpId && acc.Branch.Id == branchId)
                .Count();
            return count;
        }

        private static void UpdateExistingAccount(AccountViewModel accountViewModel, Account account)
        {
            account.Code = accountViewModel.Code;
            account.Name = accountViewModel.Name;
            account.Description = accountViewModel.Description;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

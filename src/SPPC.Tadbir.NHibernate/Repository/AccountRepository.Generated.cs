// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-18 12:59:46 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using BabakSoft.Platform.Common;
using BabakSoft.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Provides operations required for managing a single account and its child items in the underlying database.
    /// </summary>
    public partial class AccountRepository : IAccountRepository
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
        /// Retrieves all accounts in specified fiscal period and corporate branch from database.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>A collection of <see cref="AccountViewModel"/> objects retrieved from database</returns>
        public IList<AccountViewModel> GetAccounts(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var accounts = repository
                .GetByCriteria(acc => acc.FiscalPeriod.Id == fpId
                    && acc.Branch.Id == branchId)
                .OrderBy(acc => acc.Code)
                .Select(item => _mapper.Map<AccountViewModel>(item))
                .ToList();
            return accounts;
        }

        /// <summary>
        /// Retrieves a single account specified by Id from database.
        /// </summary>
        /// <param name="accountId">Identifier of an existing account</param>
        /// <returns>The account retrieved from database as a <see cref="AccountViewModel"/> object</returns>
        public AccountViewModel GetAccount(int accountId)
        {
            AccountViewModel accountViewModel = null;
            var repository = _unitOfWork.GetRepository<Account>();
            var account = repository.GetByID(accountId);
            if (account != null)
            {
                accountViewModel = _mapper.Map<AccountViewModel>(account);
            }

            return accountViewModel;
        }

        /// <summary>
        /// Inserts or updates a single account in database.
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

        static partial void UpdateExistingAccount(AccountViewModel accountViewModel, Account account);

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

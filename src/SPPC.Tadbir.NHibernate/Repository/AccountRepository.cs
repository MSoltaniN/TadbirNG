using System;
using System.Collections.Generic;
using System.Linq;
using BabakSoft.Platform.Common;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.NHibernate
{
    public partial class AccountRepository
    {
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
            var account = repository.GetByID(accountId);
            if (account != null)
            {
                accountViewModel = _mapper.Map<AccountFullViewModel>(account);
            }

            return accountViewModel;
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

        static partial void UpdateExistingAccount(AccountViewModel accountViewModel, Account account)
        {
            account.Code = accountViewModel.Code;
            account.Name = accountViewModel.Name;
            account.Description = accountViewModel.Description;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SwForAll.Platform.Common;
using SwForAll.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Provides operations required for managing a single transaction and its child items in the underlying database.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class using specified
        /// unit of work implementation and domain mapper.
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> implementation to use for all database operations
        /// in this repository.</param>
        /// <param name="mapper">Domain mapper to use for mapping between entitiy and view model classes</param>
        /// </summary>
        public TransactionRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all transactions in specified fiscal period from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <returns>A collection of <see cref="TransactionViewModel"/> objects retrieved from repository</returns>
        public IList<TransactionViewModel> GetTransactions(int fpId)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transactions = repository
                .GetByCriteria(txn => txn.FiscalPeriod.Id == fpId)
                .OrderBy(txn => txn.Date)
                .Select(item => _mapper.Map<TransactionViewModel>(item))
                .ToList();
            return transactions;
        }

        /// <summary>
        /// Retrieves a single financial transaction with detail information from repository
        /// </summary>
        /// <param name="transactionId">Unique identifier of an existing transaction</param>
        /// <returns>The transaction retrieved from repository as a <see cref="TransactionFullViewModel"/> object</returns>
        public TransactionFullViewModel GetTransactionDetail(int transactionId)
        {
            TransactionFullViewModel transactionDetail = null;
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transaction = repository.GetByID(transactionId);
            if (transaction != null)
            {
                transactionDetail = _mapper.Map<TransactionFullViewModel>(transaction);
            }

            return transactionDetail;
        }

        /// <summary>
        /// Inserts or updates a single transaction in repository.
        /// </summary>
        /// <param name="transaction">Item to insert or update</param>
        public void SaveTransaction(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            EnsureHasValidFiscalPeriod(transaction);
            EnsureHasValidUsers(transaction);
            var repository = _unitOfWork.GetRepository<FiscalPeriod>();
            var userRepository = _unitOfWork.GetRepository<User>();
            var fiscalPeriod = repository.GetByID(transaction.FiscalPeriodId);
            var creator = userRepository.GetByID(transaction.CreatorId);
            var modifier = userRepository.GetByID(transaction.LastModifierId);
            EnsureExistingFiscalPeriod(fiscalPeriod);
            EnsureExistingUsers(creator, modifier);

            var existing = fiscalPeriod.Transactions
                .Where(txn => txn.Id == transaction.Id)
                .SingleOrDefault();
            if (existing == null)
            {
                var newTransaction = _mapper.Map<Transaction>(transaction);
                newTransaction.FiscalPeriod = fiscalPeriod;
                newTransaction.Creator = creator;
                newTransaction.LastModifier = modifier;
                fiscalPeriod.Transactions.Add(newTransaction);
                creator.CreatedTransactions.Add(newTransaction);
                modifier.ModifiedTransactions.Add(newTransaction);
                repository.Update(fiscalPeriod);
                userRepository.Update(creator);
                userRepository.Update(modifier);
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Validates the specified transaction to make sure it fulfills all business rules. Current implementation
        /// verifies that transaction date is between start and end dates of the fiscal period in which the transaction
        /// is defined.
        /// </summary>
        /// <param name="transaction">Transaction that needs to be validated</param>
        /// <returns>True if the transaction fulfills all business rules; otherwise, returns false.</returns>
        public bool IsValidTransaction(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            var repository = _unitOfWork.GetRepository<FiscalPeriod>();
            var fiscalPeriod = repository.GetByID(transaction.FiscalPeriodId);
            DateTime transactionDate = DateTime.MinValue;
            JalaliDateTime jalali = null;
            if (JalaliDateTime.TryParse(transaction.Date, out jalali))
            {
                transactionDate = jalali.ToGregorian();
            }

            bool isValid = (fiscalPeriod != null)
                && (transactionDate >= fiscalPeriod.StartDate)
                && (transactionDate <= fiscalPeriod.EndDate);
            return isValid;
        }

        private static void EnsureHasValidFiscalPeriod(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            if (transaction.FiscalPeriodId <= 0)
            {
                throw ExceptionBuilder.NewArgumentException("Target fiscal period is invalid.", "transaction.FiscalPeriodId");
            }
        }

        private static void EnsureExistingFiscalPeriod(FiscalPeriod fiscalPeriod)
        {
            if (fiscalPeriod == null)
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Target fiscal period could not be found.", "transaction.FiscalPeriodId");
            }
        }

        private static void EnsureHasValidUsers(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            if (transaction.CreatorId <= 0 || transaction.LastModifierId <= 0)
            {
                throw ExceptionBuilder.NewArgumentException("Creator and/or last modifier is invalid.");
            }
        }

        private void EnsureExistingUsers(User creator, User modifier)
        {
            if (creator == null || modifier == null)
            {
                throw ExceptionBuilder.NewArgumentException("Creator and/or last modifier could not be found.");
            }
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

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
            var repository = _unitOfWork.GetRepository<Transaction>();
            var existing = repository.GetByID(transaction.Id);
            if (existing == null)
            {
                var newTransaction = _mapper.Map<Transaction>(transaction);
                repository.Insert(newTransaction);
            }
            else
            {
                UpdateExistingTransaction(existing, transaction);
                repository.Update(existing);
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes an existing financial transaction from repository.
        /// </summary>
        /// <param name="transactionId">Identifier of the transaction to delete</param>
        public bool DeleteTransaction(int transactionId)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transaction = repository.GetByID(transactionId);
            if (transaction != null)
            {
                transaction.Lines.Clear();
                repository.Update(transaction);
                repository.Delete(transaction);
                _unitOfWork.Commit();
            }

            return (transaction != null);
        }

        /// <summary>
        /// Retrieves a single financial article from repository.
        /// </summary>
        /// <param name="articleId">Unique identifier of an existing article</param>
        /// <returns>The article retrieved from repository as a <see cref="TransactionLineViewModel"/> object</returns>
        public TransactionLineViewModel GetArticle(int articleId)
        {
            TransactionLineViewModel articleViewModel = null;
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var article = repository.GetByID(articleId);
            if (article != null)
            {
                articleViewModel = _mapper.Map<TransactionLineViewModel>(article);
            }

            return articleViewModel;
        }

        /// <summary>
        /// Retrieves a single financial transaction line (article) with detail information from repository.
        /// </summary>
        /// <param name="articleId">Unique identifier of an existing transaction line (article)</param>
        /// <returns>The transaction line (article) retrieved from repository as a
        /// <see cref="TransactionLineFullViewModel"/> object</returns>
        public TransactionLineFullViewModel GetArticleDetails(int articleId)
        {
            TransactionLineFullViewModel articleDetails = null;
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var article = repository.GetByID(articleId);
            if (article != null)
            {
                articleDetails = _mapper.Map<TransactionLineFullViewModel>(article);
            }

            return articleDetails;
        }

        /// <summary>
        /// Inserts or updates a single transaction line (article) in repository.
        /// </summary>
        /// <param name="article">Article to insert or update</param>
        public void SaveArticle(TransactionLineViewModel article)
        {
            Verify.ArgumentNotNull(article, "article");
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var existing = repository.GetByID(article.Id);
            if (existing == null)
            {
                var newArticle = _mapper.Map<TransactionLine>(article);
                repository.Insert(newArticle);
            }
            else
            {
                UpdateExistingArticle(existing, article);
                repository.Update(existing);
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

        /// <summary>
        /// Deletes an existing financial transaction line (article) from repository.
        /// </summary>
        /// <param name="articleId">Identifier of the article to delete</param>
        public void DeleteArticle(int articleId)
        {
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var article = repository.GetByID(articleId);
            if (article != null)
            {
                repository.Delete(article);
                _unitOfWork.Commit();
            }
        }

        private static void UpdateExistingTransaction(Transaction existing, TransactionViewModel transaction)
        {
            existing.No = transaction.No;
            existing.Date = JalaliDateTime.Parse(transaction.Date).ToGregorian();
            existing.Description = transaction.Description;
        }

        private static void UpdateExistingArticle(TransactionLine existing, TransactionLineViewModel article)
        {
            existing.Account = new Account() { Id = article.AccountId };
            existing.Currency = new Currency() { Id = article.CurrencyId };
            existing.Debit = article.Debit;
            existing.Credit = article.Credit;
            existing.Description = article.Description;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

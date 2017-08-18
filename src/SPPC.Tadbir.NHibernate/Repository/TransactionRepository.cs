using System;
using System.Collections.Generic;
using System.Linq;
using BabakSoft.Platform.Common;
using BabakSoft.Platform.Persistence;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Workflow;

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
        /// Retrieves all transactions in specified fiscal period and corporate branch from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>A collection of <see cref="TransactionViewModel"/> objects retrieved from repository</returns>
        public IList<TransactionViewModel> GetTransactions(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transactions = repository
                .GetByCriteria(txn => txn.FiscalPeriod.Id == fpId
                    && txn.Branch.Id == branchId)
                .OrderBy(txn => txn.Date)
                .Select(txn => _mapper.Map<TransactionViewModel>(txn))
                .Select(txn => AddWorkItemInfo(txn))
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
                var historyRepository = _unitOfWork.GetRepository<WorkItemHistory>();
                var history = historyRepository
                    .GetByCriteria(hist => hist.EntityId == transactionId)
                    .OrderByDescending(hist => hist.Date)
                    .OrderByDescending(hist => hist.Time)
                    .Select(hist => _mapper.Map<HistoryItemViewModel>(hist));
                (transactionDetail.Actions as List<HistoryItemViewModel>).AddRange(history);
                transactionDetail.Transaction = AddWorkItemInfo(transactionDetail.Transaction);
            }

            return transactionDetail;
        }

        /// <summary>
        /// Retrieves summary information for an existing transaction.
        /// </summary>
        /// <param name="transactionId">Unique identifier of an existing transaction</param>
        /// <returns>The transaction summary retrieved from repository as a <see cref="TransactionSummaryViewModel"/> object</returns>
        public TransactionSummaryViewModel GetTransactionSummary(int transactionId)
        {
            TransactionSummaryViewModel summary = default(TransactionSummaryViewModel);
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transaction = repository.GetByID(transactionId);
            if (transaction != null)
            {
                summary = _mapper.Map<TransactionSummaryViewModel>(transaction);
            }

            _unitOfWork.Commit();
            return summary;
        }

        /// <summary>
        /// Retrieves summary information for an existing transaction.
        /// </summary>
        /// <param name="documentId">Unique identifier of the document related to an existing transaction</param>
        /// <returns>The transaction summary retrieved from repository as a <see cref="TransactionSummaryViewModel"/> object</returns>
        public TransactionSummaryViewModel GetTransactionSummaryFromDocument(int documentId)
        {
            TransactionSummaryViewModel summary = default(TransactionSummaryViewModel);
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transaction = repository
                .GetByCriteria(txn => txn.Document.Id == documentId)
                .FirstOrDefault();
            if (transaction != null)
            {
                summary = _mapper.Map<TransactionSummaryViewModel>(transaction);
            }

            _unitOfWork.Commit();
            return summary;
        }

        /// <summary>
        /// Inserts or updates a single transaction in repository.
        /// </summary>
        /// <param name="transaction">Item to insert or update</param>
        public void SaveTransaction(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            var repository = _unitOfWork.GetRepository<Transaction>();
            if (transaction.Id == 0)
            {
                var newTransaction = _mapper.Map<Transaction>(transaction);
                UpdateAction(newTransaction);
                repository.Insert(newTransaction);
            }
            else
            {
                var existing = repository.GetByID(transaction.Id);
                if (existing != null)
                {
                    UpdateExistingTransaction(existing, transaction);
                    UpdateAction(existing);
                    repository.Update(existing);
                }
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
            var documentRepository = _unitOfWork.GetRepository<Document>();
            var transaction = repository.GetByID(transactionId);
            if (transaction != null)
            {
                transaction.Document.Actions.Clear();
                documentRepository.Update(transaction.Document);
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
            if (article.Id == 0)
            {
                var newArticle = _mapper.Map<TransactionLine>(article);
                repository.Insert(newArticle);
            }
            else
            {
                var existing = repository.GetByID(article.Id);
                if (existing != null)
                {
                    UpdateExistingArticle(existing, article);
                    repository.Update(existing);
                }
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
            var mainAction = existing.Document.Actions.First();
            mainAction.ModifiedBy = new User() { Id = transaction.Document.Actions.First().ModifiedById };
        }

        private static void UpdateExistingArticle(TransactionLine existing, TransactionLineViewModel article)
        {
            existing.Account = new Account() { Id = article.AccountId };
            existing.Currency = new Currency() { Id = article.CurrencyId };
            existing.Debit = article.Debit;
            existing.Credit = article.Credit;
            existing.Description = article.Description;
        }

        private TransactionViewModel AddWorkItemInfo(TransactionViewModel transaction)
        {
            var repository = _unitOfWork.GetRepository<WorkItemDocument>();
            var document = repository
                .GetByCriteria(wid => wid.Document.Id == transaction.Document.Id
                    && wid.DocumentType == DocumentTypeName.Transaction)
                .FirstOrDefault();
            if (document != null)
            {
                transaction.WorkItemId = document.WorkItem.Id;
                transaction.WorkItemTargetId = document.WorkItem.Target.Id;
                transaction.WorkItemAction = document.WorkItem.Action;
            }

            return transaction;
        }

        private void UpdateAction(Transaction transaction)
        {
            if (transaction.Id == 0)
            {
                var mainAction = transaction.Document.Actions.First();
                mainAction.Document = transaction.Document;
                mainAction.CreatedDate = DateTime.Now;
                mainAction.ModifiedDate = DateTime.Now;
            }
            else
            {
                var mainAction = transaction.Document.Actions.First();
                mainAction.ModifiedDate = DateTime.Now;
            }
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

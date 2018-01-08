using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.Persistence
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
            var query = GetTransactionQuery(
                repository, txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId);
            var transactions = query
                .Select(txn => _mapper.Map<TransactionViewModel>(txn))
                .ToList();
            return transactions
                .Select(txn => AddWorkItemInfo(txn))
                .ToList();
        }

        /// <summary>
        /// Asynchronously retrieves all transactions in specified fiscal period and branch from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>A collection of <see cref="TransactionViewModel"/> objects retrieved from repository</returns>
        public async Task<IList<TransactionViewModel>> GetTransactionsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Transaction>();
            var query = GetTransactionQuery(
                repository, txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId);
            var transactions = await query
                .Select(txn => _mapper.Map<TransactionViewModel>(txn))
                .ToListAsync();
            foreach (var transaction in transactions)
            {
                await AddWorkItemInfoAsync(transaction);
            }

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
            var query = GetTransactionWithLinesQuery(repository, txn => txn.Id == transactionId);
            var transaction = query.SingleOrDefault();
            if (transaction != null)
            {
                transactionDetail = _mapper.Map<TransactionFullViewModel>(transaction);
                var historyRepository = _unitOfWork.GetRepository<WorkItemHistory>();
                var historyQuery = GetHistoryQuery(historyRepository, hist => hist.EntityId == transactionId);
                var history = historyQuery
                    .Select(hist => _mapper.Map<HistoryItemViewModel>(hist))
                    .ToList();
                (transactionDetail.Actions as List<HistoryItemViewModel>).AddRange(history);
                transactionDetail.Transaction = AddWorkItemInfo(transactionDetail.Transaction);
            }

            return transactionDetail;
        }

        /// <summary>
        /// Asynchronously retrieves a single financial transaction with detail information from repository.
        /// </summary>
        /// <param name="transactionId">Unique identifier of an existing transaction</param>
        /// <returns>The transaction retrieved from repository as a <see cref="TransactionFullViewModel"/> object</returns>
        public async Task<TransactionFullViewModel> GetTransactionDetailAsync(int transactionId)
        {
            TransactionFullViewModel transactionDetail = null;
            var repository = _unitOfWork.GetAsyncRepository<Transaction>();
            var query = GetTransactionWithLinesQuery(repository, txn => txn.Id == transactionId);
            var transaction = await query.SingleOrDefaultAsync();
            if (transaction != null)
            {
                transactionDetail = _mapper.Map<TransactionFullViewModel>(transaction);
                var historyRepository = _unitOfWork.GetRepository<WorkItemHistory>();
                var historyQuery = GetHistoryQuery(historyRepository, hist => hist.EntityId == transactionId);
                var history = await historyQuery
                    .Select(hist => _mapper.Map<HistoryItemViewModel>(hist))
                    .ToListAsync();
                (transactionDetail.Actions as List<HistoryItemViewModel>).AddRange(history);
                await AddWorkItemInfoAsync(transactionDetail.Transaction);
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
                var query = GetTransactionQuery(repository, txn => txn.Id == transaction.Id);
                var existing = query.SingleOrDefault();
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
        /// Asynchronously inserts or updates a single transaction in repository.
        /// </summary>
        /// <param name="transaction">Item to insert or update</param>
        public async Task SaveTransactionAsync(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            var repository = _unitOfWork.GetAsyncRepository<Transaction>();
            if (transaction.Id == 0)
            {
                var newTransaction = _mapper.Map<Transaction>(transaction);
                UpdateAction(newTransaction);
                repository.Insert(newTransaction);
            }
            else
            {
                var existing = await repository.GetByIDAsync(transaction.Id);
                if (existing != null)
                {
                    UpdateExistingTransaction(existing, transaction);
                    UpdateAction(existing);
                    repository.Update(existing);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Deletes an existing financial transaction from repository.
        /// </summary>
        /// <param name="transactionId">Identifier of the transaction to delete</param>
        public bool DeleteTransaction(int transactionId)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var documentRepository = _unitOfWork.GetRepository<Document>();
            var query = GetTransactionWithLinesQuery(repository, txn => txn.Id == transactionId);
            var transaction = query.SingleOrDefault();
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
        /// Asynchronously deletes an existing financial transaction from repository.
        /// </summary>
        /// <param name="transactionId">Identifier of the transaction to delete</param>
        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Transaction>();
            var documentRepository = _unitOfWork.GetAsyncRepository<Document>();
            var query = GetTransactionWithLinesQuery(repository, txn => txn.Id == transactionId);
            var transaction = await query.SingleOrDefaultAsync();
            if (transaction != null)
            {
                transaction.Document.Actions.Clear();
                documentRepository.Update(transaction.Document);
                transaction.Lines.Clear();
                repository.Update(transaction);
                repository.Delete(transaction);
                await _unitOfWork.CommitAsync();
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
            var article = repository.GetByID(
                articleId,
                art => art.Transaction, art => art.Account, art => art.Currency,
                art => art.Branch, art => art.FiscalPeriod);
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
            var query = GetArticleDetailsQuery(repository, art => art.Id == articleId);
            var article = query.SingleOrDefault();
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

        private static void UpdateExistingArticle(TransactionLine existing, TransactionLineViewModel article)
        {
            existing.Account = new Account() { Id = article.AccountId ?? 0 };
            existing.Currency = new Currency() { Id = article.CurrencyId ?? 0 };
            existing.Debit = article.Debit;
            existing.Credit = article.Credit;
            existing.Description = article.Description;
        }

        private static void UpdateAction(Transaction transaction)
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

        private void UpdateExistingTransaction(Transaction existing, TransactionViewModel transaction)
        {
            var userRepository = _unitOfWork.GetRepository<User>();
            existing.No = transaction.No;
            existing.Date = JalaliDateTime.Parse(transaction.Date).ToGregorian();
            existing.Description = transaction.Description;
            var mainAction = existing.Document.Actions.First();
            mainAction.ModifiedBy = userRepository.GetByID(transaction.Document.Actions.First().ModifiedById);
        }

        private IQueryable<Transaction> GetTransactionQuery(
            IRepository<Transaction> repository, Expression<Func<Transaction, bool>> criteria)
        {
            var transactionsQuery = repository
                .GetAllAsQuery()
                .Include(txn => txn.Lines)
                .Include(txn => txn.FiscalPeriod)
                .Include(txn => txn.Branch)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Type)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Status)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.CreatedBy)
                            .ThenInclude(usr => usr.Person)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ModifiedBy)
                            .ThenInclude(usr => usr.Person)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ConfirmedBy)
                            .ThenInclude(usr => usr.Person)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ApprovedBy)
                            .ThenInclude(usr => usr.Person)
                .Where(criteria);
            return transactionsQuery;
        }

        private IQueryable<Transaction> GetTransactionWithLinesQuery(
            IRepository<Transaction> repository, Expression<Func<Transaction, bool>> criteria)
        {
            var transactionsQuery = repository
                .GetAllAsQuery()
                .Include(txn => txn.Lines)
                    .ThenInclude(line => line.Account)
                .Include(txn => txn.Lines)
                    .ThenInclude(line => line.Currency)
                .Include(txn => txn.Lines)
                    .ThenInclude(line => line.FiscalPeriod)
                .Include(txn => txn.Lines)
                    .ThenInclude(line => line.Branch)
                        .ThenInclude(br => br.Company)
                .Include(txn => txn.FiscalPeriod)
                .Include(txn => txn.Branch)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Type)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Status)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.CreatedBy)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ModifiedBy)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ConfirmedBy)
                .Include(txn => txn.Document)
                    .ThenInclude(doc => doc.Actions)
                        .ThenInclude(act => act.ApprovedBy)
                .Where(criteria);
            return transactionsQuery;
        }

        private IQueryable<WorkItemHistory> GetHistoryQuery(
            IRepository<WorkItemHistory> repository, Expression<Func<WorkItemHistory, bool>> criteria)
        {
            var query = repository
                .GetAllAsQuery()
                .Include(hist => hist.User)
                .Include(hist => hist.Role)
                .Where(criteria)
                .OrderByDescending(hist => hist.Date)
                .OrderByDescending(hist => hist.Time);
            return query;
        }

        private IQueryable<TransactionLine> GetArticleDetailsQuery(
            IRepository<TransactionLine> repository, Expression<Func<TransactionLine, bool>> criteria)
        {
            var query = repository
                .GetAllAsQuery()
                .Include(art => art.Account)
                .Include(art => art.Transaction)
                .Include(art => art.FiscalPeriod)
                .Include(art => art.Currency)
                .Include(art => art.Branch)
                    .ThenInclude(br => br.Company)
                .Where(criteria);
            return query;
        }

        private TransactionViewModel AddWorkItemInfo(TransactionViewModel transaction)
        {
            var repository = _unitOfWork.GetRepository<WorkItemDocument>();
            var document = repository
                .GetByCriteria(wid => wid.Document.Id == transaction.Document.Id
                    && wid.DocumentType == DocumentTypeName.Transaction,
                    wid => wid.Document, wid => wid.WorkItem)
                .FirstOrDefault();
            if (document != null)
            {
                transaction.WorkItemId = document.WorkItem.Id;
                transaction.WorkItemTargetId = document.WorkItem.Target.Id;
                transaction.WorkItemAction = document.WorkItem.Action;
            }

            return transaction;
        }

        private async Task<TransactionViewModel> AddWorkItemInfoAsync(TransactionViewModel transaction)
        {
            var repository = _unitOfWork.GetAsyncRepository<WorkItemDocument>();
            var documents = await repository
                .GetByCriteriaAsync(wid => wid.Document.Id == transaction.Document.Id
                    && wid.DocumentType == DocumentTypeName.Transaction,
                    wid => wid.Document, wid => wid.WorkItem);
            var document = documents.FirstOrDefault();
            if (document != null)
            {
                transaction.WorkItemId = document.WorkItem.Id;
                transaction.WorkItemTargetId = document.WorkItem.Target.Id;
                transaction.WorkItemAction = document.WorkItem.Action;
            }

            return transaction;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

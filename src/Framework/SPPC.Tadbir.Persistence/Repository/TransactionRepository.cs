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
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را پیاده سازی می کند.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public TransactionRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Transaction Operations

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<IList<TransactionViewModel>> GetTransactionsAsync(
            int fpId, int branchId, GridOptions gridOptions = null)
        {
            var query = GetTransactionQuery(
                txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId, gridOptions);
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
        /// به روش آسنکرون، سند مالی با شناسه دیتابیسی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه دیتابیسی</returns>
        public async Task<TransactionViewModel> GetTransactionAsync(int transactionId)
        {
            TransactionViewModel transactionViewModel = null;
            var query = GetTransactionQuery(txn => txn.Id == transactionId);
            var transaction = await query.SingleOrDefaultAsync();
            if (transaction != null)
            {
                transactionViewModel = _mapper.Map<TransactionViewModel>(transaction);
                AddWorkItemInfo(transactionViewModel);
            }

            return transactionViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public async Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Transaction>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سند مالی را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="transaction">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        public async Task<TransactionViewModel> SaveTransactionAsync(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            Transaction transactionModel = default(Transaction);
            var repository = _unitOfWork.GetAsyncRepository<Transaction>();
            if (transaction.Id == 0)
            {
                transactionModel = _mapper.Map<Transaction>(transaction);
                UpdateAction(transactionModel);
                repository.Insert(transactionModel, txn => txn.Document, txn => txn.Document.Actions);
            }
            else
            {
                transactionModel = await repository
                    .GetEntityQuery()
                    .Where(txn => txn.Id == transaction.Id)
                    .Include(txn => txn.FiscalPeriod)
                    .Include(txn => txn.Branch)
                    .Include(txn => txn.Document)
                        .ThenInclude(doc => doc.Actions)
                    .SingleOrDefaultAsync();
                if (transactionModel != null)
                {
                    UpdateExistingTransaction(transactionModel, transaction);
                    UpdateAction(transactionModel);
                    repository.Update(transactionModel, txn => txn.Document, txn => txn.Document.Actions);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<TransactionViewModel>(transactionModel);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی برای حذف</param>
        public async Task DeleteTransactionAsync(int transactionId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Transaction>();
            var transaction = await repository.GetByIDWithTrackingAsync(
                transactionId, txn => txn.Lines, txn => txn.Document);
            if (transaction != null)
            {
                var documentRepository = _unitOfWork.GetAsyncRepository<Document>();
                var document = await documentRepository.GetByIDWithTrackingAsync(
                    transaction.Document.Id, doc => doc.Actions);
                transaction.Lines.Clear();
                document.Actions.Clear();
                repository.Delete(transaction);
                documentRepository.Delete(document);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی داده شده را برای مطابقت با کلیه قواعد کاری برنامه اعتبارسنجی می کند
        /// </summary>
        /// <param name="transaction">سند مالی که باید اعتبارسنجی شود</param>
        /// <returns>مقدار بولی درست در صورت مطابقت کامل با قواعد کاری، در غیر این صورت مقدار بولی نادرست</returns>
        public async Task<bool> IsValidTransactionAsync(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriod = await repository.GetByIDAsync(transaction.FiscalPeriodId);
            bool isValid = (fiscalPeriod != null)
                && (transaction.Date >= fiscalPeriod.StartDate)
                && (transaction.Date <= fiscalPeriod.EndDate);
            return isValid;
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public IList<TransactionViewModel> GetTransactions(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var query = GetTransactionQuery(
                txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId, gridOptions);
            var transactions = query
                .Select(txn => _mapper.Map<TransactionViewModel>(txn))
                .ToList();
            return transactions
                .Select(txn => AddWorkItemInfo(txn))
                .ToList();
        }

        /// <summary>
        /// سند مالی با شناسه دیتابیسی مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه دیتابیسی</returns>
        public TransactionViewModel GetTransaction(int transactionId)
        {
            TransactionViewModel transactionViewModel = null;
            var query = GetTransactionQuery(txn => txn.Id == transactionId);
            var transaction = query.SingleOrDefault();
            if (transaction != null)
            {
                transactionViewModel = _mapper.Map<TransactionViewModel>(transaction);
                AddWorkItemInfo(transactionViewModel);
            }

            return transactionViewModel;
        }

        /// <summary>
        /// تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        public int GetCount(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var count = repository
                .GetCountByCriteria(
                    txn => txn.FiscalPeriod.Id == fpId && txn.Branch.Id == branchId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// اطلاعات یک سند مالی را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="transaction">سند مالی برای ایجاد یا اصلاح</param>
        public void SaveTransaction(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            var repository = _unitOfWork.GetRepository<Transaction>();
            if (transaction.Id == 0)
            {
                var newTransaction = _mapper.Map<Transaction>(transaction);
                UpdateAction(newTransaction);
                repository.Insert(newTransaction, txn => txn.Document, txn => txn.Document.Actions);
            }
            else
            {
                var existing = repository
                    .GetEntityQuery()
                    .Where(txn => txn.Id == transaction.Id)
                    .Include(txn => txn.Document)
                        .ThenInclude(doc => doc.Actions)
                    .SingleOrDefault();
                if (existing != null)
                {
                    UpdateExistingTransaction(existing, transaction);
                    UpdateAction(existing);
                    repository.Update(existing, txn => txn.Document, txn => txn.Document.Actions);
                }
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// سند مالی مشخص شده با شناسه دیتابیسی را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی سند مالی برای حذف</param>
        public void DeleteTransaction(int transactionId)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transaction = repository.GetByIDWithTracking(
                transactionId, txn => txn.Lines, txn => txn.Document);
            if (transaction != null)
            {
                var documentRepository = _unitOfWork.GetRepository<Document>();
                var document = documentRepository.GetByIDWithTracking(
                    transaction.Document.Id, doc => doc.Actions);
                transaction.Lines.Clear();
                document.Actions.Clear();
                repository.Delete(transaction);
                documentRepository.Delete(document);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// اطلاعات سند مالی داده شده را برای مطابقت با کلیه قواعد کاری برنامه اعتبارسنجی می کند
        /// </summary>
        /// <param name="transaction">سند مالی که باید اعتبارسنجی شود</param>
        /// <returns>مقدار بولی درست در صورت مطابقت کامل با قواعد کاری، در غیر این صورت مقدار بولی نادرست</returns>
        public bool IsValidTransaction(TransactionViewModel transaction)
        {
            Verify.ArgumentNotNull(transaction, "transaction");
            var repository = _unitOfWork.GetRepository<FiscalPeriod>();
            var fiscalPeriod = repository.GetByID(transaction.FiscalPeriodId);
            bool isValid = (fiscalPeriod != null)
                && (transaction.Date >= fiscalPeriod.StartDate)
                && (transaction.Date <= fiscalPeriod.EndDate);
            return isValid;
        }

        /// <summary>
        /// اطلاعات خلاصه سند مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه دیتابیسی یکی از اسناد مالی موجود</param>
        /// <returns>اطلاعات خلاصه سند مالی مشخص شده با شناسه دیتابیسی</returns>
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
        /// اطلاعات خلاصه سند مشخص شده با شناسه دیتابیسی مستند اداری مرتبط را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی یکی از مستندهای اداری موجود</param>
        /// <returns>اطلاعات خلاصه سند مالی مشخص شده با شناسه دیتابیسی مستند اداری مرتبط</returns>
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

        #endregion

        #endregion

        #region Transaction Line Operations

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        public async Task<IList<TransactionLineViewModel>> GetArticlesAsync(
            int transactionId, GridOptions gridOptions = null)
        {
            var query = GetTransactionLinesQuery(transactionId, gridOptions);
            var lines = await query
                .Select(line => _mapper.Map<TransactionLineViewModel>(line))
                .ToListAsync();
            return lines;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه دیتابیسی</returns>
        public async Task<TransactionLineViewModel> GetArticleAsync(int articleId)
        {
            TransactionLineViewModel articleViewModel = null;
            var repository = _unitOfWork.GetAsyncRepository<TransactionLine>();
            var query = GetArticleDetailsQuery(repository, art => art.Id == articleId);
            var article = await query.SingleOrDefaultAsync();
            if (article != null)
            {
                articleViewModel = _mapper.Map<TransactionLineViewModel>(article);
            }

            return articleViewModel;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات کامل آرتیکل مشخص شده با شناسه دیتابیسی</returns>
        public async Task<TransactionLineFullViewModel> GetArticleDetailsAsync(int articleId)
        {
            TransactionLineFullViewModel articleDetails = null;
            var repository = _unitOfWork.GetAsyncRepository<TransactionLine>();
            var query = GetArticleDetailsQuery(repository, art => art.Id == articleId);
            var article = await query.SingleOrDefaultAsync();
            if (article != null)
            {
                articleDetails = _mapper.Map<TransactionLineFullViewModel>(article);
            }

            return articleDetails;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        public async Task<int> GetArticleCountAsync(int transactionId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<TransactionLine>();
            var count = await repository.GetCountByCriteriaAsync(
                line => line.Transaction.Id == transactionId, gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سطر سند مالی (آرتیکل) را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
        public async Task<TransactionLineViewModel> SaveArticleAsync(TransactionLineViewModel article)
        {
            Verify.ArgumentNotNull(article, "article");
            TransactionLine lineModel = default(TransactionLine);
            var repository = _unitOfWork.GetAsyncRepository<TransactionLine>();
            if (article.Id == 0)
            {
                lineModel = _mapper.Map<TransactionLine>(article);
                repository.Insert(lineModel);
            }
            else
            {
                lineModel = await repository.GetByIDAsync(article.Id);
                if (lineModel != null)
                {
                    UpdateExistingArticle(lineModel, article);
                    repository.Update(lineModel);
                }
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<TransactionLineViewModel>(lineModel);
        }

        /// <summary>
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل برای حذف</param>
        public async Task DeleteArticleAsync(int articleId)
        {
            var repository = _unitOfWork.GetAsyncRepository<TransactionLine>();
            var article = await repository.GetByIDAsync(articleId);
            if (article != null)
            {
                article.Account = null;
                article.DetailAccount = null;
                article.CostCenter = null;
                article.Project = null;
                article.Branch = null;
                article.Currency = null;
                article.FiscalPeriod = null;
                article.Transaction = null;
                repository.Delete(article);
                await _unitOfWork.CommitAsync();
            }
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// آرتیکل های یک سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سند مشخص شده با شناسه عددی</returns>
        public IList<TransactionLineViewModel> GetArticles(int transactionId, GridOptions gridOptions = null)
        {
            var query = GetTransactionLinesQuery(transactionId, gridOptions);
            var lines = query
                .Select(line => _mapper.Map<TransactionLineViewModel>(line))
                .ToList();
            return lines;
        }

        /// <summary>
        /// اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه دیتابیسی</returns>
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
        /// اطلاعات کامل سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل موجود</param>
        /// <returns>اطلاعات کامل آرتیکل مشخص شده با شناسه دیتابیسی</returns>
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
        /// تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        public int GetArticleCount(int transactionId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var count = repository.GetCountByCriteria(line => line.Transaction.Id == transactionId, gridOptions);
            return count;
        }

        /// <summary>
        /// اطلاعات یک سطر سند مالی (آرتیکل) را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
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
        /// سطر سند مالی (آرتیکل) مشخص شده با شناسه دیتابیسی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل برای حذف</param>
        public void DeleteArticle(int articleId)
        {
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var article = repository.GetByID(articleId);
            if (article != null)
            {
                article.Account = null;
                article.DetailAccount = null;
                article.CostCenter = null;
                article.Project = null;
                article.Branch = null;
                article.Currency = null;
                article.FiscalPeriod = null;
                article.Transaction = null;
                repository.Delete(article);
                _unitOfWork.Commit();
            }
        }

        #endregion

        #endregion

        private static void UpdateExistingArticle(TransactionLine existing, TransactionLineViewModel article)
        {
            existing.AccountId = article.AccountId ?? 0;
            existing.DetailId = article.DetailId;
            existing.CostCenterId = article.CostCenterId;
            existing.ProjectId = article.ProjectId;
            existing.CurrencyId = article.CurrencyId ?? 0;
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
            existing.Date = transaction.Date;
            existing.Description = transaction.Description;
            existing.Document.EntityNo = transaction.No;
            var mainAction = existing.Document.Actions.First();
            mainAction.ModifiedBy = userRepository.GetByID(transaction.Document.Actions.First().ModifiedById);
        }

        private IQueryable<Transaction> GetTransactionQuery(
            Expression<Func<Transaction, bool>> criteria, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transactionsQuery = repository
                .GetEntityQuery(gridOptions)
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
            transactionsQuery = (gridOptions != null)
                ? transactionsQuery
                    .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                    .Take(gridOptions.Paging.PageSize)
                : transactionsQuery;
            return transactionsQuery;
        }

        private IQueryable<TransactionLine> GetTransactionLinesQuery(int transactionId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetRepository<TransactionLine>();
            var linesQuery = repository
                .GetEntityQuery(gridOptions)
                .Include(line => line.Transaction)
                .Include(line => line.Account)
                .Include(line => line.DetailAccount)
                .Include(line => line.CostCenter)
                .Include(line => line.Project)
                .Include(line => line.Currency)
                .Include(line => line.FiscalPeriod)
                .Include(line => line.Branch)
                .Where(line => line.Transaction.Id == transactionId);
            linesQuery = (gridOptions != null)
                ? linesQuery
                    .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                    .Take(gridOptions.Paging.PageSize)
                : linesQuery;
            return linesQuery;
        }

        private IQueryable<WorkItemHistory> GetHistoryQuery(
            IRepository<WorkItemHistory> repository, Expression<Func<WorkItemHistory, bool>> criteria)
        {
            var query = repository
                .GetEntityQuery()
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
                .GetEntityQuery()
                .Include(art => art.Account)
                .Include(art => art.DetailAccount)
                .Include(art => art.CostCenter)
                .Include(art => art.Project)
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

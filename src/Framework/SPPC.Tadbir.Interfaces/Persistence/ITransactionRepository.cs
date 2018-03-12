using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات اسناد مالی و آرتیکل های آنها را تعریف می کند.
    /// </summary>
    public interface ITransactionRepository
    {
        #region Transaction Operations

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<IList<TransactionViewModel>> GetTransactionsAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، سند مالی با شناسه عددی مشخص شده را به همراه اطلاعات کامل آن از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه عددی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه عددی به همراه اطلاعات کامل آن</returns>
        Task<TransactionFullViewModel> GetTransactionDetailAsync(int transactionId);

        /// <summary>
        /// به روش آسنکرون، تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سند مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="transaction">سند مالی برای ایجاد یا اصلاح</param>
        /// <returns>مدل نمایشی سند ایجاد یا اصلاح شده</returns>
        Task<TransactionViewModel> SaveTransactionAsync(TransactionViewModel transaction);

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="transactionId">شناسه عددی سند مالی برای حذف</param>
        Task DeleteTransactionAsync(int transactionId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی داده شده را برای مطابقت با کلیه قواعد کاری برنامه اعتبارسنجی می کند
        /// </summary>
        /// <param name="transaction">سند مالی که باید اعتبارسنجی شود</param>
        /// <returns>مقدار بولی درست در صورت مطابقت کامل با قواعد کاری، در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsValidTransactionAsync(TransactionViewModel transaction);

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// کلیه اسناد مالی را که در دوره مالی و شعبه مشخص شده تعریف شده اند، از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        IList<TransactionViewModel> GetTransactions(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// سند مالی با شناسه عددی مشخص شده را به همراه اطلاعات کامل آن از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه عددی یکی از اسناد مالی موجود</param>
        /// <returns>سند مالی مشخص شده با شناسه عددی به همراه اطلاعات کامل آن</returns>
        TransactionFullViewModel GetTransactionDetail(int transactionId);

        /// <summary>
        /// اطلاعات یک سند مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="transaction">سند مالی برای ایجاد یا اصلاح</param>
        void SaveTransaction(TransactionViewModel transaction);

        /// <summary>
        /// سند مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="transactionId">شناسه عددی سند مالی برای حذف</param>
        void DeleteTransaction(int transactionId);

        /// <summary>
        /// اطلاعات سند مالی داده شده را برای مطابقت با کلیه قواعد کاری برنامه اعتبارسنجی می کند
        /// </summary>
        /// <param name="transaction">سند مالی که باید اعتبارسنجی شود</param>
        /// <returns>مقدار بولی درست در صورت مطابقت کامل با قواعد کاری، در غیر این صورت مقدار بولی نادرست</returns>
        bool IsValidTransaction(TransactionViewModel transaction);

        /// <summary>
        /// اطلاعات خلاصه سند مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="transactionId">شناسه عددی یکی از اسناد مالی موجود</param>
        /// <returns>اطلاعات خلاصه سند مالی مشخص شده با شناسه عددی</returns>
        TransactionSummaryViewModel GetTransactionSummary(int transactionId);

        /// <summary>
        /// اطلاعات خلاصه سند مشخص شده با شناسه عددی مستند اداری مرتبط را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="documentId">شناسه عددی یکی از مستندهای اداری موجود</param>
        /// <returns>اطلاعات خلاصه سند مالی مشخص شده با شناسه عددی مستند اداری مرتبط</returns>
        TransactionSummaryViewModel GetTransactionSummaryFromDocument(int documentId);

        #endregion

        #endregion

        #region Transaction Line Operations

        #region Asynchronous Methods

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه عددی</returns>
        Task<TransactionLineViewModel> GetArticleAsync(int articleId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات کامل آرتیکل مشخص شده با شناسه عددی</returns>
        Task<TransactionLineFullViewModel> GetArticleDetailsAsync(int articleId);

        /// <summary>
        /// Retrieves the count of all transaction line items in a specified financial transaction
        /// </summary>
        /// <param name="transactionId">Identifier of an existing transaction</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records</param>
        /// <returns>Count of all transaction line items</returns>
        Task<int> GetArticleCountAsync(int transactionId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سطر سند مالی (آرتیکل) را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
        Task<TransactionLineViewModel> SaveArticleAsync(TransactionLineViewModel article);

        /// <summary>
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل برای حذف</param>
        Task DeleteArticleAsync(int articleId);

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه عددی</returns>
        TransactionLineViewModel GetArticle(int articleId);

        /// <summary>
        /// اطلاعات کامل سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات کامل آرتیکل مشخص شده با شناسه عددی</returns>
        TransactionLineFullViewModel GetArticleDetails(int articleId);

        /// <summary>
        /// اطلاعات یک سطر سند مالی (آرتیکل) را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
        void SaveArticle(TransactionLineViewModel article);

        /// <summary>
        /// سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل برای حذف</param>
        void DeleteArticle(int articleId);

        #endregion

        #endregion
    }
}

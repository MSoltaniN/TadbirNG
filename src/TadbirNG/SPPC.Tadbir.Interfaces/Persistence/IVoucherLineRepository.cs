using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات آرتیکل های مالی را تعریف می کند.
    /// </summary>
    public interface IVoucherLineRepository
    {
        /// <summary>
        /// به روش آسنکرون، آرتیکل های یک سند مشخص شده با شناسه عددی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آرتیکل های سندمشخص شده با شناسه عددی</returns>
        Task<PagedList<VoucherLineViewModel>> GetArticlesAsync(int voucherId, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل موجود</param>
        /// <returns>اطلاعات آرتیکل مشخص شده با شناسه عددی</returns>
        Task<VoucherLineViewModel> GetArticleAsync(int articleId);

        /// <summary>
        /// به روش آسنکرون، تعداد آرتیکل های یک سند مالی را بعد از اعمال فیلتر (در صورت وجود)
        /// خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="voucherId">شناسه یکی از اسناد مالی موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد آرتیکل های سند مالی بعد از اعمال فیلتر</returns>
        Task<int> GetArticleCountAsync<TViewModel>(int voucherId, GridOptions gridOptions = null)
            where TViewModel : class, new();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک سطر سند مالی (آرتیکل) را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="article">آرتیکل برای ایجاد یا اصلاح</param>
        Task<VoucherLineViewModel> SaveArticleAsync(VoucherLineViewModel article);

        /// <summary>
        /// به روش آسنکرون، علامتگذاری مشخص شده را روی آرتیکل سند اعمال می کند
        /// </summary>
        /// <param name="mark">اطلاعات علامتکذاری آرتیکل</param>
        Task SaveArticleMarkAsync(VoucherLineMarkViewModel mark);

        /// <summary>
        /// به روش آسنکرون، سطر سند مالی (آرتیکل) مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه عددی آرتیکل برای حذف</param>
        Task DeleteArticleAsync(int articleId);

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteArticlesAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، تعداد کل آرتیکل های ثبت شده را برمیگرداند
        /// </summary>
        /// <returns>تعداد کل آرتیکل ها</returns>
        Task<int> GetAllArticlesCountAsync();

        /// <summary>
        /// به روش آسنکرون، نوع مفهومی سند را با توجه به شناسه آرتیکل داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مورد نظر</param>
        /// <returns>نوع مفهومی سند مرتبط با شناسه آرتیکل داده شده</returns>
        Task<int> GetLineSubjectTypeAsync(int articleId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده متعلق به مجموعه حساب های صندوق یا بانک است یا نه
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی حساب مورد نظر</param>
        /// <returns>مقدار بولی درست در صورت متعلق بودن شماره حساب، در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsCashOrBankAccountAsync(int accountId);
    }
}

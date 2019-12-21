using System;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبه مانده مولفه های حساب را تعریف می کند
    /// </summary>
    public interface IBalanceUtility : IReportUtility
    {
        /// <summary>
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetBalanceAsync(int itemId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetBalanceAsync(int itemId, int number);

        /// <summary>
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را در سند مالی از نوع داده شده
        /// محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="type">نوع سیستمی مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده محاسبه شده برای سرفصل حسابداری</returns>
        Task<decimal> GetSpecialVoucherBalanceAsync(int itemId, VoucherType type);

        /// <summary>
        /// اطلاعات مولفه حساب با شناسه داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه مولفه حساب مورد نظر</param>
        /// <returns>اطلاعات مولفه حساب مورد نظر</returns>
        /// <remarks>این متد قاعدتاً باید توسط کلاسی پیاده سازی شود که اطلاعات داخلی درباره نوع مولفه حساب را داشته باشد</remarks>
        Task<TreeEntity> GetAccountItemAsync(int itemId);

        /// <summary>
        /// اطلاعات مولفه حساب با شناسه داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه مولفه حساب مورد نظر</param>
        /// <returns>اطلاعات مولفه حساب مورد نظر</returns>
        Task<TModel> GetAccountItemAsync<TModel>(int itemId)
            where TModel : class, ITreeEntity;
    }
}

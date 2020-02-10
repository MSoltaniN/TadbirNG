using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence.Utility
{
    public interface IAccountItemUtility : IReportUtility
    {
        /// <summary>
        /// اطلاعات مولفه حساب با شناسه داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه مولفه حساب مورد نظر</param>
        /// <returns>اطلاعات مولفه حساب مورد نظر</returns>
        /// <remarks>این متد قاعدتاً باید توسط کلاسی پیاده سازی شود که اطلاعات داخلی درباره نوع مولفه حساب را داشته باشد</remarks>
        Task<TreeEntity> GetItemAsync(int itemId);

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
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را در نوع سند سیستمی داده شده
        /// محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="type">نوع سند سیستمی مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetBalanceAsync(int itemId, VoucherType type);

        Task<ValueTuple<decimal, decimal>> GetTurnoverAsync(int itemId, DateTime from, DateTime to);

        Task<ValueTuple<decimal, decimal>> GetTurnoverAsync(int itemId, int from, int to);

        Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity accountItem);
    }
}

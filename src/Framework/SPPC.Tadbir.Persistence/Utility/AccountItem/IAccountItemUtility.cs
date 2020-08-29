using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبات گردش و مانده را برای مولفه های مختلف حساب تعریف می کند
    /// </summary>
    public interface IAccountItemUtility : IReportUtility
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات مولفه حساب با شناسه داده شده را خوانده و برمی گرداند
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
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را در اسناد مالی با مأخذ داده شده
        /// محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="origin">مأخذ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetBalanceAsync(int itemId, VoucherOriginId origin);

        /// <summary>
        /// به روش آسنکرون، مبالغ گردش بدهکار و بستانکار برای مولفه حساب مورد نظر را
        /// در محدوده تاریخی داده شده محاسبه می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای محدوده تاریخی مورد نظر</param>
        /// <param name="to">تاریخ انتهای محدوده تاریخی مورد نظر</param>
        /// <returns>مبالغ گردش محاسبه شده برای مولفه حساب</returns>
        Task<ValueTuple<decimal, decimal>> GetTurnoverAsync(int itemId, DateTime from, DateTime to);

        /// <summary>
        /// به روش آسنکرون، مبالغ گردش بدهکار و بستانکار برای مولفه حساب مورد نظر را
        /// در محدوده اسناد داده شده محاسبه می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">اولین سند در محدوده مورد نظر</param>
        /// <param name="to">آخرین سند در محدوده مورد نظر</param>
        /// <returns>مبالغ گردش محاسبه شده برای مولفه حساب</returns>
        Task<ValueTuple<decimal, decimal>> GetTurnoverAsync(int itemId, int from, int to);

        /// <summary>
        /// عبارت شرطی مورد نیاز برای انجام محاسبات مولفه حساب را برمی گرداند
        /// </summary>
        /// <param name="accountItem">اطلاعات مولفه حساب مورد نظر</param>
        /// <returns>عبارت شرطی</returns>
        Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity accountItem);
    }
}

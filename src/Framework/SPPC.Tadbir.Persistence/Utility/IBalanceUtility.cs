using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Utility
{
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

        Task<TreeEntity> GetAccountItemAsync(int itemId);

        Task<TModel> GetAccountItemAsync<TModel>(int itemId)
            where TModel : class, ITreeEntity;
    }
}

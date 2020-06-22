using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// عملیات کمکی مورد نیاز برای استفاده از مجموعه حساب ها را تعریف می کند
    /// </summary>
    public interface IAccountCollectionUtility
    {
        /// <summary>
        /// به روش آسنکرون، حساب های قابل استفاده تعریف شده در یک مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب</param>
        /// <param name="branchId">شعبه مورد نظر برای خواندن حساب های مجموعه حساب
        /// که در صورت تعیین نشدن، شعبه جاری فرض می شود</param>
        /// <returns>مجموعه ای از حساب های قابل استفاده در یک مجموعه حساب</returns>
        /// <remarks>منظور از حساب های قابل استفاده، حساب هایی است که می توان در آرتیکل های سند از آنها استفاده کرد.
        /// لازم به یادآوری است که حساب های یک مجموعه حساب ممکن است در سطوح ماقبل آخر به یک مجموعه حساب تخصیص داده شوند
        /// و مستقیماً قابل استفاده در آرتیکل های سند نباشند.</remarks>
        Task<IList<Account>> GetUsableCollectionAccountsAsync(int collectionId, int? branchId = null);
    }
}

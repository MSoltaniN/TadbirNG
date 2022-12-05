using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.CodeChallenge.Common;

namespace SPPC.CodeChallenge.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن لیست موجودیت ها به صورت مجموعه ای از کلید و مقدار را تعریف می کند.
    /// کلید برابر شناسه دیتابیسی موجودیت و مقدار برابر نام موجودیت خواهد بود
    /// </summary>
    public interface ILookupRepository
    {
        /// <summary>
        /// به روش آسنکرون، لیست استان ها را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>لیست استان ها</returns>
        Task<IList<KeyValue>> GetProvincesAsync();

        /// <summary>
        /// به روش آسنکرون، لیست شهرهای یک استان را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="provinceId">شناسه دیتابیسی استان</param>
        /// <returns>لیست شهرهای یک استان</returns>
        Task<IList<KeyValue>> GetCitiesAsync(int provinceId);
    }
}

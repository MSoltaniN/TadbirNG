using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.CodeChallenge.Common;
using SPPC.CodeChallenge.Mapper;

namespace SPPC.CodeChallenge.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن لیست موجودیت ها به صورت مجموعه ای از کلید و مقدار را پیاده سازی می کند.
    /// کلید برابر شناسه دیتابیسی موجودیت و مقدار برابر نام موجودیت خواهد بود
    /// </summary>
    public class LookupRepository : ILookupRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="mapper">امکان تبدیل کلاس ها به یکدیگر را فراهم می کند</param>
        public LookupRepository(IDomainMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، لیست استان ها را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>لیست استان ها</returns>
        public async Task<IList<KeyValue>> GetProvincesAsync()
        {
            // TODO: Use EF Core to read provinces. Use _mapper to convert to KeyValue.
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، لیست شهرهای یک استان را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="provinceId">شناسه دیتابیسی استان</param>
        /// <returns>لیست شهرهای یک استان</returns>
        public async Task<IList<KeyValue>> GetCitiesAsync(int provinceId)
        {
            // TODO: Use EF Core to read cities. Use _mapper to convert to KeyValue.
            throw new NotImplementedException();
        }

        private readonly IDomainMapper _mapper;
    }
}

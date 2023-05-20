using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر عملیات ارزی را تعریف می کند
    /// </summary>
    public interface ICurrencyBookRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات دفتر عملیات ارزی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <returns>اطلاعات دفتر عملیات ارزی با مشخصات داده شده</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookAsync(CurrencyBookParameters parameters);

        /// <summary>
        /// به روش آسنکرون، تمامی ارزهای استفاده شده در آرتیکل های سند را به همراه مجموع بدهکار و بستانکار برمی گرداند
        /// </summary>
        /// <param name="parameters">مجموعه پارامترهای مورد نیاز برای گزارش گیری</param>
        /// <returns>اطلاعات دفتر عملیات ارزی برای کلیه ارزها</returns>
        Task<CurrencyBookViewModel> GetCurrencyBookAllCurrenciesAsync(CurrencyBookParameters parameters);
    }
}

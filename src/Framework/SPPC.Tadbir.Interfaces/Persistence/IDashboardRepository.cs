using System.Globalization;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه اطلاعات خلاصه در داشبورد را تعریف می کند
    /// </summary>
    public interface IDashboardRepository
    {
        /// <summary>
        /// به روش آسنکرون، مقادیر خلاصه محاسبه شده برای نمایش در داشبورد را خوانده و برمی گرداند
        /// </summary>
        /// <param name="calendar">تقویم مورد استفاده برای نمودارهای ماهیانه</param>
        /// <returns>اطلاعات مالی محاسبه شده</returns>
        Task<DashboardSummariesViewModel> GetSummariesAsync(Calendar calendar);
    }
}

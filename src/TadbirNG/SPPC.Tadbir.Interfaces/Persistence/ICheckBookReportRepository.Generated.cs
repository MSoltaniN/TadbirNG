using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دفتر دسته های چک را تعریف می کند
    /// </summary>
    public interface ICheckBookReportRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه دفتر دسته های چک را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دسته های چک تعریف شده</returns>
        Task<PagedList<CheckBookReportViewModel>> GetCheckBooksReportAsync(GridOptions gridOptions = null);
    }
}

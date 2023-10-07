using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.ProductScope;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت واحدها را تعریف می کند
    /// </summary>
    public interface IUnitRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه واحدها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از واحدها تعریف شده</returns>
        Task<PagedList<UnitViewModel>> GetUnitsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، واحد با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="unitId">شناسه عددی یکی از واحدها موجود</param>
        /// <returns>واحد مشخص شده با شناسه عددی</returns>
        Task<UnitViewModel> GetUnitAsync(int unitId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک واحد را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="unit">واحد مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی واحد ایجاد یا اصلاح شده</returns>
        Task<UnitViewModel> SaveUnitAsync(UnitViewModel unit);

        /// <summary>
        /// به روش آسنکرون، واحد مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="unitId">شناسه عددی واحد مورد نظر برای حذف</param>
        Task DeleteUnitAsync(int unitId);

        /// <summary>
        /// به روش آسنکرون، واحدها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="unitIds">مجموعه ای از شناسه های عددی واحدها مورد نظر برای حذف</param>
        Task DeleteUnitsAsync(IList<int> unitIds);
    }
}

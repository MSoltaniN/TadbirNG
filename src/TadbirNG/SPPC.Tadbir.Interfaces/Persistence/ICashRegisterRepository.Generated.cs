using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت صندوق ها را تعریف می کند
    /// </summary>
    public interface ICashRegisterRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه صندوق ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از صندوق ها تعریف شده</returns>
        Task<PagedList<CashRegisterViewModel>> GetCashRegistersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، صندوق با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashregisterId">شناسه عددی یکی از صندوق ها موجود</param>
        /// <returns>صندوق مشخص شده با شناسه عددی</returns>
        Task<CashRegisterViewModel> GetCashRegisterAsync(int cashregisterId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک صندوق را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="cashregister">صندوق مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی صندوق ایجاد یا اصلاح شده</returns>
        Task<CashRegisterViewModel> SaveCashRegisterAsync(CashRegisterViewModel cashregister);

        /// <summary>
        /// به روش آسنکرون، صندوق مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashregisterId">شناسه عددی صندوق مورد نظر برای حذف</param>
        Task DeleteCashRegisterAsync(int cashregisterId);

        /// <summary>
        /// به روش آسنکرون، صندوق ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashRegisterIds">مجموعه ای از شناسه های عددی صندوق ها مورد نظر برای حذف</param>
        Task DeleteCashRegistersAsync(IList<int> cashRegisterIds);
    }
}

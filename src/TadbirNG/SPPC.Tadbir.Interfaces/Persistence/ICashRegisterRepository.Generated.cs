using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
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
        /// <returns>مجموعه ای از صندوق های تعریف شده</returns>
        Task<PagedList<CashRegisterViewModel>> GetCashRegistersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، صندوق با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی یکی از صندوق های موجود</param>
        /// <returns>صندوق مشخص شده با شناسه عددی</returns>
        Task<CashRegisterViewModel> GetCashRegisterAsync(int cashRegisterId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک صندوق را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="cashRegister">صندوق مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی صندوق ایجاد یا اصلاح شده</returns>
        Task<CashRegisterViewModel> SaveCashRegisterAsync(CashRegisterViewModel cashRegister);

        /// <summary>
        /// به روش آسنکرون، صندوق مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی صندوق مورد نظر برای حذف</param>
        Task DeleteCashRegisterAsync(int cashRegisterId);

        /// <summary>
        /// به روش آسنکرون، صندوق های مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashRegisterIds">مجموعه ای از شناسه های عددی صندوق های مورد نظر برای حذف</param>
        Task DeleteCashRegistersAsync(IList<int> cashRegisterIds);

        /// <summary>
        /// به روش آسنکرون، کاربران اختصاص داده شده به صندوق را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی یکی از صندوق های موجود</param>
        /// <returns>مجموعه ای از کاربران تخصیص داده شده به صندوق</returns>
        Task<RelatedItemsViewModel> GetCashRegisterUsersAsync(int cashRegisterId);

        /// <summary>
        /// به روش آسنکرون، کاربران را به صندوق تخصیص می دهد
        /// </summary>
        /// <param name="userCashRegisters">اطلاعات نمایشی کاربران</param>
        Task SaveCashRegisterUsersAsync(RelatedItemsViewModel userCashRegisters);

        /// <summary>
        /// به روش آسنکرون، بررسی می کند که نام صندوق تکراری هست یا خیر
        /// </summary>
        /// <param name="cashRegister">صندوق مورد نظر</param>
        /// <returns>برای نام تکراری مقدار درست و در غیر این صورت 
        /// مقدار نادرست برمی گرداند</returns>
        Task<bool> IsDuplicateCashRegisterName(CashRegisterViewModel cashRegister);

        /// <summary>
        /// به روش آسنکرون، بررسی می کند که آیا به صندوق کاربر اختصاص یافته یا خیر
        /// </summary>
        /// <param name="cashRegisterId">شناسه یکتای صندوق مورد نظر</param>
        /// <returns>اگر به صندوق، کاربر اختصاص یافته مقدار درست و در غیر این صورت 
        /// مقدار نادرست برمی گرداند</returns>
        Task<bool> HasAssignedUsersToCashRegAsync(int cashRegisterId);
    }
}

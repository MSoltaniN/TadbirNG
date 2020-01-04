using System;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر حساب را تعریف می کند
    /// </summary>
    public interface IAccountBookRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر حساب بر حسب تاریخ</returns>
        Task<AccountBookViewModel> GetAccountBookAsync(AccountBookParameters parameters);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر حساب به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر حساب به تفکیک شعبه</returns>
        Task<AccountBookViewModel> GetAccountBookByBranchAsync(AccountBookParameters parameters);

        /// <summary>
        /// به روش آسنکرون، مولفه حساب قبلی قابل دسترسی نسبت به مولفه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مولفه حساب</param>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب جاری</param>
        /// <returns>اطلاعات نمایشی مختصر برای مولفه حساب قبلی</returns>
        Task<AccountItemBriefViewModel> GetPreviousAccountItemAsync(int viewId, int itemId);

        /// <summary>
        /// به روش آسنکرون، مولفه حساب بعدی قابل دسترسی نسبت به مولفه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مولفه حساب</param>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب جاری</param>
        /// <returns>اطلاعات نمایشی مختصر برای مولفه حساب بعدی</returns>
        Task<AccountItemBriefViewModel> GetNextAccountItemAsync(int viewId, int itemId);
    }
}

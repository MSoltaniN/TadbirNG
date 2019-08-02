using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مجموعه حساب ها را تعریف می کند
    /// </summary>
    public interface IAccountSetRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه کلیه حساب های تخصیص داده شده
        /// به یک مجموعه حساب خاص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب مورد نظر</param>
        /// <returns>اطلاعات حساب های تخصیص داده شده به مجموعه حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountSetItemsAsync(AccountCollectionId collectionId);

        /// <summary>
        /// خلاصه اطلاعات حساب های مرتبط با مجموعه حساب های کسر از فروش (برگشت از فروش و تخفیفات فروش) را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب های مرتبط با مجموعه حساب های کسر از فروش</returns>
        Task<IList<AccountItemBriefViewModel>> GetSalesDeficitAccountsAsync();
    }
}

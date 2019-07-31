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
        /// اطلاعات خلاصه کلیه حساب های تخصیص داده شده به یک مجموعه حساب خاص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب مورد نظر</param>
        /// <returns>اطلاعات حساب های تخصیص داده شده به مجموعه حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountSetItems(AccountCollectionId collectionId);

        /// <summary>
        /// خلاصه اطلاعات حساب کل را برای مجموعه حساب فروش خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب کل برای مجموعه حساب فروش</returns>
        Task<AccountItemBriefViewModel> GetSalesAccountAsync();

        /// <summary>
        /// خلاصه اطلاعات حساب کل را برای مجموعه حساب برگشت از فروش و تخفیفات خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب کل برای مجموعه حساب برگشت از فروش و تخفیفات</returns>
        Task<AccountItemBriefViewModel> GetSalesDeficitAccountAsync();

        /// <summary>
        /// خلاصه اطلاعات حساب های کل را برای مجموعه حساب دارایی های جاری خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب های کل برای مجموعه حساب دارایی های جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetLiquidAssetAccountsAsync();

        /// <summary>
        /// خلاصه اطلاعات حساب های کل را برای مجموعه حساب بدهی های جاری خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب های کل برای مجموعه حساب بدهی های جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetLiquidLiabilityAccountsAsync();
    }
}

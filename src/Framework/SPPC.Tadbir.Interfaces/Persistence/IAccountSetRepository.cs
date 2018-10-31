using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مجموعه حساب ها را تعریف می کند
    /// </summary>
    public interface IAccountSetRepository
    {
        /// <summary>
        /// خلاصه اطلاعات حساب کل را برای مجموعه حساب بانک خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب کل برای مجموعه حساب بانک</returns>
        Task<AccountItemBriefViewModel> GetBankAccountAsync();

        /// <summary>
        /// خلاصه اطلاعات حساب کل را برای مجموعه حساب صندوق خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات حساب کل برای مجموعه حساب صندوق</returns>
        Task<AccountItemBriefViewModel> GetCashierAccountAsync();

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

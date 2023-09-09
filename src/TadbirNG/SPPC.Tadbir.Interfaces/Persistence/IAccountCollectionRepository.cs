using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مجموعه حساب را تعریف می کند.
    /// </summary>
    public interface IAccountCollectionRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی طبقه بندی های مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از اطلاعات نمایشی طبقه بندی های مجموعه حساب</returns>
        Task<IList<AccountCollectionCategoryViewModel>> GetCollectionCategoriesAsync();

        /// <summary>
        /// به روش آسنکرون، حساب های انتخاب شده برای یک مجموعه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های انتخاب شده در یک مجموعه حساب</returns>
        Task<IList<AccountCollectionAccountViewModel>> GetCollectionAccountsAsync(
            int collectionId, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، حساب های یک مجموعه حساب را اضافه میکند
        /// </summary>
        /// <param name="accounts">اطلاعات حساب های یک مجموعه حساب</param>
        /// <param name="collectionId">شناسه یکتای مجموعه حساب انتخاب شده</param>
        Task AddCollectionAccountsAsync(int collectionId, IList<AccountCollectionAccountViewModel> accounts);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که شعبه داده شده امکان تعریف حساب برای مجموعه حساب را دارد یا نه
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر</param>
        /// <param name="collectionId">شناسه دیتابیسی مجموعه حساب مورد نظر</param>
        /// <returns>برای مجموعه حسابهای تک حسابی، شعبه داده شده باید بالاترین شعبه در ساختار درختی باشد.
        /// ولی برای سایر مجموعه حسابها هر شعبه ای می تواند حسابهای مجموعه حساب را تعیین کند</returns>
        Task<bool> CanBranchManageCollectionAsync(int branchId, int collectionId);

        /// <summary>
        ///به روش آسنکرون، لیست حساب های تخصیص یافته به مجموعه حساب های صندوق و بانک را برمی گرداند.
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست حساب های تخصیص یافته به مجموعه حساب های صندوق و بانک</returns>
        Task<IList<AccountItemBriefViewModel>> GetCashAndBankAccountsAsync(GridOptions gridOptions);
    }
}

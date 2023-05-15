using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات گروه های حساب را تعریف می کند.
    /// </summary>
    public interface IAccountGroupRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی کلیه گروه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از اطلاعات نمایشی گروه های حساب</returns>
        Task<PagedList<AccountGroupViewModel>> GetAccountGroupsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی گروه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر</param>
        /// <returns>اطلاعات نمایشی گروه حساب</returns>
        Task<AccountGroupViewModel> GetAccountGroupAsync(int groupId);

        /// <summary>
        /// مجوعه ای از حساب های کل زیرمجموعه گروه مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه مورد نظر</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه حساب های کل زیرمجموعه</returns>
        Task<PagedList<AccountViewModel>> GetGroupLedgerAccountsAsync(int groupId, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک گروه حساب را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountGroup">گروه حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی گروه حساب ایجاد یا اصلاح شده</returns>
        Task<AccountGroupViewModel> SaveAccountGroupAsync(AccountGroupViewModel accountGroup);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا گروه حساب مورد نظر قابل حذف هست یا نه؟
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر برای حذف</param>
        /// <returns>اگر قابل حذف باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> CanDeleteAccountGroupAsync(int groupId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک گروه حساب موجود را حذف می کند
        /// </summary>
        /// <param name="groupId">شناسه گروه حساب مورد نظر برای حذف</param>
        Task DeleteAccountGroupAsync(int groupId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مجموعه ای از گروه های حساب موجود را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteAccountGroupsAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، تمام گروه های حساب را خوانده و برمیگرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه گروه های حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountGroupsBriefAsync();

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که گروه حساب داده شده تکراری است یا نه
        /// </summary>
        /// <param name="accountGroup">اطلاعات نمایشی گروه حساب مورد نظر برای بررسی</param>
        /// <returns>در صورتی که نام گروه حساب تکراری باشد، مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsDuplicateGroupAsync(AccountGroupViewModel accountGroup);
    }
}

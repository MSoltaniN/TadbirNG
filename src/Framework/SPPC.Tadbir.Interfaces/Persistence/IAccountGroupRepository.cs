using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

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
        Task<IList<AccountGroupViewModel>> GetAccountGroupsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی گروه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه حساب مورد نظر</param>
        /// <returns>اطلاعات نمایشی گروه حساب</returns>
        Task<AccountGroupViewModel> GetAccountGroupAsync(int groupId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای گروه حساب را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای گروه حساب</returns>
        Task<ViewViewModel> GetAccountGroupMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، تعداد گروه های حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد گروه های حساب تعریف شده</returns>
        Task<int> GetCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد حساب های کل زیرمجموعه گروه حساب مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه مورد نظر</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد حساب های کل زیرمجموعه گروه حساب</returns>
        Task<int> GetSubItemCountAsync(int groupId, GridOptions gridOptions = null);

        /// <summary>
        /// مجوعه ای از حساب های کل زیرمجموعه گروه مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه دیتابیسی گروه مورد نظر</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه حساب های کل زیرمجموعه</returns>
        Task<IList<AccountViewModel>> GetGroupLedgerAccountsAsync(int groupId, GridOptions gridOptions = null);

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
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده
        /// مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);

        /// <summary>
        /// به روش آسنکرون، تمام گروه های حساب را خوانده و برمیگرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه گروه های حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountGroupsBriefAsync();
    }
}

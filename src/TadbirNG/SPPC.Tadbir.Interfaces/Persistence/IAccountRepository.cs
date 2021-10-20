using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات سرفصل های حسابداری را تعریف می کند.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه حساب هایی را که در دوره مالی و شعبه جاری برنامه تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های تعریف شده در دوره مالی و شعبه جاری برنامه</returns>
        Task<PagedList<AccountViewModel>> GetAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، کلیه حساب های قابل انتخاب در دوره مالی و شعبه جاری برنامه را
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل انتخاب در دوره مالی و شعبه جاری برنامه</returns>
        /// <remarks>این متد حسابهای غیرفعال در دوره مالی جاری برنامه را از فهرست خروجی فیلتر می کند</remarks>
        Task<PagedList<AccountViewModel>> GetAccountsLookupAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، حساب با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه عددی یکی از حساب های موجود</param>
        /// <returns>حساب مشخص شده با شناسه عددی</returns>
        Task<AccountViewModel> GetAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، برای حساب والد مشخص شده حساب زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی حساب والد - اگر مقدار نداشته باشد حساب جدید
        /// در سطح کل پیشنهاد می شود</param>
        /// <returns>مدل نمایشی کلی حساب </returns>
        Task<AccountFullDataViewModel> GetNewChildAccountAsync(int? parentId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری در سطح کل را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه سرفصل های حسابداری در سطح کل</returns>
        Task<IList<AccountItemBriefViewModel>> GetLedgerAccountsAsync();

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری یک گروه حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="groupId">شناسه یکتای دیتابیسی گروه حساب</param>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه سرفصل های حسابداری یک گروه حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetLedgerAccountsByGroupIdAsync(int groupId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری زیرمجموعه یک سرفصل حسابداری مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکی از سرفصل های حسابداری موجود</param>
        /// <returns>مجموعه ای از سرفصل های حسابداری زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountChildrenAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک حساب را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="accountFullView">اطلاعات مالیاتی طرف حساب مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی حساب ایجاد یا اصلاح شده</returns>
        Task<AccountFullDataViewModel> SaveAccountAsync(AccountFullDataViewModel accountFullView);

        /// <summary>
        /// به روش آسنکرون، حساب مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountId">شناسه عددی حساب مورد نظر برای حذف</param>
        Task DeleteAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، حساب های مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="accountIds">مجموعه ای از شناسه های عددی حساب های مورد نظر برای حذف</param>
        Task DeleteAccountsAsync(IList<int> accountIds);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد حساب مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="account">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر کد حساب تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        /// <remarks>اگر کد حساب در حسابی با شناسه یکتای همین حساب به کار رفته باشد (مثلاً در حالتی که
        /// یک حساب در حالت ویرایش است) در این صورت مقدار "نادرست" را برمی گرداند</remarks>
        Task<bool> IsDuplicateFullCodeAsync(AccountViewModel account);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که نام حساب مورد نظر بین حساب های همسطح با حساب والد یکسان تکراری است یا نه
        /// </summary>
        /// <param name="account">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر نام حساب تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        /// <remarks>اگر نام حساب در حسابی با شناسه یکتای همین حساب به کار رفته باشد (مثلاً در حالتی که
        /// یک حساب در حالت ویرایش است) در این صورت مقدار "نادرست" را برمی گرداند</remarks>
        Task<bool> IsDuplicateNameAsync(AccountViewModel account);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که حساب مورد نظر زیرمجموعه یک حساب رابط است یا نه
        /// </summary>
        /// <param name="account">مدل نمایشی حساب مورد نظر</param>
        /// <returns>اگر حساب والد از نوع حساب رابط باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsAssociationChildAccountAsync(AccountViewModel account);

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که حساب با شناسه داده شده می تواند زیرمجموعه داشته باشد یا نه
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی حساب مورد نظر</param>
        /// <returns>اگر حساب مورد نظر امکان داشتن زیرمجموعه را داشته باشد مقدار"درست" و
        /// در غیر این صورت مقدار "نادرست" را برمیگرداند</returns>
        Task<bool> CanHaveChildrenAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRelatedAccountAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حساب انتخاب شده دارای حساب زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده دارای حساب زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا حساب انتخاب شده در مجموعه حسابی وجود دارد یا نه
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns>در حالتی که حساب مشخص شده در مجموعه حسابی باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedInAccountCollectionAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، کد کامل حساب با شناسه داده شده را برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی یکی از حساب های موجود</param>
        /// <returns>اگر حساب با شناسه داده شده وجود نداشته باشد مقدار خالی
        /// و در غیر این صورت کد کامل را برمی گرداند</returns>
        Task<string> GetAccountFullCodeAsync(int accountId);

        /// <summary>
        /// به روش آسنکرون، تعداد کل حساب های ثبت شده را برمی گرداند
        /// </summary>
        /// <returns>تعداد کل حساب ها</returns>
        Task<int> GetAllAccountsCountAsync();

        /// <summary>
        /// به روش آسنکرون، حساب با سایر مشخصات حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <returns></returns>
        Task<AccountFullDataViewModel> GetAccountFullDataAsync(int accountId);
    }
}

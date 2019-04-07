using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات تفصیلی های شناور را تعریف می کند.
    /// </summary>
    public interface IDetailAccountRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<IList<DetailAccountViewModel>> GetDetailAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<IList<KeyValue>> GetDetailAccountsLookupAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات از آن استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new();

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه عددی یکی از تفصیلی های شناور موجود</param>
        /// <returns>تفصیلی شناور مشخص شده با شناسه عددی</returns>
        Task<DetailAccountViewModel> GetDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور در سطح اول را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از تفصیلی های شناور در سطح اول</returns>
        Task<IList<AccountItemBriefViewModel>> GetDetailAccountsLedgerAsync();

        /// <summary>
        /// به روش آسنکرون، شناورهای زیرمجموعه را برای تفصیلی شناور مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی های شناور زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetDetailAccountChildrenAsync(int detailId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای تفصیلی شناور را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای تفصیلی شناور</returns>
        Task<ViewViewModel> GetDetailAccountMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک تفصیلی شناور را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="detailAccount">تفصیلی شناور مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی تفصیلی شناور ایجاد یا اصلاح شده</returns>
        Task<DetailAccountViewModel> SaveDetailAccountAsync(DetailAccountViewModel detailAccount);

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="faccountId">شناسه عددی تفصیلی شناور مورد نظر برای حذف</param>
        Task DeleteDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="detailIds">مجموعه ای از شناسه های عددی تفصیلی های مورد نظر برای حذف</param>
        Task DeleteDetailAccountsAsync(IList<int> detailIds);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد تفصیلی شناور مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="detailAccount">مدل نمایشی تفصیلی شناور مورد نظر</param>
        /// <returns>اگر کد تفصیلی شناور تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateDetailAccountAsync(DetailAccountViewModel detailAccount);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRelatedDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده دارای شناور زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از شناور های موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده دارای شناور زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر تفصیلی شناور را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر تفصیلی شناور</param>
        /// <returns>اگر تفصیلی شناور والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        Task<string> GetDetailAccountFullCodeAsync(int parentId);

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}

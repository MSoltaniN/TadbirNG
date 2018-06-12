using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را پیاده سازی میکند.
    /// </summary>
    public interface ICompanyRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در شرکت مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در شرکت مشخص شده</returns>
        Task<IList<CompanyViewModel>> GetCompaniesAsync(int companyId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد شرکت های تعریف شده در شرکت مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد شرکت های تعریف شده در شرکت مشخص شده</returns>
        Task<int> GetCountAsync(int companyId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون،شرکت با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه عددی یکی از شرکت های موجود</param>
        /// <returns>شرکت مشخص شده با شناسه عددی</returns>
        Task<CompanyViewModel> GetCompanyAsync(int companyId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای شرکت را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فرا داده ای تعریف شده برای شرکت</returns>
        Task<EntityViewModel> GetCompanyMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شرکت را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="company">شرکت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        Task<CompanyViewModel> SaveCompanyAsync(CompanyViewModel company);

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="companyId">شناسه عددی شرکت مورد نظر برای حذف</param>
        Task DeleteCompanyAsync(int companyId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شرکت انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="companyId">شناسه یکتای یکی از شرکت های موجود</param>
        /// <returns>در حالتی که شرکت مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int companyId);
    }
}

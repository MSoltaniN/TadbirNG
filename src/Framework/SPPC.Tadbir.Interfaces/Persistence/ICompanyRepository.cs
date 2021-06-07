using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را پیاده سازی میکند.
    /// </summary>
    public interface ICompanyRepository : IRepositoryBase
    {
        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در برنامه تعریف شده اند خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در برنامه</returns>
        Task<PagedList<CompanyDbViewModel>> GetCompaniesAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون،شرکت با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه عددی یکی از شرکت های موجود</param>
        /// <returns>شرکت مشخص شده با شناسه عددی</returns>
        Task<CompanyDbViewModel> GetCompanyAsync(int companyId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شرکت را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="company">شرکت مورد نظر برای ایجاد یا اصلاح</param>
        /// <param name="webHostPath">مسیر ریشه نرم افزار</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        Task<CompanyDbViewModel> SaveCompanyAsync(CompanyDbViewModel company, string webHostPath);

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="companyId">شناسه عددی شرکت مورد نظر برای حذف</param>
        Task DeleteCompanyAsync(int companyId);

        /// <summary>
        /// به روش آسنکرون، شرکت های مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteCompaniesAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که نام وارد شده برای دیتابیس تکراری است یا نه
        /// </summary>
        /// <param name="company">شرکت مورد نظر</param>
        /// <returns>اگر نام دیتابیس تکراری بود مقدار درست در غیر اینصورت مقدار نادرست را برمی گرداند</returns>
        Task<bool> IsDuplicateCompanyAsync(CompanyDbViewModel company);

        /// <summary>
        /// مشخص می کند که نام کاربری وارد شده تکراری است یا نه
        /// </summary>
        /// <param name="company">شرکت مورد نظر</param>
        /// <returns>اگر نام کاربری تکراری بود مقدار درست در غیر اینصورت مقدار نادرست را برمی گرداند</returns>
        bool IsDuplicateCompanyUserName(CompanyDbViewModel company);
    }
}

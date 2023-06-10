using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را تعریف میکند.
    /// </summary>
    public interface ICompanyRepository : IRepositoryBase
    {
        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در برنامه تعریف شده اند خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در برنامه</returns>
        Task<PagedList<CompanyDbViewModel>> GetCompaniesAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون،شرکت با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه عددی یکی از شرکت های موجود</param>
        /// <returns>شرکت مشخص شده با شناسه عددی</returns>
        Task<CompanyDbViewModel> GetCompanyAsync(int companyId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شرکت را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="company">شرکت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        Task<CompanyDbViewModel> SaveCompanyAsync(CompanyDbViewModel company);

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="companyId">شناسه عددی شرکت مورد نظر برای حذف</param>
        Task DeleteCompanyAsync(int companyId);

        /// <summary>
        /// به روش آسنکرون، شرکت های مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteCompaniesAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از شرکت های موجود را برمی گرداند که نام شرکت یا نام دیتابیس آنها
        /// مشابه شرکت داده شده است
        /// </summary>
        /// <param name="company">شرکت مورد نظر</param>
        /// <returns>مجموعه ای از شرکت های موجود با نام شرکت یا نام دیتابیس تکراری</returns>
        Task<IEnumerable<CompanyDbViewModel>> GetDuplicateCompaniesAsync(CompanyDbViewModel company);

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک شرکت را خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه یکی از شرکت های موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        Task<RelatedItemsViewModel> GetCompanyRolesAsync(int companyId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های دارای دسترسی به یک شرکت را ذخیره می کند
        /// </summary>
        /// <param name="companyRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        Task SaveCompanyRolesAsync(RelatedItemsViewModel companyRoles);
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را پیاده سازی میکند.
    /// </summary>
    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public CompanyRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata)
            : base(unitOfWork, mapper, metadata)
        {
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در برنامه تعریف شده اند خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در برنامه</returns>
        public async Task<IList<CompanyDbViewModel>> GetCompaniesAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var companies = await repository.GetAllAsync(gridOptions);
            return companies
                .Select(c => Mapper.Map<CompanyDbViewModel>(c))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد شرکت های تعریف شده در برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد شرکت های تعریف شده در شرکت مشخص شده</returns>
        public async Task<int> GetCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var count = await repository.GetCountByCriteriaAsync(c => true, gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون،شرکت با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه عددی یکی از شرکت های موجود</param>
        /// <returns>شرکت مشخص شده با شناسه عددی</returns>
        public async Task<CompanyDbViewModel> GetCompanyAsync(int companyId)
        {
            CompanyDbViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                item = Mapper.Map<CompanyDbViewModel>(company);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای شرکت را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فرا داده ای تعریف شده برای شرکت</returns>
        public async Task<EntityViewModel> GetCompanyMetadataAsync()
        {
            return await Metadata.GetEntityMetadataAsync<CompanyDb>();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شرکت را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="company">شرکت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        public async Task<CompanyDbViewModel> SaveCompanyAsync(CompanyDbViewModel company)
        {
            Verify.ArgumentNotNull(company, "company");
            var companyModel = default(CompanyDb);
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            if (company.Id == 0)
            {
                companyModel = Mapper.Map<CompanyDb>(company);
                repository.Insert(companyModel);
            }
            else
            {
                companyModel = await repository.GetByIDAsync(
                    company.Id);
                if (companyModel != null)
                {
                    UpdateExistingCompany(company, companyModel);
                    repository.Update(companyModel);
                }
            }

            await UnitOfWork.CommitAsync();
            return Mapper.Map<CompanyDbViewModel>(companyModel);
        }

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="companyId">شناسه عددی شرکت مورد نظر برای حذف</param>
        public async Task DeleteCompanyAsync(int companyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                repository.Delete(company);
                await UnitOfWork.CommitAsync();
            }
        }

        private static void UpdateExistingCompany(CompanyDbViewModel companyViewModel, CompanyDb company)
        {
            company.Name = companyViewModel.Name;
            company.Description = companyViewModel.Description;
            company.DbName = companyViewModel.DbName;
            company.DbPath = companyViewModel.DbPath;
        }
    }
}

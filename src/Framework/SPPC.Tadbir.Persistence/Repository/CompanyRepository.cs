using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شرکت را پیاده سازی میکند.
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadataRepository">امکان خواندن متادیتا برای یک موجودیت را فراهم می کند</param>
        public CompanyRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadataRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _metadataRepository = metadataRepository;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شرکت هایی را که در شرکت مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شرکت های تعریف شده در شرکت مشخص شده</returns>
        public async Task<IList<CompanyViewModel>> GetCompaniesAsync(int companyId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Company>();
            var branches = await repository
                .GetByCriteriaAsync(
                    b => b.ParentId == companyId,
                    gridOptions, b => b.Parent, b => b.Children);
            return branches
                .Select(item => _mapper.Map<CompanyViewModel>(item))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد شرکت های تعریف شده در شرکت مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد شرکت های تعریف شده در شرکت مشخص شده</returns>
        public async Task<int> GetCountAsync(int companyId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Company>();
            var count = await repository
                .GetCountByCriteriaAsync(
                    c => c.ParentId == companyId,
                    gridOptions);
            return count;
        }

        /// <summary>
        /// به روش آسنکرون،شرکت با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه عددی یکی از شرکت های موجود</param>
        /// <returns>شرکت مشخص شده با شناسه عددی</returns>
        public async Task<CompanyViewModel> GetCompanyAsync(int companyId)
        {
            CompanyViewModel item = null;
            var repository = _unitOfWork.GetAsyncRepository<Company>();
            var company = await repository.GetByIDAsync(
                companyId, b => b.Children);
            if (company != null)
            {
                item = _mapper.Map<CompanyViewModel>(company);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای شرکت را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فرا داده ای تعریف شده برای شرکت</returns>
        public async Task<EntityViewModel> GetCompanyMetadataAsync()
        {
            return await _metadataRepository.GetEntityMetadataAsync<Company>();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شرکت را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="company">شرکت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شرکت ایجاد یا اصلاح شده</returns>
        public async Task<CompanyViewModel> SaveCompanyAsync(CompanyViewModel company)
        {
            Verify.ArgumentNotNull(company, "company");
            Company companyModel = default(Company);
            var repository = _unitOfWork.GetAsyncRepository<Company>();
            if (company.Id == 0)
            {
                companyModel = _mapper.Map<Company>(company);
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

            await _unitOfWork.CommitAsync();
            return _mapper.Map<CompanyViewModel>(companyModel);
        }

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="companyId">شناسه عددی شرکت مورد نظر برای حذف</param>
        public async Task DeleteCompanyAsync(int companyId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Company>();
            var company = await repository.GetByIDAsync(companyId);
            if (company != null)
            {
                company.Parent = null;
                repository.Delete(company);
                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شرکت انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="companyId">شناسه یکتای یکی از شرکت های موجود</param>
        /// <returns>در حالتی که شرکت مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool?> HasChildrenAsync(int companyId)
        {
            bool? hasChildren = null;
            var repository = _unitOfWork.GetAsyncRepository<Company>();
            var company = await repository.GetByIDAsync(companyId, b => b.Children);
            if (company != null)
            {
                hasChildren = company.Children.Count > 0;
            }

            return hasChildren;
        }

        private static void UpdateExistingCompany(CompanyViewModel companyViewModel, Company company)
        {
            company.Name = companyViewModel.Name;
            company.Description = companyViewModel.Description;
        }

        private IAppUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
        private IMetadataRepository _metadataRepository;
    }
}

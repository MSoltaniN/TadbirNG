using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات متداول برای کار با مولفه های بردار حساب با ساختار درختی را پیاده سازی می کند
    /// </summary>
    public class AccountItemRepository : IAccountItemRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public AccountItemRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه سرفصل های حسابداری در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafAccountsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var leafAccounts = await repository
                .GetByCriteriaAsync(acc => acc.FiscalPeriod.Id == fpId
                    && acc.Branch.Id == branchId
                    && acc.Children.Count == 0);
            return leafAccounts
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه تفصیلی های شناور در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafDetailAccountsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var leafDetails = await repository
                .GetByCriteriaAsync(facc => facc.FiscalPeriod.Id == fpId
                    && facc.Branch.Id == branchId
                    && facc.Children.Count == 0);
            return leafDetails
                .Select(facc => _mapper.Map<AccountItemBriefViewModel>(facc))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه مراکز هزینه در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafCostCentersAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var leafCenters = await repository
                .GetByCriteriaAsync(cc => cc.FiscalPeriod.Id == fpId
                    && cc.Branch.Id == branchId
                    && cc.Children.Count == 0);
            return leafCenters
                .Select(cc => _mapper.Map<AccountItemBriefViewModel>(cc))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه ها در آخرین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه پروژه ها در آخرین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetLeafProjectsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var leafProjects = await repository
                .GetByCriteriaAsync(prj => prj.FiscalPeriod.Id == fpId
                    && prj.Branch.Id == branchId
                    && prj.Children.Count == 0);
            return leafProjects
                .Select(prj => _mapper.Map<AccountItemBriefViewModel>(prj))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از سرفصل های حسابداری در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه سرفصل های حسابداری در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootAccountsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var rootAccounts = await repository
                .GetByCriteriaAsync(
                    acc => acc.FiscalPeriod.Id == fpId
                        && acc.Branch.Id == branchId
                        && acc.Parent == null,
                    acc => acc.Children);
            return rootAccounts
                .Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه تفصیلی های شناور در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var rootDetails = await repository
                .GetByCriteriaAsync(
                    facc => facc.FiscalPeriod.Id == fpId
                        && facc.Branch.Id == branchId
                        && facc.Parent == null,
                    facc => facc.Children);
            return rootDetails
                .Select(facc => _mapper.Map<AccountItemBriefViewModel>(facc))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه مراکز هزینه در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootCostCentersAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var rootCenters = await repository
                .GetByCriteriaAsync(
                    cc => cc.FiscalPeriod.Id == fpId
                        && cc.Branch.Id == branchId
                        && cc.Parent == null,
                    cc => cc.Children);
            return rootCenters
                .Select(cc => _mapper.Map<AccountItemBriefViewModel>(cc))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه ها در اولین سطح ساختار درختی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>مجموعه پروژه ها در بالاترین سطح</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetRootProjectsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var rootProjects = await repository
                .GetByCriteriaAsync(
                    prj => prj.FiscalPeriod.Id == fpId
                        && prj.Branch.Id == branchId
                        && prj.Parent == null,
                    prj => prj.Children);
            return rootProjects
                .Select(prj => _mapper.Map<AccountItemBriefViewModel>(prj))
                .ToList();
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

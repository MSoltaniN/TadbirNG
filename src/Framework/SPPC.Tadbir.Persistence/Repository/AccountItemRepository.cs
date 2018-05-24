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
        /// حساب های زیرمجموعه را برای حساب مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکی از حساب های موجود</param>
        /// <returns>مدل نمایشی حساب های زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildAccounts(int accountId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId, acc => acc.Children);
            if (account != null)
            {
                children.AddRange(account.Children.Select(acc => _mapper.Map<AccountItemBriefViewModel>(acc)));
            }

            return children;
        }

        /// <summary>
        /// شناورهای زیرمجموعه را برای تفصیلی شناور مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی های شناور زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildDetailAccounts(int detailId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detail = await repository.GetByIDAsync(detailId, facc => facc.Children);
            if (detail != null)
            {
                children.AddRange(detail.Children.Select(facc => _mapper.Map<AccountItemBriefViewModel>(facc)));
            }

            return children;
        }

        /// <summary>
        /// مراکز هزینه زیرمجموعه را برای مرکز هزینه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مراکز هزینه زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildCostCenters(int costCenterId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenter = await repository.GetByIDAsync(costCenterId, cc => cc.Children);
            if (costCenter != null)
            {
                children.AddRange(costCenter.Children.Select(cc => _mapper.Map<AccountItemBriefViewModel>(cc)));
            }

            return children;
        }

        /// <summary>
        /// پروژه های زیرمجموعه را برای پروژه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکی از پروژه های موجود</param>
        /// <returns>مدل نمایشی پروژه های زیرمجموعه</returns>
        public async Task<IList<AccountItemBriefViewModel>> GetChildProjects(int projectId)
        {
            var children = new List<AccountItemBriefViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var project = await repository.GetByIDAsync(projectId, prj => prj.Children);
            if (project != null)
            {
                children.AddRange(project.Children.Select(prj => _mapper.Map<AccountItemBriefViewModel>(prj)));
            }

            return children;
        }

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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
        /// مجموعه ای از سرفصل های حسابداری در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن لیست موجودیت ها به صورت مجموعه ای از کلید و مقدار را پیاده سازی می کند.
    /// کلید برابر شناسه دیتابیسی موجودیت و مقدار برابر نام موجودیت خواهد بود
    /// </summary>
    public class LookupRepository : ILookupRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public LookupRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Finance Subsystem Lookup

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetAccountsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(acc => acc.FiscalPeriod.Id == fpId
                    && acc.Branch.Id == branchId);
            return accounts
                .OrderBy(acc => acc.FullCode)
                .Select(acc => _mapper.Map<KeyValue>(acc));
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(det => det.FiscalPeriod.Id == fpId
                    && det.Branch.Id == branchId);
            return detailAccounts
                .OrderBy(det => det.FullCode)
                .Select(det => _mapper.Map<KeyValue>(det));
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetCostCentersAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(cc => cc.FiscalPeriod.Id == fpId
                    && cc.Branch.Id == branchId);
            return costCenters
                .OrderBy(cc => cc.FullCode)
                .Select(cc => _mapper.Map<KeyValue>(cc));
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetProjectsAsync(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(prj => prj.FiscalPeriod.Id == fpId
                    && prj.Branch.Id == branchId);
            return projects
                .OrderBy(prj => prj.FullCode)
                .Select(prj => _mapper.Map<KeyValue>(prj));
        }

        /// <summary>
        /// به روش آسنکرون، ارزهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        public async Task<IEnumerable<KeyValue>> GetCurrenciesAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Currency>();
            var currencies = await repository
                .GetAllAsync();
            return currencies
                .OrderBy(curr => curr.Name)
                .Select(curr => _mapper.Map<KeyValue>(curr));
        }

        /// <summary>
        /// به روش آسنکرون، شرکت های تعریف شده و قابل دسترسی توسط کاربر مشخص شده را به صورت مجموعه ای
        /// از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از شرکت های قابل دسترسی</returns>
        public async Task<IList<KeyValue>> GetUserAccessibleCompaniesAsync(int userId)
        {
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            var companies = new List<KeyValue>();
            if (user != null)
            {
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.Role)
                        .ToArray(),
                    role => companies.AddRange(
                        role.RoleBranches
                            .Select(rb => rb.Branch)
                            .Select(br => br.Company)
                            .Distinct(new EntityEqualityComparer())
                            .Select(c => _mapper.Map<KeyValue>(c))));
            }

            return companies;
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی تعریف شده در یک شرکت و قابل دسترسی توسط یک کاربر را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه دوره های مالی تعریف شده در یک شرکت مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetUserAccessibleFiscalPeriodsAsync(int companyId, int userId)
        {
            var fiscalPeriods = new List<FiscalPeriod>();
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.Role)
                        .ToArray(),
                    role => fiscalPeriods.AddRange(
                        role.RoleFiscalPeriods
                            .Select(rfp => rfp.FiscalPeriod)
                            .Where(fp => fp.Company.Id == companyId)));
                fiscalPeriods = fiscalPeriods
                    .Distinct(new EntityEqualityComparer())
                    .Cast<FiscalPeriod>()
                    .ToList();
            }

            return fiscalPeriods
                .OrderBy(fp => fp.Name)
                .Select(fp => _mapper.Map<KeyValue>(fp));
        }

        /// <summary>
        /// به روش آسنکرون، شعب سازمانی تعریف شده در یک شرکت و قابل دسترسی توسط یک کاربر را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه شعب سازمانی تعریف شده در یک شرکت مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetUserAccessibleBranchesAsync(int companyId, int userId)
        {
            var branches = new List<Branch>();
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.Role)
                        .ToArray(),
                    role => branches.AddRange(
                        role.RoleBranches
                            .Select(rb => rb.Branch)
                            .Where(br => br.Company.Id == companyId)));
                branches = branches
                    .Distinct(new EntityEqualityComparer())
                    .Cast<Branch>()
                    .ToList();
            }

            return branches
                .OrderBy(br => br.Name)
                .Select(br => _mapper.Map<KeyValue>(br));
        }

        #endregion

        private IQueryable<User> GetUserQuery(int userId)
        {
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var query = repository
                .GetEntityQuery()
                .Where(usr => usr.Id == userId)
                .Include(usr => usr.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RoleBranches)
                            .ThenInclude(rb => rb.Branch)
                                .ThenInclude(br => br.Company)
                .Include(usr => usr.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RoleFiscalPeriods)
                            .ThenInclude(rfp => rfp.FiscalPeriod)
                                .ThenInclude(fp => fp.Company);
            return query;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;

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
        public LookupRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper)
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
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetAccountsAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var accounts = await repository
                .GetByCriteriaAsync(acc => acc.FiscalPeriod.Id <= fpId
                    && acc.Branch.Id == branchId);
            return accounts
                .Select(acc => _mapper.Map<KeyValue>(acc))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetDetailAccountsAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccounts = await repository
                .GetByCriteriaAsync(det => det.FiscalPeriod.Id <= fpId
                    && det.Branch.Id == branchId);
            return detailAccounts
                .Select(det => _mapper.Map<KeyValue>(det))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetCostCentersAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<CostCenter>();
            var costCenters = await repository
                .GetByCriteriaAsync(cc => cc.FiscalPeriod.Id <= fpId
                    && cc.Branch.Id == branchId);
            return costCenters
                .Select(cc => _mapper.Map<KeyValue>(cc))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetProjectsAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Project>();
            var projects = await repository
                .GetByCriteriaAsync(prj => prj.FiscalPeriod.Id <= fpId
                    && prj.Branch.Id == branchId);
            return projects
                .Select(prj => _mapper.Map<KeyValue>(prj))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اسناد مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه اسناد مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetVouchersAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<Voucher>();
            var vouchers = await repository
                .GetByCriteriaAsync(voucher => voucher.FiscalPeriod.Id == fpId
                    && voucher.Branch.Id == branchId);
            return vouchers
                .Select(voucher => _mapper.Map<KeyValue>(voucher))
                .Apply(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی تعریف شده در دوره مالی و شعبه مشخص شده را
        /// به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه آرتیکل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetVoucherLinesAsync(int fpId, int branchId, GridOptions gridOptions = null)
        {
            var repository = _unitOfWork.GetAsyncRepository<VoucherLine>();
            var lines = await repository
                .GetByCriteriaAsync(line => line.FiscalPeriod.Id == fpId
                    && line.Branch.Id == branchId);
            return lines
                .Select(line => _mapper.Map<KeyValue>(line))
                .Apply(gridOptions);
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
            _unitOfWork.UseSystemContext();
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            var companies = new List<int>();
            if (user != null)
            {
                _unitOfWork.UseCompanyContext();
                var relatedRepository = _unitOfWork.GetAsyncRepository<RoleBranch>();
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.Role)
                        .ToArray(),
                    role =>
                    {
                        var roleBranchesModel = relatedRepository.GetByCriteria(
                            rb => rb.RoleId == role.Id, null, rb => rb.Branch);
                        companies.AddRange(
                            roleBranchesModel
                                .Select(rb => rb.Branch.CompanyId));
                    });
            }

            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<CompanyDb>();
            var userCompanies = await repository
                .GetEntityQuery()
                .Where(c => companies
                    .Distinct()
                    .Contains(c.Id))
                .Select(c => _mapper.Map<KeyValue>(c))
                .ToListAsync();
            _unitOfWork.UseCompanyContext();
            return userCompanies;
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
            _unitOfWork.UseSystemContext();
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                _unitOfWork.UseCompanyContext();
                var relatedRepository = _unitOfWork.GetAsyncRepository<RoleFiscalPeriod>();
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.Role)
                        .ToArray(),
                    role =>
                    {
                        var rolePeriodsModel = relatedRepository.GetByCriteria(
                            rfp => rfp.RoleId == role.Id, null, rfp => rfp.FiscalPeriod);
                        fiscalPeriods.AddRange(
                            rolePeriodsModel
                                .Select(rfp => rfp.FiscalPeriod)
                                .Where(fp => fp.CompanyId == companyId));
                    });
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
            _unitOfWork.UseSystemContext();
            var query = GetUserQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                _unitOfWork.UseCompanyContext();
                var relatedRepository = _unitOfWork.GetAsyncRepository<RoleBranch>();
                Array.ForEach(
                    user.UserRoles
                        .Select(ur => ur.Role)
                        .ToArray(),
                    role =>
                    {
                        var roleBranchesModel = relatedRepository.GetByCriteria(
                            rb => rb.RoleId == role.Id, null, rb => rb.Branch);
                        branches.AddRange(
                            roleBranchesModel
                                .Select(rb => rb.Branch)
                                .Where(br => br.CompanyId == companyId));
                    });
                branches = branches
                    .Distinct(new EntityEqualityComparer())
                    .Cast<Branch>()
                    .ToList();
            }

            return branches
                .OrderBy(br => br.Name)
                .Select(br => _mapper.Map<KeyValue>(br));
        }

        /// <summary>
        /// ماهیت های قابل استفاده در تعریف گروه های حساب را
        /// به صورت مجموعه ای از متن های چندزبانه برمی گرداند
        /// </summary>
        /// <returns>مجموعه ماهیت های قابل استفاده در تعریف گروه های حساب</returns>
        public IList<KeyValue> GetAccountGroupCategories()
        {
            var categories = new List<KeyValue>();
            var items = new string[]
            {
                "CategoryAsset", "CategoryAssociation", "CategoryCapital",
                "CategoryCoordination", "CategoryExpense", "CategoryIncome",
                "CategoryLiability", "CategoryPurchase", "CategorySales"
            };
            Array.ForEach(items, item => categories.Add(new KeyValue(item, item)));
            return categories;
        }

        /// <summary>
        /// به روش آسنکرون، گروه های حساب تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه گروه های حساب تعریف شده</returns>
        public async Task<IEnumerable<KeyValue>> GetAccountGroupsAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<AccountGroup>();
            return await repository
                .GetEntityQuery()
                .Select(grp => _mapper.Map<KeyValue>(grp))
                .ToListAsync();
        }

        #endregion

        #region Security Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، اطلاعات اشخاص تعریف شده در برنامه را به صورت یک دیکشنری
        /// که بر حسب شناسه دیتابیسی کاربر ایندکس شده برمی گرداند
        /// </summary>
        /// <returns>مجموعه اطلاعات کاربران موجود به صورت دیکشنری</returns>
        public async Task<IDictionary<int, string>> GetUserPersonsAsync()
        {
            var userPersons = new Dictionary<int, string>();
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var all = await repository
                .GetEntityQuery(user => user.Person)
                .Select(user => new KeyValuePair<int, string>(
                    user.Id, String.Format("{0}، {1}", user.Person.LastName, user.Person.FirstName)))
                .ToArrayAsync();
            Array.ForEach(all, kv => userPersons.Add(kv.Key, kv.Value));
            _unitOfWork.UseCompanyContext();
            return userPersons;
        }

        /// <summary>
        /// به روش آسنکرون، نقش های امنیتی تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه نقش های امنیتی تعریف شده</returns>
        public async Task<IList<KeyValue>> GetRolesAsync(GridOptions gridOptions = null)
        {
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<Role>();
            var roles = await repository
                .GetAllAsync();
            var lookup = roles
                .OrderBy(role => role.Name)
                .Select(role => _mapper.Map<KeyValue>(role))
                .Apply(gridOptions)
                .ToList();
            _unitOfWork.UseCompanyContext();
            return lookup;
        }

        #endregion

        #region Metadata Subsystem lookup

        /// <summary>
        /// به روش آسنکرون، موجودیت های تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های تعریف شده</returns>
        public async Task<IList<KeyValue>> GetEntityViewsAsync(GridOptions gridOptions = null)
        {
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<View>();
            var views = await repository
                .GetAllAsync();
            var lookup = views
                .Select(view => _mapper.Map<KeyValue>(view))
                .Apply(gridOptions)
                .ToList();
            _unitOfWork.UseCompanyContext();
            return lookup;
        }

        /// <summary>
        /// به روش آسنکرون، موجودیت های سلسله مراتبی (درختی) را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه موجودیت های درختی</returns>
        public async Task<IList<KeyValue>> GetTreeViewsAsync(GridOptions gridOptions = null)
        {
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<View>();
            var views = await repository
                .GetByCriteriaAsync(vu => vu.IsHierarchy);
            var lookup = views
                .Select(view => _mapper.Map<KeyValue>(view))
                .Apply(gridOptions)
                .ToList();
            _unitOfWork.UseCompanyContext();
            return lookup;
        }

        #endregion

        private IQueryable<User> GetUserQuery(int userId)
        {
            var repository = _unitOfWork.GetAsyncRepository<User>();
            var query = repository
                .GetEntityQuery()
                .Where(usr => usr.Id == userId)
                .Include(usr => usr.UserRoles)
                    .ThenInclude(ur => ur.Role);
            return query;
        }

        private IAppUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

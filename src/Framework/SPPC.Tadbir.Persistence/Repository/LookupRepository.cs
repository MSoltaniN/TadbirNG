using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Inventory;
using SPPC.Tadbir.Model.Procurement;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.ViewModel.Procurement;

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

        #region Asynchronous Methods

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
        /// به روش آسنکرون، دوره های مالی تعریف شده در یک شرکت مشخص شده را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه دوره های مالی تعریف شده در یک شرکت مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetFiscalPeriodsAsync(int companyId)
        {
            var repository = _unitOfWork.GetAsyncRepository<FiscalPeriod>();
            var fiscalPeriods = await repository
                .GetByCriteriaAsync(fp => fp.Company.Id == companyId);
            return fiscalPeriods
                .OrderBy(fp => fp.Name)
                .Select(fp => _mapper.Map<KeyValue>(fp));
        }

        /// <summary>
        /// به روش آسنکرون، شعب سازمانی تعریف شده در یک شرکت مشخص شده را به صورت مجموعه ای از
        /// کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه شعب سازمانی تعریف شده در یک شرکت مشخص شده</returns>
        public async Task<IEnumerable<KeyValue>> GetBranchesAsync(int companyId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Branch>();
            var branches = await repository
                .GetByCriteriaAsync(br => br.Company.Id == companyId);
            return branches
                .OrderBy(br => br.Name)
                .Select(br => _mapper.Map<KeyValue>(br));
        }

        #endregion

        #region Synchronous Methods (May be removed in the future)

        /// <summary>
        /// سرفصل های حسابداری تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه سرفصل های مالی تعریف شده در دوره و شعبه مشخص شده</returns>
        public IEnumerable<KeyValue> GetAccounts(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var accounts = repository
                .GetByCriteria(acc => acc.FiscalPeriod.Id == fpId
                    && acc.Branch.Id == branchId)
                .OrderBy(acc => acc.FullCode)
                .Select(acc => _mapper.Map<KeyValue>(acc));
            return accounts;
        }

        /// <summary>
        /// تفصیلی های شناور تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه تفصیلی های شناور تعریف شده در دوره و شعبه مشخص شده</returns>
        public IEnumerable<KeyValue> GetDetailAccounts(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<DetailAccount>();
            var detailAccounts = repository
                .GetByCriteria(det => det.FiscalPeriod.Id == fpId
                    && det.Branch.Id == branchId)
                .OrderBy(det => det.FullCode)
                .Select(det => _mapper.Map<KeyValue>(det));
            return detailAccounts;
        }

        /// <summary>
        /// مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه مراکز هزینه تعریف شده در دوره و شعبه مشخص شده</returns>
        public IEnumerable<KeyValue> GetCostCenters(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<CostCenter>();
            var costCenters = repository
                .GetByCriteria(cc => cc.FiscalPeriod.Id == fpId
                    && cc.Branch.Id == branchId)
                .OrderBy(cc => cc.FullCode)
                .Select(cc => _mapper.Map<KeyValue>(cc));
            return costCenters;
        }

        /// <summary>
        /// پروژه های تعریف شده در دوره مالی و شعبه مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه دیتابیسی یکی از شعب موجود</param>
        /// <returns>مجموعه پروژه های تعریف شده در دوره و شعبه مشخص شده</returns>
        public IEnumerable<KeyValue> GetProjects(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<Project>();
            var projects = repository
                .GetByCriteria(prj => prj.FiscalPeriod.Id == fpId
                    && prj.Branch.Id == branchId)
                .OrderBy(prj => prj.FullCode)
                .Select(prj => _mapper.Map<KeyValue>(prj));
            return projects;
        }

        /// <summary>
        /// ارزهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه ارز های تعریف شده</returns>
        public IEnumerable<KeyValue> GetCurrencies()
        {
            var repository = _unitOfWork.GetRepository<Currency>();
            var currencies = repository
                .GetAll()
                .OrderBy(curr => curr.Name)
                .Select(curr => _mapper.Map<KeyValue>(curr));
            return currencies;
        }

        /// <summary>
        /// دوره های مالی تعریف شده در یک شرکت مشخص شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی یکی از شرکت های موجود</param>
        /// <returns>مجموعه دوره های مالی تعریف شده در یک شرکت مشخص شده</returns>
        public IEnumerable<KeyValue> GetFiscalPeriods(int companyId)
        {
            var repository = _unitOfWork.GetRepository<FiscalPeriod>();
            var fiscalPeriods = repository
                .GetByCriteria(fp => fp.Company.Id == companyId)
                .OrderBy(fp => fp.Name)
                .Select(fp => _mapper.Map<KeyValue>(fp));
            return fiscalPeriods;
        }

        #endregion

        #endregion

        #region Inventory Subsystem Lookup

        /// <summary>
        /// انبارهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه انبارهای تعریف شده</returns>
        public IEnumerable<KeyValue> GetWarehouses()
        {
            var repository = _unitOfWork.GetRepository<Warehouse>();
            var warehouses = repository
                .GetAll()
                .OrderBy(wh => wh.Name)
                .Select(wh => _mapper.Map<KeyValue>(wh));
            return warehouses;
        }

        /// <summary>
        /// کالاهای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه کالاهای تعریف شده</returns>
        public IEnumerable<KeyValue> GetProducts()
        {
            var repository = _unitOfWork.GetRepository<Product>();
            var products = repository
                .GetAll()
                .OrderBy(p => p.Name)
                .Select(p => _mapper.Map<KeyValue>(p));
            return products;
        }

        /// <summary>
        /// واحدهای شمارش تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه واحدهای شمارش تعریف شده</returns>
        public IEnumerable<KeyValue> GetUnitsOfMeasurement()
        {
            var repository = _unitOfWork.GetRepository<UnitOfMeasurement>();
            var units = repository
                .GetAll()
                .OrderBy(uom => uom.Name)
                .Select(uom => _mapper.Map<KeyValue>(uom));
            return units;
        }

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک سطر موجودی کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات پایه مورد نیاز سطر موجودی کالا</returns>
        public InventoryDependsViewModel GetInventoryDepends()
        {
            var depends = new InventoryDependsViewModel();
            CopyCollection(GetUnitsOfMeasurement(), depends.Units);
            CopyCollection(GetProducts(), depends.Products);
            CopyCollection(GetWarehouses(), depends.Warehouses);
            return depends;
        }

        #endregion

        #region Procurement Subsystem Lookup

        /// <summary>
        /// انواع درخواست کالای تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه انواع درخواست کالای تعریف شده</returns>
        public IEnumerable<KeyValue> GetRequisitionVoucherTypes()
        {
            var repository = _unitOfWork.GetRepository<RequisitionVoucherType>();
            var voucherTypes = repository
                .GetAll()
                .OrderBy(rvt => rvt.Name)
                .Select(rvt => _mapper.Map<KeyValue>(rvt));
            return voucherTypes;
        }

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات پایه مورد نیاز درخواست کالا</returns>
        public VoucherDependsViewModel GetRequisitionDepends()
        {
            var depends = new VoucherDependsViewModel();
            CopyCollection(GetRequisitionVoucherTypes(), depends.VoucherTypes);
            CopyCollection(GetAccounts(1, 1), depends.Accounts);
            CopyCollection(GetDetailAccounts(1, 1), depends.DetailAccounts);
            CopyCollection(GetCostCenters(1, 1), depends.CostCenters);
            CopyCollection(GetProjects(1, 1), depends.Projects);
            CopyCollection(GetPartners(), depends.Partners);
            CopyCollection(GetBusinessUnits(), depends.Units);
            CopyCollection(GetWarehouses(), depends.Warehouses);
            return depends;
        }

        /// <summary>
        /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک سطر درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات پایه مورد نیاز سطر درخواست کالا</returns>
        public VoucherLineDependsViewModel GetRequisitionLineDepends()
        {
            var depends = new VoucherLineDependsViewModel();
            CopyCollection(GetAccounts(1, 1), depends.Accounts);
            CopyCollection(GetDetailAccounts(1, 1), depends.DetailAccounts);
            CopyCollection(GetCostCenters(1, 1), depends.CostCenters);
            CopyCollection(GetProjects(1, 1), depends.Projects);
            CopyCollection(GetProducts(), depends.Products);
            CopyCollection(GetUnitsOfMeasurement(), depends.Units);
            CopyCollection(GetWarehouses(), depends.Warehouses);
            return depends;
        }

        #endregion

        /// <summary>
        /// شرکای کاری تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه شرکای کاری تعریف شده</returns>
        public IEnumerable<KeyValue> GetPartners()
        {
            var repository = _unitOfWork.GetRepository<BusinessPartner>();
            var partners = repository
                .GetAll()
                .OrderBy(bp => bp.Name)
                .Select(bp => _mapper.Map<KeyValue>(bp));
            return partners;
        }

        /// <summary>
        /// واحدهای سازمانی تعریف شده را به صورت مجموعه ای از کلید و مقدار برمی گرداند
        /// </summary>
        /// <returns>مجموعه واحدهای سازمانی تعریف شده</returns>
        public IEnumerable<KeyValue> GetBusinessUnits()
        {
            var repository = _unitOfWork.GetRepository<BusinessUnit>();
            var units = repository
                .GetAll()
                .OrderBy(bu => bu.Name)
                .Select(bu => _mapper.Map<KeyValue>(bu));
            return units;
        }

        private static void CopyCollection(IEnumerable<KeyValue> source, IList<KeyValue> destination)
        {
            Array.ForEach(source.ToArray(), item => destination.Add(item));
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

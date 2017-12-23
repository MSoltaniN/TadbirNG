using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Provides repository operations for getting different types of key/value collections (lookups) from
    /// the underlying database.
    /// </summary>
    public class LookupRepository : ILookupRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public LookupRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all financial account items in the specified fiscal period as a collection of
        /// <see cref="KeyValue"/> objects. The key for each entry is the unique identifier of corresponding
        /// account in database.
        /// </summary>
        /// <param name="fpId">Unique identifier of an existing fiscal period</param>
        /// <param name="branchId">Unique identifier of the branch to look for accounts</param>
        /// <returns>Collection of all account items in the specified fiscal period.</returns>
        public IEnumerable<KeyValue> GetAccounts(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var accounts = repository
                .GetByCriteria(acc => acc.FiscalPeriod.Id == fpId
                    && acc.Branch.Id == branchId)
                .OrderBy(acc => acc.Code)
                .Select(acc => _mapper.Map<KeyValue>(acc));
            return accounts;
        }

        /// <summary>
        /// Retrieves all detail account objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding detail account in database.
        /// </summary>
        /// <returns>Collection of all detail account items.</returns>
        public IEnumerable<KeyValue> GetDetailAccounts()
        {
            var repository = _unitOfWork.GetRepository<DetailAccount>();
            var detailAccounts = repository
                .GetAll()
                .OrderBy(det => det.Name)
                .Select(det => _mapper.Map<KeyValue>(det));
            return detailAccounts;
        }

        /// <summary>
        /// Retrieves all cost center objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding cost center in database.
        /// </summary>
        /// <returns>Collection of all cost center items.</returns>
        public IEnumerable<KeyValue> GetCostCenters()
        {
            var repository = _unitOfWork.GetRepository<CostCenter>();
            var costCenters = repository
                .GetAll()
                .OrderBy(cc => cc.Name)
                .Select(cc => _mapper.Map<KeyValue>(cc));
            return costCenters;
        }

        /// <summary>
        /// Retrieves all project objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding project in database.
        /// </summary>
        /// <returns>Collection of all project items.</returns>
        public IEnumerable<KeyValue> GetProjects()
        {
            var repository = _unitOfWork.GetRepository<Project>();
            var projects = repository
                .GetAll()
                .OrderBy(prj => prj.Name)
                .Select(prj => _mapper.Map<KeyValue>(prj));
            return projects;
        }

        /// <summary>
        /// Retrieves all currency objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding currency in database.
        /// </summary>
        /// <returns>Collection of all currency items.</returns>
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
        /// Retrieves all fiscal period objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding fiscal period in data store.
        /// </summary>
        /// <returns>Collection of all fiscal period items.</returns>
        public IEnumerable<KeyValue> GetFiscalPeriods()
        {
            var repository = _unitOfWork.GetRepository<FiscalPeriod>();
            var fiscalPeriods = repository
                .GetAll()
                .OrderBy(fp => fp.Name)
                .Select(fp => _mapper.Map<KeyValue>(fp));
            return fiscalPeriods;
        }

        /// <summary>
        /// Retrieves all business partner objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding business partner in database.
        /// </summary>
        /// <returns>Collection of all business partner items.</returns>
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
        /// Retrieves all business unit objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding business unit in database.
        /// </summary>
        /// <returns>Collection of all business unit items.</returns>
        public IEnumerable<KeyValue> GetBusinessUnits()
        {
            var repository = _unitOfWork.GetRepository<BusinessUnit>();
            var units = repository
                .GetAll()
                .OrderBy(bu => bu.Name)
                .Select(bu => _mapper.Map<KeyValue>(bu));
            return units;
        }

        /// <summary>
        /// Retrieves all warehouse objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding warehouse in database.
        /// </summary>
        /// <returns>Collection of all warehouse items.</returns>
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
        /// Retrieves all product objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding product in data store.
        /// </summary>
        /// <returns>Collection of all product items.</returns>
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
        /// Retrieves all unit of measurement (UOM) objects as a collection of <see cref="KeyValue"/> objects.
        /// The key for each entry is the unique identifier of corresponding unit of measurement (UOM) in data store.
        /// </summary>
        /// <returns>Collection of all unit of measurement (UOM) items.</returns>
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
        /// Retrieves all requisition voucher type objects as a collection of <see cref="KeyValue"/> objects.
        /// The key for each entry is the unique identifier of corresponding requisition voucher type in database.
        /// </summary>
        /// <returns>Collection of all requisition voucher type items.</returns>
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
            CopyCollection(GetDetailAccounts(), depends.DetailAccounts);
            CopyCollection(GetCostCenters(), depends.CostCenters);
            CopyCollection(GetProjects(), depends.Projects);
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
            CopyCollection(GetDetailAccounts(), depends.DetailAccounts);
            CopyCollection(GetCostCenters(), depends.CostCenters);
            CopyCollection(GetProjects(), depends.Projects);
            CopyCollection(GetProducts(), depends.Products);
            CopyCollection(GetUnitsOfMeasurement(), depends.Units);
            CopyCollection(GetWarehouses(), depends.Warehouses);
            return depends;
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

        private static void CopyCollection(IEnumerable<KeyValue> source, IList<KeyValue> destination)
        {
            Array.ForEach(source.ToArray(), item => destination.Add(item));
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}

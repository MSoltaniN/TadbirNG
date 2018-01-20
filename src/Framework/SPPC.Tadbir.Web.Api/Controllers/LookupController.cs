using System;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public partial class LookupController : Controller
    {
        public LookupController(ILookupRepository repository)
        {
            _repository = repository;
        }

        #region Finance Subsystem API

        // GET: api/lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccountsLookup(int fpId, int branchId)
        {
            var accountLookup = _repository.GetAccounts(fpId, branchId);
            return Json(accountLookup);
        }

        // GET: api/lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public IActionResult GetDetailAccountsLookup(int fpId, int branchId)
        {
            var lookup = _repository.GetDetailAccounts(fpId, branchId);
            return Json(lookup);
        }

        // GET: api/lookup/costcenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public IActionResult GetCostCentersLookup(int fpId, int branchId)
        {
            var lookup = _repository.GetCostCenters(fpId, branchId);
            return Json(lookup);
        }

        // GET: api/lookup/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public IActionResult GetProjectsLookup(int fpId, int branchId)
        {
            var lookup = _repository.GetProjects(fpId, branchId);
            return Json(lookup);
        }

        // GET: api/lookup/currencies
        [Route(LookupApi.CurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public IActionResult GetCurrenciesLookup()
        {
            var currencyLookup = _repository.GetCurrencies();
            return Json(currencyLookup);
        }

        // GET: api/lookup/fps/company/{companyId:min(1)}
        [Route(LookupApi.CompanyFiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public IActionResult GetFiscalPeriodsLookup(int companyId)
        {
            var fiscalPeriodLookup = _repository.GetFiscalPeriods(companyId);
            return Json(fiscalPeriodLookup);
        }

        #endregion

        #region Inventory Subsystem API

        // GET: api/lookup/warehouses
        [Route(LookupApi.WarehousesUrl)]
        public IActionResult GetWarehousesLookup()
        {
            var lookup = _repository.GetWarehouses();
            return Json(lookup);
        }

        // GET: api/lookup/products
        [Route(LookupApi.ProductsUrl)]
        public IActionResult GetProductsLookup()
        {
            var lookup = _repository.GetProducts();
            return Json(lookup);
        }

        // GET: api/lookup/uoms
        [Route(LookupApi.UnitsOfMeasurementUrl)]
        public IActionResult GetUnitsOfMeasurementLookup()
        {
            var lookup = _repository.GetUnitsOfMeasurement();
            return Json(lookup);
        }

        // GET: api/lookup/invdepends
        [Route(LookupApi.ProductInventoryDependsUrl)]
        public IActionResult GetProductInventoryDependencies()
        {
            var depends = _repository.GetInventoryDepends();
            return Json(depends);
        }

        #endregion

        #region Procurement Subsystem API

        // GET: api/lookup/rvtypes
        [Route(LookupApi.RequisitionVoucherTypesUrl)]
        public IActionResult GetRequisitionVoucherTypesLookup()
        {
            var lookup = _repository.GetRequisitionVoucherTypes();
            return Json(lookup);
        }

        // GET: api/lookup/rvdepends
        [Route(LookupApi.RequisitionVoucherDependsUrl)]
        public IActionResult GetRequisitionVoucherDependencies()
        {
            var depends = _repository.GetRequisitionDepends();
            return Json(depends);
        }

        // GET: api/lookup/rvldepends
        [Route(LookupApi.RequisitionVoucherLineDependsUrl)]
        public IActionResult GetRequisitionVoucherLineDependencies()
        {
            var depends = _repository.GetRequisitionLineDepends();
            return Json(depends);
        }

        #endregion

        // GET: api/lookup/partners
        [Route(LookupApi.PartnersUrl)]
        public IActionResult GetPartnersLookup()
        {
            var lookup = _repository.GetPartners();
            return Json(lookup);
        }

        // GET: api/lookup/units
        [Route(LookupApi.UnitsUrl)]
        public IActionResult GetUnitsLookup()
        {
            var lookup = _repository.GetBusinessUnits();
            return Json(lookup);
        }

        private ILookupRepository _repository;
    }
}

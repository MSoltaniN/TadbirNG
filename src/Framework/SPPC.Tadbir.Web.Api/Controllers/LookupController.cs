using System;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class LookupController : Controller
    {
        public LookupController(ILookupRepository repository)
        {
            _repository = repository;
        }

        // GET: api/lookup/accounts/fp/{fpId:int}/branch/{branchId:int}
        [Route(LookupApi.FiscalPeriodBranchAccountsUrl)]
        //[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccountsLookup(int fpId, int branchId)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var accountLookup = _repository.GetAccounts(fpId, branchId);
            return Json(accountLookup);
        }

        // GET: api/lookup/faccounts
        [Route(LookupApi.DetailAccountsUrl)]
        public IActionResult GetDetailAccountsLookup()
        {
            var lookup = _repository.GetDetailAccounts();
            return Json(lookup);
        }

        // GET: api/lookup/costcenters
        [Route(LookupApi.CostCentersUrl)]
        public IActionResult GetCostCentersLookup()
        {
            var lookup = _repository.GetCostCenters();
            return Json(lookup);
        }

        // GET: api/lookup/projects
        [Route(LookupApi.ProjectsUrl)]
        public IActionResult GetProjectsLookup()
        {
            var lookup = _repository.GetProjects();
            return Json(lookup);
        }

        // GET: api/lookup/currencies
        [Route(LookupApi.CurrenciesUrl)]
        public IActionResult GetCurrenciesLookup()
        {
            var currencyLookup = _repository.GetCurrencies();
            return Json(currencyLookup);
        }

        // GET: api/lookup/fps
        [Route(LookupApi.FiscalPeriodsUrl)]
        public IActionResult GetFiscalPeriodsLookup()
        {
            var fiscalPeriodLookup = _repository.GetFiscalPeriods();
            return Json(fiscalPeriodLookup);
        }

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

        // GET: api/lookup/invdepends
        [Route(LookupApi.ProductInventoryDependsUrl)]
        public IActionResult GetProductInventoryDependencies()
        {
            var depends = _repository.GetInventoryDepends();
            return Json(depends);
        }

        private ILookupRepository _repository;
    }
}

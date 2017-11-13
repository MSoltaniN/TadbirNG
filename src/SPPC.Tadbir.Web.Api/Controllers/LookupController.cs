using System;
using System.Collections.Generic;
using System.Web.Http;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class LookupController : ApiController
    {
        public LookupController(ILookupRepository repository)
        {
            _repository = repository;
        }

        // GET: api/lookup/accounts/fp/{fpId:int}/branch/{branchId:int}
        [Route(LookupApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IHttpActionResult GetAccountsLookup(int fpId, int branchId)
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
        public IHttpActionResult GetDetailAccountsLookup()
        {
            var lookup = _repository.GetDetailAccounts();
            return Json(lookup);
        }

        // GET: api/lookup/costcenters
        [Route(LookupApi.CostCentersUrl)]
        public IHttpActionResult GetCostCentersLookup()
        {
            var lookup = _repository.GetCostCenters();
            return Json(lookup);
        }

        // GET: api/lookup/projects
        [Route(LookupApi.ProjectsUrl)]
        public IHttpActionResult GetProjectsLookup()
        {
            var lookup = _repository.GetProjects();
            return Json(lookup);
        }

        // GET: api/lookup/currencies
        [Route(LookupApi.CurrenciesUrl)]
        public IHttpActionResult GetCurrenciesLookup()
        {
            var currencyLookup = _repository.GetCurrencies();
            return Json(currencyLookup);
        }

        // GET: api/lookup/fps
        [Route(LookupApi.FiscalPeriodsUrl)]
        public IHttpActionResult GetFiscalPeriodsLookup()
        {
            var fiscalPeriodLookup = _repository.GetFiscalPeriods();
            return Json(fiscalPeriodLookup);
        }

        // GET: api/lookup/partners
        [Route(LookupApi.PartnersUrl)]
        public IHttpActionResult GetPartnersLookup()
        {
            var lookup = _repository.GetPartners();
            return Json(lookup);
        }

        // GET: api/lookup/units
        [Route(LookupApi.UnitsUrl)]
        public IHttpActionResult GetUnitsLookup()
        {
            var lookup = _repository.GetBusinessUnits();
            return Json(lookup);
        }

        // GET: api/lookup/warehouses
        [Route(LookupApi.WarehousesUrl)]
        public IHttpActionResult GetWarehousesLookup()
        {
            var lookup = _repository.GetWarehouses();
            return Json(lookup);
        }

        // GET: api/lookup/products
        [Route(LookupApi.ProductsUrl)]
        public IHttpActionResult GetProductsLookup()
        {
            var lookup = _repository.GetProducts();
            return Json(lookup);
        }

        // GET: api/lookup/uoms
        [Route(LookupApi.UnitsOfMeasurementUrl)]
        public IHttpActionResult GetUnitsOfMeasurementLookup()
        {
            var lookup = _repository.GetUnitsOfMeasurement();
            return Json(lookup);
        }

        // GET: api/lookup/rvtypes
        [Route(LookupApi.RequisitionVoucherTypesUrl)]
        public IHttpActionResult GetRequisitionVoucherTypesLookup()
        {
            var lookup = _repository.GetRequisitionVoucherTypes();
            return Json(lookup);
        }

        // GET: api/lookup/rvdepends
        [Route(LookupApi.RequisitionVoucherDependsUrl)]
        public IHttpActionResult GetRequisitionVoucherDependencies()
        {
            var depends = _repository.GetRequisitionDepends();
            return Json(depends);
        }

        // GET: api/lookup/rvldepends
        [Route(LookupApi.RequisitionVoucherLineDependsUrl)]
        public IHttpActionResult GetRequisitionVoucherLineDependencies()
        {
            var depends = _repository.GetRequisitionLineDepends();
            return Json(depends);
        }

        // GET: api/lookup/rvdepends
        [Route(LookupApi.ProductInventoryDependsUrl)]
        public IHttpActionResult GetProductInventoryDependencies()
        {
            var depends = _repository.GetInventoryDepends();
            return Json(depends);
        }

        private ILookupRepository _repository;
    }
}

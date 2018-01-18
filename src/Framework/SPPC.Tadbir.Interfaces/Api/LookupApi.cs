using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with entity lookup collections.
    /// </summary>
    public sealed class LookupApi
    {
        private LookupApi()
        {
        }

        /// <summary>
        /// API client URL for lookup collection of all accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccounts = "lookup/accounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsUrl = "lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all currencies
        /// </summary>
        public const string Currencies = "lookup/currencies";

        /// <summary>
        /// API server route URL for lookup collection of all currencies
        /// </summary>
        public const string CurrenciesUrl = "lookup/currencies";

        /// <summary>
        /// API client URL for lookup collection of all fiscal periods
        /// </summary>
        public const string FiscalPeriods = "lookup/fps";

        /// <summary>
        /// API server route URL for lookup collection of all fiscal periods
        /// </summary>
        public const string FiscalPeriodsUrl = "lookup/fps";

        /// <summary>
        /// API client URL for lookup collection of all floating (detail) accounts
        /// </summary>
        public const string DetailAccounts = "lookup/faccounts";

        /// <summary>
        /// API server route URL for lookup collection of all floating (detail) accounts
        /// </summary>
        public const string DetailAccountsUrl = "lookup/faccounts";

        /// <summary>
        /// API client URL for lookup collection of all cost centers
        /// </summary>
        public const string CostCenters = "lookup/costcenters";

        /// <summary>
        /// API server route URL for lookup collection of all cost centers
        /// </summary>
        public const string CostCentersUrl = "lookup/costcenters";

        /// <summary>
        /// API client URL for lookup collection of all projects
        /// </summary>
        public const string Projects = "lookup/projects";

        /// <summary>
        /// API server route URL for lookup collection of all projects
        /// </summary>
        public const string ProjectsUrl = "lookup/projects";

        /// <summary>
        /// API client URL for lookup collection of all business partners
        /// </summary>
        public const string Partners = "lookup/partners";

        /// <summary>
        /// API server route URL for lookup collection of all business partners
        /// </summary>
        public const string PartnersUrl = "lookup/partners";

        /// <summary>
        /// API client URL for lookup collection of all business units
        /// </summary>
        public const string Units = "lookup/units";

        /// <summary>
        /// API server route URL for lookup collection of all business units
        /// </summary>
        public const string UnitsUrl = "lookup/units";

        /// <summary>
        /// API client URL for lookup collection of all warehouses
        /// </summary>
        public const string Warehouses = "lookup/warehouses";

        /// <summary>
        /// API server route URL for lookup collection of all warehouses
        /// </summary>
        public const string WarehousesUrl = "lookup/warehouses";

        /// <summary>
        /// API client URL for lookup collection of all products
        /// </summary>
        public const string Products = "lookup/products";

        /// <summary>
        /// API server route URL for lookup collection of all products
        /// </summary>
        public const string ProductsUrl = "lookup/products";

        /// <summary>
        /// API client URL for lookup collection of all units of measurement
        /// </summary>
        public const string UnitsOfMeasurement = "lookup/uoms";

        /// <summary>
        /// API server route URL for lookup collection of all units of measurement
        /// </summary>
        public const string UnitsOfMeasurementUrl = "lookup/uoms";

        /// <summary>
        /// API client URL for lookup collections of all requisition voucher types
        /// </summary>
        public const string RequisitionVoucherTypes = "lookup/rvtypes";

        /// <summary>
        /// API server route URL for lookup collections of all requisition voucher types
        /// </summary>
        public const string RequisitionVoucherTypesUrl = "lookup/rvtypes";

        /// <summary>
        /// API client URL for lookup collections of all dependencies required by a requisition voucher
        /// </summary>
        public const string RequisitionVoucherDepends = "lookup/rvdepends";

        /// <summary>
        /// API server route URL for lookup collections of all dependencies required by a requisition voucher
        /// </summary>
        public const string RequisitionVoucherDependsUrl = "lookup/rvdepends";

        /// <summary>
        /// API client URL for lookup collections of all dependencies required by a requisition voucher line
        /// </summary>
        public const string RequisitionVoucherLineDepends = "lookup/rvldepends";

        /// <summary>
        /// API server route URL for lookup collections of all dependencies required by a requisition voucher line
        /// </summary>
        public const string RequisitionVoucherLineDependsUrl = "lookup/rvldepends";

        /// <summary>
        /// API client URL for lookup collections of all dependencies required by a product inventory
        /// </summary>
        public const string ProductInventoryDepends = "lookup/invdepends";

        /// <summary>
        /// API server route URL for lookup collections of all dependencies required by a product inventory
        /// </summary>
        public const string ProductInventoryDependsUrl = "lookup/invdepends";
    }
}

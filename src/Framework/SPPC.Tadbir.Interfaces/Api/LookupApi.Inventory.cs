using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public partial class LookupApi
    {
        #region Inventory Subsystem Resources

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
        /// API client URL for lookup collections of all dependencies required by a product inventory
        /// </summary>
        public const string ProductInventoryDepends = "lookup/invdepends";

        /// <summary>
        /// API server route URL for lookup collections of all dependencies required by a product inventory
        /// </summary>
        public const string ProductInventoryDependsUrl = "lookup/invdepends";

        #endregion
    }
}

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Inventory
{
    public partial class ProductCategory
    {
        /// <summary>
        /// Gets a collection of all product categories that are immediately below this item in the category hierarchy
        /// </summary>
        public IList<ProductCategory> Children { get; set; }
    }
}

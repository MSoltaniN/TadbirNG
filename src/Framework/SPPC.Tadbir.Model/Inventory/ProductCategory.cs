using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Inventory
{
    public partial class ProductCategory
    {
        public IList<ProductCategory> Children { get; set; }
    }
}

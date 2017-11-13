using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            InverseParent = new HashSet<ProductCategory>();
            Product = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string FullCode { get; set; }
        public string Name { get; set; }
        public short Level { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ProductCategory Parent { get; set; }
        public ICollection<ProductCategory> InverseParent { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}

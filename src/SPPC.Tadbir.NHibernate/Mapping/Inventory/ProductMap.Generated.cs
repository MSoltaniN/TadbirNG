// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 8:07:49 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Inventory;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="Product"/> entity class.
    /// </summary>
    public partial class ProductMap : ClassMap<Product>
    {
        /// <summary>
        /// Initializes a new instance of the ProductMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public ProductMap()
        {
            Schema("Inventory");
            Table("Product");
            Id(x => x.Id)
                .Column("ProductID")
                .GeneratedBy.Identity();
            Map(x => x.Code)
                .Length(64)
                .Not.Nullable();
            Map(x => x.Name)
                .Length(128)
                .Not.Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();
            Map(x => x.ModifiedDate);

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.Category)
                .Column("CategoryID")
                .Cascade.None()
                .Not.LazyLoad();
        }
    }
}

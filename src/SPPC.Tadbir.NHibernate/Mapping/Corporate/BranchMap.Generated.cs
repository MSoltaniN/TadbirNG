// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-15 2:58:07 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Corporate;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="Branch"/> entity class.
    /// </summary>
    public partial class BranchMap : ClassMap<Branch>
    {
        /// <summary>
        /// Initializes a new instance of the BranchMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public BranchMap()
        {
            Schema("Corporate");
            Table("Branch");
            Id(x => x.Id)
                .Column("BranchID")
                .GeneratedBy.Identity();
            Map(x => x.Name)
                .Length(128)
                .Not.Nullable();
            Map(x => x.Description)
                .Length(512)
                .Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();
            Map(x => x.ModifiedDate);

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.Company)
                .Column("CompanyID")
                .Not.LazyLoad()
                .Cascade.All();
            HasMany(x => x.FiscalPeriods)
                .KeyColumn("BranchID")
                .LazyLoad()
                .Cascade.All();
        }
    }
}

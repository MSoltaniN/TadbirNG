// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-17 10:52:37 AM
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
            Map(x => x.Level)
                .Not.Nullable();
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
                .Cascade.None();
            HasManyToMany(x => x.Roles)
                .Schema("Auth")
                .Table("RoleBranch")
                .ParentKeyColumn("BranchID")
                .ChildKeyColumn("RoleID")
                .Cascade.None().Inverse()
                .LazyLoad();
            References(x => x.Parent)
                .Column("ParentID")
                .Not.LazyLoad()
                .Cascade.None();
        }
    }
}

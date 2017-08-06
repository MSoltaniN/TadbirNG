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

using SPPC.Tadbir.Model.Corporate;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="BusinessUnit"/> entity class.
    /// </summary>
    public partial class BusinessUnitMap : ClassMap<BusinessUnit>
    {
        /// <summary>
        /// Initializes a new instance of the BusinessUnitMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public BusinessUnitMap()
        {
            Schema("Corporate");
            Table("BusinessUnit");
            Id(x => x.Id)
                .Column("UnitID")
                .GeneratedBy.Identity();
            Map(x => x.Name)
                .Length(64)
                .Not.Nullable();
            Map(x => x.Description)
                .Length(256)
                .Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();
            Map(x => x.ModifiedDate);

            MapReferences();
        }

        private void MapReferences()
        {
        }
    }
}

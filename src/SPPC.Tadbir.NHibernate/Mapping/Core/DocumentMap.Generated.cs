// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-03 7:01:06 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Core;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="Document"/> entity class.
    /// </summary>
    public partial class DocumentMap : ClassMap<Document>
    {
        /// <summary>
        /// Initializes a new instance of the DocumentMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public DocumentMap()
        {
            Schema("Core");
            Table("Document");
            Id(x => x.Id)
                .Column("DocumentID")
                .GeneratedBy.Identity();
            Map(x => x.EntityNo)
                .Length(64)
                .Not.Nullable();
            Map(x => x.No)
                .Column("[No]")
                .Length(64)
                .Not.Nullable();
            Map(x => x.OperationalStatus)
                .Length(64)
                .Not.Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();
            Map(x => x.ModifiedDate);

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.Type)
                .Column("TypeID")
                .Cascade.None()
                .Not.LazyLoad();
            References(x => x.Status)
                .Column("StatusID")
                .Cascade.None()
                .Not.LazyLoad();
            HasMany(x => x.Actions)
                .KeyColumn("DocumentID")
                .LazyLoad()
                .Inverse()
                .Cascade.All();
        }
    }
}

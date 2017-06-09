// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-04-27 12:15:21 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Model.Workflow;
using FluentNHibernate.Mapping;

namespace SPPC.Tadbir.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="WorkItemDocument"/> entity class.
    /// </summary>
    public partial class WorkItemDocumentMap : ClassMap<WorkItemDocument>
    {
        /// <summary>
        /// Initializes a new instance of the WorkItemDocumentMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public WorkItemDocumentMap()
        {
            Schema("Workflow");
            Table("WorkItemDocument");
            Id(x => x.Id)
                .Column("DocumentItemID")
                .GeneratedBy.Identity();
            Map(x => x.DocumentId)
                .Not.Nullable();
            Map(x => x.DocumentType)
                .Length(128)
                .Not.Nullable();
            Map(x => x.RowGuid, "rowguid")
                .Generated.Insert();
            Map(x => x.ModifiedDate);

            MapReferences();
        }

        private void MapReferences()
        {
            References(x => x.WorkItem)
                .Column("WorkItemID")
                .Not.LazyLoad()
                .Cascade.None();
        }
    }
}

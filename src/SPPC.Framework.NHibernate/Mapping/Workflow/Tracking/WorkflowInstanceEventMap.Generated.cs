// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-07-08 4:27:50 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using FluentNHibernate.Mapping;
using SPPC.Framework.Model.Workflow.Tracking;

namespace SPPC.Framework.NHibernate.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="WorkflowInstanceEvent"/> entity class.
    /// </summary>
    public partial class WorkflowInstanceEventMap : ClassMap<WorkflowInstanceEvent>
    {
        /// <summary>
        /// Initializes a new instance of the WorkflowInstanceEventMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public WorkflowInstanceEventMap()
        {
            Schema("Workflow.Tracking");
            Table("WorkflowInstanceEvent");
            Id(x => x.Id)
                .Column("EventID")
                .GeneratedBy.Identity();
            Map(x => x.WorkflowInstanceId)
                .Not.Nullable();
            Map(x => x.ActivityDefinition)
                .Length(256)
                .Nullable();
            Map(x => x.RecordNumber)
                .Not.Nullable();
            Map(x => x.State)
                .Length(128)
                .Nullable();
            Map(x => x.TraceLevelId)
                .Nullable();
            Map(x => x.Reason)
                .Length(2048)
                .Nullable();
            Map(x => x.ExceptionDetails)
                .Length(65536)
                .Nullable();
            Map(x => x.SerializedAnnotations)
                .Length(65536)
                .Nullable();
            Map(x => x.TimeCreated)
                .Not.Nullable();
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

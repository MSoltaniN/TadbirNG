// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-07-08 4:28:12 PM
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
    /// Defines fluent mappings for the <see cref="CustomTrackingEvent"/> entity class.
    /// </summary>
    public partial class CustomTrackingEventMap : ClassMap<CustomTrackingEvent>
    {
        /// <summary>
        /// Initializes a new instance of the CustomTrackingEventMap class and specifies NHibernate mappings
        /// using Fluent NHibernate API.
        /// </summary>
        public CustomTrackingEventMap()
        {
            Schema("Workflow.Tracking");
            Table("CustomTrackingEvent");
            Id(x => x.Id)
                .Column("EventID")
                .GeneratedBy.Identity();
            Map(x => x.WorkflowInstanceId)
                .Not.Nullable();
            Map(x => x.RecordNumber)
                .Nullable();
            Map(x => x.TraceLevelId)
                .Nullable();
            Map(x => x.CustomRecordName)
                .Length(2048)
                .Nullable();
            Map(x => x.ActivityName)
                .Length(1024)
                .Nullable();
            Map(x => x.ActivityId)
                .Length(256)
                .Nullable();
            Map(x => x.ActivityInstanceId)
                .Length(256)
                .Nullable();
            Map(x => x.ActivityType)
                .Length(2048)
                .Nullable();
            Map(x => x.SerializedData)
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

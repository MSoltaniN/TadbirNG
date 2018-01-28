// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-07-08 4:28:01 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Framework.Model.Workflow.Tracking;

namespace SPPC.Workflow.Persistence.Mapping
{
    /// <summary>
    /// Defines fluent mappings for the <see cref="ExtendedActivityEvent"/> entity class.
    /// </summary>
    internal sealed class ExtendedActivityEventMap
    {
        private ExtendedActivityEventMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<ExtendedActivityEvent> builder)
        {
            builder.ToTable("ExtendedActivityEvent", "WFTracking");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("EventID");
            builder.Property(e => e.ActivityId)
                .HasMaxLength(256);
            builder.Property(e => e.ActivityInstanceId)
                .HasMaxLength(256);
            builder.Property(e => e.ActivityName)
                .HasMaxLength(1024);
            builder.Property(e => e.ActivityRecordType)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.ActivityType)
                .HasMaxLength(2048);
            builder.Property(e => e.ChildActivityId)
                .HasMaxLength(256);
            builder.Property(e => e.ChildActivityInstanceId)
                .HasMaxLength(256);
            builder.Property(e => e.ChildActivityName)
                .HasMaxLength(1024);
            builder.Property(e => e.ChildActivityType)
                .HasMaxLength(2048);
            builder.Property(e => e.FaultDetails)
                .HasColumnType("nvarchar(MAX)");
            builder.Property(e => e.FaultHandlerActivityId)
                .HasMaxLength(256);
            builder.Property(e => e.FaultHandlerActivityInstanceId)
                .HasMaxLength(256);
            builder.Property(e => e.FaultHandlerActivityName)
                .HasMaxLength(1024);
            builder.Property(e => e.FaultHandlerActivityType)
                .HasMaxLength(2048);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.SerializedAnnotations)
                .HasColumnType("nvarchar(MAX)");
            builder.Property(e => e.TimeCreated)
                .HasColumnType("datetime");
        }
    }
}

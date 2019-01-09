// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.469
//     Template Version: 1.0
//     Generation Date: 2018-12-12 4:40:33 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Reporting;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class CoreReportMap
    {
        private CoreReportMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<CoreReport> builder)
        {
            builder.ToTable("CoreReport", "Reporting");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CoreReportID");
            builder.Property(e => e.ParentId)
                .IsRequired(false);
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.IsGroup)
                .IsRequired();
            builder.Property(e => e.Template);
            builder.Property(e => e.TemplateLtr);
            builder.Property(e => e.ResourceKeys);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_CoreReport_Reporting_Parent");
        }
    }
}

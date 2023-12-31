// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.550
//     Template Version: 1.0
//     Generation Date: 01/31/1398 02:46:15 ب.ظ
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
    internal static class ReportMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Report", "Reporting");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ReportID");
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.ServiceUrl)
                .HasMaxLength(256);
            builder.Property(e => e.IsGroup)
                .IsRequired();
            builder.Property(e => e.IsSystem)
                .IsRequired();
            builder.Property(e => e.IsDefault)
                .IsRequired();
            builder.Property(e => e.IsDynamic)
                .IsRequired();            
            builder.Property(e => e.ResourceKeys);
            builder.Property(e => e.SubsystemId)
                .IsRequired();
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
                .HasConstraintName("FK_Reporting_Report_Reporting_Parent");
            builder.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_Report_Auth_CreatedBy");
            builder.HasOne(e => e.View)
                .WithMany()
                .HasForeignKey(e => e.ViewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_Report_Metadata_ReportView");
        }
    }
}

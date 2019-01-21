// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.485
//     Template Version: 1.0
//     Generation Date: 2019-01-09 4:37:25 PM
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
    internal sealed class ParameterMap
    {
        private ParameterMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Parameter> builder)
        {
            builder.ToTable("Parameter", "Reporting");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ParamID");
            builder.Property(e => e.FieldName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Operator)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.DataType)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ControlType)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.CaptionKey)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.DefaultValue)
                .HasMaxLength(64);
            builder.Property(e => e.MinValue)
                .HasMaxLength(64);
            builder.Property(e => e.MaxValue)
                .HasMaxLength(64);
            builder.Property(e => e.DescriptionKey)
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Report)
                .WithMany(d => d.Parameters)
                .HasForeignKey(e => e.ReportId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reporting_Parameter_Reporting_Report");
        }
    }
}
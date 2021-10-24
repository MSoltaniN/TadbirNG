// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.823
//     Template Version: 1.0
//     Generation Date: 11/29/1398 03:49:57 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class SysLogSettingMap
    {
        internal static void BuildMapping(EntityTypeBuilder<SysLogSetting> builder)
        {
            builder.ToTable("SysLogSetting", "Config");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("SysLogSettingID");
            builder.Property(e => e.IsEnabled)
                .IsRequired();
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Source)
                .WithMany()
                .HasForeignKey(e => e.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_LogSetting_Metadata_Source");
            builder.HasOne(e => e.EntityType)
                .WithMany()
                .HasForeignKey(e => e.EntityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_LogSetting_Metadata_EntityType");
            builder.HasOne(e => e.Operation)
                .WithMany()
                .HasForeignKey(e => e.OperationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_LogSetting_Metadata_Operation")
                .IsRequired();
        }
    }
}
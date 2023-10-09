// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.338
//     Template Version: 1.0
//     Generation Date: 2018-07-11 3:20:59 PM
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
    internal static class SettingMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Setting", "Config");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("SettingID");
            builder.Property(e => e.Subsystem)
                .HasMaxLength(32);
            builder.Property(e => e.TitleKey)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.Type)
                .IsRequired();
            builder.Property(e => e.ScopeType)
                .IsRequired();
            builder.Property(e => e.ModelType)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.Values)
                .IsRequired()
                .HasMaxLength(2048);
            builder.Property(e => e.DefaultValues)
                .IsRequired()
                .HasMaxLength(2048);
            builder.Property(e => e.DescriptionKey)
                .HasMaxLength(1028);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey("ParentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_Setting_Config_Parent");
        }
    }
}

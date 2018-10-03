// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.415
//     Template Version: 1.0
//     Generation Date: 2018-10-03 4:20:28 PM
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
    internal static class ViewSettingMap
    {
        internal static void BuildMapping(EntityTypeBuilder<ViewSetting> builder)
        {
            builder.ToTable("ViewSetting", "Config");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ViewSettingID");
            builder.Property(e => e.SettingId)
                .IsRequired();
            builder.Property(e => e.ViewId)
                .IsRequired();
            builder.Property(e => e.ModelType)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.Values)
                .IsRequired();
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Setting)
                .WithMany()
                .HasForeignKey(e => e.SettingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_ViewSetting_Config_Setting");
            builder.HasOne(e => e.View)
                .WithMany()
                .HasForeignKey(e => e.ViewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_ViewSetting_Config_View");
        }
    }
}

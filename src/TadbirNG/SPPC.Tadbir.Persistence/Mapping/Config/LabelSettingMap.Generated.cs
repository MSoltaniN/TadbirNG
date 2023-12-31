// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.1049
//     Template Version: 1.0
//     Generation Date: 2020-12-29 6:30:02 PM
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
    internal static class LabelSettingMap
    {
        internal static void BuildMapping(EntityTypeBuilder<LabelSetting> builder)
        {
            builder.ToTable("LabelSetting", "Config");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("LabelSettingID");
            builder.Property(e => e.ModelType)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.Values)
                .IsRequired();
            builder.Property(e => e.DefaultValues)
                .IsRequired();
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Setting)
                .WithMany()
                .HasForeignKey("SettingID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_LabelSetting_Config_Setting");
            builder.HasOne(e => e.CustomForm)
                .WithMany()
                .HasForeignKey("CustomFormID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_LabelSetting_Metadata_CustomForm");
        }
    }
}

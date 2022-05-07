// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1355
//     Template Version: 1.0
//     Generation Date: 2022-04-08 10:23:15 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class ValidRowPermissionMap
    {
        internal static void BuildMapping(EntityTypeBuilder<ValidRowPermission> builder)
        {
            builder.ToTable("ValidRowPermission", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ValidRowPermissionID");
            builder.Property(e => e.AccessMode)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.View)
                .WithMany()
                .HasForeignKey("ViewID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Metadata_ValidRowPermission_Metadata_View");
        }
    }
}

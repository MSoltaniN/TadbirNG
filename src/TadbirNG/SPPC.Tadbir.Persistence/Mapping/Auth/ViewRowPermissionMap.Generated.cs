// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.343
//     Template Version: 1.0
//     Generation Date: 2018-07-17 8:20:21 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class ViewRowPermissionMap
    {
        private ViewRowPermissionMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<ViewRowPermission> builder)
        {
            builder.ToTable("ViewRowPermission", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("RowPermissionID");
            builder.Property(e => e.AccessMode)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Value);
            builder.Property(e => e.Value2);
            builder.Property(e => e.TextValue)
                .HasMaxLength(64);
            builder.Property(e => e.Items)
                .HasMaxLength(2048);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auth_ViewRowPermission_Auth_Role");
            builder.HasOne(e => e.View)
                .WithMany()
                .HasForeignKey(e => e.ViewID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auth_ViewRowPermission_Metadata_View");
        }
    }
}
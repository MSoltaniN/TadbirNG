using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class PermissionGroupMap
    {
        private PermissionGroupMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<PermissionGroup> builder)
        {
            builder.ToTable("PermissionGroup", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PermissionGroupID");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.EntityName)
                .HasMaxLength(64);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}

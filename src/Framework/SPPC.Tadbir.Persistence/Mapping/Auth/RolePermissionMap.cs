using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping.Auth
{
    internal sealed class RolePermissionMap
    {
        private RolePermissionMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission", "Auth");
            builder.HasKey(e => new { e.RoleId, e.PermissionId });

            builder.HasOne(d => d.Role)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId);
            builder.HasOne(d => d.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId);
        }
    }
}

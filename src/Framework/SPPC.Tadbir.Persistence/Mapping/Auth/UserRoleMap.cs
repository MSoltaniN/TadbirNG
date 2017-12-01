using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class UserRoleMap
    {
        private UserRoleMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole", "Auth");
            builder.HasKey(e => new { e.UserId, e.RoleId });

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId);
            builder.HasOne(d => d.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId);
        }
    }
}

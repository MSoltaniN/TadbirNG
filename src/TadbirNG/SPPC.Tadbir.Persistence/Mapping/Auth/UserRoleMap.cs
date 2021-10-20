using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class UserRoleMap
    {
        internal static void BuildMapping(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("UserRoleID");
            builder.HasAlternateKey(e => new { e.UserId, e.RoleId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId);
            builder.HasOne(d => d.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId);
        }
    }
}

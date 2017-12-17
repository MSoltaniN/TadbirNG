using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class RoleBranchMap
    {
        private RoleBranchMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<RoleBranch> builder)
        {
            builder.ToTable("RoleBranch", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("RoleBranchID");
            builder.HasAlternateKey(e => new { e.RoleId, e.BranchId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.RoleBranches)
                .HasForeignKey(d => d.RoleId);
            builder.HasOne(d => d.Branch)
                .WithMany(p => p.RoleBranches)
                .HasForeignKey(d => d.BranchId);
        }
    }
}

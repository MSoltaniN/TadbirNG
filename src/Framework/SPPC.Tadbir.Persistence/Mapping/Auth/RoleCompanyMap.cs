using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class RoleCompanyMap
    {
        internal static void BuildMapping(EntityTypeBuilder<RoleCompany> builder)
        {
            builder.ToTable("RoleCompany", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("RoleCompanyID");
            builder.HasAlternateKey(e => new { e.RoleId, e.CompanyId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Company)
                .WithMany(p => p.RoleCompanies)
                .HasForeignKey(d => d.CompanyId);
            builder.HasOne(d => d.Role)
                .WithMany(p => p.RoleCompanies)
                .HasForeignKey(d => d.RoleId);
        }
    }
}

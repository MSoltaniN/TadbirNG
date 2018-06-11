using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class RoleFiscalPeriodMap
    {
        internal static void BuildMapping(EntityTypeBuilder<RoleFiscalPeriod> builder)
        {
            builder.ToTable("RoleFiscalPeriod", "Auth");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("RoleFiscalPeriodID");
            builder.HasAlternateKey(e => new { e.RoleId, e.FiscalPeriodId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.RoleFiscalPeriods)
                .HasForeignKey(d => d.RoleId);
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany(p => p.RoleFiscalPeriods)
                .HasForeignKey(d => d.FiscalPeriodId);
        }
    }
}

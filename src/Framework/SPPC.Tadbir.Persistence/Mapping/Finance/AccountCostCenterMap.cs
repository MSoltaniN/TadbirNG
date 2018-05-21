using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class AccountCostCenterMap
    {
        private AccountCostCenterMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<AccountCostCenter> builder)
        {
            builder.ToTable("AccountCostCenter", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountCostCenterID");
            builder.HasAlternateKey(e => new { e.AccountId, e.CostCenterId });
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithMany(p => p.AccountCostCenters)
                .HasForeignKey(d => d.AccountId);
            builder.HasOne(d => d.CostCenter)
                .WithMany(p => p.AccountCostCenters)
                .HasForeignKey(d => d.CostCenterId);
        }
    }
}

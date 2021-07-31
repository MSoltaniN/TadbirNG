using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping.Finance
{
    internal static class InactiveAccountMap
    {
        internal static void BuildMapping(EntityTypeBuilder<InactiveAccount> builder)
        {
            builder.ToTable("InactiveAccount", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("InactiveAccountID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithMany()
                .HasForeignKey(d => d.AccountId);
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey(d => d.FiscalPeriodId);
        }
    }
}

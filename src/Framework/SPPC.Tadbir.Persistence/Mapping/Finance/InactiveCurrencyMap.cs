using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping.Finance
{
    internal static class InactiveCurrencyMap
    {
        internal static void BuildMapping(EntityTypeBuilder<InactiveCurrency> builder)
        {
            builder.ToTable("InactiveCurrency", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("InactiveCurrencyID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Currency)
                .WithMany()
                .HasForeignKey(d => d.CurrencyId);
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey(d => d.FiscalPeriodId);
        }
    }
}

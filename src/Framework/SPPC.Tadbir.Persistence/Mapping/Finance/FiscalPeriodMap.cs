using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class FiscalPeriodMap
    {
        internal static void BuildMapping(EntityTypeBuilder<FiscalPeriod> builder)
        {
            builder.ToTable("FiscalPeriod", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("FiscalPeriodID");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.EndDate)
                .HasColumnType("datetime");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.StartDate)
                .HasColumnType("datetime");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class VoucherMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("VoucherID");
            builder.Property(e => e.No)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Date)
                .HasColumnType("datetime");
            builder.Property(e => e.Reference)
                .HasMaxLength(64);
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Corporate_Branch");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey(e => e.FiscalPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Finance_FiscalPeriod");
        }
    }
}

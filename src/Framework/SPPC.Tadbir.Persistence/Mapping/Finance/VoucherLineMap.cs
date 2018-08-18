using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class VoucherLineMap
    {
        internal static void BuildMapping(EntityTypeBuilder<VoucherLine> builder)
        {
            builder.ToTable("VoucherLine", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("LineID");
            builder.Property(e => e.Credit)
                .HasColumnType("money");
            builder.Property(e => e.Debit)
                .HasColumnType("money");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Account)
                .WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_VoucherLine_Finance_Account");
            builder.HasOne(d => d.DetailAccount)
                .WithMany()
                .HasForeignKey(d => d.DetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_VoucherLine_Finance_DetailAccount");
            builder.HasOne(d => d.CostCenter)
                .WithMany()
                .HasForeignKey(d => d.CostCenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_VoucherLine_Finance_CostCenter");
            builder.HasOne(d => d.Project)
                .WithMany()
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_VoucherLine_Finance_Project");
            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_VoucherLine_Corporate_Branch");
            builder.HasOne(d => d.Currency)
                .WithMany()
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_VoucherLine_Finance_Currency");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey(e => e.FiscalPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_VoucherLine_Finance_FiscalPeriod");
            builder.HasOne(d => d.Voucher)
                .WithMany(p => p.Lines)
                .HasForeignKey("VoucherID")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Finance_VoucherLine_Finance_Voucher");
        }
    }
}

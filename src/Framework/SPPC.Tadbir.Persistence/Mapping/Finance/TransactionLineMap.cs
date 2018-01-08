using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class TransactionLineMap
    {
        private TransactionLineMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<TransactionLine> builder)
        {
            builder.ToTable("TransactionLine", "Finance");
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
                .WithMany(p => p.TransactionLines)
                .HasForeignKey("AccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_TransactionLine_Finance_Account");
            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_TransactionLine_Corporate_Branch");
            builder.HasOne(d => d.Currency)
                .WithMany(p => p.TransactionLines)
                .HasForeignKey("CurrencyID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_TransactionLine_Finance_Currency");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_TransactionLine_Finance_FiscalPeriod");
            builder.HasOne(d => d.Transaction)
                .WithMany(p => p.Lines)
                .HasForeignKey("TransactionID")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Finance_TransactionLine_Finance_Transaction");
        }
    }
}

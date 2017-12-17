using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class TransactionMap
    {
        private TransactionMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("TransactionID");
            builder.Property(e => e.Date)
                .HasColumnType("datetime");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.No)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Transaction_Corporate_Branch");
            builder.HasOne(d => d.Document)
                .WithMany()
                .HasForeignKey("DocumentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Transaction_Core_Document");
            //builder.HasOne(d => d.FiscalPeriod)
            //    .WithMany()
            //    .HasForeignKey("FiscalPeriodID")
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Finance_Transaction_Finance_FiscalPeriod");
        }
    }
}

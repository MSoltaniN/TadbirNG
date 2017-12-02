using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class AccountMap
    {
        private AccountMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountID");
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Account_Corporate_Branch");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany(p => p.Accounts)
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Account_Finance_FiscalPeriod");
        }
    }
}

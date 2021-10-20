using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class DetailAccountMap
    {
        internal static void BuildMapping(EntityTypeBuilder<DetailAccount> builder)
        {
            builder.ToTable("DetailAccount", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("DetailAccountID");
            builder.Property(e => e.BranchScope)
                .IsRequired();
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.FullCode)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.Level)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentId)
                .HasConstraintName("FK_Finance_DetailAccount_Finance_Parent");
            builder.HasOne(d => d.Currency)
                .WithMany()
                .HasForeignKey(e => e.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_DetailAccount_Finance_Currency");
            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_DetailAccount_Corporate_Branch");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey(e => e.FiscalPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_DetailAccount_Finance_FiscalPeriod");
        }
    }
}

// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.655
//     Template Version: 1.0
//     Generation Date: 04/22/1398 03:58:02 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class CurrencyMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currency", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CurrencyID");
            builder.Property(e => e.BranchScope)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(8);
            builder.Property(e => e.TaxCode)
                .IsRequired();
            builder.Property(e => e.MinorUnit)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.Multiplier)
                .IsRequired();
            builder.Property(e => e.DecimalCount)
                .IsRequired();
            builder.Property(e => e.IsActive)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Currency_Corporate_Branch");
        }
    }
}

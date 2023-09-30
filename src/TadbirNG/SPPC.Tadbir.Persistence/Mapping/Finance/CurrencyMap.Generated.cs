// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1557
//     Template Version: 1.0
//     Generation Date: 8/1/2023 5:11:46 PM
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
                .HasColumnName("CurrencyId");
            builder.Property(e => e.CreatedById)
                .IsRequired();
            builder.Property(e => e.CreatedByName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.CreatedDate)
                .IsRequired();
            builder.Property(e => e.ModifiedById)
                .IsRequired();
            builder.Property(e => e.ModifiedByName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.BranchScope)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(8);
            builder.Property(e => e.TaxCode)
                .IsRequired();
            builder.Property(e => e.MinorUnit)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.DecimalCount)
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

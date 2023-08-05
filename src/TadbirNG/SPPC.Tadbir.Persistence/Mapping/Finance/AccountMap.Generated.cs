// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1557
//     Template Version: 1.0
//     Generation Date: 8/2/2023 9:28:21 AM
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
    internal static class AccountMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("AccountID");
            builder.Property(e => e.GroupId)
                .IsRequired(false);
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
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.FullCode)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(e => e.Level)
                .IsRequired();
            builder.Property(e => e.IsActive)
                .IsRequired();
            builder.Property(e => e.IsCurrencyAdjustable)
                .IsRequired();
            builder.Property(e => e.TurnoverMode)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Account_Finance_Parent");
            builder.HasOne(e => e.FiscalPeriod)
                .WithMany()
                .HasForeignKey(e => e.FiscalPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Account_Finance_FiscalPeriod");
            builder.HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Account_Corporate_Branch");
            builder.HasOne(e => e.Group)
                .WithMany()
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Account_Finance_Group");

            builder.HasOne(d => d.CustomerTaxInfo)
                .WithOne(p => p.Account)
                .HasForeignKey<CustomerTaxInfo>("AccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_CustomerTaxInfo_Finance_Account");

            builder.HasOne(d => d.AccountOwner)
                .WithOne(p => p.Account)
                .HasForeignKey<AccountOwner>("AccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_AccountOwner_Finance_Account");
        }
    }
}

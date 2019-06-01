// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.584
//     Template Version: 1.0
//     Generation Date: 02/28/1398 04:20:49 ب.ظ
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
    internal static class VoucherMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("VoucherID");
            builder.Property(e => e.No)
                .IsRequired();
            builder.Property(e => e.DailyNo)
                .IsRequired();
            builder.Property(e => e.Date)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(e => e.Reference)
                .HasMaxLength(64);
            builder.Property(e => e.Association)
                .HasMaxLength(64);
            builder.Property(e => e.IsBalanced)
                .IsRequired();
            builder.Property(e => e.Type)
                .IsRequired();
            builder.Property(e => e.SubjectType)
                .IsRequired();
            builder.Property(e => e.SaveCount)
                .IsRequired();
            builder.Property(e => e.StatusId)
                .HasColumnName("StatusID")
                .IsRequired();
            builder.Property(e => e.IssuedById)
                .HasColumnName("IssuedByID")
                .IsRequired();
            builder.Property(e => e.ModifiedById)
                .HasColumnName("ModifiedByID")
                .IsRequired();
            builder.Property(e => e.ConfirmedById)
                .HasColumnName("ConfirmedByID");
            builder.Property(e => e.ApprovedById)
                .HasColumnName("ApprovedByID");
            builder.Property(e => e.IssuerName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ModifierName)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ConfirmerName)
                .HasMaxLength(64);
            builder.Property(e => e.ApproverName)
                .HasMaxLength(64);
            builder.Property(e => e.Description)
                .HasMaxLength(1024);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.FiscalPeriod)
                .WithMany()
                .HasForeignKey(e => e.FiscalPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Finance_FiscalPeriod");
            builder.HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Corporate_Branch");
            builder.HasOne(e => e.Document)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Core_Document");
            builder.HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Core_Status");
            builder.HasOne(e => e.IssuedBy)
                .WithMany()
                .HasForeignKey(e => e.IssuedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Auth_IssuedBy");
            builder.HasOne(e => e.ModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.ModifiedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Auth_ModifiedBy");
            builder.HasOne(e => e.ConfirmedBy)
                .WithMany()
                .HasForeignKey(e => e.ConfirmedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Auth_ConfirmedBy");
            builder.HasOne(e => e.ApprovedBy)
                .WithMany()
                .HasForeignKey(e => e.ApprovedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Voucher_Auth_ApprovedBy");
        }
    }
}

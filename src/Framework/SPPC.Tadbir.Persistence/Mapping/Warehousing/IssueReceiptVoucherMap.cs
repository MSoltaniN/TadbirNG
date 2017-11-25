using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Warehousing;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class IssueReceiptVoucherMap
    {
        private IssueReceiptVoucherMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<IssueReceiptVoucher> builder)
        {
            builder.ToTable("IssueReceiptVoucher", "Warehousing");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("VoucherID");
            builder.Property(e => e.Description)
                .HasMaxLength(256);
            builder.Property(e => e.IsActive)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.No)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Reference)
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.Timestamp)
                .IsRequired()
                .IsRowVersion();

            builder.HasOne(d => d.ActingPartner)
                .WithMany()
                .HasForeignKey("ActingPartnerID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Contact_ActingPartner");
            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Corporate_Branch");
            builder.HasOne(d => d.Document)
                .WithMany()
                .HasForeignKey("DocumentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Core_Document");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Finance_FiscalPeriod");
            builder.HasOne(d => d.PartnerFullAccount)
                .WithMany()
                .HasForeignKey("PartnerFullAccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Finance_PartnerFullAccount");
            builder.HasOne(d => d.PartnerFullDetail)
                .WithMany()
                .HasForeignKey("PartnerFullDetailID")
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Finance_PartnerFullDetail");
            builder.HasOne(d => d.PricedVoucher)
                .WithMany()
                .HasForeignKey("PricedVoucherID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Warehousing_PricedVoucher");
            builder.HasOne(d => d.Warehouse)
                .WithMany()
                .HasForeignKey("WarehouseID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Inventory_Warehouse");
        }
    }
}

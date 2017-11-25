using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Procurement;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class RequisitionVoucherLineMap
    {
        private RequisitionVoucherLineMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<RequisitionVoucherLine> builder)
        {
            builder.ToTable("RequisitionVoucherLine", "Procurement");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("LineID");
            builder.Property(e => e.DeliveredDate)
                .HasColumnType("datetime");
            builder.Property(e => e.Description)
                .HasMaxLength(256);
            builder.Property(e => e.IsActive)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.LastOrderedDate)
                .HasColumnType("datetime");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.PromisedDate)
                .HasColumnType("datetime");
            builder.Property(e => e.RequiredDate)
                .HasColumnType("datetime");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.Timestamp)
                .IsRequired()
                .IsRowVersion();

            builder.HasOne(d => d.Action)
                .WithMany()
                .HasForeignKey("ActionID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Core_DocumentAction");
            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Corporate_Branch");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Finance_FiscalPeriod");
            builder.HasOne(d => d.FullAccount)
                .WithMany()
                .HasForeignKey("FullAccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Finance_FullAccount");
            builder.HasOne(d => d.FullDetail)
                .WithMany()
                .HasForeignKey("FullDetailID")
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Finance_FullDetail");
            builder.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey("ProductID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Inventory_Product");
            builder.HasOne(d => d.Uom)
                .WithMany()
                .HasForeignKey("UomID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Inventory_Uom");
            builder.HasOne(d => d.Voucher)
                .WithMany(p => p.Lines)
                .HasForeignKey("VoucherID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Procurement_RequisitionVoucher");
            builder.HasOne(d => d.Warehouse)
                .WithMany()
                .HasForeignKey("WarehouseID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Inventory_Warehouse");
        }
    }
}

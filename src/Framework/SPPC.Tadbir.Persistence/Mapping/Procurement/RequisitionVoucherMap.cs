using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Procurement;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class RequisitionVoucherMap
    {
        private RequisitionVoucherMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<RequisitionVoucher> builder)
        {
            builder.ToTable("RequisitionVoucher", "Procurement");
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
            builder.Property(e => e.OrderedDate)
                .HasColumnType("datetime");
            builder.Property(e => e.PromisedDate)
                .HasColumnType("datetime");
            builder.Property(e => e.Reason)
                .HasMaxLength(256);
            builder.Property(e => e.Reference)
                .HasMaxLength(64);
            builder.Property(e => e.RequiredDate)
                .HasColumnType("datetime");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.Timestamp)
                .IsRequired()
                .IsRowVersion();
            builder.Property(e => e.WarehouseComment)
                .HasMaxLength(256);

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Corporate_Branch");
            builder.HasOne(d => d.Document)
                .WithMany()
                .HasForeignKey("DocumentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Core_Document");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Finance_FiscalPeriod");
            builder.HasOne(d => d.FullAccount)
                .WithMany()
                .HasForeignKey("FullAccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Finance_FullAccount");
            builder.HasOne(d => d.FullDetail)
                .WithMany()
                .HasForeignKey("FullDetailID")
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Finance_FullDetail");
            builder.HasOne(d => d.Receiver)
                .WithMany()
                .HasForeignKey("ReceiverID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Contact_Receiver");
            builder.HasOne(d => d.ReceiverUnit)
                .WithMany()
                .HasForeignKey("ReceiverUnitID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Corporate_ReceiverUnit");
            builder.HasOne(d => d.Requester)
                .WithMany()
                .HasForeignKey("RequesterID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Contact_Requester");
            builder.HasOne(d => d.RequesterUnit)
                .WithMany()
                .HasForeignKey("RequesterUnitID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Corporate_RequesterUnit");
            builder.HasOne(d => d.ServiceJob)
                .WithMany()
                .HasForeignKey("ServiceJobID")
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Core_ServiceJob");
            builder.HasOne(d => d.Type)
                .WithMany(p => p.RequisitionVouchers)
                .HasForeignKey("VoucherTypeID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Procurement_RequisitionVoucherType");
            builder.HasOne(d => d.Warehouse)
                .WithMany()
                .HasForeignKey("WarehouseID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurement_RequisitionVoucher_Inventory_Warehouse");
        }
    }
}

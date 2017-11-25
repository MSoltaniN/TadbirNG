using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Sales;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class InvoiceLineMap
    {
        private InvoiceLineMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.ToTable("InvoiceLine", "Sales");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("LineID");
            builder.Property(e => e.Description)
                .HasMaxLength(256);
            builder.Property(e => e.IsActive)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.Timestamp)
                .IsRequired()
                .IsRowVersion();

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Corporate_Branch");
            builder.HasOne(d => d.Currency)
                .WithMany()
                .HasForeignKey("CurrencyID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Finance_Currency");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Finance_FiscalPeriod");
            builder.HasOne(d => d.FullAccount)
                .WithMany()
                .HasForeignKey("FullAccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Finance_FullAccount");
            builder.HasOne(d => d.FullDetail)
                .WithMany()
                .HasForeignKey("FullDetailID")
                .HasConstraintName("FK_Sales_InvoiceLine_Finance_FullDetail");
            builder.HasOne(d => d.Invoice)
                .WithMany(p => p.Lines)
                .HasForeignKey("InvoiceID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Sales_Invoice");
            builder.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey("ProductID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Inventory_Product");
            builder.HasOne(d => d.RequisitionVoucher)
                .WithMany()
                .HasForeignKey("RequisitionVoucherID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Procurement_RequisitionVoucher");
            builder.HasOne(d => d.Uom)
                .WithMany()
                .HasForeignKey("UomID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Inventory_Uom");
            builder.HasOne(d => d.Warehouse)
                .WithMany()
                .HasForeignKey("WarehouseID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLine_Inventory_Warehouse");
        }
    }
}

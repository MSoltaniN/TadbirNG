using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Sales;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class InvoiceMap
    {
        private InvoiceMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice", "Sales");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("InvoiceID");
            builder.Property(e => e.ContractNo)
                .HasMaxLength(64);
            builder.Property(e => e.Date)
                .HasColumnType("datetime");
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
            builder.Property(e => e.ShipmentNo)
                .HasMaxLength(64);
            builder.Property(e => e.Timestamp)
                .IsRequired()
                .IsRowVersion();

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Corporate_Branch");
            builder.HasOne(d => d.Customer)
                .WithMany()
                .HasForeignKey("CustomerID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Contact_Customer");
            builder.HasOne(d => d.Document)
                .WithMany()
                .HasForeignKey("DocumentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Core_Document");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Finance_FiscalPeriod");
            builder.HasOne(d => d.FullAccount)
                .WithMany()
                .HasForeignKey("FullAccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Finance_FullAccount");
            builder.HasOne(d => d.FullDetail)
                .WithMany()
                .HasForeignKey("FullDetailID")
                .HasConstraintName("FK_Sales_Invoice_Finance_FullDetail");
            builder.HasOne(d => d.IssueReceiptVoucher)
                .WithMany()
                .HasForeignKey("IssueReceiptVoucherID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Warehousing_IssueReceiptVoucher");
            builder.HasOne(d => d.PartnerFullAccount)
                .WithMany()
                .HasForeignKey("PartnerFullAccountID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Finance_PartnerFullAccount");
            builder.HasOne(d => d.PartnerFullDetail)
                .WithMany()
                .HasForeignKey("PartnerFullDetailID")
                .HasConstraintName("FK_Sales_Invoice_Finance_PartnerFullDetail");
            builder.HasOne(d => d.Partner)
                .WithMany()
                .HasForeignKey("PartnerID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Contact_BusinessPartner");
            builder.HasOne(d => d.ReferenceInvoice)
                .WithMany()
                .HasForeignKey("ReferenceInvoiceID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoice_Sales_ReferenceInvoice");
        }
    }
}

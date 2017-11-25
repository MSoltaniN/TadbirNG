using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Inventory;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class ProductInventoryMap
    {
        private ProductInventoryMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<ProductInventory> builder)
        {
            builder.ToTable("ProductInventory", "Inventory");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ProductInventoryID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_ProductInventory_Corporate_Branch");

            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_ProductInventory_Finance_FiscalPeriod");

            builder.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey("ProductID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_ProductInventory_Inventory_Product");

            builder.HasOne(d => d.Warehouse)
                .WithMany()
                .HasForeignKey("WarehouseID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_ProductInventory_Inventory_Warehouse");
        }
    }
}

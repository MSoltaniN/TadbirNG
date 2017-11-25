using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Inventory;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class ProductMap
    {
        private ProductMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Inventory");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ProductID");
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey("CategoryID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Product_Inventory_ProductCategory");
        }
    }
}

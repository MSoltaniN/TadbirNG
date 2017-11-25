using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Inventory;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class ProductCategoryMap
    {
        private ProductCategoryMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory", "Inventory");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CategoryID");
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.FullCode)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.Level)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey("ParentID")
                .HasConstraintName("FK_Inventory_ProductCategory_Inventory_Parent");
        }
    }
}

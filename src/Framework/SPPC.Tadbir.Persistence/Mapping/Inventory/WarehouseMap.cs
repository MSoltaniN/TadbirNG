using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Inventory;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class WarehouseMap
    {
        private WarehouseMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouse", "Inventory");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("WarehouseID");
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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Inventory;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class UnitOfMeasurementMap
    {
        private UnitOfMeasurementMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<UnitOfMeasurement> builder)
        {
            builder.ToTable("UOM", "Inventory");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("UomID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class BusinessUnitMap
    {
        private BusinessUnitMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<BusinessUnit> builder)
        {
            builder.ToTable("BusinessUnit", "Corporate");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("UnitID");
            builder.Property(e => e.Description)
                .HasMaxLength(256);
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

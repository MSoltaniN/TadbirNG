using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class ProvinceMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Province", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ProvinceID");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.Code)
                .HasMaxLength(4);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}

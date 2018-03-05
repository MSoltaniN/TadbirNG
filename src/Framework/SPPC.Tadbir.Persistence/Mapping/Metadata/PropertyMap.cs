using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class PropertyMap
    {
        private PropertyMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Property", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PropertyID");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.DotNetType)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.StorageType)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.ScriptType)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.Length)
                .IsRequired();
            builder.Property(e => e.IsFixedLength)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(e => e.IsNullable)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(e => e.NameResourceId)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.AllowSorting)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(e => e.AllowFiltering)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Entity)
                .WithMany(p => p.Properties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Metadata_Property_Metadata_Entity");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class EntityMap
    {
        private EntityMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Entity> builder)
        {
            builder.ToTable("Entity", "Metadata");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("EntityID");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.IsHierarchy)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(e => e.IsCartableIntegrated)
                .HasColumnType("bit")
                .IsRequired();
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}

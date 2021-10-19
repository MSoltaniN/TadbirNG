using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Core;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class DocumentTypeMap
    {
        internal static void BuildMapping(EntityTypeBuilder<DocumentType> builder)
        {
            builder.ToTable("DocumentType", "Core");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("TypeID");
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

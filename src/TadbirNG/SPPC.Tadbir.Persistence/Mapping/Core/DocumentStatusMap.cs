using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Core;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class DocumentStatusMap
    {
        internal static void BuildMapping(EntityTypeBuilder<DocumentStatus> builder)
        {
            builder.ToTable("DocumentStatus", "Core");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("StatusID");
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

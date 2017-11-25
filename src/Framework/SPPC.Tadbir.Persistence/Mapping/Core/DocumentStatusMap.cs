using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Core;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class DocumentStatusMap
    {
        private DocumentStatusMap()
        {
        }

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

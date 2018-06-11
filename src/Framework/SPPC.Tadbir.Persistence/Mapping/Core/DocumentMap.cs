using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Core;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class DocumentMap
    {
        internal static void BuildMapping(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Document", "Core");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("DocumentID");
            builder.Property(e => e.EntityNo)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.No)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.OperationalStatus)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Status)
                .WithMany()
                .HasForeignKey("StatusID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_Document_Core_DocumentStatus");
            builder.HasOne(d => d.Type)
                .WithMany()
                .HasForeignKey("TypeID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_Document_Core_DocumentType");
        }
    }
}

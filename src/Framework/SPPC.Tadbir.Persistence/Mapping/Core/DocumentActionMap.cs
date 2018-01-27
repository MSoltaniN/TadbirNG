using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Core;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class DocumentActionMap
    {
        private DocumentActionMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<DocumentAction> builder)
        {
            builder.ToTable("DocumentAction", "Core");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ActionID");
            builder.Property(e => e.ApprovedDate)
                .HasColumnType("datetime");
            builder.Property(e => e.ConfirmedDate)
                .HasColumnType("datetime");
            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime");
            builder.Property(e => e.LineId).HasColumnName("LineID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.ApprovedBy)
                .WithMany(p => p.ApprovedActions)
                .HasForeignKey("ApprovedByID")
                .HasConstraintName("FK_Core_DocumentAction_Auth_ApprovedBy");
            builder.HasOne(d => d.ConfirmedBy)
                .WithMany(p => p.ConfirmedActions)
                .HasForeignKey("ConfirmedByID")
                .HasConstraintName("FK_Core_DocumentAction_Auth_ConfirmedBy");
            builder.HasOne(d => d.CreatedBy)
                .WithMany(p => p.CreatedActions)
                .HasForeignKey("CreatedByID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_DocumentAction_Auth_CreatedBy");
            builder.HasOne(d => d.Document)
                .WithMany(p => p.Actions)
                .HasForeignKey("DocumentID")
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Core_DocumentAction_Core_Document");
            builder.HasOne(d => d.ModifiedBy)
                .WithMany(p => p.ModifiedActions)
                .HasForeignKey("ModifiedByID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_DocumentAction_Auth_ModifiedBy");
        }
    }
}

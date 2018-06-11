using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Workflow;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal static class WorkItemDocumentMap
    {
        internal static void BuildMapping(EntityTypeBuilder<WorkItemDocument> builder)
        {
            builder.ToTable("WorkItemDocument", "Workflow");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("DocumentItemID");
            builder.Property(e => e.DocumentType)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false);
            builder.Property(e => e.EntityId)
                .HasColumnName("EntityID");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Document)
                .WithMany()
                .HasForeignKey("DocumentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflow_WorkItemDocument_Core_Document");

            builder.HasOne(d => d.WorkItem)
                .WithMany(p => p.Documents)
                .HasForeignKey("WorkItemID")
                .HasConstraintName("FK_Workflow_WorkItemDocument_Workflow_WorkItem");
        }
    }
}

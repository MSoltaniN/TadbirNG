using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Workflow;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class WorkItemMap
    {
        private WorkItemMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<WorkItem> builder)
        {
            builder.ToTable("WorkItem", "Workflow");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("WorkItemID");
            builder.Property(e => e.Action)
                .IsRequired()
                .HasMaxLength(64)
                .IsUnicode(false);
            builder.Property(e => e.Date)
                .HasColumnType("datetime");
            builder.Property(e => e.DocumentType)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.Remarks)
                .HasMaxLength(1024);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasOne(d => d.CreatedBy)
                .WithMany()
                .HasForeignKey("CreatedByID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflow_WorkItem_Auth_User");
            builder.HasOne(d => d.Target)
                .WithMany()
                .HasForeignKey("TargetID")
                .HasConstraintName("FK_Workflow_WorkItem_Auth_Role");
        }
    }
}

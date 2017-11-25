using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Workflow;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class WorkItemHistoryMap
    {
        private WorkItemHistoryMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<WorkItemHistory> builder)
        {
            builder.ToTable("WorkItemHistory", "Workflow");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("HistoryItemID");
            builder.Property(e => e.Action)
                .IsRequired()
                .HasMaxLength(64)
                .IsUnicode(false);
            builder.Property(e => e.Date)
                .HasColumnType("datetime");
            builder.Property(e => e.EntityId)
                .HasColumnName("EntityID");
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

            builder.HasOne(d => d.Document)
                .WithMany()
                .HasForeignKey("DocumentID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflow_WorkItemHistory_Core_Document");

            builder.HasOne(d => d.Role)
                .WithMany()
                .HasForeignKey("RoleID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflow_WorkItemHistory_Auth_Role");

            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey("UserID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflow_WorkItemHistory_Auth_User");
        }
    }
}

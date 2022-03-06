using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Reporting;

namespace SPPC.Tadbir.Persistence.Mapping.Reporting
{
    internal sealed class SystemIssueMap
    {
        private SystemIssueMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<SystemIssue> builder)
        {
            builder.ToTable("SystemIssue", "Reporting");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("SystemIssueID");
            builder.Property(e => e.TitleKey)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(e => e.ApiUrl)
                .HasMaxLength(128);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(e => e.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_SystemIssue_Reporting_Parent");
            builder.HasOne(d => d.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_SystemIssue_Auth_Permission");
            builder.HasOne(d => d.View)
                .WithMany()
                .HasForeignKey(d => d.ViewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reporting_SystemIssue_Metadata_View");
        }
    }
}

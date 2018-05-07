using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class ProjectMap
    {
        private ProjectMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project", "Finance");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ProjectID");
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(e => e.FullCode)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.Level)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");

            builder.HasOne(d => d.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentId)
                .HasConstraintName("FK_Finance_Project_Finance_Parent");
            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey("BranchID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Project_Corporate_Branch");
            builder.HasOne(d => d.FiscalPeriod)
                .WithMany()
                .HasForeignKey("FiscalPeriodID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finance_Project_Finance_FiscalPeriod");
        }
    }
}

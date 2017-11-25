using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Persistence.Mapping
{
    internal sealed class BranchMap
    {
        private BranchMap()
        {
        }

        internal static void BuildMapping(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branch", "Corporate");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("BranchID");
            builder.Property(e => e.Description)
                .HasMaxLength(512);
            builder.Property(e => e.Level)
                .HasDefaultValueSql("((0))");
            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.RowGuid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
            builder.HasOne(d => d.Company)
                .WithMany(p => p.Branches)
                .HasForeignKey("CompanyID")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Corporate_Branch_Corporate_Company");
        }
    }
}
